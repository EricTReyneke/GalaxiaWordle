using Business.GalaxiaWordle.Data.Models;
using Business.GalaxiaWordle.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GalaxiaWordle.Pages
{
    public class RegisterModel : PageModel
    {
        #region Fields
        IRegistration _registration;
        #endregion

        #region Constructors
        public RegisterModel(IRegistration registration)
        {
            _registration = registration;
        }
        #endregion

        #region Public Methods
        public void Index()
        {

        }

        public IActionResult OnPostCreateNewUser(UserInformation userInformation)
        {
            if(_registration.CreateNewUser(userInformation))
                return new RedirectResult("/Login");

            return Page();
        }
        #endregion
    }
}