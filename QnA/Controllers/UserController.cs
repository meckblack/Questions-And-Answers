using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QnA.Data;
using QnA.Models;

namespace QnA.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public UserController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_db.User.Any(u => u.Email == user.Email))
                    {
                        ViewData["doubleemail"] = "Sorry Email already exists";
                    }
                    if (_db.User.Any(u => u.Username == user.Username))
                    {
                        ViewData["doubleusername"] = "Sorry Username already exists";
                    }
                    else
                    {
                        var _user = new User
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Username = user.Username,
                            Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                            ConfirmPassword = BCrypt.Net.BCrypt.HashPassword(user.ConfirmPassword),
                        };

                        await _db.User.AddAsync(_user);
                        await _db.SaveChangesAsync();

                        return RedirectToAction("SignIn", "User");

                    }
                }
            }
            catch
            {
                return View();
            }
            return View();
        }

        #endregion

        #region Sigin IN

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(User user)
        {
            try
            {
                var _user = _db.User.Where(u => u.Email == user.Email).SingleOrDefault();

                if (_user != null)
                {
                    var _password = BCrypt.Net.BCrypt.Verify(user.Password, _user.Password);

                    if (_password == true)
                    {
                        _session.SetInt32("usersessionid", _user.UserId);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewData["mismatch"] = "Email and Password do not match";
                    }
                }
                else
                {
                    ViewData["mismatch"] = "Email doesn't exist";
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        #endregion


    }
}