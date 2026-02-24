using Domain.Models;

namespace Domain.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendReservationEmailAsync(string toEmail, string donorName, string hospitalName, string hospitalAddress, DateTime date, int bloodRequestId, string cancellationToken);
        Task<bool> SendCancellationEmailAsync(string toEmail, string donorName, string hospitalName, string hospitalAddress, DateTime date);
        Task<bool> SendAcceptingNotificationEmailAsync(Requester requester);
        Task<bool> SendRejectingNotificationEmailAsync(Requester requester);
    }
}
