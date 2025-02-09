using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using RadioDJManager.Events;
using RadioDJManager.Properties;

namespace RadioDJManager.Models
{
    [Serializable]
    public class EventAction : INotifyPropertyChanged
    {
        [XmlIgnore] private string _actionValue;

        public string ActionValue
        {
            get { return _actionValue; }
            set
            {
                _actionValue = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore] private List<string> _actionOptions;

        [XmlIgnore]
        public List<string> ActionOptions
        {
            get { return _actionOptions; }
            set
            {
                _actionOptions = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore] private string _selectedOption;

        public string SelectedOption
        {
            get { return _selectedOption; }
            set
            {
                _selectedOption = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore] private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (_isChecked == value)
                    return;

                _isChecked = value;
                
                if (_isChecked)
                {
                    //ActionCheckedAggregator.GetEvent<ActionCheckedEvent>().Publish(this);

                    ActionCheckedEvent?.Invoke(this, new EventArgs());

                }

                OnPropertyChanged();
            }
        }

        [XmlIgnore] private string _lenght;

        public string Lenght
        {
            get { return _lenght; }
            set
            {
                _lenght = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore] private string _songFolder;

        public string SongFolder
        {
            get { return _songFolder; }
            set
            {
                _songFolder = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore] private int _songIndex;

        public int SongIndex
        {
            get { return _songIndex; }
            set
            {
                _songIndex = value;
                OnPropertyChanged();
            }
        } 

        //public IEventAggregator ActionCheckedAggregator { get; set; }
        [field: NonSerialized]
        public event EventHandler ActionCheckedEvent;

        public EventAction()
        {
            //ActionCheckedAggregator = new EventAggregator();
            _actionOptions = new List<string>();
            ActionOptions = new List<string>()
                                            {
                                                "None",
                                                "Rotate by name order",
                                                "Rotate by 1st least played",
                                                "Rotate by 2nd least played",
                                                "Rotate by 3rd least played",
                                                "Rotate by 4th least played",
                                                "Rotate by 5th least played"
                                            };
            IsChecked = false;
            SongIndex = 0;
        }


        #region INC
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion

    }
}
