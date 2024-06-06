using Back_End.Context;
using Back_End.IServices;
using Back_End.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Services
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly AplicationDbContext aplicationDbContext;
        public ResetPasswordService(AplicationDbContext aplicationDbContext)
        {
            this.aplicationDbContext = aplicationDbContext;
        }

        public async Task<bool> SaveResetPassword(ResetPassword resetPassword)
        {
            try
            {

                aplicationDbContext.ResetPassword.Add(resetPassword);
                await aplicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public async Task<ResetPassword> GetResetPasswordByUserId(int userId)
        {
            try
            {
                return await aplicationDbContext.ResetPassword.FirstOrDefaultAsync(x => x.userId == userId);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool> DeleteResetPassword(ResetPassword resetPassword)
        {
            try
            {
                aplicationDbContext.ResetPassword.Remove(resetPassword);
                await aplicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<ResetPassword> GetResetPasswordByUrl(string url)
        {
            return await aplicationDbContext.ResetPassword.FirstOrDefaultAsync(x=>x.Url.Equals(url));
        }

      
    }
}
