using ContactManagementApi.Data;
using ContactManagementApi.DTOs;
using ContactManagementApi.Models;
using ContactManagementApi.Services;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementApi.Tests.Services
{
    public class ContactServiceTests
    {
        [Fact]
        public async Task CreateAsync_ShouldCreateContact()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using var context = new ApplicationDbContext(options);

            context.Companies.Add(new Company
            {
                CompanyId = 1,
                CompanyName = "Google"
            });

            context.Countries.Add(new Country
            {
                CountryId = 1,
                CountryName = "Germany"
            });

            await context.SaveChangesAsync();

            var service = new ContactService(context);

            var dto = new ContactDto
            {
                ContactName = "Ana",
                CompanyId = 1,
                CountryId = 1
            };

            var result = await service.CreateAsync(dto);

            Assert.NotNull(result);

            Assert.Equal("Ana", result.ContactName);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenContactDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);

            var service = new ContactService(context);

            var result = await service.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task FilterAsync_ShouldReturnFilteredContacts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);

            context.Companies.Add(new Company
            {
                CompanyId = 1,
                CompanyName = "Google"
            });

            context.Countries.Add(new Country
            {
                CountryId = 1,
                CountryName = "Germany"
            });

            context.Contacts.Add(new Contact
            {
                ContactId = 1,
                ContactName = "Ana",
                CompanyId = 1,
                CountryId = 1
            });

            await context.SaveChangesAsync();

            var service = new ContactService(context);

            var result = await service.FilterAsync(1, 1);

            Assert.Single(result);
        }
    }
}
