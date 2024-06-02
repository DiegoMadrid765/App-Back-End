using Back_End.Models;

namespace Back_End.IServices
{
    public interface IResetPasswordService
    {
        Task<bool> SaveResetPassword(ResetPassword resetPassword);
    }
}
