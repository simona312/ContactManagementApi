using ContactManagementApi.Data;
using ContactManagementApi.DTOs;
using ContactManagementApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContactManagementApi.Services
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ContactService> _logger;

        public ContactService(ApplicationDbContext context, ILogger<ContactService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<ContactDetailsDto>> GetAllAsync(int page, int pageSize)
        {
            _logger.LogInformation("Getting all contacts. Page: {Page}, PageSize: {PageSize}", page, pageSize);

            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            return await _context.Contacts
                .Include(x => x.Company)
                .Include(x => x.Country)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new ContactDetailsDto
                {
                    ContactId = x.ContactId,
                    ContactName = x.ContactName,
                    CompanyName = x.Company.CompanyName,
                    CountryName = x.Country.CountryName
                })
                .ToListAsync();
        }

        public async Task<ContactDetailsDto?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting contact with id {Id}", id);

            return await _context.Contacts
                .Include(x => x.Company)
                .Include(x => x.Country)
                .Where(x => x.ContactId == id)
                .Select(x => new ContactDetailsDto
                {
                    ContactId = x.ContactId,
                    ContactName = x.ContactName,
                    CompanyName = x.Company.CompanyName,
                    CountryName = x.Country.CountryName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Contact> CreateAsync(ContactDto dto)
        {
            _logger.LogInformation("Creating contact {ContactName}", dto.ContactName);

            var contact = new Contact
            {
                ContactName = dto.ContactName,
                CompanyId = dto.CompanyId,
                CountryId = dto.CountryId
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Contact created with id {Id}", contact.ContactId);

            return contact;
        }

        public async Task<bool> UpdateAsync(int id, ContactDto dto)
        {
            _logger.LogInformation("Updating contact with id {Id}", id);

            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                _logger.LogWarning("Contact with id {Id} was not found", id);
                return false;
            }

            contact.ContactName = dto.ContactName;
            contact.CompanyId = dto.CompanyId;
            contact.CountryId = dto.CountryId;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Contact with id {Id} updated successfully", id);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting contact with id {Id}", id);

            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                _logger.LogWarning("Contact with id {Id} was not found", id);
                return false;
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Contact with id {Id} deleted successfully", id);

            return true;
        }

        public async Task<IEnumerable<ContactDetailsDto>> FilterAsync(int? countryId, int? companyId)
        {
            _logger.LogInformation("Filtering contacts. CountryId: {CountryId}, CompanyId: {CompanyId}", countryId, companyId);

            var query = _context.Contacts
                .Include(x => x.Company)
                .Include(x => x.Country)
                .AsQueryable();

            if (countryId.HasValue)
            {
                query = query.Where(x => x.CountryId == countryId.Value);
            }

            if (companyId.HasValue)
            {
                query = query.Where(x => x.CompanyId == companyId.Value);
            }

            return await query
                .Select(x => new ContactDetailsDto
                {
                    ContactId = x.ContactId,
                    ContactName = x.ContactName,
                    CompanyName = x.Company.CompanyName,
                    CountryName = x.Country.CountryName
                })
                .ToListAsync();
        }
    }
}