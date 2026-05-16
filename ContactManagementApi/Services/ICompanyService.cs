using ContactManagementApi.DTOs;
using ContactManagementApi.Models;

namespace ContactManagementApi.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(int id);
        Task<Company> CreateAsync(CompanyDto dto);
        Task<bool> UpdateAsync(int id, CompanyDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
