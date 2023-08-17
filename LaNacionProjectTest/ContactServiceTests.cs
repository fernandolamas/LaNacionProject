using LaNacionProject.DataAccess;
using LaNacionProject.Services.ContactService;
using LaNacionProject.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LaNacionProjectTest
{
    [TestClass]
    public class ContactServiceTests 
    {
        private ContextFactory RetrieveContextFactory()
        {
            string connectionString = "Server=localhost;Database=LaNacionProjectTest;Trusted_Connection=True;";

            var options = new DbContextOptionsBuilder<ContextFactory>()
            .UseSqlServer(connectionString)
            .Options;
            return new ContextFactory(options);
        }


        [TestMethod]
        public async Task CreateContactAsync_ShouldCreateContact()
        {
            //Given...

            PhoneNumber phoneNumber = new PhoneNumber
            {
                Type = "Personal",
                Number = "1140556992"
            };
            List<PhoneNumber> lstPhoneNumber = new List<PhoneNumber>() { phoneNumber };

            Address address = new Address
            {
                State = "Buenos Aires",
                City = "Ciudad Autónoma de Buenos Aires"
            };

            Contact contactToCreate = new Contact
            {
                Name = "Fernando",
                Company = "CNH",
                ProfileImage = "http://www.img.com/myimage.jpg",
                Email = "fernandolamasw@gmail.com",
                Birthdate = new DateTime(1998, 1, 22),
                PhoneNumber = lstPhoneNumber,
                Address = address
            };


            using ContextFactory context = RetrieveContextFactory();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            ContactServices contactService = new ContactServices(context);

            //When...
            Contact? createdContact = await contactService.CreateContactAsync(contactToCreate);
            
            //Then...
            Assert.IsNotNull(createdContact);
        }
        [TestMethod]
        public async Task SearchExistingContactByEmail_ShouldFindCreatedContact()
        {
            //Given...
            string email = "fernandolamasw@gmail.com";

            using ContextFactory context = RetrieveContextFactory();

            ContactServices contactService = new ContactServices(context);

            //When...

            List<Contact> contact = await contactService.GetContactByEmailOrPhoneNumberAsync(email);

            //Then...
            Assert.IsTrue(contact.Any());
        }
        [TestMethod]

        public async Task CreateExistingContact_ShouldRejectByRepeated()
        {
            //Given...

            PhoneNumber phoneNumber = new PhoneNumber
            {
                Type = "Work",
                Number = "1140556992"
            };
            List<PhoneNumber> lstPhoneNumber = new List<PhoneNumber>() { phoneNumber };

            Address address = new Address
            {
                State = "Buenos Aires",
                City = "Ciudad Autónoma de Buenos Aires"
            };

            Contact contactToCreate = new Contact
            {
                Name = "Ezequiel",
                Company = "CNH",
                ProfileImage = "http://www.img.com/myimage.jpg",
                Email = "fernandolamasw@gmail.com",
                Birthdate = new DateTime(1998, 1, 22),
                PhoneNumber = lstPhoneNumber,
                Address = address
            };

            using ContextFactory context = RetrieveContextFactory();

            ContactServices contactService = new ContactServices(context);

            //When...
            try
            {
                Contact? contact = await contactService.CreateContactAsync(contactToCreate);
                //Then...
                Assert.Fail();

            }catch(Exception ex)
            {
                Assert.AreEqual<Type>(typeof(Exception), ex.GetType());
            }
        }
    }
}