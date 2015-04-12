using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace StickingPlace.UnitTests.LinqDatabaseHelpers
{
    [TestFixture]
    public class PredicateBuilderTests
    {
        List<string> Names;

        [SetUp]
        public void Setup()
        {
            // setup names to be searched
            Names = new List<string>
            {
                "Walter Bishop",
                "Peter Bishop",
                "Olivia Dunham",
                "Phillip Broyles"
            };            
        }

        [Test]
        public void Can_Build_a_predicate_with_ors()
        {
            var keywords = new List<string> {"Bishop", "Olivia"};

            var predicate = PredicateBuilder.False<string>();   // since we're building ORs, we must start with false
            foreach (string keyword in keywords)
            {
                var k = keyword;
                predicate = predicate.Or(p => p.Contains(k));
            }

            var result = Names.AsQueryable()        // since PredicateBuilder generates an Expression, it must be used with IQueryable
                .Where(predicate)                   // it won't work with List or IEnumerable
                .ToList();                          // so it's really best for Linq providers like NHiberate.Linq or Linq to Entity Framework

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.Count(x => x == "Walter Bishop"), Is.EqualTo(1));
            Assert.That(result.Count(x => x == "Peter Bishop"), Is.EqualTo(1));
            Assert.That(result.Count(x => x == "Olivia Dunham"), Is.EqualTo(1));
        }

        [Test]
        public void Can_Build_a_predicate_with_ands()
        {
            var keywords = new List<string> { "Bishop", "Peter" };

            var predicate = PredicateBuilder.True<string>();   // since we're building ANDs, we must start with true
            foreach (string keyword in keywords)
            {
                var k = keyword;
                predicate = predicate.And(p => p.Contains(k));
            }

            var result = Names.AsQueryable()        // since PredicateBuilder generates an Expression, it must be used with IQueryable
                .Where(predicate)                   // it won't work with List or IEnumerable
                .ToList();                          // so it's really best for Linq providers like NHiberate.Linq or Linq to Entity Framework

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.Count(x => x == "Peter Bishop"), Is.EqualTo(1));
        }
    }
}