using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Serialization;
using RadioDJManager.Data;
using RadioDJManager.Events;
using RadioDJManager.Models;

namespace RadioDJManager.ViewModels
{
    [Serializable]
    public class EventViewModel : ViewModelBase
    {

        #region Variables

        [XmlIgnore]
        private string _calculatedTime;

        public string CalculatedTime
        {
            get { return _calculatedTime; }
            set
            {
                _calculatedTime = value;
                OnPropertyChanged(nameof(CalculatedTime));
                //CalculatedTimeChangedAggregator.GetEvent<CalculatedTimeChangedEvent>().Publish(_calculatedTime);
                //if (CalculatedTimeChangedEvent != null)
                //    CalculatedTimeChangedEvent(this,new EventArgs());

            }
        }

        [XmlIgnore]
        private string _eventDisplay;

        
        public string EventDisplay
        {
            get { return _eventDisplay; }
            set
            {
                if(_eventDisplay != null && _eventDisplay.Equals(value))
                    return;

                _eventDisplay = value;
                OnPropertyChanged(nameof(EventDisplay));
                EventAggregator.Publish(new CalculatedTimeChangedMsg(_calculatedTime));
                //ShowNotification($"EventDisplay : {EventDisplay}");
            }
        }

        [XmlIgnore]
        private int _eventIndex;
        
        public int EventIndex
        {
            get { return _eventIndex; }
            set
            {
                _eventIndex = value;
                OnPropertyChanged(nameof(EventIndex));
            }
        }

        [XmlIgnore]
        private string _dataFileName;

        public string DataFileName
        {
            get { return _dataFileName; }
            set
            {
                _dataFileName = value;
                OnPropertyChanged(nameof(DataFileName));
            }
        }

        [XmlIgnore]
        private RadioEvent _model;

        public RadioEvent Model
        {
            get { return _model; }
            set
            {
                UnregisterFromActionsEvents();

                _model = value;

                RegisterToActionsEvents();

                //ModelCopy = ObjectCopier.Clone(Model);

                OnPropertyChanged(nameof(Model));

                SelectedAction = null;
                ActionIndex = -1;

                //ShowNotification($"Model : {EventDisplay}");
            }
        }


        [XmlIgnore]
        private int _index;

        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                OnPropertyChanged(nameof(Index));
            }
        }

        [XmlIgnore]
        private EventAction _selectedAction;

        public EventAction SelectedAction
        {
            get { return _selectedAction; }
            set
            {
                _selectedAction = value;

                if (_selectedAction != null)
                {
                    _selectedAction.IsChecked = true;
                }
                
                //CalculateEventFiringTime(DateTime.Now);
                OnPropertyChanged(nameof(SelectedAction));
            }
        }

        [XmlIgnore]
        private int _actionIndex;

        public int ActionIndex
        {
            get { return _actionIndex; }
            set
            {
                _actionIndex = value;

                if (_actionIndex == -1)
                    EventDisplay = "EFT : N/A";
                else
                {
                    CalculateEventFiringTime(DateTime.Now);
                }
                
                OnPropertyChanged(nameof(ActionIndex));

                //ShowNotification($"ActionIndex Changed : {_actionIndex}");
            }
        }

        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        public EventViewModel()
        {
            Model = new RadioEvent();
            SelectedAction = new EventAction();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idx"></param>
        public EventViewModel(int idx):this()
        {
            Index = idx;
            
            //Model = new evnt();
            //Db = SetupDb();
            //SelectedAction = new Action();
            //CalculatedTimeChangedAggregator = new EventAggregator();
        }

        /// <summary>
        /// Calculates when the event will be fired
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnActionChecked(object sender, EventArgs args)//Action action
        {
            CalculateEventFiringTime(DateTime.Now);
        }

        /// <summary>
        /// Calculates when the event will be fired and updates the CalculatedTime and the EventDisplay
        /// </summary>
        /// <param name="eventDate"></param>
        public void CalculateEventFiringTime(DateTime eventDate)
        {
            try
            {
                if (Model == null)
                {
                    ShowMessage("Model is null", "Error Calculating event firing time");
                    return;
                }
                if (Model.actions == null)
                {
                    ShowMessage("Model.actions is null", "Error Calculating event firing time");
                    return;
                }


                if (_selectedAction == null)
                {
                    _selectedAction = Model.actions.FirstOrDefault(a => a.IsChecked);
                }

                if (_selectedAction != null && !string.IsNullOrEmpty(Model.time))
                {
                    /*Get the time of the prayer from the csv ==> Get the prayers time corresponding to date*/
                    var date = eventDate.ToString("yyyy-MM-dd");

                    var csvDate = date.GetDayElementForDateString();

                    if (csvDate == null)
                        return;

                    /*Set up Model time*/
                    switch (Index)
                    {
                        case 1:
                            {
                                Model.time = csvDate.Subh;
                                break;
                            }

                        case 3:
                            {
                                Model.time = csvDate.Dhuhr;
                                break;
                            }
                        case 4:
                            {
                                Model.time = csvDate.Asr;
                                break;
                            }
                        case 5:
                            {
                                Model.time = csvDate.Maghrib;
                                break;
                            }
                        case 6:
                            {
                                Model.time = csvDate.Isha;
                                break;
                            }

                        default:
                            break;
                    }


                    double totalDuration = 0;
                    var track = Model.actions.FirstOrDefault(t => t.ActionValue.Equals(_selectedAction.ActionValue, StringComparison.OrdinalIgnoreCase) &&
                                                                  t.ActionValue.Trim().StartsWith("Load Track By ID"));

                    //if (track == null)
                    //    return;

                    if (track != null)
                    {
                        var id = track.GetSongId();
                        if ((id[0] != -1) && (id[1] != -1))
                        {
                            var song = DbContext.songs.Find(id[1]);
                            totalDuration += Convert.ToDouble(song.duration);
                        }
                    }


                    var timeDouble = Model.time.ConvertTimeStringToSeconds();
                    timeDouble -= totalDuration;
                    //timeDouble = Math.Round(timeDouble, 2);//Math.Truncate
                    CalculatedTime = timeDouble.ConvertTimeToString();
                    //Model.CalculatedTime = CalculatedTime;
                    EventDisplay = $"Event Firing Time : {CalculatedTime} | Athan Time : {Model.time}";
                    //CalculatedTimeChangedAggregator.GetEvent<CalculatedTimeChangedEvent>().Publish(_calculatedTime);
                    //EventDisplay = string.Format("EFT : {0}", CalculatedTime);

                    //ModernDialog.ShowMessage(EventDisplay,"EventDisplay",MessageBoxButton.OK);
                }

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "Error Calculating event firing time");
            }
        }


        public void CalculateSecondaryEventFiringTime()
        {
            try
            {
                if (Model == null)
                {
                    ShowMessage("Model is null", "Error Calculating secondary event firing time");
                    return;
                }
                if (Model.actions == null)
                {
                    ShowMessage("Model.actions is null", "Error Calculating secondary event firing time");
                    return;
                }

                if (_selectedAction != null && Model.time != null)
                {
                    double trackDuration = 0;
                    var track = Model.actions.FirstOrDefault(t=>!t.ActionValue.Equals(_selectedAction.ActionValue) && 
                                                             t.ActionValue.Trim().StartsWith("Load Track By ID"));
                    if (track != null)
                    {
                        var id = track.GetSongId();
                        if ((id[0] != -1) && (id[1] != -1))
                        {
                            var song = DbContext.songs.Find(id[1]);
                            trackDuration += Convert.ToDouble(song.duration);
                        }
                    }



                    var eventTime = Model.time.ConvertTimeStringToSeconds();
                    eventTime -= trackDuration;
                    CalculatedTime = eventTime.ConvertTimeToString();
                    //EventDisplay = string.Format("EFT : {0} | AT : {1}", CalculatedTime, Model.time);
                    EventDisplay = $"EFT : {CalculatedTime} ";
                }

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "Error Calculating secondary event firing time");
            }
        }

    

        /// <summary>
        /// Applies the rotation by changing the 'data' field in the event
        /// </summary>
        public void ApplyRotation()
        {

#if DEBUG
            //Get the folder for all the songs
            //GetAllSongsFolders();
#endif
            if (Model?.actions == null) return;

            foreach (var act in Model.actions)//action <==> track
            {
                //if (act.IsChecked)//Uncomment to rotate only the selected track
                if (act.ActionValue != null &&
                    act.ActionValue.Contains("Load Track By ID") &&
                    act.SelectedOption != null)
                    {
                        var songs = act.SongFolder?.GetSongsFromFolder(DbContext);//GetSongsFromFolder(act.SongFolder);
                        //if (songs == null) continue;
                        songs = songs?.OrderBy(x => x.count_played).ToList();

                        Song s = null;

                        switch (act.SelectedOption.Trim())
                        {
                            case "None":
                                {
                                    break;
                                }
                            case "Rotate by name order"://Rotate by name order
                                {
                                    songs = songs.OrderBy(x => x.path).ToList();
                                    if (act.SongIndex > songs.Count() - 1)
                                    {
                                        act.SongIndex = 0; //Reset the index
                                    }

                                    s = songs.ElementAt(act.SongIndex);
                                    act.SongIndex++;

                                    break;
                                }
                            case "Rotate by 1st least played":
                                {
                                    s = songs.ElementAt(0);
                                    break;
                                }
                            case "Rotate by 2nd least played":
                                {
                                    s = songs.ElementAt(1);
                                    break;
                                }
                            case "Rotate by 3rd least played":
                                {
                                    s = songs.ElementAt(2);
                                    break;
                                }
                            case "Rotate by 4th least played":
                                {
                                    s = songs.ElementAt(3);
                                    break;
                                }
                            case "Rotate by 5th least played":
                                {
                                    s = songs.ElementAt(4);
                                    break;
                                }

                            default:
                                break;
                        }

                        if (s != null)
                        {
                            //Load Track By ID|0|36|the daylights - black dove mp3 download.mp3|Top
                            var parts = act.ActionValue.Split('|');
                            if (parts.Length >= 5)
                            {
                                parts[2] = s.ID.ToString();

                                var pathParts = s.path.Split('\\');//get the file name
                                parts[3] = pathParts[pathParts.Length - 1];

                                act.ActionValue = $"{parts[0]}|{parts[1]}|{parts[2]}|{parts[3]}|{parts[4]}";
                            }
                        }

                        //break;//foreach
                    }
            }

            var sb = new StringBuilder();
            foreach (var act in Model.actions)
            {
                sb.Append(act.ActionValue + Environment.NewLine);
            }

            Model.data = sb.ToString().TrimEnd('\n', '\r')/*.TrimEnd('\r')*/;//.TrimEnd(Environment.NewLine)

        }


        /// <summary>
        /// Gets all the songs' folders
        /// </summary>
        /// <returns></returns>
        public void GetAllSongsFolders()
        {
            if (Model?.actions == null) return;

            foreach (var track in Model.actions)
            {
                track.SongFolder = track.GetSongFolder(DbContext); //GetSongFolder(track);
            }

        }
        

        /// <summary>
        /// Unregisters from the ActionCheckedEvent event in the actions 
        /// </summary>
        public void UnregisterFromActionsEvents()
        {
            if (_model?.actions == null)
                return;

            foreach (var action in _model.actions)
            {
                //action.ActionCheckedAggregator.GetEvent<ActionCheckedEvent>().Unsubscribe(CalculateFiringTime);
                action.ActionCheckedEvent -= OnActionChecked;
            }
        }


        /// <summary>
        /// Registers from the ActionCheckedEvent event in the actions 
        /// </summary>
        public void RegisterToActionsEvents()
        {
            if (_model?.actions == null)
                return;

            //Id = _model.ID;
            foreach (var action in _model.actions)
            {
                //action.ActionCheckedAggregator.GetEvent<ActionCheckedEvent>().Subscribe(CalculateFiringTime);
                action.ActionCheckedEvent += OnActionChecked;
            }
        }

        public override void Cleanup()
        {
            
        }
    }
}
