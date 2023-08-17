using LaNacionProject.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaNacionProject.Services.ContactService
{
    public interface IContactServices
    {
        public Task<Contact> CreateContactAsync(Contact contact);
        public Task<Contact?> GetContactByIdAsync(long id);
        public Task<Contact?> UpdateContactAsync(Contact contact);
        public Task DeleteContactAsync(long id);
        public Task<List<Contact>> GetContactByEmailOrPhoneNumberAsync(string find);
        public Task<List<Contact>> GetAllContactsFromSameStateOrCityAsync(string find);
        public Task<bool> ContactWithPropertiesAlreadyExists(Contact contact);  
    }
}
