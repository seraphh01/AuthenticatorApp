using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticatorApp.Models
{
    public class GenerateOtpModel
    {
        [Required]
        public string UserId { get; set; }
    }
}
