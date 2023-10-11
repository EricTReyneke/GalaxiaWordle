using Business.GalaxiaWordle.Interfaces;

namespace Business.GalaxiaWordle.Games
{
    public class WordleGalaxia : IWordle
    {
        #region Fields
        string _wrodleWord;
        #endregion

        #region Constructors
        public WordleGalaxia()
        {
            _wrodleWord = "Bring".ToLower();
        }
        #endregion

        #region Public Methods
        public Dictionary<int, string> ValidateWordleWord(string wordleWord) =>
            ValidateWordCorrespondence(wordleWord);
        #endregion

        #region Private Methods
        /// <summary>
        /// Validates the correspondence of a word against the Wordle word and returns a dictionary
        /// where the key is the index of a letter and the value is the validation result ("Green", "Yellow", or "Red").
        /// </summary>
        /// <param name="wordleWord">The Wordle word to be validated against.</param>
        /// <returns>A dictionary with the index of a letter as the key and the validation result as the value.</returns>
        private Dictionary<int, string> ValidateWordCorrespondence(string wordleWord) =>
            wordleWord.Select((letter, index) => new { letter, index })
                .ToDictionary(x => x.index, x => ValidateLetter(Convert.ToChar(x.letter.ToString().ToLower()), x.index));

        /// <summary>
        /// Validates a single letter against the Wordle word and returns a string indicating the validation result.
        /// "Green" if the letter is in the correct position, "Yellow" if the letter is in the Wordle word but in the wrong position,
        /// and "Red" if the letter is not in the Wordle word.
        /// </summary>
        /// <param name="wordleWord">The Wordle word to be validated against.</param>
        /// <param name="character">The character to validate.</param>
        /// <param name="letterIndex">The index of the character in the word being validated.</param>
        /// <returns>A string ("Green", "Yellow", or "Red") indicating the validation result.</returns>
        private string ValidateLetter(char character, int letterIndex)
        {
            List<string> validationList = _wrodleWord.Select((letter, index) =>
            {
                if (letter.Equals(character))
                {
                    if (index.Equals(letterIndex))
                        return "Green";

                    return "Yellow";
                }

                return "Red";
            }).ToList();

            if (validationList[letterIndex] == "Green")
                return "Green";
            else if (validationList.Any(validationList => validationList.Equals("Yellow")))
                return "Yellow";

            return "Red";
        }
        #endregion
    }
}