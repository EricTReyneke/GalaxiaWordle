using Business.DynamicModelReflector.Interfaces;
using Business.GalaxiaWordle.Data.Models;
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
        public bool CreateNewUser(UserInformation userInformation)
        {
            _modelreflector
                .Create(FinalizeUsersInformation(userInformation))
                .Execute();

            return true;
        }
        #endregion

        #region Private Method
        /// <summary>
        /// Hashes the password and adds the correct Id.
        /// </summary>
        /// <param name="userInformation">User Information objec that comes from the front end.</param>
        /// <returns>Finalize Users Information Object.</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException if userInformation is null.</exception>
        private UserInformation FinalizeUsersInformation(UserInformation userInformation)
        {
            if (userInformation == null) throw new ArgumentNullException(nameof(userInformation));

            userInformation.Password = _passwordHashers.HashPassword(userInformation.Password);

            IEnumerable<UserInformation> users = new List<UserInformation>();

            _modelreflector
                .Load(users)
                .Select(users => users.Id)
                .Execute();

            if (!users.Any())
                userInformation.Id = 1;
            else
                userInformation.Id = users.Max(u => u.Id) + 1;

            return userInformation;
        }
        #endregion
    }
}