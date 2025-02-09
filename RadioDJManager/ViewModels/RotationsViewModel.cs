using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Messaging;
using RadioDJManager.CustomControls;
using RadioDJManager.Events;
using RadioDJManager.Models;
using EventAction = RadioDJManager.Models.EventAction;

namespace RadioDJManager.ViewModels
{
    public class RotationsViewModel : ViewModelBase, ISubscriber<DayChangedMsg>, ISubscriber<DeleteRotatorMsg>
    {
        #region Variables
        public ObservableCollection<RadioEvent> EventList { get; set; }
        private ObservableCollection<RotatorViewModel> _rotators;
        public ObservableCollection<RotatorViewModel> Rotators
        {
            get => _rotators;
            set
            {
                _rotators = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RotatorControl> _rotatorControls;
        public ObservableCollection<RotatorControl> RotatorControls
        {
            get => _rotatorControls;
            set
            {
                _rotatorControls = value;
                OnPropertyChanged();
            }
        }

        public ICommand ApplyRotationsC { get; set; }
        private string _connectionString { get; set; }

        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                if (_connectionString == value)
                    return;

                _connectionString = value;
                OnPropertyChanged();
                RefreshDbContext();
            }
        }
        private string _dataFileName { get; set; }

        public string DataFileName
        {
            get { return _dataFileName; }
            set
            {
                _dataFileName = value;
                OnPropertyChanged();
            }
        }
        
        private bool _saveSession { get; set; }

        public bool SaveSession
        {
            get { return _saveSession; }
            set
            {
                _saveSession = value;
                OnPropertyChanged();
            }
        }

        private bool _isRotationActivated { get; set; }

        public bool IsRotationActivated
        {
            get { return _isRotationActivated; }
            set
            {
                _isRotationActivated = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddRotatorCommand { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public RotationsViewModel()
        {
            SaveSession = Properties.Settings.Default.SaveSession;
            EventList = new ObservableCollection<RadioEvent>();
            RotatorControls = new ObservableCollection<RotatorControl>();

            ApplyRotationsC = new RelayCommand(ApplyRotations, _=> true);
            AddRotatorCommand = new RelayCommand(AddRotator, _ => true);

            DataFileName = "States\\Rotations_State.dat";
            IsRotationActivated = false;

            LoadEvents();

            if (SaveSession)
            {
                InvokeDispatcher(() =>
                {
                    Rotators = new ObservableCollection<RotatorViewModel>(Utils.Instance.LoadState<List<RotatorViewModel>>(DataFileName));

                    foreach (var rotator in Rotators)
                    {
                        rotator.EventList = EventList;
                    }
                });

            }
            else
                Rotators = new ObservableCollection<RotatorViewModel>();

            LoadRotators();
        }

        private void LoadRotators()
        {
            for (int i = 0; i < Rotators.Count; i++)
            {
                var rotator = Rotators[i];

                LoadRotator(rotator);
            }
        }

        private void AddRotator(object obj)
        {
            var rotator = new RotatorViewModel(EventList);

            Rotators.Add(rotator);

            LoadRotator(rotator);
        }

        private void LoadRotator(RotatorViewModel rotator)
        {
            rotator.Index = _rotators.IndexOf(rotator);

            RotatorControls.Add(new RotatorControl(rotator)
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center
            });
        }


        /// <summary>
        /// Loads the event list from the database
        /// </summary>
        private void LoadEvents()
        {
            try
            {
                EventList.Clear();
                //RefreshDbContext();
                //var EvList = Db.events.ToList();

                foreach (var ev in DbContext.events.ToList())
                {
                    /*Add the actions of the event*/
                    foreach (var action in ev.GetActions())
                    {
                        var ac = new EventAction();
                        ac.ActionValue = action;
                        var id = ac.GetSongId();

                        /*Check if Song ID and Category ID are valid*/
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

                EventAggregator.Publish(new AthanLoadedMsg("Events loaded for rotations."));
            }
            catch (Exception ex)
            {
                ShowMessage(ex.GetLastErrorMessage(), "Error loading events for rotations.");
            }
        }

        /// <summary>
        /// Applies the rotations
        /// </summary>
        private void ApplyRotations(object input = null)
        {

            try
            {
                /*Get the folder for all the songs*/
                LoadAllSongFolders();
                
                //if (IsRotationActivated)
                    IsRotationActivated = false;

                foreach (var rotator in Rotators)
                {
                    RotateTrack(rotator);
                    var dbEvent = DbContext.events.Find(rotator.Model.ID);
                    if (dbEvent == null)
                    {
                        ShowMessage($"Event with ID: {rotator.Model.ID} not found in the Database.", "Error @RotationsViewModel.ApplyRotations");
                        continue;
                    }

                    dbEvent.data = rotator.Model.data;

                    DbContext.SaveChanges();
                }

                if (SaveSession)
                    Utils.Instance.SaveState(DataFileName, Rotators);

                //if (!IsRotationActivated)
                //    ModernDialog.ShowMessage("Rotations Applied!", "Rotations Saved", MessageBoxButton.OK);

                ShowNotification("Rotations Saved!");
            }
            catch (Exception ex)
            {
                if (!IsRotationActivated)
                  ShowMessage(ex.Message, "Error Applying Rotations");
            }
        }

        /// <summary>
        /// Applies the rotation by changing the 'data' field in the event
        /// </summary>
        private void RotateTrack(RotatorViewModel rotator)
        {
            if (rotator?.Model?.actions == null) return;

            foreach (var action in rotator.Model.actions)
            {
                if (
                        action.IsChecked && 
                        action.ActionValue != null && 
                        action.ActionValue.Contains("Load Track By ID")
                    )
                    {
                        var id = action.GetSongId();
                        if ((id[0] != -1) && (id[1] != -1))
                        {
                            var song = DbContext.songs.Find(id[1]);
                            action.SongFolder = Path.GetDirectoryName(song.path);
                        }

                        var songList = action.SongFolder
                                            .GetSongsFromFolder(DbContext)
                                            .OrderBy(x => x.path)
                                            .ToList();


                        Song s = null;

                        if (action.SongIndex > songList.Count()-1)
                        {
                            action.SongIndex = 0; //Reset the index
                        }

                        s = songList.ElementAt(action.SongIndex);
                        action.SongIndex++;

                        if (s != null)
                        {
                            /*Load Track By ID|0|36|the daylights - black dove mp3 download.mp3|Top*/
                            var parts = action.ActionValue.Split('|');
                            if (parts.Length >= 5)
                            {
                                parts[2] = s.ID.ToString();

                                //var pathParts = s.path.Split('\\');//get the file name
                                //parts[3] = pathParts[pathParts.Length - 1];

                                parts[3] = Path.GetFileName(s.path);
                                action.ActionValue = $"{parts[0]}|{parts[1]}|{parts[2]}|{parts[3]}|{parts[4]}";
                            }
                        }

                        break;//foreach
                    }
            }

            var sb = new StringBuilder();
            foreach (var act in rotator.Model.actions)
            {
                sb.Append(act.ActionValue + Environment.NewLine);
            }

            rotator.Model.data = sb.ToString().TrimEnd('\n', '\r')/*.TrimEnd('\r')*/;//.TrimEnd(Environment.NewLine)
        }
        
        /// <summary>
        /// Gets all the songs' folders
        /// </summary>
        /// <returns></returns>
        private void LoadAllSongFolders()
        {
            //if (Model == null) return;
            //if (Model.actions == null) return;

            foreach (var rotator in Rotators)
            {
                foreach (var track in rotator.Model.actions)
                {
                    track.SongFolder = track.GetSongFolder(DbContext); //GetSongFolder(track);
                }
                
            }

        }


        /// <summary>
        /// Called when the day changes to load the new athan times from the csv file an apply them to the database
        /// </summary>
        /// <param name="msg"></param>
        public void HandleMessage(DayChangedMsg msg)
        {
            InvokeDispatcher(() =>
            {
                if (!IsRotationActivated)
                    IsRotationActivated = true;

                ApplyRotations();
            });

        }

        /// <summary>
        /// Called when a rotation control must be deleted
        /// </summary>
        /// <param name="msg"></param>
        public void HandleMessage(DeleteRotatorMsg msg)
        {
            if (RotatorControls.Count > 0)
            {
                RotatorControls.RemoveAt(msg.Content);
                Rotators.Remove(_rotators.ElementAt(msg.Content));

                foreach (var rot in Rotators)
                {
                    if (rot.Index > 0)
                        rot.Index--;
                }
            }
        }

        public override void Cleanup()
        {
            EventList = null;
            Rotators = null;
            ApplyRotationsC = null;
            DbContext?.Dispose();
        }
        #endregion

    }
}
