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
using Tracker_Software.Model;
using Tracker_Software.ViewModel;
using Tracker_Software.Views;

namespace Tracker_Software
{
   
    public partial class CreatePatientUserControl : UserControl
    {
        public CreatePatientUserControl()
        {
            InitializeComponent();

        }


        void OnAddClick(object sender, EventArgs e)
        {
            //For each dialog we use the same instance of ViewModel
            var customerDialogBox = new AddNote(((CreatePatientViewModel)this.DataContext).AddNoteViewModel);

            //No need to check DialogResult - it is respnsibility of ViewModel to interpret the result of commands
            //View only displays the window
            customerDialogBox.ShowDialog();
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added) CreatePatientViewModel.Errors += 1;
            if (e.Action == ValidationErrorEventAction.Removed) CreatePatientViewModel.Errors -= 1;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var errors = GrdCustomer.GetValue(Validation.ErrorsProperty);
        }
    }
}
