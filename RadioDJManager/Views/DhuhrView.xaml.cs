using System.Windows.Controls;
using RadioDJManager.ViewModels;

namespace RadioDJManager.Views
{
    /// <summary>
    /// Interaction logic for DhuhrView.xaml
    /// </summary>
    public partial class DhuhrView : UserControl
    {
        private AthanViewModel _viewModel;
        public DhuhrView()
        {
            InitializeComponent();
            _viewModel = new AthanViewModel(3);
            DataContext = _viewModel;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //InitializeComponent();
            //viewModel = new AthanViewModel(3);
            //DataContext = viewModel;
        }

    }
}
