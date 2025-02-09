using System.Windows.Controls;
using RadioDJManager.ViewModels;

namespace RadioDJManager.Views
{
    /// <summary>
    /// Interaction logic for AsrView.xaml
    /// </summary>
    public partial class AsrView : UserControl
    {
        private AthanViewModel _viewModel;
        public AsrView()
        {
            InitializeComponent();
            _viewModel = new AthanViewModel(4);
            DataContext = _viewModel;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //InitializeComponent();
            //viewModel = new AthanViewModel(4);
            //DataContext = viewModel;
        }

    }
}
