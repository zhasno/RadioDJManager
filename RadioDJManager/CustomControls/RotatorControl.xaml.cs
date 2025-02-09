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
using EventAction = RadioDJManager.Models.EventAction;

namespace RadioDJManager.CustomControls
{
    /// <summary>
    /// Interaction logic for RotatorControl.xaml
    /// </summary>
    public partial class RotatorControl : UserControl
    {
        public RotatorControl(RotatorViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = sender as ComboBox;
            var selectedAction = combobox.SelectedItem as EventAction;
            var selectedEvent = cbEvents.SelectedItem as RadioEvent;

            if (selectedEvent == null || selectedAction == null)
                return;

            foreach (var ac in selectedEvent.actions)
            {
                if (ac.ActionValue.Trim().Equals(selectedAction.ActionValue.Trim()))
                {
                    ac.IsChecked = true;

                    //break;
                }
                else
                {
                    ac.IsChecked = false;
                }
            }
        }

        private void cbEvents_Loaded(object sender, RoutedEventArgs e)
        {
            //((ComboBox)sender).GetBindingExpression(ComboBox.SelectedItemProperty)
            //                    .UpdateTarget();

            var combobox = sender as ComboBox;
            var selectedAction = combobox.SelectedItem as EventAction;
            var selectedEvent = cbEvents.SelectedItem as RadioEvent;
        }
    }
}
