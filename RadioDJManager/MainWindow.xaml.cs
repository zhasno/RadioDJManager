using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;
using Messaging;
using RadioDJManager.Messages;
using RadioDJManager.ViewModels;

namespace RadioDJManager 
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AppMetroWindowBase
    {
        private MainWindowViewModel _viewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new MainWindowViewModel();
            
            _viewModel.ExitEvent += Exit;
            _viewModel.ShowWindowEvent += ShowWindow;
            _viewModel.NavigateToSettingsEvent += NavigateToSettings;

            DataContext = _viewModel;

            CheckAppSettings();
        }

        private void NavigateToSettings(object sender, EventArgs e)
        {
            Maximize();
            ChangeSettingsFlyout.IsOpen = true;

            //NavigationCommands.GoToPage.Execute("/Pages/Settings.xaml", (ModernFrame)GetTemplateChild("tb"));
            //ContentSource = new Uri("/Pages/Settings.xaml",UriKind.RelativeOrAbsolute);

        }

        private void ShowWindow(object sender, EventArgs e)
        {
            Maximize();

        }

        private void ModernWindow_StateChanged(object sender, EventArgs e)
        {
            if(this.WindowState == WindowState.Minimized)
                Visibility = Visibility.Collapsed;
            
        }

        private void Maximize()
        {
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Maximized;
                Visibility = Visibility.Visible;
            }
        }

        private async void ModernWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = await this.ShowMessageAsync("Closing the application", "Are you sure ?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Negative)
                e.Cancel = true;

            else
            {
                _viewModel.Dispose();
            }
        }

        private void Exit(object sender, EventArgs e)
        {
            Close();
        }

        private async Task Close()
        {
            var result = await this.ShowMessageAsync("Closing the application", "Are you sure ?", MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                _viewModel.Dispose();
                Environment.Exit(0);
            }

        }


        private void CheckAppSettings()
        {
            var isValid = !string.IsNullOrEmpty(RadioDJManager.Properties.Settings.Default.CSVPath) &&
                          File.Exists(RadioDJManager.Properties.Settings.Default.CSVPath) &&
                          !string.IsNullOrEmpty(RadioDJManager.Properties.Settings.Default.Database) &&
                          !string.IsNullOrEmpty(RadioDJManager.Properties.Settings.Default.Password) &&
                          !string.IsNullOrEmpty(RadioDJManager.Properties.Settings.Default.Server) &&
                          !string.IsNullOrEmpty(RadioDJManager.Properties.Settings.Default.Username) &&
                          !string.IsNullOrEmpty(RadioDJManager.Properties.Settings.Default.Database);

            if (!isValid)
                ChangeSettingsFlyout.IsOpen = true;
        }
    }
}
