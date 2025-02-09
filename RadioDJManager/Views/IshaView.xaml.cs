using System.Windows.Controls;
using RadioDJManager.ViewModels;

namespace RadioDJManager.Views
{
    /// <summary>
    /// Interaction logic for IshaView.xaml
    /// </summary>
    public partial class IshaView : UserControl
    {
        private AthanViewModel _viewModel;
        public IshaView()
        {
            InitializeComponent();
            _viewModel = new AthanViewModel(6);
            DataContext = _viewModel;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //InitializeComponent();
            //viewModel = new AthanViewModel(6);
            //DataContext = viewModel;
        }

    }
}
