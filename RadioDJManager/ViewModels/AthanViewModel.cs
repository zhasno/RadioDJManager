using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Messaging;
using RadioDJManager.Events;
using RadioDJManager.Models;
using EventAction = RadioDJManager.Models.EventAction;

namespace RadioDJManager.ViewModels
{
    public class AthanViewModel : ViewModelBase,
        ISubscriber<SessionSaveChangedMsg>,
        ISubscriber<DayChangedMsg>,
        ISubscriber<ApplyChangesMsg>,
        ISubscriber<CalculatedTimeChangedMsg>
    {
        #region Variables

        public ICommand ApplyChangesC { get; set; }

        private EventViewModel _mainEvent;
        public EventViewModel MainEvent
        {
            get { return _mainEvent; }
            set
            {
                _mainEvent = value;
                OnPropertyChanged();
            }
        }

        private EventViewModel _eventBeforeAthan;
        public EventViewModel EventBeforeAthan
        {
            get {return _eventBeforeAthan; }
            set
            {
                _eventBeforeAthan = value;
                OnPropertyChanged();
            }
        }

        private EventViewModel _eventAfterAthan;
        public EventViewModel EventAfterAthan
        {
            get { return _eventAfterAthan; }
            set
            {
                _eventAfterAthan = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RadioEvent> _eventList;

        public ObservableCollection<RadioEvent> EventList
        {
            get => _eventList;
            set
            {
                _eventList = value;
                OnPropertyChanged();
            }
        }

        private int _index;

        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                OnPropertyChanged(nameof(Index));
                //LoadEvents();

                SetFilename(Index);

                /*Load the previous session*/
                if (SaveSession)
                {
                    var me = new EventViewModel(Index);
                    me.DataFileName = MainEvent.DataFileName;
                    LoadEventEntityData(ref me);
                    MainEvent = me;

                    var eba = new EventViewModel(Index);
                    eba.DataFileName = EventBeforeAthan.DataFileName;
                    LoadEventEntityData(ref eba);
                    EventBeforeAthan = eba;

                    var eaa = new EventViewModel(Index);
                    eaa.DataFileName = EventAfterAthan.DataFileName;
                    LoadEventEntityData(ref eaa);
                    EventAfterAthan = eaa;

                    /*if the events are loaded then the view model needs to re-subscribe to the main event CalculatedTimeChangedAggregator*/
                    
                    //MainEvent.CalculatedTimeChangedAggregator
                    //        .GetEvent<CalculatedTimeChangedEvent>()
                    //        .Subscribe(MainEventTimeChanged);

                    
                }

            }
        }

        private bool _isRotationActivated;

        public bool IsRotationActivated
        {
            get { return _isRotationActivated; }
            set
            {
                _isRotationActivated = value;
                OnPropertyChanged(nameof(IsRotationActivated));
            }
        }

        private bool _saveSession;

        public bool SaveSession
        {
            get { return _saveSession; }
            set
            {
                _saveSession = value;
                OnPropertyChanged(nameof(SaveSession));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idx">determines the prayer(i.e:subh,dhuhr,...)</param>
        public AthanViewModel(int idx)
        {
            SaveSession = Properties.Settings.Default.SaveSession;

            MainEvent = new EventViewModel(idx);
            EventBeforeAthan = new EventViewModel(idx);
            EventAfterAthan = new EventViewModel(idx);

            EventList = new ObservableCollection<RadioEvent>();
            ApplyChangesC = new RelayCommand(ApplyChanges, _=> true);

            IsRotationActivated = false;

            Index = idx;

            LoadEvents();

            MainEvent.EventIndex = GetEventIndex(MainEvent.Model);
            EventBeforeAthan.EventIndex = GetEventIndex(EventBeforeAthan.Model);
            EventAfterAthan.EventIndex = GetEventIndex(EventAfterAthan.Model);

            if (SaveSession)
            {
                MainEvent.CalculateEventFiringTime(DateTime.Now);
                EventBeforeAthan.CalculateEventFiringTime(DateTime.Now);
                EventAfterAthan.CalculateEventFiringTime(DateTime.Now);
            }
               
            ShowNotification($"View Model Instantiated. Index: {idx}");
        }

        private void SetSaveState(bool obj)
        {
            SaveSession = obj;
        }

        private void MainEventTimeChanged(string obj)
        {
            MainEvent.Model.time = MainEvent.CalculatedTime;
            //Save(MainEvent.Model);

            if (EventAfterAthan.Model.data != null)
            {
                /*Calculate when the adan event will end*/
                var eventEndTime = MainEvent.Model.CalculateEventEndTime(DbContext);

                /*EventAfterAthan firing time = MainEvent ending time [i.e : MainEvent firing time + Sum(MainEvent tracks length) ]*/
                EventAfterAthan.Model.time = eventEndTime[1].ConvertTimeToString();

                /*To update EventAfterAthan CalculatedTime and EventDisplay*/
                EventAfterAthan.CalculateSecondaryEventFiringTime();

                /*Save EventAfterAthan stating time to the database (No rotation is made yet)*/
                //Save(EventAfterAthan.Model);
            }

            /*Same as above but for EventBeforeAthan*/
            if (EventBeforeAthan.Model.data != null)
            {
                var eventEndTime = EventBeforeAthan.Model.CalculateEventEndTime(DbContext);

                /*EventBeforeAthan firing time = MainEvent firing time - Sum(EventBeforeAthan tracks length)*/
                EventBeforeAthan.Model.time = (MainEvent.Model.time.ConvertTimeStringToSeconds() - eventEndTime[0]).ConvertTimeToString();
                
                //Utils.Instance.ConvertTimeToString(MainEvent.CalculateEventEndTime()[1] - EventBeforeAthan.CalculateEventEndTime()[0]);
                EventBeforeAthan.CalculateSecondaryEventFiringTime();
                //Save(EventBeforeAthan.Model);
            }

            //EventBeforeAthan.CalculateEventFiringTime(DateTime.Now);
            //EventAfterAthan.CalculateEventFiringTime(DateTime.Now);
        }


        /// <summary>
        /// Gets the event index from the events list
        /// </summary>
        /// <param name="inputEvent"></param>
        /// <returns></returns>
        private int GetEventIndex(RadioEvent inputEvent)
        {
            var idx = 0;
            foreach (var ev in EventList)
            {
                if (ev.ID == inputEvent.ID)
                {
                    break;
                }
                else
                {
                    idx++;
                }
            }

            return idx;
        }

        /// <summary>
        /// Loads the event list from the database
        /// </summary>
        public void LoadEvents()
        {
            try
            {
                EventList.Clear();
                RefreshDbContext();
                var evList = DbContext.events.ToList();

                foreach (var ev in evList)
                {
                    foreach (var action in ev.GetActions())
                    {
                        var ac = new EventAction {ActionValue = action};
                        var id = ac.GetSongId();

                        if ((id[0] != -1) && (id[1] != -1))
                        {
                            var song = DbContext.songs.Find(id[1]);
                            var d = TimeSpan.FromMinutes(Convert.ToDouble(song.duration)).ToString();
                            var p = d.Split(':');
                            ac.Lenght = $"{p[0]}:{p[1]}";
                        }

                        ev.actions.Add(ac);
                    }

                    EventList.Add(ev);

                }

                //Utils.Instance.SubhLoadedAggregator.GetEvent<AthanLoadedEvent>().Publish("Subh Loaded");
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "Error loading events");
            }
        }


        /// <summary>
        /// Saves a modified  event to the database
        /// </summary>
        /// <param name="eventToSave"></param>
        private void Save(RadioEvent eventToSave)
        {
            try
            {
                var dbEvent = DbContext.events.Find(eventToSave.ID);
                if (dbEvent == null)
                {
                    ShowMessage($"Event with ID: {eventToSave.ID} not found in the Database.", "Error @AthanViewModel.Save");
                    return;
                }
                    
                //eventToSave.time = CalculatedTime;

                /*Repeat by date*/
                eventToSave.type = dbEvent.type = 1;
                eventToSave.hours = dbEvent.hours = "&";

                /*Every day*/
                eventToSave.day = dbEvent.day = "&1&2&3&4&5&6&0";

                /*The event is activated*/
                eventToSave.enabled = dbEvent.enabled = "True";

                dbEvent.data = eventToSave.data;
                dbEvent.time = eventToSave.time;
                dbEvent.date = eventToSave.date;
                dbEvent.catID = eventToSave.catID;

                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "Error @AthanViewModel.Save");
            }
        }


        /// <summary>
        /// Updates the database with the new values
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateDatabase(DateTime obj)
        {
            try
            {
                if (IsRotationActivated)//if rotation is activated,rotate the tracks for the main and secondary events
                {
                    MainEvent.ApplyRotation();

                    MainEventTimeChanged("");//Recalculate CalculatedTime after rotation

                    EventBeforeAthan.SelectedAction.SelectedOption = "Rotate by name order";
                    EventBeforeAthan.ApplyRotation();

                    EventAfterAthan.SelectedAction.SelectedOption = "Rotate by name order";
                    EventAfterAthan.ApplyRotation();
                }
                  
                MainEvent.Model.time = MainEvent.CalculatedTime;
                Save(MainEvent.Model);

                if (EventBeforeAthan.Model.data != null)
                {
                    EventBeforeAthan.Model.time =
                        (MainEvent.Model.time.ConvertTimeStringToSeconds() - EventBeforeAthan.Model.CalculateEventEndTime(DbContext)[0])
                        .ConvertTimeToString();

                    //EventBeforeAthan.Model.time = Utils.Instance.ConvertTimeToString(MainEvent.CalculateEventEndTime()[1] - EventBeforeAthan.CalculateEventEndTime()[0]);
                    Save(EventBeforeAthan.Model);
                }

                if (EventAfterAthan.Model.data != null)
                {
                    EventAfterAthan.Model.time = MainEvent.Model.CalculateEventEndTime(DbContext)[1].ConvertTimeToString();
                    Save(EventAfterAthan.Model); 
                }

                //if (!IsRotationActivated)
                //    ModernDialog.ShowMessage("Changes saved!", "Database updated", MessageBoxButton.OK);

            }
            catch (EntityException ex)
            {
                if (!IsRotationActivated)
                    ShowMessage(ex.Message, "Error Updating Database");
            }

        }


        /// <summary>
        /// Applies the changes to the database and saves the current state
        /// </summary>
        public void ApplyChanges(object input = null)
        {
            //Get the folder for all the songs
            MainEvent.GetAllSongsFolders();
            EventBeforeAthan.GetAllSongsFolders();
            EventAfterAthan.GetAllSongsFolders();

            if (IsRotationActivated)
                IsRotationActivated = false;

            UpdateDatabase(DateTime.Today);
            //Utils.Instance.ApplyChangesAggregator.GetEvent<ApplyChangesEvent>().Publish(DateTime.Now);

            Task.Factory.StartNew(() =>
            {
                if (SaveSession)
                {
                    Utils.Instance.SaveState(MainEvent.DataFileName, MainEvent);
                    Utils.Instance.SaveState(EventBeforeAthan.DataFileName, EventBeforeAthan);
                    Utils.Instance.SaveState(EventAfterAthan.DataFileName, EventAfterAthan);
                }

            });


            if (!IsRotationActivated)
                ShowNotification("Changes Saved!");
        }



        /// <summary>
        /// Sets the filename where the state will be saved
        /// </summary>
        /// <param name="idx"></param>
        private void SetFilename(int idx)
        {
            switch (idx)
            {
                case 1:
                    {
                        MainEvent.DataFileName = "States\\Subh_State.dat";
                        EventBeforeAthan.DataFileName = $"{MainEvent.DataFileName}{1}";
                        EventAfterAthan.DataFileName = $"{MainEvent.DataFileName}{2}";
                        break;
                    }
                case 3:
                    {
                        MainEvent.DataFileName = "States\\Dhuhr_State.dat";
                        EventBeforeAthan.DataFileName = $"{MainEvent.DataFileName}{1}";
                        EventAfterAthan.DataFileName = $"{MainEvent.DataFileName}{2}";
                        break;
                    }
                case 4:
                    {
                        MainEvent.DataFileName = "States\\Asr_State.dat";
                        EventBeforeAthan.DataFileName = $"{MainEvent.DataFileName}{1}";
                        EventAfterAthan.DataFileName = $"{MainEvent.DataFileName}{2}";
                        break;
                    }
                case 5:
                    {
                        MainEvent.DataFileName = "States\\Maghrib_State.dat";
                        EventBeforeAthan.DataFileName = $"{MainEvent.DataFileName}{1}";
                        EventAfterAthan.DataFileName = $"{MainEvent.DataFileName}{2}";
                        break;
                    }
                case 6:
                    {
                        MainEvent.DataFileName = "States\\Isha_State.dat";
                        EventBeforeAthan.DataFileName = $"{MainEvent.DataFileName}{1}";
                        EventAfterAthan.DataFileName = $"{MainEvent.DataFileName}{2}";
                        break;
                    }

                default:
                {
                    MainEvent.DataFileName = "";
                    EventBeforeAthan.DataFileName = $"{MainEvent.DataFileName}{1}";
                    EventAfterAthan.DataFileName = $"{MainEvent.DataFileName}{2}";
                    break; 
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputEventEntity"></param>
        private void LoadEventEntityData(ref EventViewModel inputEventEntity)
        {
            var mod = Utils.Instance.LoadState<EventViewModel>(inputEventEntity.DataFileName);
            if (mod.Model.data != null)
            {
                inputEventEntity.Model = DbContext.events.Find(mod.Model.ID);

                if (inputEventEntity.Model == null)
                    return;

                inputEventEntity.Model.actions = mod.Model.actions;
                inputEventEntity.ActionIndex = mod.ActionIndex;
                inputEventEntity.CalculatedTime = mod.CalculatedTime;
                inputEventEntity.EventDisplay = mod.EventDisplay;
                inputEventEntity.EventIndex = mod.EventIndex;
                inputEventEntity.Index = mod.Index;
                inputEventEntity.SelectedAction = mod.SelectedAction;

                inputEventEntity.RegisterToActionsEvents();
                //if (Model.CalculatedTime != null)
                //    CalculatedTime = Model.CalculatedTime;
            }
        }


        #region HandleMessage
        public void HandleMessage(SessionSaveChangedMsg msg)
        {
            InvokeDispatcher(() =>
            {
                SaveSession = msg.Content;
            });
            
        }

        public void HandleMessage(DayChangedMsg msg)
        {
            InvokeDispatcher(() =>
            {
                if (!IsRotationActivated)
                    IsRotationActivated = true;

                UpdateDatabase(msg.Content);
            });

        }

        public void HandleMessage(ApplyChangesMsg msg)
        {
            InvokeDispatcher(() =>
            {
                UpdateDatabase(msg.Content);
            });
            
        }

        public void HandleMessage(CalculatedTimeChangedMsg msg)
        {
            InvokeDispatcher(() =>
            {
                MainEventTimeChanged(msg.Content);
            });
            
        } 
        #endregion

        /// <summary>
        /// Cleanup
        /// </summary>
        public override void Cleanup()
        {
            MainEvent?.Dispose();
            EventList = null;
            DbContext?.Dispose();
            ApplyChangesC = null;
        }
       
        #endregion

    }
}
