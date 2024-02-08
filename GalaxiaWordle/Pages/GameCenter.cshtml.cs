using Business.GalaxiaWordle.Data.Models;
using Business.GalaxiaWordle.Interfaces;
using GalaxiaWordle.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GalaxiaWordle.Pages
{
    public class GameCenterModel : PageModel
    {
        #region Fields
        IWordle _wordle;
        IWordleApiClient _wordleApiClient;

        #endregion

        #region Properties
        public string ErrorMessage { get; set; }
        public int WordLength { get; set; }
        #endregion

        #region Constructors
        public GameCenterModel(IWordle wordle, IWordleApiClient wordleApiClient)
        {
            _wordle = wordle;
            _wordleApiClient = wordleApiClient;
        }
        #endregion

        #region Public methods
        public void OnGet()
        {
        }

        public JsonResult OnPostValidateWordleWord(string wordleWord)
        {
            try
            {
                _wordle.WordleWord = HttpContext.Session.GetString("wordleWord");
                return new JsonResult(_wordle.ValidateWordleWord(wordleWord));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ErrorMessage, $"Error occured: {ex}");
                return new JsonResult("Failure.");
            }
        }

        public async Task<JsonResult> OnPostSetWordLength(string wordLength)
        {
            int wordLengthInt = int.Parse(wordLength);
            WordLength = wordLengthInt;
            if (wordLength == "Please select a value")
                return new JsonResult("Continue.");

            if (!string.IsNullOrEmpty(wordLength))
            {
                _wordleApiClient.SetWordleWordLength(wordLengthInt);
                HttpContext.Session.SetInt32("wordLength", wordLengthInt);
                HttpContext.Session.SetString("wordleWord", await _wordleApiClient.CallWordleWord());
                return new JsonResult("Success.");
            }

            return new JsonResult("Failure.");
        }
        public IActionResult OnGetWordleGame(string wordLength)
        {
            WordleGameContext wordleGameContext = new() { WordLength = int.Parse(wordLength) };
            return Partial("_wordleGame", wordleGameContext);
        }
        #endregion
    }
}