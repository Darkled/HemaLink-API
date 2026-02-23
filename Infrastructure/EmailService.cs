using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;

        public EmailService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = config["SendGrid:ApiKey"]!;

        }

        public async Task<bool> SendReservationEmailAsync(string toEmail, string donorName, string hospitalName, string hospitalAddress, DateTime date)
        {
            var client = _httpClientFactory.CreateClient("SendGrid");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var emailData = new
            {
                personalizations = new[]
                {
                    new { to = new[] { new { email = toEmail } } }
                },
                from = new { email = "ezewheel@gmail.com", name = "HemaLink" },
                subject = "Confirmación de tu reserva de donación",
                content = new[]
                {
                    new {
                        type = "text/html",
                        value = $@"
                            <!DOCTYPE html>
                            <html>
                            <head>
                                <meta charset='utf-8'>
                                <link href=""https://fonts.googleapis.com/css2?family=Roboto:wght@700&display=swap"" rel=""stylesheet"">
                            </head>
                            <body style='margin: 0; padding: 0; font-family: system-ui, -apple-system, ""Segoe UI""; color: #333; line-height: 1.6;'>

                                <div style='background-color: #EBEBEB; padding: 20px; width: 50%; margin: auto; text-align: center; border-bottom: 1px solid #eeeeee;'>
                                    <img src='https://i.ibb.co/5W9KZvXK/logo.png' alt='HemaLink' style='vertical-align: middle; margin-right: 10px; height: 50px;'>
                                </div>

                                <div style='padding: 40px 20px; max-width: 600px; margin: 0 auto;'>
                                    
                                    <p style='font-size: 16px;'>Hola, <strong>{donorName}</strong>,</p>
                                    
                                    <p style='font-size: 16px;'>Tu reserva ha sido confirmada.</p>

                                    <div style='background-color: #ffffff; border-left: 4px solid #d32f2f; border-right: 1px solid #e0e0e0; border-top: 1px solid #e0e0e0; border-bottom: 1px solid #e0e0e0; padding: 20px; margin: 25px 0; border-radius: 4px;'>
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
                                </div>
                            </body>
                            </html>"
                    }
                }
            };

            var response = await client.PostAsJsonAsync("mail/send", emailData);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendCancellationEmailAsync(string toEmail, string donorName, string hospitalName, string hospitalAddress, DateTime date)
        {
            var client = _httpClientFactory.CreateClient("SendGrid");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var emailData = new
            {
                personalizations = new[]
                {
                    new { to = new[] { new { email = toEmail } } }
                },
                from = new { email = "ezewheel@gmail.com", name = "HemaLink" },
                subject = "Notificación sobre cancelación de donación",
                content = new[]
                {
                    new {
                        type = "text/html",
                        value = $@"
                            <!DOCTYPE html>
                            <html>
                            <head>
                                <meta charset='utf-8'>
                                <link href=""https://fonts.googleapis.com/css2?family=Roboto:wght@700&display=swap"" rel=""stylesheet"">
                            </head>
                            <body style='margin: 0; padding: 0; font-family: system-ui, -apple-system, ""Segoe UI""; color: #333; line-height: 1.6;'>

                                <div style='background-color: #EBEBEB; padding: 20px; width: 50%; margin: auto; text-align: center; border-bottom: 1px solid #eeeeee;'>
                                    <img src='https://i.ibb.co/5W9KZvXK/logo.png' alt='HemaLink' style='vertical-align: middle; margin-right: 10px; height: 50px;'>
                                </div>

                                <div style='padding: 40px 20px; max-width: 600px; margin: 0 auto;'>
                                    
                                    <p style='font-size: 16px;'>Hola, <strong>{donorName}</strong>,</p>
                                    
                                    <p style='font-size: 16px;'>Te informamos que {hospitalName} decidió dar de baja la siguiente campaña de donación:</p>

                                    <div style='background-color: #ffffff; border-left: 4px solid #d32f2f; border-right: 1px solid #e0e0e0; border-top: 1px solid #e0e0e0; border-bottom: 1px solid #e0e0e0; padding: 20px; margin: 25px 0; border-radius: 4px;'>
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
                                    </p>
                                </div>
                            </body>
                            </html>"
                    }
                }
            };

            var response = await client.PostAsJsonAsync("mail/send", emailData);

            return response.IsSuccessStatusCode;
        }
    }
}