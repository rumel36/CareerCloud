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
    public class CompanyJobEducationPocoController : Controller
    {
        private readonly CareerCloudContext _context;

        public CompanyJobEducationPocoController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: CompanyJobEducationPoco
        public async Task<IActionResult> Index()
        {
            var careerCloudContext = _context.CompanyJobEducation.Include(c => c.CompanyJob);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: CompanyJobEducationPoco/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobEducationPoco = await _context.CompanyJobEducation
                .Include(c => c.CompanyJob)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobEducationPoco == null)
            {
                return NotFound();
            }

            return View(companyJobEducationPoco);
        }

        // GET: CompanyJobEducationPoco/Create
        public IActionResult Create(Guid? id)
        {
            //   ViewData["Job"] = new SelectList(_context.CompanyJob, "Id", "Id");

            // ViewData["Job"] = new SelectList(_context.CompanyJob, "Id", "Id");
            ViewBag.Job = new SelectList(_context.CompanyJob, "Id", "Id");
           // TempData["job"] = id;
            //TempData.Keep();
            return View();
        }

        // POST: CompanyJobEducationPoco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Job,Major,Importance")] CompanyJobEducationPoco companyJobEducationPoco)
        {
            if (ModelState.IsValid)
            {
                companyJobEducationPoco.Id = Guid.NewGuid();
                //companyJobEducationPoco.Job = (Guid)TempData["job"];
                _context.Add(companyJobEducationPoco);
                await _context.SaveChangesAsync();
               // TempData.Keep();
                return RedirectToAction("Create", new { Controller = "CompanyJobSkillPoco", job = companyJobEducationPoco.Job });
            }
            ViewData["Job"] = new SelectList(_context.CompanyJob, "Id", "Id", companyJobEducationPoco.Job);
            return View(companyJobEducationPoco);
        }

        // GET: CompanyJobEducationPoco/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobEducationPoco = await _context.CompanyJobEducation.FindAsync(id);
            if (companyJobEducationPoco == null)
            {
                return NotFound();
            }
            ViewData["Job"] = new SelectList(_context.CompanyJob, "Id", "Id", companyJobEducationPoco.Job);
            return View(companyJobEducationPoco);
        }

        // POST: CompanyJobEducationPoco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Job,Major,Importance")] CompanyJobEducationPoco companyJobEducationPoco)
        {
            if (id != companyJobEducationPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyJobEducationPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyJobEducationPocoExists(companyJobEducationPoco.Id))
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
            ViewData["Job"] = new SelectList(_context.CompanyJob, "Id", "Id", companyJobEducationPoco.Job);
            return View(companyJobEducationPoco);
        }

        // GET: CompanyJobEducationPoco/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobEducationPoco = await _context.CompanyJobEducation
                .Include(c => c.CompanyJob)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobEducationPoco == null)
            {
                return NotFound();
            }

            return View(companyJobEducationPoco);
        }

        // POST: CompanyJobEducationPoco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyJobEducationPoco = await _context.CompanyJobEducation.FindAsync(id);
            _context.CompanyJobEducation.Remove(companyJobEducationPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyJobEducationPocoExists(Guid id)
        {
            return _context.CompanyJobEducation.Any(e => e.Id == id);
        }
    }
}
