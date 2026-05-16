using ContactManagementApi.DTOs;
using ContactManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using ContactManagementApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace ContactManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDetailsDto>>> GetContacts(
            int page = 1,
            int pageSize = 10)
        {
            var contacts = await _contactService.GetAllAsync(page, pageSize);

            return Ok(contacts);
        }


        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ContactDetailsDto>>> FilterContacts(
         int? countryId,
         int? companyId)
        {
            var contacts = await _contactService.FilterAsync(countryId, companyId);

            return Ok(contacts);
        }


            [HttpPost]
        public async Task<ActionResult<Contact>> CreateContact(ContactDto dto)
        {

          var contact = await _contactService.CreateAsync(dto);

            return Ok(contact);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDetailsDto>> GetContactById(int id)
        {
            var contact = await _contactService.GetByIdAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, ContactDto dto)
        {
            var updated = await _contactService.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var deleted = await _contactService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
