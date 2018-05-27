using AglPets;
using AglPets.Models;
using AglPets.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AglPetsTest
{
    public class PetsDataProviderTest
    {
        [Theory]
        [MemberData(nameof(ListOwnersWithPetsTestCases))]
        public async void ListOwnersWithCatsTest(string expectedOwner, Func<Owner, bool> ownerCondition)
        {
            const string jsonString = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]},{\"name\":\"Steve\",\"gender\":\"Male\",\"age\":45,\"pets\":null},{\"name\":\"Fred\",\"gender\":\"Male\",\"age\":40,\"pets\":[{\"name\":\"Tom\",\"type\":\"Cat\"},{\"name\":\"Max\",\"type\":\"Cat\"},{\"name\":\"Sam\",\"type\":\"Dog\"},{\"name\":\"Jim\",\"type\":\"Cat\"}]},{\"name\":\"Samantha\",\"gender\":\"Female\",\"age\":40,\"pets\":[{\"name\":\"Tabby\",\"type\":\"Cat\"}]},{\"name\":\"Alice\",\"gender\":\"Female\",\"age\":64,\"pets\":[{\"name\":\"Simba\",\"type\":\"Cat\"},{\"name\":\"Nemo\",\"type\":\"Fish\"}]}]\r\n";

            var mockWebService = new Mock<IPetsWebService>();
            mockWebService.Setup(x => x.ListOwnerJsonAsync()).ReturnsAsync(jsonString);

            var dataProvider = new PetsDataProvider(mockWebService.Object);

            var result = await dataProvider.ListOwnersAsync();

            Assert.True(result.Count() == 6, "Result does not contain 6 owners");

            Assert.True(result.Any(ownerCondition), string.Format("Result does not contain matching owner for '{0}'", expectedOwner));
        }

        public static IEnumerable<object[]> ListOwnersWithPetsTestCases => new[]
        {
            new object[] { "Bob", new Func<Owner, bool>(x => x.Name == "Bob" && x.Age == 23 && x.Gender == Gender.Male && x.Pets.Count() == 2)},
            new object[] { "Jennifer", new Func<Owner, bool>(x => x.Name == "Jennifer" && x.Age == 18 && x.Gender == Gender.Female && x.Pets.Count() == 1)},
            new object[] { "Steve", new Func<Owner, bool>(x => x.Name == "Steve" && x.Age == 45 && x.Gender == Gender.Male && !x.Pets.Any())},
            new object[] { "Fred", new Func<Owner, bool>(x => x.Name == "Fred" && x.Age == 40 && x.Gender == Gender.Male && x.Pets.Count() == 4)},
            new object[] { "Samantha", new Func<Owner, bool>(x => x.Name == "Samantha" && x.Age == 40 && x.Gender == Gender.Female && x.Pets.Count() == 1)},
            new object[] { "Alice", new Func<Owner, bool>(x => x.Name == "Alice" && x.Age == 64 && x.Gender == Gender.Female && x.Pets.Count() == 2)}
        };
    }
}
