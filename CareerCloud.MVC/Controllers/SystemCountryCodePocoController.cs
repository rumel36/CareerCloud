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
    public class SystemCountryCodePocoController : Controller
    {
        private readonly CareerCloudContext _context;

        public SystemCountryCodePocoController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: SystemCountryCodePoco
        public async Task<IActionResult> Index()
        {
            return View(await _context.SystemCountryCode.ToListAsync());
        }

        // GET: SystemCountryCodePoco/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCountryCodePoco = await _context.SystemCountryCode
                .FirstOrDefaultAsync(m => m.Code == id);
            if (systemCountryCodePoco == null)
            {
                return NotFound();
            }

            return View(systemCountryCodePoco);
        }

        // GET: SystemCountryCodePoco/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SystemCountryCodePoco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name")] SystemCountryCodePoco systemCountryCodePoco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemCountryCodePoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemCountryCodePoco);
        }

        // GET: SystemCountryCodePoco/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCountryCodePoco = await _context.SystemCountryCode.FindAsync(id);
            if (systemCountryCodePoco == null)
            {
                return NotFound();
            }
            return View(systemCountryCodePoco);
        }

        // POST: SystemCountryCodePoco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,Name")] SystemCountryCodePoco systemCountryCodePoco)
        {
            if (id != systemCountryCodePoco.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemCountryCodePoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemCountryCodePocoExists(systemCountryCodePoco.Code))
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
            return View(systemCountryCodePoco);
        }

        // GET: SystemCountryCodePoco/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCountryCodePoco = await _context.SystemCountryCode
                .FirstOrDefaultAsync(m => m.Code == id);
            if (systemCountryCodePoco == null)
            {
                return NotFound();
            }

            return View(systemCountryCodePoco);
        }

        // POST: SystemCountryCodePoco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var systemCountryCodePoco = await _context.SystemCountryCode.FindAsync(id);
            _context.SystemCountryCode.Remove(systemCountryCodePoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemCountryCodePocoExists(string id)
        {
            return _context.SystemCountryCode.Any(e => e.Code == id);
        }
    }
}
