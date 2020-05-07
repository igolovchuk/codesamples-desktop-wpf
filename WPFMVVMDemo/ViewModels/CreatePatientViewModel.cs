using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Tracker_Software.Model;
using Tracker_Software.Models;
using Tracker_Software.ViewModels;

namespace Tracker_Software.ViewModel
{
    public class CreatePatientViewModel : ViewModelBase
    {
        #region Fields

        private readonly IDataService _dataService;

        private PatientModel _newPatient;

        private IEnumerable<Gender> ddlItemSource_Gender;

        private IEnumerable<Race> ddlItemSource_Race;

        private static List<string> ddlItemSource_Facilities;

        private string _selectedFacility;

        #endregion

        #region Properties

        public static int Errors { get; set; }

        public AddNoteViewModel AddNoteViewModel { get; set; }

        public string SelectedFacility
        {
            get
            {             
                return _selectedFacility; }
            set
            {
                _selectedFacility = value;
                RaisePropertyChanged(() => SelectedFacility);
            }
        }

        public PatientModel NewPatient
        {
            get
            {
                if (_newPatient == null)
                    _newPatient = new PatientModel();
                return _newPatient;
            }
            set
            {
                _newPatient = value;
                RaisePropertyChanged(() => NewPatient);
            }
        }

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

        public List<string> DDLItemSource_Facilities
        {
            get
            {            
                return _dataService.RetrieveFacilities(ddlItemSource_Facilities);
            }
            set
            {
                ddlItemSource_Facilities = value;
                RaisePropertyChanged(() => DDLItemSource_Facilities);
            }
        }

        #endregion

        #region Commands

        public ICommand SavePatientCommand { get; private set; }

        public ICommand CancelSavingCommand { get; private set; }

        #endregion

        #region Constructors

        public CreatePatientViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _selectedFacility = DDLItemSource_Facilities[0];
            NewPatient.Notes = new ObservableCollection<string>();
            AddNoteViewModel = new AddNoteViewModel();
            AddNoteViewModel.Save += AddNoteViewModelOnSave;
            AddNoteViewModel.Cancel += AddNoteViewModelOnCancel;
            SavePatientCommand = new RelayCommand(ExecuteSavePatientCommand, CanExecuteSavePatientCommand);
            CancelSavingCommand = new RelayCommand(ExecuteCancelSavingCommand);

          
        }

        #endregion

        #region Methods

        private void AddNoteViewModelOnSave(object sender, EventArgs eventArgs)
        {
            NewPatient.Notes.Add(AddNoteViewModel.Note);
            AddNoteViewModel.Reset();
        }

        private void AddNoteViewModelOnCancel(object sender, EventArgs eventArgs)
        {
            AddNoteViewModel.Reset();
        }


        private void ExecuteCancelSavingCommand()
        {
            ((MainViewModel) Application.Current.MainWindow.DataContext).CurrentView = new PatientsUserControl();
        }

        private void ExecuteSavePatientCommand()
        {
            _dataService.CreatePatient(NewPatient, SelectedFacility);
            PatientsViewModel.UpdatePatients();
            
        }

        private bool CanExecuteSavePatientCommand()
        {
            if (Errors == 0)
                return true;
            return false;
        }
       

        #endregion
    }
}