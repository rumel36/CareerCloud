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
    public class ApplicantProfilePocoesController : Controller
    {
        private readonly CareerCloudContext _context;

        public ApplicantProfilePocoesController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: ApplicantProfilePocoes
        public async Task<IActionResult> Index()
        {
            List<ApplicantProfileVM> applicantProfiles = new List<ApplicantProfileVM>();

         //   if (id == null)
         //       return BadRequest(400);

            var careerCloudContext = _context.ApplicantProfile.Include(a => a.SecurityLogin).Include(a => a.SystemCountryCode);
            foreach(ApplicantProfilePoco applicant in careerCloudContext){
                applicantProfiles.Add(new ApplicantProfileVM()
                {
                    Id = applicant.Id,
                    Name = applicant.SecurityLogin.FullName,
                    ApplicantProfile = applicant
                }) ;
            
            }
            return View(applicantProfiles);
        }

        // GET: ApplicantProfilePocoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantProfilePoco = await _context.ApplicantProfile
                .Include(a => a.SecurityLogin)
                .Include(a => a.SystemCountryCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantProfilePoco == null)
            {
                return NotFound();
            }

            return View(applicantProfilePoco);
        }

        // GET: ApplicantProfilePocoes/Create
        public IActionResult Create()
        {
            ViewData["Login"] = new SelectList(_context.SecurityLogin, "Id", "Id");
            ViewData["Country"] = new SelectList(_context.SystemCountryCode, "Code", "Code");
            return View();
        }

        // POST: ApplicantProfilePocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,CurrentSalary,CurrentRate,Currency,Country,Province,Street,City,PostalCode")] ApplicantProfilePoco applicantProfilePoco)
        {
            if (ModelState.IsValid)
            {
                applicantProfilePoco.Id = Guid.NewGuid();
                _context.Add(applicantProfilePoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Login"] = new SelectList(_context.SecurityLogin, "Id", "Id", applicantProfilePoco.Login);
            ViewData["Country"] = new SelectList(_context.SystemCountryCode, "Code", "Code", applicantProfilePoco.Country);
            return View(applicantProfilePoco);
        }

        // GET: ApplicantProfilePocoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantProfilePoco = await _context.ApplicantProfile.FindAsync(id);
            if (applicantProfilePoco == null)
            {
                return NotFound();
            }
            ViewData["Login"] = new SelectList(_context.SecurityLogin, "Id", "Id", applicantProfilePoco.Login);
            ViewData["Country"] = new SelectList(_context.SystemCountryCode, "Code", "Code", applicantProfilePoco.Country);
            return View(applicantProfilePoco);
        }

        // POST: ApplicantProfilePocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Login,CurrentSalary,CurrentRate,Currency,Country,Province,Street,City,PostalCode")] ApplicantProfilePoco applicantProfilePoco)
        {
            if (id != applicantProfilePoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantProfilePoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantProfilePocoExists(applicantProfilePoco.Id))
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
            ViewData["Login"] = new SelectList(_context.SecurityLogin, "Id", "Id", applicantProfilePoco.Login);
            ViewData["Country"] = new SelectList(_context.SystemCountryCode, "Code", "Code", applicantProfilePoco.Country);
            return View(applicantProfilePoco);
        }

        // GET: ApplicantProfilePocoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantProfilePoco = await _context.ApplicantProfile
                .Include(a => a.SecurityLogin)
                .Include(a => a.SystemCountryCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantProfilePoco == null)
            {
                return NotFound();
            }

            return View(applicantProfilePoco);
        }

        // POST: ApplicantProfilePocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            var applicantProfilePoco = await _context.ApplicantProfile.FindAsync(id);
            _context.ApplicantProfile.Remove(applicantProfilePoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantProfilePocoExists(Guid id)
        {
            return _context.ApplicantProfile.Any(e => e.Id == id);
        }

    }
}
