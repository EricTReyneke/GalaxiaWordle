namespace Business.GalaxiaWordle.Interfaces
{
    public interface IWordle
    {
        /// <summary>
        /// Validates the world word and returns a dectionary indicating what letters are correct, are in the incorrect index or which letters arnt correct.
        /// </summary>
        /// <param name="wordleWord">Wordle word form the front end.</param>
        /// <returns>dectionary indicating what letters are correct, are in the incorrect index or which letters arnt correct.</returns>
        Dictionary<int, string> ValidateWordleWord(string wordleWord);
    }
}