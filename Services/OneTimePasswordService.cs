using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AuthenticatorApp.Services
{
    public class OneTimePasswordService : IOneTimePasswordService
    {
        private static readonly TimeSpan PasswordValidityPeriod = TimeSpan.FromSeconds(30);
        private readonly ConcurrentDictionary<string, (string Password, DateTime ExpiryTime)> _otpDictionary = new ConcurrentDictionary<string, (string, DateTime)>();

        public string GenerateOneTimePassword(string userId, DateTime dateTime)
        {
            using var cryptoRng = new RNGCryptoServiceProvider();
            var otpBytes = new byte[4]; // will result in a 8-char OTP
            cryptoRng.GetBytes(otpBytes);
            var otp = BitConverter.ToString(otpBytes).Replace("-", "").ToLower();

            var expiryTime = dateTime.Add(PasswordValidityPeriod);

            _otpDictionary[userId] = (otp, expiryTime);

            return otp;
        }

        public bool ValidateOneTimePassword(string userId, string otp)
        {
            if (!_otpDictionary.TryGetValue(userId, out var otpData))
            {
                return false;
            }

            if (DateTime.UtcNow > otpData.ExpiryTime)
            {
                _otpDictionary.TryRemove(userId, out _);
                return false;
            }

            return otpData.Password == otp;
        }
    }
}
