using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using Messaging;
using RadioDJManager.Data;
using RadioDJManager.Messages;
using RadioDJManager.Properties;

namespace RadioDJManager.ViewModels
{
    [Serializable]
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        #region Properties

        [XmlIgnore]
        protected RadioDbContext DbContext {get;set;}

        [XmlIgnore]
        protected EventAggregator EventAggregator { get; set; }

        [XmlIgnore]
        public ICommand CancelCommand { get; set; }

        #endregion

        public ViewModelBase()
        {
            DbContext = Utils.CreateDbContext();

            CancelCommand = new RelayCommand(CancelAction, _ => true);

            EventAggregator = EventAggregator.Instance;
            EventAggregator.Subscribe(this);
        }

        protected virtual void CancelAction(object obj)
        {
            //EventAggregator.Publish(new CloseWindowMsg(typeof(T)));
        }

        protected void ExecuteAndHandleErrors(string errorMsgTitle, Action actionToExecute)
        {
            try
            {
                actionToExecute.Invoke();
            }
            catch (Exception ex)
            {
                EventAggregator.Publish(new ShowUiMessage(errorMsgTitle, ex.GetLastInnerException().Message));
            }
        }

        protected void ExecuteAndNotify<T>(string errorMsgTitle, string successMsg, Type windowType, UIMessage<T> msg, Action actionToExecute)
        {
            try
            {
                actionToExecute.Invoke();

                PublishOnUiThread(msg);

                if (windowType != null)
                    PublishOnUiThread(new CloseWindowMsg(windowType));

                if (!string.IsNullOrEmpty(successMsg))
                    PublishOnUiThread(new ShowUiMessage(successMsg, successMsg));
            }
            catch (Exception ex)
            {
                PublishOnUiThread(new ShowUiMessage(errorMsgTitle ?? "Error", ex.GetLastErrorMessage()));
            }

        }

        protected void ExecuteAndNotify<T>(string errorMsgTitle, string successMsg, string flyoutName, UIMessage<T> msg, Action actionToExecute)
        {
            try
            {
                actionToExecute.Invoke();

                //EventAggregator.Publish(new UpdateViewDataMsg());
                if (msg != null)
                    PublishOnUiThread(msg);

                if (!string.IsNullOrEmpty(flyoutName))
                    PublishOnUiThread(new CloseFlyoutMsg(flyoutName));

                if (!string.IsNullOrEmpty(successMsg))
                    PublishOnUiThread(new ShowUiMessage(successMsg, successMsg));
            }
            catch (Exception ex)
            {
                PublishOnUiThread(new ShowUiMessage(errorMsgTitle ?? "Error", ex.GetLastErrorMessage()));
            }

        }
        protected void InvokeDispatcher(Action action)
        {
            if (action == null) return;
            App.Current.Dispatcher.Invoke(action);
        }

        protected void PublishOnUiThread<T>(T msg) where T : class
        {
            InvokeDispatcher(() => EventAggregator.Publish(msg));
        }

        protected void ShowMessage(string message, string title)
        {
            PublishOnUiThread(new ShowUiMessage(title, message));
        }

        protected void ShowNotification(string message)
        {
            PublishOnUiThread(new ShowUiNotificationMsg(message));
        }

        protected void RequestConfirmation(ConfirmationTypes type)
        {
            PublishOnUiThread(new ConfirmationRequestMsg() { Type = type });
        }

        /// <summary>
        /// Refreshes the data context
        /// </summary>
        protected void RefreshDbContext()
        {
            DbContext = Utils.CreateDbContext();
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region IDisposable
        private bool _disposed;
        public abstract void Cleanup();
        public void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {

                Cleanup();
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
