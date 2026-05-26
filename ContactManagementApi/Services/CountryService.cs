using ContactManagementApi.Data;
using ContactManagementApi.DTOs;
using ContactManagementApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContactManagementApi.Services
{
    public class CountryService : ICountryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CountryService> _logger;

        public CountryService(ApplicationDbContext context, ILogger<CountryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            _logger.LogInformation("Getting all countries");

            return await _context.Countries
                .ToListAsync();
        }

        public async Task<Country?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting country with id {Id}", id);

            return await _context.Countries
                .FirstOrDefaultAsync(x => x.CountryId == id);
        }

        public async Task<Country> CreateAsync(CountryDto dto)
        {
            _logger.LogInformation("Creating country {CountryName}", dto.CountryName);

            var country = new Country
            {
                CountryName = dto.CountryName
            };

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Country created with id {Id}", country.CountryId);

            return country;
        }

        public async Task<bool> UpdateAsync(int id, CountryDto dto)
        {
            _logger.LogInformation("Updating country with id {Id}", id);

            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                _logger.LogWarning("Country with id {Id} not found", id);
                return false;
            }

            country.CountryName = dto.CountryName;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Country updated successfully");

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting country with id {Id}", id);

            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                _logger.LogWarning("Country with id {Id} not found", id);
                return false;
            }

            _context.Countries.Remove(country);

            await _context.SaveChangesAsync();

            _logger.LogInformation("Country deleted successfully");

            return true;
        }
    }
}