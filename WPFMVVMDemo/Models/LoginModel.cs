using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker_Software.Notification;

namespace Tracker_Software.Models
{
    public class LoginModel : PropertyChangedNotification 
    {
       
        [Required(ErrorMessage = @"User Name is Required")]
        public string UserName
        {
            get { return GetValue(() => UserName); }
            set { SetValue(() => UserName, value); }
        }

        [Required]
        [StringLength(100, ErrorMessage = "Password {0} must be less than {2} points.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return GetValue(() => Password); }
            set { SetValue(() => Password, value); }
        }
    }
}
