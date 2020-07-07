using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;

namespace CareerCloud.MVC.Controllers
{
    public class ApplicantResumePocoController : Controller
    {
        private readonly CareerCloudContext _context;

        public ApplicantResumePocoController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: ApplicantResumePoco
        public async Task<IActionResult> Index(Guid? id)
        {
            if (id == null)
                return BadRequest();

            var careerCloudContext = _context.ApplicantResume.Where(a => a.Applicant == id);
            if (careerCloudContext.Count() == 0)
                return NotFound();

            return View(await careerCloudContext.ToListAsync());
        }

        // GET: ApplicantResumePoco/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantResumePoco = await _context.ApplicantResume
                .Include(a => a.ApplicantProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantResumePoco == null)
            {
                return NotFound();
            }

            return View(applicantResumePoco);
        }

        // GET: ApplicantResumePoco/Create
        public IActionResult Create()
        {
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfile, "Id", "Id");
            return View();
        }

        // POST: ApplicantResumePoco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Applicant,Resume,LastUpdated")] ApplicantResumePoco applicantResumePoco)
        {
            if (ModelState.IsValid)
            {
                applicantResumePoco.Id = Guid.NewGuid();
                _context.Add(applicantResumePoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfile, "Id", "Id", applicantResumePoco.Applicant);
            return View(applicantResumePoco);
        }

        // GET: ApplicantResumePoco/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantResumePoco = await _context.ApplicantResume.FindAsync(id);
            if (applicantResumePoco == null)
            {
                return NotFound();
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfile, "Id", "Id", applicantResumePoco.Applicant);
            return View(applicantResumePoco);
        }

        // POST: ApplicantResumePoco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Applicant,Resume,LastUpdated")] ApplicantResumePoco applicantResumePoco)
        {
            if (id != applicantResumePoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantResumePoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantResumePocoExists(applicantResumePoco.Id))
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
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfile, "Id", "Id", applicantResumePoco.Applicant);
            return View(applicantResumePoco);
        }

        // GET: ApplicantResumePoco/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantResumePoco = await _context.ApplicantResume
                .Include(a => a.ApplicantProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantResumePoco == null)
            {
                return NotFound();
            }

            return View(applicantResumePoco);
        }

        // POST: ApplicantResumePoco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicantResumePoco = await _context.ApplicantResume.FindAsync(id);
            _context.ApplicantResume.Remove(applicantResumePoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantResumePocoExists(Guid id)
        {
            return _context.ApplicantResume.Any(e => e.Id == id);
        }
    }
}
