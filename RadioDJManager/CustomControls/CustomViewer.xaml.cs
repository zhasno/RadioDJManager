using System;
using System.Collections.Generic;
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
using RadioDJManager.Models;
using RadioDJManager.ViewModels;

namespace RadioDJManager.CustomControls
{
    /// <summary>
    /// Interaction logic for CustomViewer.xaml
    /// </summary>
    public partial class CustomViewer : UserControl
    {
        private RadioEvent _viewModel { get; set; }
        public CustomViewer(RadioEvent viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
