using ContactManagementApi.Data;
using ContactManagementApi.DTOs;
using ContactManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementApi.Services
{
    public class CountryService : ICountryService
    {
        private readonly ApplicationDbContext _context;

        public CountryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country?> GetByIdAsync(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task<Country> CreateAsync(CountryDto dto)
        {
            var country = new Country
            {
                CountryName = dto.CountryName
            };

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return country;
        }

        public async Task<bool> UpdateAsync(int id, CountryDto dto)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return false;
            }

            country.CountryName = dto.CountryName;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return false;
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<object> GetCompanyStatisticsByCountryIdAsync(int countryId)
        {
            var statistics = await _context.Contacts
                .Include(x => x.Company)
                .Where(x => x.CountryId == countryId)
                .GroupBy(x => x.Company.CompanyName)
                .Select(g => new
                {
                    CompanyName = g.Key,
                    ContactsCount = g.Count()
                })
                .ToListAsync();

            return statistics;
        }
    }
}