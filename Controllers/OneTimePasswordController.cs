using AuthenticatorApp.Models;
using AuthenticatorApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AuthenticatorApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OneTimePasswordController : ControllerBase
    {
        private readonly IOneTimePasswordService _otpService;

        public OneTimePasswordController(IOneTimePasswordService otpService)
        {
            _otpService = otpService;
        }

        [HttpPost]
        public IActionResult GenerateOneTimePassword([FromBody] GenerateOtpModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var otp = _otpService.GenerateOneTimePassword(model.UserId, DateTime.UtcNow);

            return Ok(otp);
        }

        [HttpPost("validate")]
        public IActionResult ValidateOneTimePassword([FromBody] ValidateOtpModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isValid = _otpService.ValidateOneTimePassword(model.UserId, model.Otp);

            return Ok(isValid);
        }
    }

}
