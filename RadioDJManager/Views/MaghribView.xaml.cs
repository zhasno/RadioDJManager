using System.Windows.Controls;
using RadioDJManager.ViewModels;

namespace RadioDJManager.Views
{
    /// <summary>
    /// Interaction logic for MaghribView.xaml
    /// </summary>
    public partial class MaghribView : UserControl
    {
        private AthanViewModel _viewModel;
        public MaghribView()
        {
            InitializeComponent();
            _viewModel = new AthanViewModel(5);
            DataContext = _viewModel;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //InitializeComponent();
            //viewModel = new AthanViewModel(5);
            //DataContext = viewModel;
        }


    }
}
