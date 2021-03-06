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
    public class CompanyJobDescriptionPocoController : Controller
    {
        private readonly CareerCloudContext _context;

        public CompanyJobDescriptionPocoController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: CompanyJobDescriptionPoco
        public async Task<IActionResult> Index()
        {
            var careerCloudContext = _context.CompanyJobDescription.Include(c => c.CompanyJob);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: CompanyJobDescriptionPoco/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobDescriptionPoco = await _context.CompanyJobDescription
                .Include(c => c.CompanyJob)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobDescriptionPoco == null)
            {
                return NotFound();
            }

            return View(companyJobDescriptionPoco);
        }

        // GET: CompanyJobDescriptionPoco/Create
        public IActionResult Create()
        {
            ViewData["Job"] = new SelectList(_context.CompanyJob, "Id", "Id");
            return View();
        }

        // POST: CompanyJobDescriptionPoco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Job,JobName,JobDescriptions")] CompanyJobDescriptionPoco companyJobDescriptionPoco)
        {
            if (ModelState.IsValid)
            {
                companyJobDescriptionPoco.Id = Guid.NewGuid();
                _context.Add(companyJobDescriptionPoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Job"] = new SelectList(_context.CompanyJob, "Id", "Id", companyJobDescriptionPoco.Job);
            return View(companyJobDescriptionPoco);
        }

        // GET: CompanyJobDescriptionPoco/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobDescriptionPoco = await _context.CompanyJobDescription.FindAsync(id);
            if (companyJobDescriptionPoco == null)
            {
                return NotFound();
            }
            ViewData["Job"] = new SelectList(_context.CompanyJob, "Id", "Id", companyJobDescriptionPoco.Job);
            return View(companyJobDescriptionPoco);
        }

        // POST: CompanyJobDescriptionPoco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Job,JobName,JobDescriptions")] CompanyJobDescriptionPoco companyJobDescriptionPoco)
        {
            if (id != companyJobDescriptionPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyJobDescriptionPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyJobDescriptionPocoExists(companyJobDescriptionPoco.Id))
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
            ViewData["Job"] = new SelectList(_context.CompanyJob, "Id", "Id", companyJobDescriptionPoco.Job);
            return View(companyJobDescriptionPoco);
        }

        // GET: CompanyJobDescriptionPoco/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobDescriptionPoco = await _context.CompanyJobDescription
                .Include(c => c.CompanyJob)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobDescriptionPoco == null)
            {
                return NotFound();
            }

            return View(companyJobDescriptionPoco);
        }

        // POST: CompanyJobDescriptionPoco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyJobDescriptionPoco = await _context.CompanyJobDescription.FindAsync(id);
            _context.CompanyJobDescription.Remove(companyJobDescriptionPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyJobDescriptionPocoExists(Guid id)
        {
            return _context.CompanyJobDescription.Any(e => e.Id == id);
        }
    }
}
