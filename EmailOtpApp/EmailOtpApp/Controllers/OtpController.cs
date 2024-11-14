using EmailOtpApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmailOtpApp.Controllers
{
    public class OtpController : Controller
    {
        private readonly EmailOtpService _otpService;

        public OtpController(EmailOtpService otpService)
        {
            _otpService = otpService;
        }

        // This action will load the initial view with the form
        [HttpGet]
        public IActionResult Index()
        {
            return View(); // Ensure there is a corresponding Index.cshtml file in Views/Otp
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromForm] string email)
        {
            var result = await _otpService.GenerateOtpEmail(email);
            return Ok(result);
        }

        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp([FromForm] string email, [FromForm] string otp)
        {
            var result = _otpService.CheckOtp(email, otp);
            return Ok(result);
        }
    }
}
