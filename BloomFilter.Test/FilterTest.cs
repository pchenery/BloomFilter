using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomFilter.Test
{
    [TestFixture]
    public class FilterTest
    {
        BloomFilter bFilter;
        string[] words;

        [OneTimeSetUp]
        public void TestSetup()
        {
            bFilter = new BloomFilter(1200000);
            words = System.IO.File.ReadAllLines(@"C:\Work\Training\Bloom\wordlist.txt");

            foreach (var item in words)
            {
                bFilter.Add(item);
            }
        }
        [Test]
        public void IsInList()
        {
            Assert.That(words.Contains("abacus"));
        }

        [Test]
        public void IsNotInList()
        {
            Assert.IsFalse(words.Contains("stratocaster"));
        }

        [OneTimeTearDown]
        public void TestTearDown()
        {
            bFilter = null;
            words = null;
        }
    }
}
