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
        private readonly int size;

        public BloomFilter(int size)
        {
            this.size = size;
            bitArray = new BitArray(size);
        }

        public void Add(string input)
        {
            var MD5 = MD5Hash(input);
            bitArray.Set(MD5, true);

            var SHA256 = SHA256Hash(input);
            bitArray.Set(SHA256, true);
        }
        private int MD5Hash(string input)
        {
            var MD5Hash = MD5.Create();
            var hash = MD5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Math.Abs(BitConverter.ToInt32(hash, 0) % size);
        }
        private int SHA256Hash(string input)
        {
            var SHA256Hash = SHA256.Create();
            var hash = SHA256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Math.Abs(BitConverter.ToInt32(hash, 0) % size);
        }

        public bool MightContain(string input)
        {
            var MD5 = MD5Hash(input);
            var SHA256 = SHA256Hash(input);

            return bitArray.Get(MD5) && bitArray.Get(SHA256);
        }

        public double bitsUsed()
        {
            int count = 0;

            for (int i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[i])
                    count++;
            }

            return Math.Floor((double)count / size * 100);
        }
    }
}
