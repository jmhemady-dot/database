using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADNU_CFRS.Models.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Username:")]
        [Required(ErrorMessage = "Your username is invalid")]
        [StringLength(50)]
        public string username { get; set; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public EnumUserStatus status { get; set; } = EnumUserStatus.Default;

        public string message { get; set; }
    }
}