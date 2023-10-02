using Business.DynamicModelReflector.Interfaces;
using Business.GalaxiaWordle.Interfaces;

namespace Business.GalaxiaWordle.Login.Logins
{
    public class BasicLogin : ILogin
    {
        #region Fields
        IModelReflector _modelreflector;
        IPasswordHasher _passwordHashers;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs the BasicLogin class.
        /// </summary>
        /// <param name="modelReflector">Model Reflector for Database operations.</param>
        public BasicLogin(IModelReflector modelReflector, IPasswordHasher passwordHashers)
        {
            _modelreflector = modelReflector;
            _passwordHashers = passwordHashers;
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
                .Where(userLogin => userLogin.UserName == userName && userLogin.Password == _passwordHashers.HashPassword(password))
                .Execute();

            if (string.IsNullOrEmpty(userLogin.UserName) || string.IsNullOrEmpty(userLogin.Password)) 
                return false;

            return true;
        }
        #endregion
    }
}