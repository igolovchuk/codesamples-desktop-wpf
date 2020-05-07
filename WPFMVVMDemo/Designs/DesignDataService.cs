using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Tracker_Software.Model;
using Tracker_Software.Models;
using Tracker_Software.ViewModel;
using System.Windows;
using Tracker_Software.Views;

namespace Tracker_Software.Design
{
    public class DesignDataService : IDataService
    {
        #region Patient

        public void GetPatients(Action<ObservableCollection<PatientModel>, Exception> callback)
        {
            using (var context = new ApplicationDbContext())
            {
                List<Patient> patients = context.Patients.Where(p => p.IsActive).ToList();

                var patientModels = new ObservableCollection<PatientModel>();

                foreach (Patient patient in patients)
                {
                    List<string> notes =
                        context.Notes.Where(u => u.PatientId == patient.PatientId).Select(u => u.NoteText).ToList();

                    patientModels.Add(new PatientModel
                    {
                        PatientId = patient.PatientId,
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        MiddleName = patient.MiddleName,
                        DateOfBirth = patient.DateOfBirth,
                        Gender = patient.Gender,
                        PhoneNumber = patient.PhoneNumber,
                        Race = patient.Race,
                        Ethnicity = patient.Ethnicity,
                        SocialSecurityNumber = patient.SocialSecurityNumber,
                        Address = patient.Address,
                        ChronicProblems = patient.ChronicProblems,
                        Diagnosis = patient.Diagnosis,
                        MartialStatus = patient.MartialStatus,
                        Notes = new ObservableCollection<string>(notes)
                    });
                }
                callback(patientModels, null);
            }
        }

        public void CreatePatient(PatientModel newPatient, string selectedFacility)
        {
            using (var db = new ApplicationDbContext())
            {
                var selectedLocId = db.Facilities.Where(u => u.Name == selectedFacility).Select(u => u.Id);

                foreach (var id in selectedLocId)
                {

                    db.Patients.Add(new Patient
                    {
                        FirstName = newPatient.FirstName,
                        MiddleName = newPatient.MiddleName,
                        LastName = newPatient.LastName,
                        Gender = newPatient.Gender,
                        Race = newPatient.Race,
                        DateOfBirth = newPatient.DateOfBirth,
                        PhoneNumber = newPatient.PhoneNumber,
                        Ethnicity = newPatient.Ethnicity,
                        Diagnosis = newPatient.Diagnosis,
                        ChronicProblems = newPatient.ChronicProblems,
                        Address = newPatient.Address,
                        MartialStatus = newPatient.MartialStatus,
                        SocialSecurityNumber = newPatient.SocialSecurityNumber,
                        IsActive = true,
                        LocationId = id
                    });
                }
                db.SaveChanges();

                int idPat =
                    db.Patients.First(
                        u =>
                            u.FirstName == newPatient.FirstName && u.LastName == newPatient.LastName &&
                            u.DateOfBirth == newPatient.DateOfBirth).PatientId;
                foreach (string note in newPatient.Notes)
                {
                    db.Notes.Add(new Note
                    {
                        PatientId = idPat,
                        NoteText = note
                    });
                }
                db.SaveChanges();

                newPatient = null;
            }
        }

        public void DeletePatient(PatientModel selectedPatient)
        {
            using (var db = new ApplicationDbContext())
            {
                Patient patient = db.Patients.First(u => u.PatientId == selectedPatient.PatientId);
                IQueryable<Note> notes = db.Notes.Where(u => u.PatientId == patient.PatientId);
                foreach (Note note in notes)
                {
                    db.Notes.Remove(note);
                }
                patient.IsActive = false;
                
                db.SaveChanges();
            }
        }

        public void EditPatient(PatientModel editedPatient, PatientModel selectedPatient, string selectedFacility)
        {
            using (var db = new ApplicationDbContext())
            {
                Patient patient = db.Patients.First(u => u.PatientId == selectedPatient.PatientId);
                IQueryable<Note> notes = db.Notes.Where(u => u.PatientId == patient.PatientId);
                foreach (Note note in notes)
                {
                    db.Notes.Remove(note);
                }
                db.Patients.Remove(patient);

                var selectedLocId = db.Facilities.Where(u => u.Name == selectedFacility).Select(u => u.Id);

                foreach (var id in selectedLocId)
                {
                    db.Patients.Add(new Patient
                    {
                        FirstName = editedPatient.FirstName,
                        MiddleName = editedPatient.MiddleName,
                        LastName = editedPatient.LastName,
                        Gender = editedPatient.Gender,
                        Race = editedPatient.Race,
                        DateOfBirth = editedPatient.DateOfBirth,
                        PhoneNumber = editedPatient.PhoneNumber,
                        Ethnicity = editedPatient.Ethnicity,
                        Diagnosis = editedPatient.Diagnosis,
                        ChronicProblems = editedPatient.ChronicProblems,
                        Address = editedPatient.Address,
                        MartialStatus = editedPatient.MartialStatus,
                        SocialSecurityNumber = editedPatient.SocialSecurityNumber,
                        IsActive = true,
                        LocationId = id
                    });
                }
                db.SaveChanges();
                int idPat =
                    db.Patients.First(
                        u =>
                            u.FirstName == editedPatient.FirstName && u.LastName == editedPatient.LastName &&
                            u.DateOfBirth == editedPatient.DateOfBirth).PatientId;
                foreach (string note in editedPatient.Notes)
                {
                    db.Notes.Add(new Note
                    {
                        PatientId = idPat,
                        NoteText = note
                    });
                }
                db.SaveChanges();
            }
        }

        public List<string> RetrieveFacilities(List<string> ddlItemSource_Facilities)
        {
            using (var db = new ApplicationDbContext())
            {
                ddlItemSource_Facilities = (from fc in db.Facilities.ToList() where fc.IsActive == true select fc.Name).ToList();
            }
            return ddlItemSource_Facilities;
        }

        #endregion

        #region Clerk

        public void CreateClerk(ClerkModel newClerk)
        {
            using (var db = new ApplicationDbContext())
            {
                var userManager =
                    new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                var user = new ApplicationUser
                {
                    UserName = newClerk.UserName,
                    FirstName = newClerk.FirstName,
                    MiddleName = newClerk.MiddleName,
                    LastName = newClerk.LastName,
                    EmailConfirmed = true,
                    IsActive = true
                };

                userManager.Create(user, newClerk.Password);
                userManager.AddToRole(user.Id, "Clerk");
            }
        }

        public void DeleteClerk(ClerkModel selectedClerk)
        {
            using (var db = new ApplicationDbContext())
            {
                ApplicationUser clerk = db.Users.First(u => u.UserName == selectedClerk.UserName);

                clerk.IsActive = false;

                db.SaveChanges();
            }
        }

        #endregion

        #region Login

       

        public void LogIn(LoginModel current, bool isAuthenticated)
        {
            var userManager =
                new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser user = userManager.Find(current.UserName, current.Password);
            if (user != null)
            {
                
                if (userManager.IsInRole(user.Id, "Clerk") || userManager.IsInRole(user.Id, "Administrator"))
                {
                    isAuthenticated = true;
                    ((MainViewModel)Application.Current.MainWindow.DataContext).CurrentView = new PatientsUserControl();
                }
            }
            else
            {
                MessageBoxResult res = MessageBox.Show("Invalid Username or Password!", "Error!", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                if (res == MessageBoxResult.OK)
                {
                    ((MainViewModel)Application.Current.MainWindow.DataContext).CurrentView = new LoginUserControl();
                }
            }
        }

        #endregion


    }
}