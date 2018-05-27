using AglPets.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AglPets.Services
{
    public interface IOwnerModelPresenter
    {
        Task<IEnumerable<OwnerModel>> GetOwnerModelsAsync();
    }
}