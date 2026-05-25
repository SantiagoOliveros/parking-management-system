using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using ParkingApp.Application.DTOs.Vehicle;
using ParkingApp.Application.Interfaces;
using ParkingApp.Infrastructure.Configurations;

namespace ParkingApp.Infrastructure.ExternalServices;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendVehicleExitEmailAsync(VehicleExitResponse response)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress(
            _settings.SenderName,
            _settings.SenderEmail));

        email.To.Add(MailboxAddress.Parse(
            _settings.RecipientEmail));

        email.Subject = "Vehicle Exit Notification";

        email.Body = new TextPart("plain")
        {
            Text = $@"
Vehicle Exit Summary

Plate: {response.Plate}
Vehicle Type: {response.VehicleType}
Entry Time: {response.EntryTime}
Exit Time: {response.ExitTime}
Total Minutes: {response.TotalMinutes}
Total Amount: ${response.TotalAmount}
"
        };

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(
            _settings.SmtpServer,
            _settings.Port,
            MailKit.Security.SecureSocketOptions.StartTls);

        await smtp.AuthenticateAsync(
            _settings.SenderEmail,
            _settings.Password);

        await smtp.SendAsync(email);

        await smtp.DisconnectAsync(true);
    }
}