using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using Tracker_Software.Models;
using System.ComponentModel.DataAnnotations;
using Tracker_Software.Notification;
using System.Collections.ObjectModel;

namespace Tracker_Software.Model
{
    public class PatientModel : PropertyChangedNotification 
    {
    
        #region Properties

        public int PatientId
        {
            get { return GetValue(() => PatientId); }
            set { SetValue(() => PatientId, value); }
        }

        [Required(ErrorMessage = @"First Name is Required")] 
        public string FirstName
        {
            get { return GetValue(() => FirstName); }
            set { SetValue(() => FirstName, value); }
        }
     
        public string MiddleName
        {
            get { return GetValue(() => MiddleName); }
            set { SetValue(() => MiddleName, value); }
        }

        [Required(ErrorMessage = @"Last Name is Required")] 
        public string LastName
        {
            get { return GetValue(() => LastName); }
            set { SetValue(() => LastName, value); }
        }

        public Gender Gender
        {
             get { return GetValue(() => Gender); }
            set { SetValue(() => Gender, value); }
        }

        public Race Race
        {
             get { return GetValue(() => Race); }
            set { SetValue(() => Race, value); }
        }

        [Required(ErrorMessage = @"!")] 
        public DateTime? DateOfBirth
        {
            get { return GetValue(() => DateOfBirth); }
            set { SetValue(() => DateOfBirth, value); }
        }

        [Required(ErrorMessage = @"Phone Number is required")]
        [Phone(ErrorMessage = @"Phone Number is Invalid")]
        public string PhoneNumber
        {
            get { return GetValue(() => PhoneNumber); }
            set { SetValue(() => PhoneNumber, value); }
        }

        public string Ethnicity
        {
            get { return GetValue(() => Ethnicity); }
            set { SetValue(() => Ethnicity, value); }
        }

        [Required(ErrorMessage = @"Diagnosis is Required")] 
        public string Diagnosis
        {
            get { return GetValue(() => Diagnosis); }
            set { SetValue(() => Diagnosis, value); }
        }

        public string ChronicProblems
        {
            get { return GetValue(() => ChronicProblems); }
            set { SetValue(() => ChronicProblems, value); }
        }

        public string Address
        {
            get { return GetValue(() => Address); }
            set { SetValue(() => Address, value); }
        }

        [Required(ErrorMessage = @"Number is Required")] 
        public string SocialSecurityNumber
        {
            get { return GetValue(() => SocialSecurityNumber); }
            set { SetValue(() => SocialSecurityNumber, value); }
        }

        public string MartialStatus
        {
            get { return GetValue(() => MartialStatus); }
            set { SetValue(() => MartialStatus, value); }
        }

        public int LocationId
        {
            get { return GetValue(() => LocationId); }
            set { SetValue(() => LocationId, value); }
        }

        public ObservableCollection<string> Notes
        {
            get { return GetValue(() => Notes); }
            set { SetValue(() => Notes, value); }
        }

        #endregion
    }
}
