using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;
using Messaging;
using RadioDJManager.Events;
using RadioDJManager.Messages;

namespace RadioDJManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Timer _timer;
        private static DateTime _currentDate;

        public App()
        {
            _currentDate = DateTime.Now;

            _timer = new Timer(1000*60) { AutoReset = true };
            _timer.Elapsed += DateChanged;
            _timer.Enabled = true;

            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception).GetLastErrorMessage(), "Error", MessageBoxButton.OK);
        }


        /// <summary>
        /// When the day changes ,publish the event for the viewmodels to make the update on the database (ie : track rotation)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateChanged(object sender, ElapsedEventArgs e)
        {
            var date = DateTime.Now;
            var elapsed = date.Subtract(_currentDate).Days;

            if (elapsed >= 1)
            {
                _currentDate = date;

                EventAggregator.Instance.Publish(new DayChangedMsg(date));
            }
        }
    }
}
