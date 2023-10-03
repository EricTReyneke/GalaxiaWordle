using Business.GalaxiaWordle.Data.Models;
using Business.GalaxiaWordle.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GalaxiaWordle.Controllers
{
    [Route("Registration")]
    public class RegistrationController : Controller
    {
        #region Fields
        IRegistration _registration;
        #endregion

        #region Constructors
        public RegistrationController(IRegistration registration)
        {
            _registration = registration;
        }
        #endregion

        #region Public Methods
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateNewUser")]
        public IActionResult OnPostCreateNewUser([FromBody] UserInformation userInformation)
        {
            _registration.CreateNewUser(userInformation);
            return View();
        }
        #endregion
    }
}