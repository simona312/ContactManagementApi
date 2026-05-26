using ContactManagementApi.Data;
using ContactManagementApi.DTOs;
using ContactManagementApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContactManagementApi.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(ApplicationDbContext context, ILogger<CompanyService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            _logger.LogInformation("Getting all companies");

            return await _context.Companies.ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting company with id {Id}", id);

            return await _context.Companies.FindAsync(id);
        }

        public async Task<Company> CreateAsync(CompanyDto dto)
        {
            _logger.LogInformation("Creating company {CompanyName}", dto.CompanyName);

            var company = new Company
            {
                CompanyName = dto.CompanyName
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Company created with id {Id}", company.CompanyId);

            return company;
        }

        public async Task<bool> UpdateAsync(int id, CompanyDto dto)
        {
            _logger.LogInformation("Updating company with id {Id}", id);

            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                _logger.LogWarning("Company with id {Id} was not found", id);
                return false;
            }

            company.CompanyName = dto.CompanyName;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Company with id {Id} updated successfully", id);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting company with id {Id}", id);

            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                _logger.LogWarning("Company with id {Id} was not found", id);
                return false;
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Company with id {Id} deleted successfully", id);

            return true;
        }
    }
}