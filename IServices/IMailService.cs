using Back_End.Models;

namespace Back_End.IServices
{
    public interface IMailService
    {
       Task SendEmailAutorization(User user, string url);
        Task SendEmailForgotAutorization(User user, string url);
        Task<bool> SendEmailForgotPassword(User user, string url);
    }
}
