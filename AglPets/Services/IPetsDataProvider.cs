using System.Collections.Generic;
using System.Threading.Tasks;
using AglPets;
using AglPets.Models;

namespace AglPets.Services
{
    public interface IPetsDataProvider
    {
        Task<ICollection<Owner>> ListOwnersAsync();
    }
}