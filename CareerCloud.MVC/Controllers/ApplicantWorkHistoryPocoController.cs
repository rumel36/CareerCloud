﻿using System;
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
    public class ApplicantWorkHistoryPocoController : Controller
    {
        private readonly CareerCloudContext _context;

        public ApplicantWorkHistoryPocoController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: ApplicantWorkHistoryPoco
        public async Task<IActionResult> Index(Guid? id)
        {
            if (id == null)
                return BadRequest();

            var careerCloudContext = _context.ApplicantWorkHistory.Where(a => a.Applicant == id);
            if (careerCloudContext.Count() == 0)
                return NotFound();

            return View(await careerCloudContext.ToListAsync());
        }

        // GET: ApplicantWorkHistoryPoco/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantWorkHistoryPoco = await _context.ApplicantWorkHistory
                .Include(a => a.ApplicantProfile)
                .Include(a => a.SystemCountryCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantWorkHistoryPoco == null)
            {
                return NotFound();
            }

            return View(applicantWorkHistoryPoco);
        }

        // GET: ApplicantWorkHistoryPoco/Create
        public IActionResult Create()
        {
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfile, "Id", "Id");
            ViewData["CountryCode"] = new SelectList(_context.SystemCountryCode, "Code", "Code");
            return View();
        }

        // POST: ApplicantWorkHistoryPoco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Applicant,CompanyName,CountryCode,Location,JobTitle,JobDescription,StartMonth,StartYear,EndMonth,EndYear")] ApplicantWorkHistoryPoco applicantWorkHistoryPoco)
        {
            if (ModelState.IsValid)
            {
                applicantWorkHistoryPoco.Id = Guid.NewGuid();
                _context.Add(applicantWorkHistoryPoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfile, "Id", "Id", applicantWorkHistoryPoco.Applicant);
            ViewData["CountryCode"] = new SelectList(_context.SystemCountryCode, "Code", "Code", applicantWorkHistoryPoco.CountryCode);
            return View(applicantWorkHistoryPoco);
        }

        // GET: ApplicantWorkHistoryPoco/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantWorkHistoryPoco = await _context.ApplicantWorkHistory.FindAsync(id);
            if (applicantWorkHistoryPoco == null)
            {
                return NotFound();
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfile, "Id", "Id", applicantWorkHistoryPoco.Applicant);
            ViewData["CountryCode"] = new SelectList(_context.SystemCountryCode, "Code", "Code", applicantWorkHistoryPoco.CountryCode);
            return View(applicantWorkHistoryPoco);
        }

        // POST: ApplicantWorkHistoryPoco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Applicant,CompanyName,CountryCode,Location,JobTitle,JobDescription,StartMonth,StartYear,EndMonth,EndYear")] ApplicantWorkHistoryPoco applicantWorkHistoryPoco)
        {
            if (id != applicantWorkHistoryPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantWorkHistoryPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantWorkHistoryPocoExists(applicantWorkHistoryPoco.Id))
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
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfile, "Id", "Id", applicantWorkHistoryPoco.Applicant);
            ViewData["CountryCode"] = new SelectList(_context.SystemCountryCode, "Code", "Code", applicantWorkHistoryPoco.CountryCode);
            return View(applicantWorkHistoryPoco);
        }

        // GET: ApplicantWorkHistoryPoco/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantWorkHistoryPoco = await _context.ApplicantWorkHistory
                .Include(a => a.ApplicantProfile)
                .Include(a => a.SystemCountryCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantWorkHistoryPoco == null)
            {
                return NotFound();
            }

            return View(applicantWorkHistoryPoco);
        }

        // POST: ApplicantWorkHistoryPoco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicantWorkHistoryPoco = await _context.ApplicantWorkHistory.FindAsync(id);
            _context.ApplicantWorkHistory.Remove(applicantWorkHistoryPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantWorkHistoryPocoExists(Guid id)
        {
            return _context.ApplicantWorkHistory.Any(e => e.Id == id);
        }
    }
}
