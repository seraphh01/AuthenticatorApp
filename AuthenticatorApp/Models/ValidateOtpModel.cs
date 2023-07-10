using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticatorApp.Models
{
    public class ValidateOtpModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Otp { get; set; }
    }
}
