using Business.DynamicModelReflector.Interfaces;
using Business.GalaxiaWordle.Login.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Business.GalaxiaWordle.Login.Logins
{
    public class BasicLogin : ILogin
    {
        #region Fields
        IModelReflector _modelreflector;
        string _salt = "a8f5f167f44f4964e6c998dee827110c";
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs the BasicLogin class.
        /// </summary>
        /// <param name="modelReflector">Model Reflector for Database operations.</param>
        public BasicLogin(IModelReflector modelReflector)
        {
            _modelreflector = modelReflector;
        }
        #endregion

        #region Public Methods
        public bool ValidateUserCredentails(string userName, string password)
        {
            if (userName == null || password == null) 
                return false;

            Data.Models.Login userLogin = new();

            _modelreflector
                .Load(userLogin)
                .Where(userLogin => userLogin.UserName == userName && userLogin.Password == HashPassword(password))
                .Execute();

            if (string.IsNullOrEmpty(userLogin.UserName) || string.IsNullOrEmpty(userLogin.Password)) 
                return false;

            return true;
        }

        public bool CreateNewUser(string userName, string password)
        {
            if (userName == null || password == null)
                return false;

            Data.Models.Login userLogin = new()
            {
                UserName = userName,
                Password = HashPassword(password)
            };

            _modelreflector
                .Create(userLogin)
                .Execute();

            return true;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Hashed the userses password with the salt provided.
        /// </summary>
        /// <param name="password">Users Password.</param>
        /// <returns>Hashed password.</returns>
        public string HashPassword(string password)
        {
            using (Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(_salt), 10000))
                return Convert.ToBase64String(rfc2898.GetBytes(20));
        }
        #endregion
    }
}
