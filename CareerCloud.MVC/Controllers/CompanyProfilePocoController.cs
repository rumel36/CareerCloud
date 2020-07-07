using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.MVC.ViewModels;

namespace CareerCloud.MVC.Controllers
{
    public class CompanyProfilePocoController : Controller
    {
        private readonly CareerCloudContext _context;

        public CompanyProfilePocoController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: CompanyProfilePoco
        public async Task<IActionResult> Index()
        {
          
            var companyProfiles = _context.CompanyProfile.Include(a => a.CompanyDescription).Include(a => a.CompanyJob).ToList();
            List<CompanyProfileVM> cpvm = new List<CompanyProfileVM>();
            foreach (var companyProfile in companyProfiles)
            {
                cpvm.Add(new CompanyProfileVM
                {
                    Id = companyProfile.Id,
                    CompanyName = companyProfile.CompanyDescription.Where(cd => cd.LanguageId.Trim() == "EN").FirstOrDefault(cd => cd.Company == companyProfile.Id)?.CompanyName,
                    CompanyWebsite = companyProfile.CompanyWebsite,
                    ContactPhone = companyProfile.ContactPhone,
                    ContactName = companyProfile.ContactName


                });
            }

            return View(cpvm);

            // return View(await careerCloudContext.ToListAsync());
            //return View(companyProfiles);
        }

        // GET: CompanyProfilePoco/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyProfilePoco = await _context.CompanyProfile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyProfilePoco == null)
            {
                return NotFound();
            }

            return View(companyProfilePoco);
        }

        // GET: CompanyProfilePoco/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanyProfilePoco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegistrationDate,CompanyWebsite,ContactPhone,ContactName,CompanyLogo")] CompanyProfilePoco companyProfilePoco)
        {
            if (ModelState.IsValid)
            {
                companyProfilePoco.Id = Guid.NewGuid();
                _context.Add(companyProfilePoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companyProfilePoco);
        }

        // GET: CompanyProfilePoco/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyProfilePoco = await _context.CompanyProfile.FindAsync(id);
            if (companyProfilePoco == null)
            {
                return NotFound();
            }
            return View(companyProfilePoco);
        }

        // POST: CompanyProfilePoco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,RegistrationDate,CompanyWebsite,ContactPhone,ContactName,CompanyLogo")] CompanyProfilePoco companyProfilePoco)
        {
            if (id != companyProfilePoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyProfilePoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyProfilePocoExists(companyProfilePoco.Id))
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
            return View(companyProfilePoco);
        }

        // GET: CompanyProfilePoco/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyProfilePoco = await _context.CompanyProfile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyProfilePoco == null)
            {
                return NotFound();
            }

            return View(companyProfilePoco);
        }

        // POST: CompanyProfilePoco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyProfilePoco = await _context.CompanyProfile.FindAsync(id);
            _context.CompanyProfile.Remove(companyProfilePoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyProfilePocoExists(Guid id)
        {
            return _context.CompanyProfile.Any(e => e.Id == id);
        }
    }
}
