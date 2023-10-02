using Business.DynamicModelReflector.Interfaces;
using Business.GalaxiaWordle.Interfaces;

namespace Business.GalaxiaWordle.Registrations
{
    public class BasicRegistration : IRegistration
    {
        #region Fields
        IModelReflector _modelreflector;
        IPasswordHasher _passwordHashers;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs the BasicRegistration class.
        /// </summary>
        /// <param name="modelReflector">Model Reflector for Database operations.</param>
        public BasicRegistration(IModelReflector modelReflector, IPasswordHasher passwordHashers)
        {
            _modelreflector = modelReflector;
            _passwordHashers = passwordHashers;
        }
        #endregion

        #region Public Methods
        public bool CreateNewUser(string userName, string password)
        {
            if (userName == null || password == null)
                return false;

            Data.Models.Login userLogin = new()
            {
                UserName = userName,
                Password = _passwordHashers.HashPassword(password)
            };

            _modelreflector
                .Create(userLogin)
                .Execute();

            return true;
        }
        #endregion
    }
}