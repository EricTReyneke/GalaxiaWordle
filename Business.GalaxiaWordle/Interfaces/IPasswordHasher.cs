namespace Business.GalaxiaWordle.Interfaces
{
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashed the userses password with the salt provided.
        /// </summary>
        /// <param name="password">Users Password.</param>
        /// <returns>Hashed password.</returns>
        string HashPassword(string password);
    }
}