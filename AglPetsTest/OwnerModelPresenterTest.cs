using AglPets.Models;
using AglPets.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AglPetsTest
{
    public class OwnerModelPresenterTest
    {
        [Theory]
        [MemberData(nameof(GetCatListTestCases))]
        public async void GetCatListsTest(ICollection<Owner> owners, Action<IEnumerable<OwnerModel>> assertAction)
        {
            var mockPetsDataProvider = new Mock<IPetsDataProvider>();
            mockPetsDataProvider.Setup(x => x.ListOwnersAsync()).ReturnsAsync(owners);

            var catListPresenter = new OwnerModelPresenter(mockPetsDataProvider.Object);

            var result = await catListPresenter.GetOwnerModelsAsync();

            assertAction(result);
        }

        public static IEnumerable<object[]> GetCatListTestCases => new[]
        {
            new object[]
            {
                new List<Owner>
                {
                    new Owner
                    {
                        Gender = Gender.Female,
                        Pets =
                        {
                            new Pet { Name = "Bolt", Type = "Dog" }
                        }
                    }
                },
                new Action<IEnumerable<OwnerModel>>(x => Assert.Empty(x))
            },
            new object[]
            {
                new List<Owner>
                {
                    new Owner
                    {
                        Gender = Gender.Female,
                        Pets =
                        {
                            new Pet { Name = "Bolt", Type = "Dog" }
                        }
                    },
                    new Owner
                    {
                        Gender = Gender.Female,
                        Pets =
                        {
                            new Pet { Name = "Dumbo", Type = "Elephant" },
                            new Pet { Name = "Fritz", Type = "Cat" }
                        }
                    }
                },
                new Action<IEnumerable<OwnerModel>>(x => 
                {
                    Assert.True(x.Count() == 1, "Owners listed with no cats");
                    Assert.True(x./* only one list */ Single(). /* only one cat */ CatNames.Single() == "Fritz", "More than one cat");
                })
            },
            new object[]
            {
                new List<Owner>
                {
                    new Owner
                    {
                        Gender = Gender.Female,
                        Pets =
                        {
                            new Pet { Name = "Fritz", Type = "Cat" }
                        }
                    },
                    new Owner
                    {
                        Gender = Gender.Male,
                        Pets =
                        {
                            new Pet { Name = "Sylvester", Type = "Cat" }
                        }
                    },
                    new Owner
                    {
                        Gender = Gender.Male,
                        Pets =
                        {
                            new Pet { Name = "Garfield", Type = "Cat" }
                        }
                    },
                    new Owner
                    {
                        Gender = Gender.Female,
                        Pets =
                        {
                            new Pet { Name = "Lucky", Type = "Cat" }
                        }
                    }
                },
                new Action<IEnumerable<OwnerModel>>(x =>
                {
                    Assert.True(x.Count() == 2, "Cats not correctly grouped by gender");
                    for(int i = 0, count = x.Count(); i < count; i++)
                    {
                        var group = x.ElementAt(i);
                        Assert.True(group.CatNames.Count() == 2, string.Format("Wrong number of cats for gender {0}", group.Gender.ToString()));
                    }
                })
            }
        };
    }
}
