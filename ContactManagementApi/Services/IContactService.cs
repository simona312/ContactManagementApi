using ContactManagementApi.DTOs;
using ContactManagementApi.Models;

namespace ContactManagementApi.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDetailsDto>> GetAllAsync(int page, int pageSize);
        Task<ContactDetailsDto?> GetByIdAsync(int id);
        Task<Contact> CreateAsync(ContactDto dto);
        Task<bool> UpdateAsync(int id, ContactDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ContactDetailsDto>> FilterAsync(int? countryId, int? companyId);
    }
}
