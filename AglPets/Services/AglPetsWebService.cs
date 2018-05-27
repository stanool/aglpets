using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AglPets.Services
{
    public class AglPetsWebService : IPetsWebService
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;

        public AglPetsWebService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> ListOwnerJsonAsync()
        {
            var serviceUri = new Uri(_configuration["WebServiceUri"]);

            var response = await _httpClient.GetAsync(serviceUri);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();

            throw new InvalidOperationException(string.Format("Could not retrieve content from upstream service {0}", serviceUri));
        }
    }
}
