using ContactManagementApi.Data;
using ContactManagementApi.DTOs;
using ContactManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementApi.Services
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _context;

        public ContactService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactDetailsDto>> GetAllAsync(int page, int pageSize)
        {
            if (page <= 0)
            {
                page = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            
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
            var contact = new Contact
            {
                ContactName = dto.ContactName,
                CompanyId = dto.CompanyId,
                CountryId = dto.CountryId
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task<bool> UpdateAsync(int id, ContactDto dto)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return false;
            }

            contact.ContactName = dto.ContactName;
            contact.CompanyId = dto.CompanyId;
            contact.CountryId = dto.CountryId;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return false;
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ContactDetailsDto>> FilterAsync(int? countryId, int? companyId)
        {
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