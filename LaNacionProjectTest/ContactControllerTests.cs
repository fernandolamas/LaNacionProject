using LaNacionProject;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LaNacionProjectTest
{
    [TestClass]
    public class ContactControllerTests
    {
        private readonly HttpClient _httpClient;
        public ContactControllerTests()
        {
            _httpClient = new HttpClient();
            //To-Do retrieve port from a config file
            string port = "7163";
            _httpClient.BaseAddress = new Uri($"https://localhost:{port}");
        }

        [TestMethod]
        public async Task GetContactById_ShouldReturnNotFound()
        {

            //Given...
            var contact = long.MaxValue;

            //When...
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Contacts/{contact}");

            //Then...
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
