using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaNacionProject.DataAccess;
using LaNacionProject.Services.ContactService;
using LaNacionProject.Shared.Models;

namespace LaNacionProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactServices _contactServices;

        public ContactsController(IContactServices contactServices)
        {
            _contactServices = contactServices;
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> CreateContact(Contact contact)
        {
            try
            {
                Contact? createdContact = await _contactServices.CreateContactAsync(contact);
                return CreatedAtAction(nameof(GetContact), new { id = createdContact.Id }, createdContact);
            }
            catch (Exception)
            {
                if (await _contactServices.ContactWithPropertiesAlreadyExists(contact))
                {
                    return Conflict("Contact already exists");
                }
                return BadRequest("Error creating contact");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(long id)
        {
            Contact? contact = await _contactServices.GetContactByIdAsync(id);
            return contact == null ? NotFound() : Ok(contact);
        }

        [HttpPut]
        public async Task<ActionResult<Contact>> UpdateContact(Contact updatedContact)
        {
            try
            {
                Contact? contact = await _contactServices.UpdateContactAsync(updatedContact);
                return contact == null ? NotFound() : Ok(contact);
            } catch (Exception ex)
            {
                if (await _contactServices.ContactWithPropertiesAlreadyExists(updatedContact))
                {
                    return Conflict("Contact already exists");
                }
                return Problem($"Error updating contact {ex}");
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteContact(long id)
        {
            try
            {
                await _contactServices.DeleteContactAsync(id);
                return NoContent();
            } catch (ArgumentNullException)
            {
                return NotFound();
            } catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpGet]
        [Route("GetContactByEmailOrPhoneNumber")]
        public async Task<ActionResult> GetContactByEmailOrPhoneNumber([FromQuery]string find)
        {
            List<Contact> contacts = await _contactServices.GetContactByEmailOrPhoneNumberAsync(find);
            return contacts.Count == 0 ? NotFound() : Ok(contacts); 
        }
        [HttpGet]
        [Route("GetAllContactsFromSameStateOrCity")]
        public async Task<ActionResult> GetAllContactsFromSameStateOrCity([FromQuery]string find)
        {
            List<Contact> contacts = await _contactServices.GetAllContactsFromSameStateOrCityAsync(find);
            return contacts.Count == 0 ? NotFound() : Ok(contacts);
        }
    }
}
