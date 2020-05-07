using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tracker_Software.Models;

namespace Tracker_Software.Model
{
    public interface IDataService
    {
        void GetPatients(Action<ObservableCollection<PatientModel>, Exception> callback);

        void CreatePatient(PatientModel newPatient, string selectedFacility);

        List<string> RetrieveFacilities(List<string> ddlItemSource_Facilities);

        void DeletePatient(PatientModel selectedPatient);

        void EditPatient(PatientModel editedPatient, PatientModel selectedPatient, string selectedFacility);

        void CreateClerk(ClerkModel newClerk);

        void DeleteClerk(ClerkModel selectedClerk);
        
        void LogIn(LoginModel current, bool isAuthenticated);
    }
}