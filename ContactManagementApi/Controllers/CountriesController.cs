using ContactManagementApi.DTOs;
using ContactManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using ContactManagementApi.Services;

namespace ContactManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            var countries = await _countryService.GetAllAsync();

            return Ok(countries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountryById(int id)
        {
            var country = await _countryService.GetByIdAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        [HttpPost]
        public async Task<ActionResult<Country>> CreateCountry(CountryDto dto)
        {
            var country = await _countryService.CreateAsync(dto);

            return Ok(country);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, CountryDto dto)
        {
            var updated = await _countryService.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var deleted = await _countryService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{countryId}/company-statistics")]
        public async Task<IActionResult> GetCompanyStatisticsByCountryId(int countryId)
        {
            var statistics = await _countryService.GetCompanyStatisticsByCountryIdAsync(countryId);

            return Ok(statistics);
        }
    }
}
