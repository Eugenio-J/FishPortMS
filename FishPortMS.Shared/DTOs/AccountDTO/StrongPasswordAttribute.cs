using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.AccountDTO
{
    public class StrongPasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string? password = value as string;

            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            // Check for at least one letter and one digit using regular expressions
            bool hasLowercase = Regex.IsMatch(password, @"[a-z]");
            bool hasUppercase = Regex.IsMatch(password, @"[A-Z]");
            bool hasDigit = Regex.IsMatch(password, @"\d");

            return hasLowercase && hasUppercase && hasDigit;
        }
    }
}
