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
    public class SecurityLoginsLogPocoController : Controller
    {
        private readonly CareerCloudContext _context;

        public SecurityLoginsLogPocoController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: SecurityLoginsLogPoco
        public async Task<IActionResult> Index()
        {
            var careerCloudContext = _context.SecurityLoginsLog.Include(s => s.SecurityLogin);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: SecurityLoginsLogPoco/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginsLogPoco = await _context.SecurityLoginsLog
                .Include(s => s.SecurityLogin)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityLoginsLogPoco == null)
            {
                return NotFound();
            }

            return View(securityLoginsLogPoco);
        }

        // GET: SecurityLoginsLogPoco/Create
        public IActionResult Create()
        {
            ViewData["Login"] = new SelectList(_context.SecurityLogin, "Id", "Id");
            return View();
        }

        // POST: SecurityLoginsLogPoco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,SourceIP,LogonDate,IsSuccesful")] SecurityLoginsLogPoco securityLoginsLogPoco)
        {
            if (ModelState.IsValid)
            {
                securityLoginsLogPoco.Id = Guid.NewGuid();
                _context.Add(securityLoginsLogPoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Login"] = new SelectList(_context.SecurityLogin, "Id", "Id", securityLoginsLogPoco.Login);
            return View(securityLoginsLogPoco);
        }

        // GET: SecurityLoginsLogPoco/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginsLogPoco = await _context.SecurityLoginsLog.FindAsync(id);
            if (securityLoginsLogPoco == null)
            {
                return NotFound();
            }
            ViewData["Login"] = new SelectList(_context.SecurityLogin, "Id", "Id", securityLoginsLogPoco.Login);
            return View(securityLoginsLogPoco);
        }

        // POST: SecurityLoginsLogPoco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Login,SourceIP,LogonDate,IsSuccesful")] SecurityLoginsLogPoco securityLoginsLogPoco)
        {
            if (id != securityLoginsLogPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(securityLoginsLogPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecurityLoginsLogPocoExists(securityLoginsLogPoco.Id))
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
            ViewData["Login"] = new SelectList(_context.SecurityLogin, "Id", "Id", securityLoginsLogPoco.Login);
            return View(securityLoginsLogPoco);
        }

        // GET: SecurityLoginsLogPoco/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginsLogPoco = await _context.SecurityLoginsLog
                .Include(s => s.SecurityLogin)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityLoginsLogPoco == null)
            {
                return NotFound();
            }

            return View(securityLoginsLogPoco);
        }

        // POST: SecurityLoginsLogPoco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var securityLoginsLogPoco = await _context.SecurityLoginsLog.FindAsync(id);
            _context.SecurityLoginsLog.Remove(securityLoginsLogPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecurityLoginsLogPocoExists(Guid id)
        {
            return _context.SecurityLoginsLog.Any(e => e.Id == id);
        }
    }
}
