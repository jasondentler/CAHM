using System;
using System.Security.Cryptography;
using System.Text;

namespace CAHM
{
    public class Hasher : IHasher
    {
        private readonly HashAlgorithm _algorithm;

        public Hasher(HashAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        public string Hash(string salt, string secret)
        {
            var data = Encoding.UTF8.GetBytes(salt + secret);
            var result = _algorithm.ComputeHash(data);
            return Convert.ToBase64String(result);
        }

    }
}