using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using RadioDJManager.Properties;

namespace RadioDJManager.Models
{
    [Table("rotations_list")]
    public  class RotationsList : INotifyPropertyChanged
    {
        private int _ID { get; set; }

        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        private int _pID { get; set; }

        public int pID
        {
            get { return _pID; }
            set
            {
                _pID = value;
                OnPropertyChanged(nameof(pID));
            }
        }

        private int _catID { get; set; }

        public int catID
        {
            get { return _catID; }
            set
            {
                _catID = value;
                OnPropertyChanged(nameof(catID));
            }
        }
        private int _subID { get; set; }

        public int subID
        {
            get { return _subID; }
            set
            {
                _subID = value;
                OnPropertyChanged(nameof(subID));
            }
        }

        private int _genID { get; set; }

        public int genID
        {
            get { return _genID; }
            set
            {
                _genID = value;
                OnPropertyChanged(nameof(genID));
            }
        }
        private int _selType { get; set; }

        public int selType
        {
            get { return _selType; }
            set
            {
                _selType = value;
                OnPropertyChanged(nameof(selType));
            }
        }

        private int _sweeper { get; set; }

        public int sweeper
        {
            get { return _sweeper; }
            set
            {
                _sweeper = value;
                OnPropertyChanged(nameof(sweeper));
            }
        }
        private string _repeatRule { get; set; }

        public string repeatRule
        {
            get { return _repeatRule; }
            set
            {
                _repeatRule = value;
                OnPropertyChanged(nameof(repeatRule));
            }
        }

        private int _ord { get; set; }

        public int ord
        {
            get { return _ord; }
            set
            {
                _ord = value;
                OnPropertyChanged(nameof(ord));
            }
        }

        #region INC

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
