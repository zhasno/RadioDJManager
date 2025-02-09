using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Messaging;
using RadioDJManager.CustomControls;
using RadioDJManager.Events;
using RadioDJManager.ViewModels;

namespace RadioDJManager.Views
{
    /// <summary>
    /// Interaction logic for Rotations.xaml
    /// </summary>
    public partial class RotationsView : UserControl//, ISubscriber<DeleteRotatorMsg>
    {
        private RotationsViewModel _viewModel;
        //private ObservableCollection<RotatorViewModel> _rotators;

        public RotationsView()
        {
            InitializeComponent();

            _viewModel = new RotationsViewModel();
            //_rotators = new ObservableCollection<RotatorViewModel>();
            
            EventAggregator.Instance.Subscribe(this);

            //LoadRotatorsIntoStackPanel();

            DataContext = _viewModel;
        }


        //private void LoadRotatorsIntoStackPanel()
        //{
        //    for (int i = 0; i < _viewModel.Rotators.Count; i++)
        //    {
        //        var rotator = _viewModel.Rotators[i];

        //        LoadRotator(rotator);
        //    }
        //}

        //private void btnAddRotator_Click(object sender, RoutedEventArgs e)
        //{
        //    var rotator = new RotatorViewModel(_viewModel.EventList);

        //    _viewModel.Rotators.Add(rotator);

        //    LoadRotator(rotator);
        //}

        //private void LoadRotator(RotatorViewModel rotator)
        //{
        //    _rotators.Add(rotator);
        //    rotator.Index = _rotators.IndexOf(rotator);

        //    var rotatorControl = new RotatorControl(rotator)
        //    {
        //        VerticalAlignment = VerticalAlignment.Top,
        //        HorizontalAlignment = HorizontalAlignment.Center
        //    };
        //    spRotators.Children.Add(rotatorControl);
        //}

        //public void HandleMessage(DeleteRotatorMsg msg)
        //{
        //    if (spRotators.Children.Count > 0)
        //    {
        //        spRotators.Children.RemoveAt(msg.Content);
        //        _viewModel.Rotators.Remove(_rotators.ElementAt(msg.Content));
        //        _rotators.RemoveAt(msg.Content);

        //        foreach (var rot in _rotators)
        //        {
        //            if (rot.Index > 0)
        //                rot.Index--;
        //        }
        //    }
        //}
    }
}
