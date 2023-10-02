using Business.GalaxiaWordle.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Business.GalaxiaWordle.PasswordHasers
{
    public class SaltPasswordHasing : IPasswordHasher
    {
        #region Public Methods
        public string HashPassword(string password)
        {
            using (Rfc2898DeriveBytes rfc2898 = new(password, Encoding.UTF8.GetBytes("a8f5f167f44f4964e6c998dee827110c"), 10000))
                return Convert.ToBase64String(rfc2898.GetBytes(20));
        }
        #endregion
    }
}