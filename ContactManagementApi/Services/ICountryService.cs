using ContactManagementApi.DTOs;
using ContactManagementApi.Models;

namespace ContactManagementApi.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllAsync();
        Task<Country?> GetByIdAsync(int id);
        Task<Country> CreateAsync(CountryDto dto);
        Task<bool> UpdateAsync(int id, CountryDto dto);
        Task<bool> DeleteAsync(int id);

    }
}
