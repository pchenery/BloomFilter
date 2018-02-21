using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BloomFilter
{
    class Program
    {
        static string[] words;
        static BloomFilter bFilter = new BloomFilter(1200000);
        static string[] wordCheck = new string[100]; 

        static void Main(string[] args)
        {
            words = System.IO.File.ReadAllLines(@"C:\Work\Training\Bloom\wordlist.txt");

            foreach (var item in words)
            {
                bFilter.Add(item);
            }

            var percentUsed = bFilter.bitsUsed();
            Console.WriteLine("Percent Used {0}", percentUsed);

            CreateTestWords(100);
            CompareWords();
            
            Console.ReadLine();
        }

        public static bool ListContains(string word)
        {
            if (words.Contains(word)) return true;
            else return false;
        }

        public static void CreateTestWords(int total)
        {
            Random r = new Random(2);
            for (int i = 0; i < total - 10; i++)
            {
                var j = r.Next(0, words.Count());
                wordCheck[i] = words[j];
            }
            wordCheck[90] = "dawft";
            wordCheck[91] = "sdfke";
            wordCheck[92] = "merci";
            wordCheck[93] = "geodichotomy";
            wordCheck[94] = "biode";
            wordCheck[95] = "qwiet";
            wordCheck[96] = "freek";
            wordCheck[97] = "muste";
            wordCheck[98] = "plade";
            wordCheck[99] = "wigun";
        }

        public static void CompareWords()
        {
            for (int i = 0; i < wordCheck.Length; i++)
            {
                bool inBloom = bFilter.MightContain(wordCheck[i]);
                bool inList = ListContains(wordCheck[i]);

                if (inBloom && !inList)
                {
                    Console.WriteLine("False positive with {0}", wordCheck[i]);
                }
            }
        }
    }
}
