using System.ComponentModel.DataAnnotations;
using Tracker_Software.Notification;

namespace Tracker_Software.Models
{
    public class ClerkModel : PropertyChangedNotification
    {
        #region Properties

        [Required(ErrorMessage = @"User Name is Required")]
        public string UserName
        {
            get { return GetValue(() => UserName); }
            set { SetValue(() => UserName, value); }
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

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$",
            ErrorMessage = "Password must contain at least one UpperCase, one LowerCase and one Digit!")]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return GetValue(() => Password); }
            set { SetValue(() => Password, value); }
        }

        [Required]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password",
            ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword
        {
            get { return GetValue(() => ConfirmPassword); }
            set { SetValue(() => ConfirmPassword, value); }
        }

        #endregion
    }
}