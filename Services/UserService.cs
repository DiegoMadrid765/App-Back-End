using Back_End.Context;
using Back_End.IServices;
using Back_End.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Services
{
    public class UserService : IUserService
    {
        private readonly AplicationDbContext aplicationDbContext;

        public UserService(AplicationDbContext aplicationDbContext)
        {
            this.aplicationDbContext = aplicationDbContext;
        }

        public async Task<User> RegisterUser(User user)
        {
            aplicationDbContext.Users.Add(user);
            await aplicationDbContext.SaveChangesAsync();
           
            await aplicationDbContext.SaveChangesAsync();
            return user;
        }


        public async Task RegisterUrlActivateUser(ActivatedUser activatedUser)
        {
            aplicationDbContext.Add(activatedUser);
            await aplicationDbContext.SaveChangesAsync();
        }

        public async Task<User> GetUser(int userId)
        {
            return await aplicationDbContext.Users.Where(x => x.Id == userId).Select(x => new User
            {
                names = x.names,
                lastnames = x.lastnames,
                email = x.email,
                birthdate = x.birthdate,
                gender = x.gender
            }).FirstOrDefaultAsync();


        }

        public async Task<ActivatedUser> GetActivatedUser(string url)
        {
            return await aplicationDbContext.ActivatedUsers.Where(x => x.url.Equals(url)).FirstOrDefaultAsync();
        }


        public async Task ActivateUser(int id)
        {
          var useractivate= await  aplicationDbContext.ActivatedUsers.Where(x=>x.Id==id).FirstOrDefaultAsync();
            if (useractivate != null)
            {
                useractivate.activated = 1;
                aplicationDbContext.ActivatedUsers.Update(useractivate);
                await aplicationDbContext.SaveChangesAsync();
            }
           
        }

        public async Task<bool> VerifyUserActivated(int id)
        {
            var useractivate = await aplicationDbContext.ActivatedUsers.Where(x => x.userId == id).FirstOrDefaultAsync();
            if (useractivate != null)
            {

                return useractivate.activated == 1;
            }
            return false;
        }

        public async Task DeleteActivatedAccount(int id)
        {
          var user=  aplicationDbContext.ActivatedUsers.Where(x=>x.userId==id).FirstOrDefaultAsync();
            if (user != null)
            {
                aplicationDbContext.ActivatedUsers.Remove(await user);
            }
           await aplicationDbContext.SaveChangesAsync();
        }

        public async Task AddActivatedAccount(ActivatedUser user)
        {
            aplicationDbContext.ActivatedUsers.Add(user);
            
            await aplicationDbContext.SaveChangesAsync();
        }
    }
}
