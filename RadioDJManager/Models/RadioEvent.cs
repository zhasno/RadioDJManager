using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using RadioDJManager.Properties;

namespace RadioDJManager.Models
{
    [Serializable]
    [Table("event")]
    public  class RadioEvent : INotifyPropertyChanged, ICloneable
    {
        [XmlIgnore]
        private int _ID { get; set; }

        //[XmlElement]
        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private int _type { get; set; }

        //[XmlElement]
        public int type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private string _time { get; set; }

        public string time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private string _name { get; set; }

        public string name
        {
            get {return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private Nullable<System.DateTime> _date { get; set; }

        public Nullable<System.DateTime> date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private string _day { get; set; }

        public string day
        {
            get { return _day; }
            set
            {
                _day = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private string _hours { get; set; }

        public string hours
        {
            get { return _hours; }
            set
            {
                _hours = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private string _data { get; set; }

        public string data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private string _enabled { get; set; }

        public string enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private int _catID { get; set; }

        public int catID
        {
            get { return _catID; }
            set
            {
                _catID = value;
                OnPropertyChanged();
            }
        }

        [NotMapped]
        [XmlIgnore]
        private bool isSelected { get; set; }

        [NotMapped]
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }

        [NotMapped]
        [XmlIgnore]
        private ObservableCollection<EventAction> _actions { get; set; }

        [NotMapped]
        public ObservableCollection<EventAction> actions
        {
            get { return _actions; }
            set
            {
                _actions = value;
                OnPropertyChanged();
            }
        }

        [NotMapped]
        [XmlIgnore]
        private int index { get; set; }

        [NotMapped]
        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                OnPropertyChanged();
            }
        }

        //[NotMapped]
        //[XmlIgnore]
        //private string calculatedTime { get; set; }

        //[NotMapped]
        //public string CalculatedTime
        //{
        //    get { return calculatedTime; }
        //    set
        //    {
        //        calculatedTime = value;
        //        OnPropertyChanged("CalculatedTime");
        //    }
        //}

        public RadioEvent()
        {
            //_actions = new ObservableCollection<Action>();
            actions = new ObservableCollection<EventAction>();

            IsSelected = false;
        }

        public RadioEvent(RadioEvent ev)
        {
            ID = ev.ID;
            IsSelected = ev.IsSelected;
            
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

        public object Clone()
        {
            return new RadioEvent
            {

            };
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID",ID,typeof(int));
            info.AddValue("type", type, typeof(int));
            info.AddValue("time", time, typeof(string));
            info.AddValue("name", name, typeof(string));
            info.AddValue("date", date, typeof(DateTime));
            info.AddValue("day", day, typeof(string));
            info.AddValue("hours", hours, typeof(string));
            info.AddValue("data", data, typeof(string));
            info.AddValue("enabled", enabled, typeof(string));
            info.AddValue("catID", catID, typeof(int));
            info.AddValue("IsSelected", IsSelected, typeof(bool));
        }

        public RadioEvent(SerializationInfo info, StreamingContext context)
        {
            ID= (int)info.GetValue("ID", typeof(int));
            type = (int)info.GetValue("type", typeof(int));
            time = (string)info.GetValue("time", typeof(string));
            name = (string)info.GetValue("name", typeof(string));
            date = (DateTime)info.GetValue("date", typeof(DateTime));
            day = (string)info.GetValue("day", typeof(string));
            hours = (string)info.GetValue("hours", typeof(string));
            data = (string)info.GetValue("data", typeof(string));
            enabled = (string)info.GetValue("enabled", typeof(string));
            catID = (int)info.GetValue("catID", typeof(int));
            IsSelected = (bool)info.GetValue("IsSelected", typeof(bool));
        }

    }
}
