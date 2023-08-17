using LaNacionProject.DataAccess;
using LaNacionProject.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LaNacionProject.Services.ContactService
{
    public class ContactServices : IContactServices
    {
        private readonly ContextFactory _context;

        public ContactServices(ContextFactory context)
        {
            _context = context;
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            try
            {
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
                return contact;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating contact", ex);
            }
        }

        public async Task<Contact?> GetContactByIdAsync(long id) => await _context.Contacts.Include(c => c.PhoneNumber).Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Contact?> UpdateContactAsync(Contact updatedContact)
        {
            Contact? contact = await _context.Contacts.SingleOrDefaultAsync(c => c.Id == updatedContact.Id);
            if (contact == null)
            {
                return null;
            }
            contact.Name = updatedContact.Name;
            contact.Company = updatedContact.Company;
            contact.ProfileImage = updatedContact.ProfileImage;
            contact.Email = updatedContact.Email;
            contact.Birthdate = updatedContact.Birthdate;
            contact.PhoneNumber = updatedContact.PhoneNumber;
            contact.Address = updatedContact.Address;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(updatedContact.Id))
                {
                    return null;
                }
                else
                {
                    throw new Exception($"Error while updating contact id: {updatedContact.Id}");
                }
            }
            return updatedContact;
        }

        public async Task DeleteContactAsync(long id)
        {
            Contact? contact = await _context.Contacts.Include(c => c.PhoneNumber).Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == id);
            if (contact == null)
            {
                throw new ArgumentNullException("Id");
            }
            _context.Contacts.Remove(contact);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Contact>> GetContactByEmailOrPhoneNumberAsync(string find)
        {
            return await _context.Contacts.Include(c => c.PhoneNumber).Include(c => c.Address).Where(x => x.Email.Equals(find)
            || x.PhoneNumber.Any(p => p.Number.Equals(find))).ToListAsync();
        }

        public async Task<List<Contact>> GetAllContactsFromSameStateOrCityAsync(string find)
        {
            return await _context.Contacts.Include(c => c.Address).Include(c => c.PhoneNumber).Where(x => x.Address.State.Equals(find)
            || x.Address.City.Equals(find)).ToListAsync();
        }

        public bool ContactExists(long id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }

        public async Task<bool> ContactWithPropertiesAlreadyExists(Contact contactToCheck)
        {
            bool emailExists = await _context.Contacts.AnyAsync(contact => contact.Email == contactToCheck.Email);

            List<string> phoneNumbersToInsert = contactToCheck.PhoneNumber.Select(phone => phone.Number).ToList();
            bool phoneNumberExists = await _context.Contacts
                .AnyAsync(contact =>
                    contact.PhoneNumber.Any(phone => phoneNumbersToInsert.Contains(phone.Number)));


            return emailExists || phoneNumberExists ? true : false;
        }
    }
}

