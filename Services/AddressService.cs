using Back_End.Context;
using Back_End.IServices;
using Back_End.Models;

namespace Back_End.Services
{
    public class AddressService:IAddressService
    {
        private readonly AplicationDbContext aplicationDbContext;

        public AddressService(AplicationDbContext aplicationDbContext)
        {
            this.aplicationDbContext = aplicationDbContext;
        }


        public async Task AddAddress(Address address)
        {
            aplicationDbContext.Addresses.Add(address);
            await aplicationDbContext.SaveChangesAsync();
 
        
        
        }
    }  
}
