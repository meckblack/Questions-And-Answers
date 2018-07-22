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
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public QuestionsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region 

        #endregion

        #region Add Question

        [HttpGet]
        public IActionResult AddQuestion()
        {
            ViewData["isthere"] = _session.GetInt32("usersessionid");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQuestion(Questions questions)
        {
            var _userid = _session.GetInt32("usersessionid");
            if (_userid != null)
            {
                if (ModelState.IsValid)
                {
                    var _newquestion = new Questions
                    {
                        Header = questions.Header,
                        Body = questions.Body,
                        Time = Convert.ToString(DateTime.Now),
                        UserId = Convert.ToInt32(_userid)
                    };
                    _db.Question.Add(_newquestion);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                return View(questions);
            }
            else
            {
                return RedirectToAction("SignIn", "User");
            }
           
           
        }

        #endregion

        public IActionResult MyQuestions()
        {
            ViewData["isthere"] = _session.GetInt32("usersessionid");
            var id = _session.GetInt32("usersessionid");
            var _allUserQuestions = _db.Question.Where(q => q.UserId == Convert.ToInt32(id));
            return View(_allUserQuestions);
        }

    }
}