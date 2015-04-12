using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace StickingPlace.UnitTests.LinqDatabaseHelpersTests
{
    [TestFixture]
    public class IQueryableExtensionsTests
    {
        List<PersonWithShoes> Items;

        public class PersonWithShoes
        {
            public string Name { get; set; }
            public int ShoeSize { get; set; }
        }

        [SetUp]
        public void Setup()
        {
            Items = new List<PersonWithShoes>()
            {
                new PersonWithShoes{Name = "Matt", ShoeSize = 13},
                new PersonWithShoes{Name = "Robert", ShoeSize = 10},
                new PersonWithShoes{Name = "Bob", ShoeSize = 9}
            };            
        }

        [Test]
        public void Can_order_ascending_a_set_by_property_name_given_by_string()
        {
            var sortedItems = Items.AsQueryable()
                .OrderBy("ShoeSize")
                .ToList();

            Assert.That(sortedItems[0].ShoeSize, Is.EqualTo(9));
            Assert.That(sortedItems[1].ShoeSize, Is.EqualTo(10));
            Assert.That(sortedItems[2].ShoeSize, Is.EqualTo(13));
        }
        
        [Test]
        public void Can_order_descending_a_set_by_property_name_given_by_string()
        {
            var items = new List<PersonWithShoes>()
            {
                new PersonWithShoes{Name = "Matt", ShoeSize = 13},
                new PersonWithShoes{Name = "Robert", ShoeSize = 10},
                new PersonWithShoes{Name = "Bob", ShoeSize = 9}
            };

            var sortedItems = items.AsQueryable()
                .OrderByDescending("Name")
                .ToList();

            Assert.That(sortedItems[0].Name, Is.EqualTo("Robert"));
            Assert.That(sortedItems[1].Name, Is.EqualTo("Matt"));
            Assert.That(sortedItems[2].Name, Is.EqualTo("Bob"));
        }
    }
}