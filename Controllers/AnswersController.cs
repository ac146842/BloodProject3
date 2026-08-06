using BloodProject3.Areas.Identity.Data;
using BloodProject3.Models;
using BloodProject3.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BloodProject3.Controllers
{
    public class AnswersController : Controller
    {
        private readonly BloodProject3DbContext _context;

        public AnswersController(BloodProject3DbContext context)
        {
            _context = context;
        }

        // GET: Answers
        public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var students = from s in _context.Answers
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName .Contains(searchString)
                                       || s.FirstMidName .Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Questions":
                    students = students.OrderByDescending(s => s.Questions);
                    break;
                case "Donor":
                    students = students.OrderBy(s => s.Donor);
                    break;
                case "AnswersText":
                    students = students.OrderByDescending(s => s.AnswersText);
                    break;
                case "AnswerDate":
                    students = students.OrderByDescending(s => s.AnswerDate);
                    break;
                default:
                    students = students.OrderBy(s => s.FormID);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Answers>.CreateAsync(students.AsNoTracking(), pageNumber ?? 1, pageSize));

            return View(await _context.Answers.Include(a => a.Questions).Include(a => a.Donor).ToListAsync());
        }


        // GET: Answers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers
                .FirstOrDefaultAsync(m => m.AnswersID == id);
            if (answers == null)
            {
                return NotFound();
            }

            return View(answers);
        }

        // GET: Answers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnswersID,FormID,HealthQID,DonorID,AnswersText,AnswerDate")] Answers answers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(answers);
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers.FindAsync(id);
            if (answers == null)
            {
                return NotFound();
            }
            ViewData["HealthQID"] = new SelectList(_context.Questions, "HealthQID", "FormQuestions", answers.HealthQID);
            return View(answers);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnswersID,FormID,HealthQID,DonorID,AnswersText,AnswerDate")] Answers answers)
        {
            if (id != answers.AnswersID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswersExists(answers.AnswersID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HealthQID"] = new SelectList(_context.Questions, "HealthQID", "FormQuestions", answers.HealthQID);
            return View(answers);
        }

        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers
                .FirstOrDefaultAsync(m => m.AnswersID == id);
            if (answers == null)
            {
                return NotFound();
            }

            return View(answers);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answers = await _context.Answers.FindAsync(id);
            if (answers != null)
            {
                _context.Answers.Remove(answers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswersExists(int id)
        {
            return _context.Answers.Any(e => e.AnswersID == id);
        }
    }
}