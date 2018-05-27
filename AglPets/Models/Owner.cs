using System.Collections.Generic;

namespace AglPets.Models
{
    public class Owner
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public short Age { get; set; }

        private ICollection<Pet> _pets;
        public ICollection<Pet> Pets
        {
            get
            {
                return _pets ?? (_pets = new List<Pet>());
            }
        }
    }
}
