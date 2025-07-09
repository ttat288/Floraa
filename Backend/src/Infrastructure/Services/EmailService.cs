using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendAccountInvitationAsync(string email, string role, string activationToken, string tempPassword)
    {
        var subject = "Account Invitation - Flora E-commerce Platform";
        var activationLink = $"{_configuration["AppSettings:FrontendUrl"]}/complete-account?token={activationToken}";

        var body = $@"
            <h2>Welcome to Flora E-commerce Platform!</h2>
            <p>You have been invited to join as a <strong>{role}</strong>.</p>
            <p>Please click the link below to complete your account setup:</p>
            <p><a href='{activationLink}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Complete Account Setup</a></p>
            <p>This link will expire in 7 days.</p>
            <p>If you have any questions, please contact our support team.</p>
            <br>
            <p>Best regards,<br>Flora E-commerce Team</p>
        ";

        await SendEmailAsync(email, subject, body);
    }

    public async Task SendPasswordResetAsync(string email, string resetToken)
    {
        var subject = "Password Reset - Flora E-commerce Platform";
        var resetLink = $"{_configuration["AppSettings:FrontendUrl"]}/reset-password?token={resetToken}";

        var body = $@"
            <h2>Password Reset Request</h2>
            <p>You have requested to reset your password.</p>
            <p>Please click the link below to reset your password:</p>
            <p><a href='{resetLink}' style='background-color: #2196F3; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Reset Password</a></p>
            <p>This link will expire in 1 hour.</p>
            <p>If you didn't request this, please ignore this email.</p>
            <br>
            <p>Best regards,<br>Flora E-commerce Team</p>
        ";

        await SendEmailAsync(email, subject, body);
    }

    public async Task SendWelcomeEmailAsync(string email, string name)
    {
        var subject = "Welcome to Flora E-commerce Platform!";

        var body = $@"
            <h2>Welcome {name}!</h2>
            <p>Your account has been successfully created.</p>
            <p>You can now start using the Flora E-commerce Platform.</p>
            <p>If you have any questions, please contact our support team.</p>
            <br>
            <p>Best regards,<br>Flora E-commerce Team</p>
        ";

        await SendEmailAsync(email, subject, body);
    }

    private async Task SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");

            using var client = new SmtpClient(smtpSettings["Host"], int.Parse(smtpSettings["Port"]!))
            {
                Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]),
                EnableSsl = bool.Parse(smtpSettings["EnableSsl"]!)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpSettings["FromEmail"]!, smtpSettings["FromName"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
            _logger.LogInformation("Email sent successfully to {Email}", to);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {Email}", to);
            throw;
        }
    }
}
