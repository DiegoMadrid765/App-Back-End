using Back_End.Models;

namespace Back_End.IServices
{
    public interface ICountryService
    {
        Task<List<City>> getCitiesByCode(string code);
        Task<List<City>> getCitiesByCodeAndName(string code,string name);
        Task<List<Country>> getCountries();
    }
}
