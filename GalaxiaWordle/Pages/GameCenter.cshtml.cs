using Business.GalaxiaWordle.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GalaxiaWordle.Pages
{
    public class GameCenterModel : PageModel
    {
        #region Fields
        IWordle _wordle;
        #endregion

        #region Constructors
        public GameCenterModel(IWordle wordle)
        {
            _wordle = wordle;
        }
        #endregion

        #region Public methods
        public void OnGet()
        {
        }

        public JsonResult OnPostValidateWordleWord(string wordleWord) =>
            new JsonResult(_wordle.ValidateWordleWord(wordleWord));
        #endregion
    }
}