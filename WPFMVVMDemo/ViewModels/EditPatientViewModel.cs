using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Tracker_Software.Model;
using Tracker_Software.Models;
using Tracker_Software.ViewModel;

namespace Tracker_Software.ViewModels
{
    public class EditPatientViewModel : ViewModelBase
    {
        #region Fields

        private readonly IDataService _dataService;
        private PatientModel _editedPatientPatient;

        private IEnumerable<Gender> ddlItemSource_Gender;

        private IEnumerable<Race> ddlItemSource_Race;

        private List<string> ddlItemSource_Facility;

        private string _selectedFacility;

        #endregion

        #region Properties

        public string SelectedFacility
        {
            get {  return _selectedFacility; }
            set
            {
                _selectedFacility = value;
                RaisePropertyChanged(() => SelectedFacility);
            }
        }

        public List<string> DDLItemSource_Facility
        {
            get
            {
                return _dataService.RetrieveFacilities(ddlItemSource_Facility);
            }
            set
            {
                ddlItemSource_Facility = value;
                RaisePropertyChanged(() => DDLItemSource_Facility);
            }
        }

        public PatientModel EditedPatient
        {
            get { return _editedPatientPatient; }
            set
            {
                _editedPatientPatient = value;
                RaisePropertyChanged(() => EditedPatient);
            }
        }

        public PatientModel Selected { get; set; }
        public static int Errors { get; set; }

        public AddNoteViewModel AddNoteViewModel { get; set; }

        public IEnumerable<Gender> DDLItemSource_Gender
        {
            get { return Enum.GetValues(typeof (Gender)).Cast<Gender>(); }
            set
            {
                ddlItemSource_Gender = value;
                RaisePropertyChanged(() => DDLItemSource_Gender);
            }
        }

        public IEnumerable<Race> DDLItemSource_Race
        {
            get { return Enum.GetValues(typeof (Race)).Cast<Race>(); }
            set
            {
                ddlItemSource_Race = value;
                RaisePropertyChanged(() => DDLItemSource_Race);
            }
        }

        #endregion

        #region Commands

        public ICommand EditPatientsCommand { get; private set; }

        public ICommand DeletePatientsCommand { get; private set; }

        public ICommand CancelSavingCommand { get; private set; }

        #endregion

        #region Constructors

        public EditPatientViewModel(PatientModel selPat)
        {
            _dataService = new DataService();                         
            AddNoteViewModel = new AddNoteViewModel();
            AddNoteViewModel.Save += AddNoteViewModelOnSave;
            AddNoteViewModel.Cancel += AddNoteViewModelOnCancel;

            EditPatientsCommand = new RelayCommand(ExecuteEditPatientsCommand, CanExecuteEditPatientsCommand);
            DeletePatientsCommand = new RelayCommand(ExecuteDeletePatientsCommand);
            CancelSavingCommand = new RelayCommand(ExecuteCancelSavingCommand);

            EditedPatient = new PatientModel
            {
                Gender = selPat.Gender,
                Race = selPat.Race,
                FirstName = selPat.FirstName,
                MiddleName = selPat.MiddleName,
                LastName = selPat.LastName,
                PhoneNumber = selPat.PhoneNumber,
                Ethnicity = selPat.Ethnicity,
                MartialStatus = selPat.MartialStatus,
                SocialSecurityNumber = selPat.SocialSecurityNumber,
                Address = selPat.Address,
                DateOfBirth = selPat.DateOfBirth,
                Diagnosis = selPat.Diagnosis,
                ChronicProblems = selPat.ChronicProblems,
                Notes = selPat.Notes,
                LocationId = selPat.LocationId
            };
            Selected = selPat;
            _selectedFacility = new ApplicationDbContext().Patients.First(u => u.PatientId == Selected.PatientId).Facility.Name;
            
        }

        #endregion

        #region Methods

        //Add Note
        private void AddNoteViewModelOnSave(object sender, EventArgs eventArgs)
        {
            EditedPatient.Notes.Add(AddNoteViewModel.Note);
            AddNoteViewModel.Reset();
        }

        private void AddNoteViewModelOnCancel(object sender, EventArgs eventArgs)
        {
            AddNoteViewModel.Reset();
        }

        //Cancel
        private void ExecuteCancelSavingCommand()
        {
            ((MainViewModel) Application.Current.MainWindow.DataContext).CurrentView = new PatientsUserControl();
        }

        //Delete
        private void ExecuteDeletePatientsCommand()
        {
            MessageBoxResult res = MessageBox.Show("Are you sure want to delete?", "Confirmation!",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                _dataService.DeletePatient(Selected);
                PatientsViewModel.UpdatePatients();
               
            }
        }

        //Edit
        private bool CanExecuteEditPatientsCommand()
        {
            if (Errors == 0)
                return true;
            return false;
        }

        private void ExecuteEditPatientsCommand()
        {
            _dataService.EditPatient(EditedPatient, Selected, SelectedFacility);          
            PatientsViewModel.UpdatePatients();
        }

        #endregion
    }
}