File Descriptions
1. Program.cs
Sets up the application by configuring services and routing:

AddControllersWithViews: Configures MVC pattern.
Scoped Service Registration: Registers EmailOtpService for dependency injection.
Routing: Maps default controller routing to OtpController.
2. OtpController.cs
The OtpController is responsible for handling OTP-related actions:

Index: Loads the initial form view.
SendOtp: Generates and sends an OTP to the provided email.
VerifyOtp: Verifies the OTP entered by the user.
3. EmailOtpService.cs
The service handles OTP logic, including generating, storing, and verifying OTPs:

GenerateOtpEmail: Generates an OTP, stores it with an expiry, and simulates email sending.
CheckOtp: Verifies the entered OTP, removing it from storage if valid.
4. Index.cshtml
The main user interface for OTP operations:

Send OTP Form: Collects user email for OTP generation.
Verify OTP Form: Accepts user email and OTP for verification.

Setup Instructions
Clone the Repository:

bash
Copy code
git clone https://github.com/your-repo/EmailOtpApp.git
Open in Visual Studio:

Launch Visual Studio 2022.
Open the EmailOtpApp solution.
Run the Application:

Press F5 to build and run the application.
Navigate to https://localhost:5001/Otp in your browser to access the OTP interface.
Usage
Send OTP:

Enter a valid email in the Send OTP form.
The application simulates sending an OTP to the email address.
Verify OTP:

Enter the same email and the received OTP in the Verify OTP form.
If the OTP matches and is within the 1-minute window, verification succeeds.
