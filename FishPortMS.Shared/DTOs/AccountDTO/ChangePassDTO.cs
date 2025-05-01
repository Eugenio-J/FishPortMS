using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.AccountDTO
{
    public class ChangePassDTO
    {
        [
           Required(ErrorMessage = "Password field is required"),
           MinLength(8, ErrorMessage = "Password must be 8 charaters long"),
       ]
        public string CurrPassword { get; set; } = String.Empty;


        [
            Required(ErrorMessage = "Password field is required"),
            MinLength(8, ErrorMessage = "Password must be 8 charaters long"),
            StrongPassword(ErrorMessage = "Password must contain at least 1 capital letter and a number")
        ]
        public string NewPassword { get; set; } = String.Empty;


        [
            Required(ErrorMessage = "Confirm Password field is required"),
            Compare("NewPassword", ErrorMessage = "Confirm password does not match to input password")
        ]
        public string ConfirmPass { get; set; } = String.Empty;
    }
}
