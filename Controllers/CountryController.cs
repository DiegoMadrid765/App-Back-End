using Back_End.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers
{
    [ApiController]
    [Route("api/Country")]
    public class CountryController:ControllerBase
    {
        private readonly ICountryService countryService;

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }
        [HttpGet]
        [Route("getCountries")]
        public async Task<IActionResult> getCountries()
        {
            try
            {
                return Ok(await countryService.getCountries());
            }
            catch (Exception)
            {

                return BadRequest(new {error="error"});
            }
        }


        [HttpGet]
        [Route("getCitiesByCode")]
        public async Task<IActionResult> getCitiesByCode(string code,string name)
        {
            try
            {
                return Ok(await countryService.getCitiesByCodeAndName(code,name));
            }
            catch (Exception)
            {

                return BadRequest(new { error = "error" });
            }
        }
    }
}
