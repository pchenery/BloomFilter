using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloomFilter
{
    public class Program
    {
        static string[] words;
        static BloomFilter bFilter = new BloomFilter(3000000);
        static string[] wordCheck = new string[100];
        static Random rand = new Random();

        public static void Main(string[] args)
        {
            words = System.IO.File.ReadAllLines(@"C:\Work\Training\Bloom\wordlist.txt");

            foreach (var item in words)
            {
                bFilter.Add(item);
            }

            var percentUsed = bFilter.bitsUsed();
            Console.WriteLine("Percent Used {0}", percentUsed);

            for (int i = 0; i < 100; i++)
            {
                wordCheck[i] = CreateRandomWords(5);
            }

            //CreateTestWords(100);
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
            for (int i = 0; i < total - 20; i++)
            {
                var j = r.Next(0, words.Count());
                wordCheck[i] = words[j];
            }
            wordCheck[80] = "dawft";
            wordCheck[81] = "sdfke";
            wordCheck[82] = "merci";
            wordCheck[83] = "geodichotomy";
            wordCheck[84] = "biode";
            wordCheck[85] = "qwiet";
            wordCheck[86] = "freek";
            wordCheck[87] = "muste";
            wordCheck[88] = "plade";
            wordCheck[89] = "wigun";
            wordCheck[90] = "gtwsk";
            wordCheck[91] = "lphuf";
            wordCheck[92] = "mvxyu";
            wordCheck[93] = "qlfby";
            wordCheck[94] = "jedii";
            wordCheck[95] = "obiwa";
            wordCheck[96] = "jamja";
            wordCheck[97] = "pleia";
            wordCheck[98] = "ipswt";
            wordCheck[99] = "kdbxo";
        }

        public static string CreateRandomWords(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                        .Select(s => s[rand.Next(s.Length)]).ToArray());
        }

        public static void CompareWords()
        {
            int count = 0;
            for (int i = 0; i < wordCheck.Length; i++)
            {
                bool inBloom = bFilter.MightContain(wordCheck[i]);
                bool inList = ListContains(wordCheck[i]);

                if (inBloom && !inList)
                {
                    Console.WriteLine("False positive with {0}", wordCheck[i]);
                    count++;
                }
            }
            Console.WriteLine("Total False Positives {0}", count);
        }
    }
}
