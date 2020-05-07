using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Tracker_Software.Model;
using Tracker_Software.Models;
using Tracker_Software.ViewModel;
using Tracker_Software.Views;
using System.Collections.ObjectModel;

namespace Tracker_Software.ViewModels
{
    public class CreateClerkViewModel : ViewModelBase
    {
        #region Fields

        private readonly IDataService _dataService;

        private ClerkModel _newClerk;

        #endregion

        #region Properties

        public static int Errors { get; set; }

        public ClerkModel NewClerk
        {
            get
            {
                if (_newClerk == null)
                    _newClerk = new ClerkModel();
                return _newClerk;
            }
            set
            {
                _newClerk = value;
                RaisePropertyChanged(() => NewClerk);
            }
        }

        public static ObservableCollection<ClerkModel> Clerks { get; set; }

        #endregion

        #region Commands

        public ICommand SaveClerkCommand { get; private set; }

        public ICommand CancelSavingCommand { get; private set; }

        #endregion

        #region Constructors

        public CreateClerkViewModel(IDataService dataService)
        {          
            SaveClerkCommand = new RelayCommand(ExecuteSaveClerkCommand, CanExecuteSaveClerkCommand);
            CancelSavingCommand = new RelayCommand(ExecuteCancelSavingCommand);

            _dataService = dataService;
        }

        #endregion

        #region Methods

        private void ExecuteCancelSavingCommand()
        {
            NewClerk = null;    
            ((MainViewModel)Application.Current.MainWindow.DataContext).CurrentView = new LoginUserControl();         
        }

        private void ExecuteSaveClerkCommand()
        {
            _dataService.CreateClerk(NewClerk);

            NewClerk = null;
            
            ((MainViewModel)Application.Current.MainWindow.DataContext).CurrentView = new LoginUserControl();
          
        }


        private bool CanExecuteSaveClerkCommand()
        {
            if (Errors == 0)
                return true;
            return false;
        }
       
        #endregion
    }
}