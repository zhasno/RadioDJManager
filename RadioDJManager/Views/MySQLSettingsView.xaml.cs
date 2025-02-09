using System.Windows;
using System.Windows.Controls;
using RadioDJManager.ViewModels;

namespace RadioDJManager.Views
{
    /// <summary>
    /// Interaction logic for MySQLSettingsView.xaml
    /// </summary>
    public partial class MySQLSettingsView : UserControl
    {
        private MySQLSettingsViewModel viewModel { get; set; }
        public MySQLSettingsView()
        {
            viewModel = new MySQLSettingsViewModel();
            InitializeComponent();
            
            DataContext = viewModel;
            pb.Password = viewModel.Model.Password;
        }

        private void ModernButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Model.Password = pb.Password;
        }
    }
}
