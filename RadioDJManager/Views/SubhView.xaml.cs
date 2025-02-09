using System.Windows.Controls;
using RadioDJManager.ViewModels;

namespace RadioDJManager.Views
{
    /// <summary>
    /// Interaction logic for SubhView.xaml
    /// </summary>
    public partial class SubhView : UserControl
    {
        private AthanViewModel _viewModel;
        
        public SubhView()
        {
            InitializeComponent();
            _viewModel = new AthanViewModel(1);
            DataContext = _viewModel;

            //cbEvents.SelectedItem = viewModel.Model;
            //Utils.Instance.SubhLoadedAggregator.GetEvent<AthanLoadedEvent>().Subscribe(LoadActions);
            //viewModel.LoadEvents();
        }

        private void LoadActions(string obj)
        {
            //cbEvents.ItemsSource = viewModel.EventList;
            //cbEvents.SelectedItem = viewModel.Model;
            //while (spActions.Children.Count >0)
            //{
            //    spActions.Children.RemoveAt(spActions.Children.Count -1);
            //}
            //foreach (var action in viewModel.Model.actions)
            //{
            //    var viewer = new CustomViewer(action);

            //}
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //InitializeComponent();
            //viewModel = new AthanViewModel(1);
            //DataContext = viewModel;
            //DataContext = viewModel;
            
        }



    }
}
