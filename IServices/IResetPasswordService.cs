using Back_End.Models;

namespace Back_End.IServices
{
    public interface IResetPasswordService
    {
        Task<bool> DeleteResetPassword(ResetPassword resetPassword);
        Task<ResetPassword> GetResetPasswordByUrl(string url);
        Task<ResetPassword> GetResetPasswordByUserId(int userId);
        Task<bool> SaveResetPassword(ResetPassword resetPassword);
    }
}
