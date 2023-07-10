using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticatorApp.Services
{
    public interface IOneTimePasswordService
    {
        string GenerateOneTimePassword(string userId, DateTime dateTime);
        bool ValidateOneTimePassword(string userId, string otp);
    }
}
