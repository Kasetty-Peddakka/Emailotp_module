using System;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmailOtpApp.Services
{
    public class EmailOtpService
    {
        private readonly ConcurrentDictionary<string, (string Otp, DateTime Expiry)> _otpStorage = new();
        private readonly TimeSpan _otpExpiryDuration = TimeSpan.FromMinutes(1);
        private readonly Regex _allowedDomainRegex = new Regex(@"^[^@\s]+@[^@\s]+\.dso\.org\.sg$", RegexOptions.IgnoreCase);
        private readonly Random _random = new();

        public string GenerateOtp()
        {
            return _random.Next(100000, 999999).ToString();
        }

        public async Task<string> GenerateOtpEmail(string userEmail)
        {
            //if (!IsValidEmail(userEmail))
            //  return "STATUS_EMAIL_INVALID";
            if (!userEmail.EndsWith("@dso.org.sg") && !userEmail.EndsWith("@gmail.com"))
            {
                return "STATUS_EMAIL_INVALID";
            }

            string otp = GenerateOtp();
            DateTime expiryTime = DateTime.UtcNow.Add(_otpExpiryDuration);
            _otpStorage[userEmail] = (otp, expiryTime);

            bool emailSent = await SendEmailAsync(userEmail, $"Your OTP Code is {otp}. The code is valid for 1 minute.");
            return emailSent ? "STATUS_EMAIL_OK" : "STATUS_EMAIL_FAIL";
        }

        private bool IsValidEmail(string email)
        {
            return _allowedDomainRegex.IsMatch(email);
        }

        private async Task<bool> SendEmailAsync(string email, string body)
        {
            await Task.Delay(500); // Simulate sending email
            Console.WriteLine($"Email sent to {email}: {body}");
            return true;
        }

        public string CheckOtp(string userEmail, string enteredOtp)
        {
            if (_otpStorage.TryGetValue(userEmail, out var otpInfo))
            {
                if (DateTime.UtcNow > otpInfo.Expiry)
                    return "STATUS_OTP_TIMEOUT";

                if (otpInfo.Otp == enteredOtp)
                {
                    _otpStorage.TryRemove(userEmail, out _);
                    return "STATUS_OTP_OK";
                }
                return "STATUS_OTP_FAIL";
            }
            return "STATUS_OTP_FAIL";
        }
    }
}
