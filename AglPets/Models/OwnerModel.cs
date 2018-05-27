using AglPets;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections;
using System.Collections.Generic;

namespace AglPets.Models
{
    public class OwnerModel
    {
        private ICollection<string> _catNames;

        public ICollection<string> CatNames
        {
            get { return _catNames ?? (_catNames = new List<string>()); }
            set { _catNames = value; }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
    }
}
