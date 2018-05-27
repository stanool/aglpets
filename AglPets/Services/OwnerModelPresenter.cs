using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AglPets.Models;

namespace AglPets.Services
{
    public class OwnerModelPresenter : IOwnerModelPresenter
    {
        private readonly IPetsDataProvider _petsDataProvider;

        public OwnerModelPresenter(IPetsDataProvider petsDataProvider)
        {
            _petsDataProvider = petsDataProvider;
        }

        public async Task<IEnumerable<OwnerModel>> GetOwnerModelsAsync()
        {
            var owners = await _petsDataProvider.ListOwnersAsync();
            return owners
                .GroupBy(o => o.Gender)
                .OrderBy(o => o.Key)
                .Select(o => new OwnerModel
                {
                    Gender = o.Key,
                    CatNames = o.SelectMany(g => g.Pets.Where(p => p.Type == "Cat")).OrderBy(x => x.Name).Select(x => x.Name).ToList()
                })
                .Where(o => o.CatNames.Any());
        }
    }
}
