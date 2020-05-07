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
using Tracker_Software.ViewModels;

namespace Tracker_Software.Views
{
   
    public partial class LoginUserControl : UserControl
    {
        public LoginUserControl()
        {
            InitializeComponent();

        }
        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added) LoginViewModel.Errors += 1;
            if (e.Action == ValidationErrorEventAction.Removed) LoginViewModel.Errors -= 1;
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var errors = GrdCustomer.GetValue(Validation.ErrorsProperty);
        }
    }
}
