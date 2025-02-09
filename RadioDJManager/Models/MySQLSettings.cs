using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Messaging;
using RadioDJManager.Events;
using RadioDJManager.Properties;

namespace RadioDJManager.Models
{
    public class MySQLSettings : INotifyPropertyChanged
    {
        private string _server { get; set; }

        public string Server
        {
            get { return _server; }
            set
            {
                _server = value;
                OnPropertyChanged(nameof(Server));
            }
        }

        //private int port { get; set; }

        //public int Port
        //{
        //    get { return port; }
        //    set
        //    {
        //        port = value;
        //        OnPropertyChanged("Port");
        //    }
        //}

        private string _database { get; set; }

        public string Database
        {
            get { return _database; }
            set
            {
                _database = value;
                OnPropertyChanged(nameof(Database));
            }
        }

        private string _username { get; set; }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password { get; set; }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _csvPath { get; set; }

        public string CsvPath
        {
            get { return _csvPath; }
            set
            {
                _csvPath = value;
                OnPropertyChanged(nameof(CsvPath));
            }
        }

        private bool _saveSession { get; set; }

        public bool SaveSession
        {
            get { return _saveSession; }
            set
            {
                _saveSession = value;
                OnPropertyChanged(nameof(SaveSession));
                if (SaveSession)
                    SelectedChoiceIndex = 0;
                else if(!SaveSession)
                    SelectedChoiceIndex = 1;

                //Utils.Utils.Instance.SessionSaveChangedAggregator.GetEvent<SessionSaveChangedEvent>().Publish(saveSession);
                EventAggregator.Instance.Publish(new SessionSaveChangedMsg(_saveSession));
            }
        }

        public ObservableCollection<string> SaveChoices { get; set; }

        private int selectedChoiceIndex { get; set; }

        public int SelectedChoiceIndex
        {
            get { return selectedChoiceIndex; }
            set
            {
                selectedChoiceIndex = value;
                OnPropertyChanged(nameof(SelectedChoiceIndex));
            }
        }

        
        public MySQLSettings()
        {
            SaveChoices = new ObservableCollection<string>() { "True", "False" };
            
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
