using System.Security.Cryptography;
using System.Text;

namespace CAHM
{
    public class GravatarService : IGravatarService
    {
        private readonly MD5 _hashAlgorithm;

        public GravatarService(MD5 hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm;
        }

        public string GetGravatarHash(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            email = email.Trim().ToLowerInvariant();

            var hashedBytes = _hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(email));

            var sb = new StringBuilder(hashedBytes.Length*2);

            for (var i = 0; i < hashedBytes.Length; i++)
                sb.Append(hashedBytes[i].ToString("x2"));

            return sb.ToString();
        }
    }
}
