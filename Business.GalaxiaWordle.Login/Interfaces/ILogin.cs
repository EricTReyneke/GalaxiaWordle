namespace Business.GalaxiaWordle.Login.Interfaces
{
    public interface ILogin
    {
        /// <summary>
        /// Validate if the user credentials are valid.
        /// </summary>
        /// <param name="userName">Users User Name.</param>
        /// <param name="password">Users Password.</param>
        /// <returns>If the users data is in the database.</returns>
        bool ValidateUserCredentails(string userName, string password);

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userName">Users User Name.</param>
        /// <param name="password">Users Password.</param>
        /// <returns>If the creation was successful.</returns>
        bool CreateNewUser(string userName, string password);
    }
}
