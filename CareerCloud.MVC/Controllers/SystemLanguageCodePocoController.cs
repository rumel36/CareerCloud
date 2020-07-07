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
    public class SystemLanguageCodePocoController : Controller
    {
        private readonly CareerCloudContext _context;

        public SystemLanguageCodePocoController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: SystemLanguageCodePoco
        public async Task<IActionResult> Index()
        {
            return View(await _context.SystemLanguageCode.ToListAsync());
        }

        // GET: SystemLanguageCodePoco/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemLanguageCodePoco = await _context.SystemLanguageCode
                .FirstOrDefaultAsync(m => m.LanguageID == id);
            if (systemLanguageCodePoco == null)
            {
                return NotFound();
            }

            return View(systemLanguageCodePoco);
        }

        // GET: SystemLanguageCodePoco/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SystemLanguageCodePoco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LanguageID,Name,NativeName")] SystemLanguageCodePoco systemLanguageCodePoco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemLanguageCodePoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemLanguageCodePoco);
        }

        // GET: SystemLanguageCodePoco/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemLanguageCodePoco = await _context.SystemLanguageCode.FindAsync(id);
            if (systemLanguageCodePoco == null)
            {
                return NotFound();
            }
            return View(systemLanguageCodePoco);
        }

        // POST: SystemLanguageCodePoco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LanguageID,Name,NativeName")] SystemLanguageCodePoco systemLanguageCodePoco)
        {
            if (id != systemLanguageCodePoco.LanguageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemLanguageCodePoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemLanguageCodePocoExists(systemLanguageCodePoco.LanguageID))
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
            return View(systemLanguageCodePoco);
        }

        // GET: SystemLanguageCodePoco/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemLanguageCodePoco = await _context.SystemLanguageCode
                .FirstOrDefaultAsync(m => m.LanguageID == id);
            if (systemLanguageCodePoco == null)
            {
                return NotFound();
            }

            return View(systemLanguageCodePoco);
        }

        // POST: SystemLanguageCodePoco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var systemLanguageCodePoco = await _context.SystemLanguageCode.FindAsync(id);
            _context.SystemLanguageCode.Remove(systemLanguageCodePoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemLanguageCodePocoExists(string id)
        {
            return _context.SystemLanguageCode.Any(e => e.LanguageID == id);
        }
    }
}
