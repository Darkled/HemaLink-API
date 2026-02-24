using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;
        private readonly IConfiguration _config;

        public EmailService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = config["SendGrid:ApiKey"]!;
            _config = config;
        }

        private async Task<bool> SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("SendGrid");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                var senderEmail = _config["Email:SenderEmail"];
                var emailData = new
                {
                    personalizations = new[] { new { to = new[] { new { email = toEmail } } } },
                    from = new { email = senderEmail, name = "HemaLink" },
                    subject,
                    content = new[] { new { type = "text/html", value = htmlContent } }
                };

                var response = await client.PostAsJsonAsync("mail/send", emailData);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error crítico en SendEmailAsync: {ex.Message}");
                return false;
            }
        }

        private string ApplyEmailLayout(string bodyContent)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <link href=""https://fonts.googleapis.com/css2?family=Roboto:wght@700&display=swap"" rel=""stylesheet"">
                    <style>
                        @media only screen and (max-width: 600px) {{
                            .email-header {{ width: 90% !important; }}
                            .email-body {{ padding: 24px 16px !important; }}
                            .info-card {{ margin: 16px 0 !important; }}
                        }}
                    </style>
                </head>
                <body style='margin: 0; padding: 0; font-family: system-ui, -apple-system, ""Segoe UI""; color: #333; line-height: 1.6; background-color: #f5f5f5;'>

                    <div class='email-header' style='background-color: #EBEBEB; padding: 20px; width: 50%; margin: auto; text-align: center; border-bottom: 1px solid #eeeeee;'>
                        <img src='https://i.ibb.co/5W9KZvXK/logo.png' alt='HemaLink' style='vertical-align: middle; margin-right: 10px; height: 50px;'>
                    </div>

                    <div class='email-body' style='padding: 40px 20px; max-width: 600px; margin: 0 auto; background-color: #ffffff;'>
                        {bodyContent}
                    </div>

                </body>
                </html>";
        }

        public async Task<bool> SendReservationEmailAsync(string toEmail, string donorName, string hospitalName, string hospitalAddress, DateTime date, int bloodRequestId, string cancellationToken)
        {
            var cancelUrl = $"{_config["Frontend:BaseUrl"]}/donation-cancelation/{bloodRequestId}/{cancellationToken}";

            var body = $@"
                <p style='font-size: 16px;'>Hola, <strong>{donorName}</strong>,</p>
                
                <p style='font-size: 16px;'>Tu reserva ha sido confirmada.</p>

                <div class='info-card' style='background-color: #ffffff; border-left: 4px solid #d32f2f; border-right: 1px solid #e0e0e0; border-top: 1px solid #e0e0e0; border-bottom: 1px solid #e0e0e0; padding: 20px; margin: 25px 0; border-radius: 4px;'>
                    <p style='margin: 0 0 10px 0; font-size: 15px;'>
                        <strong style='color: #555;'>Solicitante:</strong> {hospitalName}
                    </p>
                    <p style='margin: 0 0 10px 0; font-size: 15px;'>
                        <strong style='color: #555;'>Lugar:</strong> {hospitalAddress}
                    </p>
                    <p style='margin: 0; font-size: 15px;'>
                        <strong style='color: #555;'>Fecha:</strong> {date:dd/MM/yyyy} &nbsp;&nbsp; 
                        <strong style='color: #555;'>Hora:</strong> {date:HH:mm}
                    </p>
                </div>

                <p style='font-size: 16px; margin-top: 30px;'>
                    Gracias por ayudar a salvar vidas,<br>
                    Equipo de HemaLink
                </p>
                <a style='color:#555;' href='{cancelUrl}'>Cancelar reserva</a>";

            return await SendEmailAsync(toEmail, "Confirmación de tu reserva de donación", ApplyEmailLayout(body));
        }

        public async Task<bool> SendCancellationEmailAsync(string toEmail, string donorName, string hospitalName, string hospitalAddress, DateTime date)
        {
            var body = $@"
                <p style='font-size: 16px;'>Hola, <strong>{donorName}</strong>,</p>
                
                <p style='font-size: 16px;'>Te informamos que {hospitalName} decidió dar de baja la siguiente campaña de donación:</p>

                <div class='info-card' style='background-color: #ffffff; border-left: 4px solid #d32f2f; border-right: 1px solid #e0e0e0; border-top: 1px solid #e0e0e0; border-bottom: 1px solid #e0e0e0; padding: 20px; margin: 25px 0; border-radius: 4px;'>
                    <p style='margin: 0 0 10px 0; font-size: 15px;'>
                        <strong style='color: #555;'>Lugar:</strong> {hospitalAddress}
                    </p>
                    <p style='margin: 0; font-size: 15px;'>
                        <strong style='color: #555;'>Fecha:</strong> {date:dd/MM/yyyy} &nbsp;&nbsp; 
                        <strong style='color: #555;'>Hora:</strong> {date:HH:mm}
                    </p>
                </div>

                <p style='font-size: 16px;'>Ya no será necesario que te presentes en el lugar en la fecha establecida.</p>

                <p style='font-size: 16px; margin-top: 30px;'>
                    Pedimos disculpas por cualquier molestia ocasionada,<br>
                    Equipo de HemaLink
                </p>";

            return await SendEmailAsync(toEmail, "Notificación sobre cancelación de donación", ApplyEmailLayout(body));
        }

        public async Task<bool> SendAcceptingNotificationEmailAsync(Requester requester)
        {
            var body = $@"
                <p style='font-size: 16px;'><strong>{requester.Name}</strong>,</p>

                <p style='font-size: 16px;'>Nos complace informarte que tu cuenta de HemaLink ha sido aceptada. Puedes iniciar sesión comenzar a crear colectas de sangre.</p>
                <p style='font-size: 16px;'>
                    Gracias por confiar en nosotros,<br>
                    Equipo de HemaLink
                </p>";
            return await SendEmailAsync(requester.Email, "Notificación de aceptación de solicitud", ApplyEmailLayout(body));

        }
        public async Task<bool> SendRejectingNotificationEmailAsync(Requester requester)
        {
            var body = $@"
                <p style='font-size: 16px;'><strong>{requester.Name}</strong>,</p>

                <p style='font-size: 16px;'>Lamentamos informarte que tu cuenta de HemaLink ha sido rechazada.</p>
                <p style='font-size: 16px;'>Si crees que se trata de un error, no dudes en contactarnos.</p>
                <p style='font-size: 16px;'>
                    Gracias por confiar en nosotros,<br>
                    Equipo de HemaLink
                </p>";
            return await SendEmailAsync(requester.Email, "Notificación de aceptación de solicitud", ApplyEmailLayout(body));
        }
    }
}