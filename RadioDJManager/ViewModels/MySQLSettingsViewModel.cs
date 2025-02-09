using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RadioDJManager.Events;
using RadioDJManager.Messages;
using RadioDJManager.Models;

namespace RadioDJManager.ViewModels
{
    public class MySQLSettingsViewModel : ViewModelBase
    {
        public MySQLSettings Model { get; set; }

        public ICommand SaveSettingsC { get; set; }
         
        public MySQLSettingsViewModel()
        {

            Model = new MySQLSettings
            {
                Server = Properties.Settings.Default.Server,
                //Port = Properties.Settings.Default.Port,
                Database = Properties.Settings.Default.Database,
                Username = Properties.Settings.Default.Username,
                Password = Properties.Settings.Default.Password,
                CsvPath = Properties.Settings.Default.CSVPath,
                SaveSession = Properties.Settings.Default.SaveSession
            };


            SaveSettingsC = new RelayCommand(SaveSettings, _=> CanSaveSettings);
        }

        public void SaveSettings(object input = null)
        {
            var oldPath = string.Copy(Properties.Settings.Default.CSVPath);
            Properties.Settings.Default.Server = Model.Server;
            //Properties.Settings.Default.Port = Model.Port;
            Properties.Settings.Default.Database = Model.Database;
            Properties.Settings.Default.Username = Model.Username;
            Properties.Settings.Default.Password = Model.Password;
            Properties.Settings.Default.CSVPath = Model.CsvPath;
            Properties.Settings.Default.SaveSession = Model.SaveSession;

            Properties.Settings.Default.Save();

            if (!oldPath.Trim().Equals(Properties.Settings.Default.CSVPath.Trim()))
                EventAggregator.Publish(new CsvPathChangedMsg(Properties.Settings.Default.CSVPath));

            ShowNotification("Settings Saved!");

            EventAggregator.Publish(new CloseFlyoutMsg(nameof(MainWindow.ChangeSettingsFlyout)));
        }

        public bool CanSaveSettings
        {
            get { return true; }
            private set { }
        }

        public override void Cleanup()
        {
        }
        
    }
}
