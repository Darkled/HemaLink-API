namespace Domain.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendReservationEmailAsync(string toEmail, string donorName, string hospitalName, string hospitalAddress, DateTime date);
        Task<bool> SendCancellationEmailAsync(string toEmail, string donorName, string hospitalName, string hospitalAddress, DateTime date);
    }
}
