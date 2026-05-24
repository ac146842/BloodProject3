using BloodProject3.Areas.Identity.Data;
using BloodProject3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BloodProject3.Controllers
{
    public class AnswersController : Controller
    {
        private readonly BloodProject3DbContext _context;

        public AnswersController(BloodProject3DbContext context)
        {
            _context = context;
        }

        private void PopulateViewBag(object selectedDonor = null, object selectedForm = null, object selectedQuestion = null)
        {
            ViewBag.DonorID = new SelectList(_context.Donor.Select(d => new { Id = d.DonorID, Name = $"{d.FirstName} {d.LastName}" }), "Id", "Name", selectedDonor);
            ViewBag.FormID = new SelectList(_context.MedicalForm, "FormID", "FormID", selectedForm);
            ViewBag.HealthQID = new SelectList(_context.Questions, "HealthQID", "FormQuestions", selectedQuestion);
        }

        public async Task<IActionResult> Index()
        {
            var answers = _context.Answers.Include(a => a.Donor);
            return View(await answers.ToListAsync());
        }

        public IActionResult Create()
        {
            PopulateViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnswersID,FormID,HealthQID,DonorID,AnswersBool,AnswerDate")] Answers answers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateViewBag(answers.DonorID, answers.FormID, answers.HealthQID);
            return View(answers);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var answers = await _context.Answers.FindAsync(id);
            if (answers == null) return NotFound();
            PopulateViewBag(answers.DonorID, answers.FormID, answers.HealthQID);
            return View(answers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnswersID,FormID,HealthQID,DonorID,AnswersBool,AnswerDate")] Answers answers)
        {
            if (id != answers.AnswersID) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(answers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateViewBag(answers.DonorID, answers.FormID, answers.HealthQID);
            return View(answers);
        }
    }
}