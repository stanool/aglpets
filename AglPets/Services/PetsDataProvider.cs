using AglPets;
using AglPets.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AglPets.Services
{
    public class PetsDataProvider : IPetsDataProvider
    {
        private readonly IPetsWebService _petsWebService;

        public PetsDataProvider(IPetsWebService petsWebService)
        {
            _petsWebService = petsWebService;
        }

        public async Task<ICollection<Owner>> ListOwnersAsync()
        {
            var json = await _petsWebService.ListOwnerJsonAsync();

            return await Task.FromResult(JsonConvert.DeserializeObject<ICollection<Owner>>(json));
        }
    }
}
