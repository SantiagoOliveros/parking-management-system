using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using ParkingApp.Application.DTOs.Vehicle;
using ParkingApp.Application.Interfaces;

namespace ParkingApp.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly HttpClient _httpClient;

    public EmailService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SendVehicleExitEmailAsync(
        VehicleExitResponse response)
    {
        // ==============================
        // STEP 1 - GET TOKEN
        // ==============================

        var tokenPayload = new
        {
            username = "proceso_pruebas",
            password = "das487d32_*"
        };

        var tokenContent =
            new StringContent(
                JsonSerializer.Serialize(tokenPayload),
                Encoding.UTF8,
                "application/json"
            );

        var tokenResponse =
            await _httpClient.PostAsync(
                "https://dev-sites.similtech.co/api-email/api/token",
                tokenContent
            );

        tokenResponse.EnsureSuccessStatusCode();

        var tokenJson =
            await tokenResponse.Content
                .ReadAsStringAsync();

        using var tokenDocument =
            JsonDocument.Parse(tokenJson);

        var token =
            tokenDocument.RootElement
                .GetProperty("token")
                .GetString();

        if (string.IsNullOrWhiteSpace(token))
        {
            throw new Exception(
                "Token was not returned by API"
            );
        }

        // ==============================
        // STEP 2 - CONFIGURE AUTH
        // ==============================

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token
            );

        // ==============================
        // STEP 3 - EMAIL PAYLOAD
        // ==============================

        var emailPayload = new
        {
            configParams = new
            {
                idUser = "proceso_pruebas",
                idMessage = Guid.NewGuid().ToString()
            },

            receivers = new
            {
                emailOrigin = "proceso_pruebas",

                to = new[]
                {
                    "santiago.oliveros07@gmail.com"
                },

                copyTo = Array.Empty<string>(),

                hiddenCopyTo = Array.Empty<string>()
            },

            email = new
            {
                subject =
                    "Vehicle Exit Notification",

                urlHeader = "",

                urlFooter = "",

                message =
                    $"""
                    Vehicle Exit Registered

                    Plate: {response.Plate}

                    Vehicle Type: {response.VehicleType}

                    Entry Time: {response.EntryTime}

                    Exit Time: {response.ExitTime}

                    Total Minutes: {response.TotalMinutes}

                    Total Amount: ${response.TotalAmount}
                    """,

                url_files = Array.Empty<string>()
            }
        };

        var emailContent =
            new StringContent(
                JsonSerializer.Serialize(emailPayload),
                Encoding.UTF8,
                "application/json"
            );

        // ==============================
        // STEP 4 - SEND EMAIL
        // ==============================

        var emailResponse =
            await _httpClient.PostAsync(
                "https://dev-sites.similtech.co/api-email/api/email/sendEmail",
                emailContent
            );

        emailResponse.EnsureSuccessStatusCode();

        Console.WriteLine(
            "Vehicle exit email request sent successfully"
        );
    }
}