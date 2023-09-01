using Back_End.Context;
using Back_End.IServices;
using Back_End.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Services
{
    public class CountryService : ICountryService
    {
        private readonly AplicationDbContext aplicationDbContext;

        public CountryService(AplicationDbContext aplicationDbContext)
        {
            this.aplicationDbContext = aplicationDbContext;
        }

        public async Task<List<Country>> getCountries()
        {
            return await aplicationDbContext.Countries.OrderBy(x => x.name).ToListAsync();
        }

        public async Task<List<City>> getCitiesByCodeAndName(string code, string name)
        {
            return await aplicationDbContext.Cities.Where(x => x.countryCode.Equals(code) && x.name.Contains(name)).ToListAsync();
        }

        public async Task<List<City>> getCitiesByCode(string code)
        {
            return await aplicationDbContext.Cities.Where(x => x.countryCode.Equals(code)).Select(x => new City { name = x.name, Id=x.Id}).OrderBy(x=>x.name).ToListAsync();
            
        }
    }
}
