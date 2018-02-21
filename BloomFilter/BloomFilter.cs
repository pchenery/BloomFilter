using System;
using System.Collections;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloomFilter
{
    public class BloomFilter
    {
        private readonly BitArray bitArray;

        public BloomFilter(int size)
        {
            bitArray = new BitArray(size);
        }

        public void Add(string input)
        {
            var MD5 = MD5Hash(input);
            bitArray.Set(MD5, true);

            var SHA256 = SHA256Hash(input);
            bitArray.Set(SHA256, true);
        }
        private static int MD5Hash(string input)
        {
            var MD5Hash = MD5.Create();
            var hash = MD5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var hashValue = BitConverter.ToUInt16(hash, 0);

            return Math.Abs(hashValue);
        }
        private static int SHA256Hash(string input)
        {
            var SHA256Hash = SHA256.Create();
            var hash = SHA256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var hashValue = BitConverter.ToUInt16(hash, 0);

            return Math.Abs(hashValue);
        }

        public bool MightContain(string input)
        {
            var MD5 = MD5Hash(input);
            var SHA256 = SHA256Hash(input);

            return bitArray.Get(MD5) && bitArray.Get(SHA256);
        }
    }
}
