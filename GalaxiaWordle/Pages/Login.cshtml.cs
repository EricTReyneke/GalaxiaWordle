using Business.GalaxiaWordle.Data.Models;
using Business.GalaxiaWordle.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GalaxiaWordle.Pages
{
    public class LoginModel : PageModel
    {
        #region Fields
        ILogin _login;
        #endregion

        #region Constructors
        public LoginModel(ILogin login)
        {
            _login = login;
        }
        #endregion

        #region Public Methods
        public void Index()
        {

        }

        public IActionResult OnPostValidateUserInfo(string userName, string password)
        {
            if (_login.ValidateUserCredentails(userName, password))
                return new RedirectResult("/Login");

            return Page();
        }
        #endregion
    }
}
