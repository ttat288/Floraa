namespace Application.Common.Interfaces;

public interface IEmailService
{
    Task SendAccountInvitationAsync(string email, string role, string activationToken, string tempPassword);
    Task SendPasswordResetAsync(string email, string resetToken);
    Task SendWelcomeEmailAsync(string email, string name);
}
