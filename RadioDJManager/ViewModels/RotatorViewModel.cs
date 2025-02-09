using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using RadioDJManager.Events;
using RadioDJManager.Models;
using EventAction = RadioDJManager.Models.EventAction;

namespace RadioDJManager.ViewModels
{
    [Serializable]
    public class RotatorViewModel : ViewModelBase
    {
        [XmlIgnore]
        private ObservableCollection<RadioEvent> _eventList;

        [XmlIgnore]
        public ObservableCollection<RadioEvent> EventList
        {
            get => _eventList;
            set
            {
                _eventList = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        public ICommand DeleteRotatorC { get; set; }

        [XmlIgnore] private RadioEvent _model;

        public RadioEvent Model
        {
            get { return _model; }
            set
            {
                if (_model?.actions != null)
                {
                    foreach (var action in _model.actions)
                    {
                        //action.ActionCheckedAggregator.GetEvent<ActionCheckedEvent>().Unsubscribe(CalculateFiringTime);
                        action.ActionCheckedEvent -= CalculateFiringTime;
                    }
                }

                _model = value;

                if (_model?.actions != null)
                {
                    //Id = model.ID;
                    foreach (var action in _model.actions)
                    {
                        //action.ActionCheckedAggregator.GetEvent<ActionCheckedEvent>().Subscribe(CalculateFiringTime);
                        action.ActionCheckedEvent += CalculateFiringTime;
                    }
                }

                OnPropertyChanged();
            }
        }

        //[XmlIgnore]
        //private int songIndex { get; set; }

        //public int SongIndex
        //{
        //    get { return songIndex; }
        //    set
        //    {
        //        songIndex = value;
        //        OnPropertyChanged("SongIndex");
        //    }
        //} 

        [XmlIgnore] private EventAction _selectedAction;

        public EventAction SelectedAction
        {
            get { return _selectedAction; }
            set
            {
                _selectedAction = value;
                OnPropertyChanged();
            }

        }

        [XmlIgnore] private int _index;

        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                OnPropertyChanged();
            }
        }

        public RotatorViewModel()
        {
            //SongIndex = 0;
            Model = new RadioEvent();

            if (SelectedAction == null)
                SelectedAction = new EventAction();

            DeleteRotatorC = new RelayCommand(DeleteRotatorControl, _ => true);
        }

        public RotatorViewModel(ObservableCollection<RadioEvent> eventList):this()
        {
            EventList = eventList;
        }

        public void DeleteRotatorControl(object input = null)
        {
            EventAggregator.Publish(new DeleteRotatorMsg(Index));
        }
        
        public override void Cleanup()
        {
            Model = null;
            SelectedAction = null;
            EventList = null;
            DeleteRotatorC = null;
            DbContext?.Dispose();
        }

        private void CalculateFiringTime(object sender, EventArgs e)
        {

        }
    }
}
