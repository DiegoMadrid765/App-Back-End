using Back_End.Context;
using Back_End.IServices;
using Back_End.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Services
{
    public class LoginService:ILoginService
    {
        private readonly AplicationDbContext aplicationDbContext;

        public LoginService(AplicationDbContext aplicationDbContext)
        {
            this.aplicationDbContext = aplicationDbContext;
        }
        public async Task<User> GetUserbyEmail(string email)
        {
            return await aplicationDbContext.Users.Where(x=>x.email.Equals(email)).FirstOrDefaultAsync();
        }

       

    }
}
