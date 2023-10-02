using Business.GalaxiaWordle.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GalaxiaWordle.Controllers
{
    public class LoginController : Controller
    {
        #region Fields
        ILogin _login;
        #endregion

        #region Constructors
        public LoginController(ILogin login)
        {
            _login = login;
        }
        #endregion

        #region Public Methods
        public IActionResult Index()
        {
            return View();
        }

        public bool ValidateUserCredentails(string userName, string password) =>
            _login.ValidateUserCredentails(userName, password);
        #endregion
    }
}