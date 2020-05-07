using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Tracker_Software.Model;
using Tracker_Software.Models;
using Tracker_Software.ViewModel;
using Tracker_Software.Views;

namespace Tracker_Software.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Fields

        private readonly IDataService _dataService;
        private LoginModel _current;

        #endregion

        #region Properties

        public LoginModel Current
        {
            get
            {
                if (_current == null)
                    _current = new LoginModel();
                return _current;
            }
            set
            {
                _current = value;
                RaisePropertyChanged(() => Current);
            }
        }

        public static int Errors { get; set; }
        public bool IsAuthenticated { get; set; }

        #endregion

        #region Commands

        public ICommand LoginCommand { get; private set; }

        public ICommand RegisterCommand { get; private set; }

        #endregion

        #region Constructors

        public LoginViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.LoginInitializer();
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RegisterCommand = new RelayCommand(ExecuteRegisterCommand);          
        }

        #endregion

        #region Methods

        private void ExecuteLoginCommand()
        {
            _dataService.LogIn(Current, IsAuthenticated);
        }

        private bool CanExecuteLoginCommand()
        {
            if (Errors == 0)
                return true;
            return false;
        }
        private void ExecuteRegisterCommand()
        {
            Current = null;
            ((MainViewModel)Application.Current.MainWindow.DataContext).CurrentView = new CreateClerkUserControl();
        }

              
        #endregion
    }
}