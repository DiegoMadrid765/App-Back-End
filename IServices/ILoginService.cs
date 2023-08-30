using Back_End.Models;

namespace Back_End.IServices
{
    public interface ILoginService
    {
        Task<User> GetUserbyEmail(string email);
    }
}
