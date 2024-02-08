namespace Business.GalaxiaWordle.Interfaces
{
    public interface IWordleApiClient
    {
        /// <summary>
        /// Hits an API which will return a random dictionary word.
        /// </summary>
        /// <returns>Random Dictionary word.</returns>
        Task<string> CallWordleWord();

        /// <summary>
        /// Sets the Wordle word length.
        /// </summary>
        /// <param name="wordLength">Wordle word length.</param>
        void SetWordleWordLength(int wordLength);
    }
}