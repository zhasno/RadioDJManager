using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using Messaging;
using RadioDJManager.Messages;

namespace RadioDJManager.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, 
                                        ISubscriber<ShowUiNotificationMsg>
                                        
    {
        #region Properties
        public ICommand ShowWindowC { get; set; }
        public ICommand NavigateToSettingsC { get; set; }
        public ICommand UpdateC { get; set; }
        public ICommand ExitC { get; set; }

        public event EventHandler ShowWindowEvent;
        public event EventHandler NavigateToSettingsEvent;
        //public event EventHandler UpdateEvent;
        public event EventHandler ExitEvent;

        private bool _isSnackbarActive;
        private string _snackbarContent;

        private SnackbarMessageQueue _messages;


        public SnackbarMessageQueue Messages
        {
            get { return _messages; }
            set { _messages = value; OnPropertyChanged(); }
        }

        public bool IsSnackbarActive
        {
            get { return _isSnackbarActive; }
            set { _isSnackbarActive = value; OnPropertyChanged(); }
        }

        public string SnackbarContent
        {
            get { return _snackbarContent; }
            set { _snackbarContent = value; OnPropertyChanged(); }
        } 
        #endregion

        public MainWindowViewModel()
        {
            ShowWindowC = new RelayCommand(ShowWindow, _=> true);
            NavigateToSettingsC = new RelayCommand(NavigateToSettings, _=> true);
            UpdateC = new RelayCommand(Update, _=> true);
            ExitC = new RelayCommand(Exit, _=> true);

            if (!Directory.Exists("States"))
                Directory.CreateDirectory("States");

            Messages = new SnackbarMessageQueue(TimeSpan.FromSeconds(1.5));
        }

        public void ShowWindow(object input = null)
        {
            ShowWindowEvent(this, new EventArgs());
        }
        public void NavigateToSettings(object input = null)
        {
            NavigateToSettingsEvent(this, new EventArgs());
            //NavigationCommands.GoToPage.Execute("/Pages/ModifyReunionFromList.xaml", (ModernFrame)GetTemplateChild("cbReunions"));

        }
        public void Update(object input = null)
        {
            //UpdateEvent(this, new EventArgs());
        }
        public void Exit(object input = null)
        {
            ExitEvent(this, new EventArgs());
        }
        
        public void HandleMessage(ShowUiNotificationMsg msg)
        {
            Messages.Enqueue(msg.Message);
        }


        public override void Cleanup()
        {
            ShowWindowC = null;
            NavigateToSettingsC = null;
            UpdateC = null;
            ExitC = null;
            ShowWindowEvent = null;
            NavigateToSettingsEvent = null;
            ExitEvent = null;
        }
    }
}
