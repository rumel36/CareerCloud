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
    public class CompanyDescriptionPocoController : Controller
    {
        private readonly CareerCloudContext _context;

        public CompanyDescriptionPocoController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: CompanyDescriptionPoco
        public async Task<IActionResult> Index(Guid? id)
        {

            if (id == null)
                return BadRequest(400);

            var careerCloudContext = _context.CompanyDescription.Where(a => a.Company == id).Include(a => a.CompanyProfile).Include(a => a.SystemLanguageCode).Where(cd => cd.LanguageId.Trim() == "EN");
            if (careerCloudContext.Count() == 0)
                return NotFound(404);



            return View(await careerCloudContext.ToListAsync());

          //  var careerCloudContext = _context.CompanyDescription.Include(c => c.CompanyProfile).Include(c => c.SystemLanguageCode);
           // return View(await careerCloudContext.ToListAsync());
        }

        // GET: CompanyDescriptionPoco/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyDescriptionPoco = await _context.CompanyDescription
                .Include(c => c.CompanyProfile)
                .Include(c => c.SystemLanguageCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyDescriptionPoco == null)
            {
                return NotFound();
            }

            return View(companyDescriptionPoco);
        }

        // GET: CompanyDescriptionPoco/Create
        public IActionResult Create()
        {
            ViewData["Company"] = new SelectList(_context.CompanyProfile, "Id", "Id");
            ViewData["LanguageId"] = new SelectList(_context.SystemLanguageCode, "LanguageID", "LanguageID");
            return View();
        }

        // POST: CompanyDescriptionPoco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Company,LanguageId,CompanyName,CompanyDescription")] CompanyDescriptionPoco companyDescriptionPoco)
        {
            if (ModelState.IsValid)
            {
                companyDescriptionPoco.Id = Guid.NewGuid();
                _context.Add(companyDescriptionPoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Company"] = new SelectList(_context.CompanyProfile, "Id", "Id", companyDescriptionPoco.Company);
            ViewData["LanguageId"] = new SelectList(_context.SystemLanguageCode, "LanguageID", "LanguageID", companyDescriptionPoco.LanguageId);
            return View(companyDescriptionPoco);
        }

        // GET: CompanyDescriptionPoco/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyDescriptionPoco = await _context.CompanyDescription.FindAsync(id);
            if (companyDescriptionPoco == null)
            {
                return NotFound();
            }
            ViewData["Company"] = new SelectList(_context.CompanyProfile, "Id", "Id", companyDescriptionPoco.Company);
            ViewData["LanguageId"] = new SelectList(_context.SystemLanguageCode, "LanguageID", "LanguageID", companyDescriptionPoco.LanguageId);
            return View(companyDescriptionPoco);
        }

        // POST: CompanyDescriptionPoco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Company,LanguageId,CompanyName,CompanyDescription")] CompanyDescriptionPoco companyDescriptionPoco)
        {
            if (id != companyDescriptionPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyDescriptionPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyDescriptionPocoExists(companyDescriptionPoco.Id))
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
            ViewData["Company"] = new SelectList(_context.CompanyProfile, "Id", "Id", companyDescriptionPoco.Company);
            ViewData["LanguageId"] = new SelectList(_context.SystemLanguageCode, "LanguageID", "LanguageID", companyDescriptionPoco.LanguageId);
            return View(companyDescriptionPoco);
        }

        // GET: CompanyDescriptionPoco/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyDescriptionPoco = await _context.CompanyDescription
                .Include(c => c.CompanyProfile)
                .Include(c => c.SystemLanguageCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyDescriptionPoco == null)
            {
                return NotFound();
            }

            return View(companyDescriptionPoco);
        }

        // POST: CompanyDescriptionPoco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyDescriptionPoco = await _context.CompanyDescription.FindAsync(id);
            _context.CompanyDescription.Remove(companyDescriptionPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyDescriptionPocoExists(Guid id)
        {
            return _context.CompanyDescription.Any(e => e.Id == id);
        }
    }
}
