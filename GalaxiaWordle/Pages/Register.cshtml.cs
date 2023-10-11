using Business.GalaxiaWordle.Data.Models;
using Business.GalaxiaWordle.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace GalaxiaWordle.Pages
{
    public class RegisterModel : PageModel
    {
        #region Fields
        IRegistration _registration;
        #endregion

        #region Properties
        public UserInformation userInformationErrors { get; set; }
        public string confirmPasswordErrors { get; set; }
        #endregion

        #region Constructors
        public RegisterModel(IRegistration registration)
        {
            _registration = registration;
            userInformationErrors = new();
        }
        #endregion

        #region Public Methods
        public void Index()
        {

        }

        public IActionResult OnPostCreateNewUser(UserInformation userInformation, string confirmPassword)
        {
            ValidateRegister(userInformation, confirmPassword);

            if (_registration.CreateNewUser(userInformation) && ModelState.IsValid)
                return new RedirectResult("/Login");

            return Page();
        }
        #endregion

        private void ValidateRegister(UserInformation userInformation, string confirmPassword)
        {
            // Validate Full Name
            if (string.IsNullOrWhiteSpace(userInformation.FullName) || userInformation.FullName?.Split(' ').Length < 2)
                ModelState.AddModelError(userInformationErrors.FullName, "Please provide a full name.");

            // Validate User Name
            if (string.IsNullOrEmpty(userInformation.UserName))
                ModelState.AddModelError(userInformationErrors.UserName, "Please provide a user name.");

            // Validate Password
            if (string.IsNullOrEmpty(userInformation.Password) || userInformation.Password.Length < 8)
                ModelState.AddModelError(userInformationErrors.Password, "Please provide a password with at least 8 characters.");

            // Validate Confirm Password
            if (string.IsNullOrEmpty(confirmPassword) || !confirmPassword.Equals(userInformation.Password))
                ModelState.AddModelError(confirmPasswordErrors, "Password confirmation does not match the password.");

            // Validate Email
            if (string.IsNullOrEmpty(userInformation.Email) ||
                !Regex.IsMatch(userInformation.Email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
                ModelState.AddModelError(userInformationErrors.Email, "Please provide a valid email address.");
        }
    }
}