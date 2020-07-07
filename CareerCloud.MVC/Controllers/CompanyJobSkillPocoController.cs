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
    public class CompanyJobSkillPocoController : Controller
    {
        private readonly CareerCloudContext _context;

        public CompanyJobSkillPocoController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: CompanyJobSkillPoco
        public async Task<IActionResult> Index()
        {
            var careerCloudContext = _context.CompanyJobSkill.Include(c => c.CompanyJob);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: CompanyJobSkillPoco/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobSkillPoco = await _context.CompanyJobSkill
                .Include(c => c.CompanyJob)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobSkillPoco == null)
            {
                return NotFound();
            }

            return View(companyJobSkillPoco);
        }

        // GET: CompanyJobSkillPoco/Create
        public IActionResult Create(Guid? id)
        {
            //ViewData["Job"] = new SelectList(_context.CompanyJob, "Id", "Id");
            ViewBag.Job = new SelectList(_context.CompanyJob, "Id", "Id");
         //   TempData["job"] = id;
          //  TempData.Keep();
            return View();
        }

        // POST: CompanyJobSkillPoco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Job,Skill,SkillLevel,Importance")] CompanyJobSkillPoco companyJobSkillPoco)
        {
            if (ModelState.IsValid)
            {
                companyJobSkillPoco.Id = Guid.NewGuid();
              //  companyJobSkillPoco.Job = (Guid)TempData["job"];
                _context.Add(companyJobSkillPoco);
                await _context.SaveChangesAsync();
                
              // Guid applicationID = (Guid)TempData["CompanyId"];
                return RedirectToAction(nameof(Index));

                //return RedirectToAction("PostedJob", new { Controller = "CompanyJobPoco", job = applicationID});
            }
            ViewData["Job"] = new SelectList(_context.CompanyJob, "Id", "Id", companyJobSkillPoco.Job);
            return View(companyJobSkillPoco);
        }

        // GET: CompanyJobSkillPoco/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobSkillPoco = await _context.CompanyJobSkill.FindAsync(id);
            if (companyJobSkillPoco == null)
            {
                return NotFound();
            }
            ViewData["Job"] = new SelectList(_context.CompanyJob, "Id", "Id", companyJobSkillPoco.Job);
            return View(companyJobSkillPoco);
        }

        // POST: CompanyJobSkillPoco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Job,Skill,SkillLevel,Importance")] CompanyJobSkillPoco companyJobSkillPoco)
        {
            if (id != companyJobSkillPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyJobSkillPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyJobSkillPocoExists(companyJobSkillPoco.Id))
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
            ViewData["Job"] = new SelectList(_context.CompanyJob, "Id", "Id", companyJobSkillPoco.Job);
            return View(companyJobSkillPoco);
        }

        // GET: CompanyJobSkillPoco/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobSkillPoco = await _context.CompanyJobSkill
                .Include(c => c.CompanyJob)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobSkillPoco == null)
            {
                return NotFound();
            }

            return View(companyJobSkillPoco);
        }

        // POST: CompanyJobSkillPoco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyJobSkillPoco = await _context.CompanyJobSkill.FindAsync(id);
            _context.CompanyJobSkill.Remove(companyJobSkillPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyJobSkillPocoExists(Guid id)
        {
            return _context.CompanyJobSkill.Any(e => e.Id == id);
        }
    }
}
