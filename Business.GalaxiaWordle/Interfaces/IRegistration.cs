using Business.GalaxiaWordle.Data.Models;

namespace Business.GalaxiaWordle.Interfaces
{
    public interface IRegistration
    {
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userName">Users User Name.</param>
        /// <param name="password">Users Password.</param>
        /// <returns>If the creation was successful.</returns>
        bool CreateNewUser(UserInformation userInformation);
    }
}