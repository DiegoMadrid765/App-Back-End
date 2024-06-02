using Back_End.Context;
using Back_End.IServices;
using Back_End.Models;

namespace Back_End.Services
{
    public class ResetPasswordService: IResetPasswordService
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
    }
}
