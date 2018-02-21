using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BloomFilter
{
    class Program
    {
        static string[] words;

        static void Main(string[] args)
        {

            words = System.IO.File.ReadAllLines(@"C:\Work\Training\Bloom\wordlist.txt");
            BloomFilter bFilter = new BloomFilter(1000000);

            foreach (var item in words)
            {
                bFilter.Add(item);
            }

            var test = bFilter.MightContain("stinkbug");
            var check = listContains("stinkbug");
            Console.WriteLine("Done");
            Console.ReadLine();

        }

        public static bool listContains(string word)
        {
            if (words.Contains(word)) return true;
            else return false;
        }
    }
}
