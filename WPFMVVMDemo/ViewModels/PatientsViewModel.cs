using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Tracker_Software.Model;
using Tracker_Software.Models;

namespace Tracker_Software.ViewModel
{
    public class PatientsViewModel : ViewModelBase
    {
        #region Fields

        private static IDataService _dataService;
        private PatientModel _selectedPatientPatient;

        #endregion

        #region Properties

        public PatientModel SelectedPatient
        {
            get { return _selectedPatientPatient; }
            set
            {
                _selectedPatientPatient = value;
                RaisePropertyChanged(() => SelectedPatient);
            }
        }

        public static ObservableCollection<PatientModel> Patients { get; set; }

        #endregion

        #region Commands

        public ICommand EditPatientsCommand { get; private set; }

        public ICommand CreatePatientsCommand { get; private set; }

        public ICommand DeletePatientsCommand { get; private set; }

        #endregion

        #region Constructors

        public PatientsViewModel(IDataService dataService)
        {
            _dataService = dataService;

            Patients = new ObservableCollection<PatientModel>();
           GetAllPatients();

            EditPatientsCommand = new RelayCommand(ExecuteEditPatientsCommand, CanExecuteEditPatientsCommand);
            CreatePatientsCommand = new RelayCommand(ExecuteCreatePatientsCommand);
            DeletePatientsCommand = new RelayCommand(ExecuteDeletePatientsCommand);

        }

        #endregion

        #region Methods

        public static void GetAllPatients()
        {
             _dataService.GetPatients(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    foreach (PatientModel patient in item)
                    {
                        Patients.Add(patient);
                    }
                });             
        }


        public static void UpdatePatients()
        {
            Patients = new ObservableCollection<PatientModel>();
            GetAllPatients();
            ((MainViewModel) Application.Current.MainWindow.DataContext).CurrentView = new PatientsUserControl();
        }

        private void ExecuteEditPatientsCommand()
        {
            ((MainViewModel) Application.Current.MainWindow.DataContext).CurrentView =
                new EditPatientUserControl(SelectedPatient);
            SelectedPatient = null;
        }

        private bool CanExecuteEditPatientsCommand()
        {
            if (SelectedPatient == null)
                return false;
            return true;
        }

        private void ExecuteCreatePatientsCommand()
        {
            ((MainViewModel) Application.Current.MainWindow.DataContext).CurrentView = new CreatePatientUserControl();
        }

        private void ExecuteDeletePatientsCommand()
        {
            MessageBoxResult res = MessageBox.Show("Are you sure want to delete?", "Confirmation!",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
            {
                ((MainViewModel) Application.Current.MainWindow.DataContext).CurrentView = new PatientsUserControl();
            }
            if (res == MessageBoxResult.Yes)
            {
                _dataService.DeletePatient(SelectedPatient);
                UpdatePatients();
            }
            
        }

       
        #endregion

    }
}