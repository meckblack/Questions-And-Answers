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
    public class AnswersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public AnswersController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion


        public IActionResult Index(int id)
        {
            ViewData["isthere"] = _session.GetInt32("usersessionid");
            _session.SetInt32("questionsessionid", id);
            var _allQuestionAnswers = _db.Answers.Where(a => a.QuestionsId == Convert.ToInt32(id));
            var allQuestionAnswers = _db.Answers.Where(a => a.QuestionsId == Convert.ToInt32(id)).ToList();
            
            return View(_allQuestionAnswers);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["id"] = _session.GetInt32("questionsessionid");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Answers answer)
        {
            if (ModelState.IsValid)
            {
                var id = Convert.ToInt32(_session.GetInt32("questionsessionid"));
                var _answer = new Answers
                {
                    Body = answer.Body,
                    QuestionsId = id
                };
               _db.Answers.Add(_answer);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index");

        }

    }
}