using System;
using Xunit;
using AuthenticatorApp.Services;

namespace AuthenticatorApp.Tests
{
    public class OneTimePasswordServiceTests
    {
        [Fact]
        public void GenerateOneTimePassword_GeneratesUniquePasswords()
        {
            var service = new OneTimePasswordService();
            var otp1 = service.GenerateOneTimePassword("user1", DateTime.UtcNow);
            var otp2 = service.GenerateOneTimePassword("user2", DateTime.UtcNow);

            Assert.NotEqual(otp1, otp2);
        }

        [Fact]
        public void ValidateOneTimePassword_ValidatesCorrectPassword()
        {
            var service = new OneTimePasswordService();
            var otp = service.GenerateOneTimePassword("user1", DateTime.UtcNow);

            Assert.True(service.ValidateOneTimePassword("user1", otp));
        }

        [Fact]
        public void ValidateOneTimePassword_RejectsIncorrectPassword()
        {
            var service = new OneTimePasswordService();
            service.GenerateOneTimePassword("user1", DateTime.UtcNow);

            Assert.False(service.ValidateOneTimePassword("user1", "incorrectpassword"));
        }

        [Fact]
        public void ValidateOneTimePassword_RejectsExpiredPassword()
        {
            var service = new OneTimePasswordService();
            var otp = service.GenerateOneTimePassword("user1", DateTime.UtcNow.AddSeconds(-31));

            Assert.False(service.ValidateOneTimePassword("user1", otp));
        }
    }
}
