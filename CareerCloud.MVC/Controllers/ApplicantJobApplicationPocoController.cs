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
    public class ApplicantJobApplicationPocoController : Controller
    {
        private readonly CareerCloudContext _context;

        public ApplicantJobApplicationPocoController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: ApplicantJobApplicationPoco
        public async Task<IActionResult> Index(Guid? id)
        {

             
            if (id == null)
                return BadRequest(400);

            //List<ApplicantJobApplicationVM> applicantJobVM = new List<ApplicantJobApplicationVM>();
            var careerCloudContext = _context.ApplicantJobApplication.Where(a => a.Applicant == id).Include(a=>a.CompanyJob);

            if (careerCloudContext.Count() == 0)
                return NotFound(404);
            ViewBag.Applicant = _context.SecurityLogin.Where(a => a.ApplicantProfile.FirstOrDefault().Id == id).FirstOrDefault()
                                     .FullName;


            return View(await careerCloudContext.ToListAsync());
            //return View(applicantJobVM);
        }


        public ActionResult SearchJob(string search)
        {
            var companyJobDescriptions = _context.CompanyJobDescription.ToList();

            List<ApplicantJobApplicationVM> appliedJobVMs = new List<ApplicantJobApplicationVM>();
            foreach (var companyJobDescription in companyJobDescriptions)
            {
                appliedJobVMs.Add(
                    new ApplicantJobApplicationVM
                    {

                        Id = companyJobDescription.Job,
                        JobTitle = companyJobDescription.JobName,
                        JobDescription = companyJobDescription.JobDescriptions
                        // ApplicationDate = applicantJobApplication.ApplicationDate

                    });
                //ViewBag.ApplicantName = applicantJobApplication.ApplicantProfile.SecurityLogin.FullName;
            }
            return View(appliedJobVMs.Where(apvm => apvm.JobTitle.StartsWith(search)));

        }

        // GET: ApplicantJobApplicationPoco/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantJobApplicationPoco = await _context.ApplicantJobApplication
                .Include(a => a.ApplicantProfile)
                .Include(a => a.CompanyJob)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantJobApplicationPoco == null)
            {
                return NotFound();
            }

            return View(applicantJobApplicationPoco);
        }

        // GET: ApplicantJobApplicationPoco/Create
        public IActionResult Create()
        {
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfile, "Id", "Id");
            ViewData["Job"] = new SelectList(_context.CompanyJob, "Id", "Id");
            //var ApplicantJobApplicationVM = new ApplicantJobApplicationVM();
            //ApplicantJobApplicationVM. = DateTime.Now;
            return View();
        }

        // POST: ApplicantJobApplicationPoco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Applicant,Job,ApplicationDate")] ApplicantJobApplicationPoco applicantJobApplicationPoco)
        {
            if (ModelState.IsValid)
            {
                applicantJobApplicationPoco.Id = Guid.NewGuid();
                _context.Add(applicantJobApplicationPoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfile, "Id", "Id", applicantJobApplicationPoco.Applicant);
            ViewData["Job"] = new SelectList(_context.CompanyJob, "Id", "Id", applicantJobApplicationPoco.Job);
            return View(applicantJobApplicationPoco);
        }

        // GET: ApplicantJobApplicationPoco/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantJobApplicationPoco = await _context.ApplicantJobApplication.FindAsync(id);
            if (applicantJobApplicationPoco == null)
            {
                return NotFound();
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfile, "Id", "Id", applicantJobApplicationPoco.Applicant);
            ViewData["Job"] = new SelectList(_context.CompanyJob, "Id", "Id", applicantJobApplicationPoco.Job);
            return View(applicantJobApplicationPoco);
        }

        // POST: ApplicantJobApplicationPoco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Applicant,Job,ApplicationDate")] ApplicantJobApplicationPoco applicantJobApplicationPoco)
        {
            if (id != applicantJobApplicationPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantJobApplicationPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantJobApplicationPocoExists(applicantJobApplicationPoco.Id))
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
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfile, "Id", "Id", applicantJobApplicationPoco.Applicant);
            ViewData["Job"] = new SelectList(_context.CompanyJob, "Id", "Id", applicantJobApplicationPoco.Job);
            return View(applicantJobApplicationPoco);
        }

        // GET: ApplicantJobApplicationPoco/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantJobApplicationPoco = await _context.ApplicantJobApplication
                .Include(a => a.ApplicantProfile)
                .Include(a => a.CompanyJob)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantJobApplicationPoco == null)
            {
                return NotFound();
            }

            return View(applicantJobApplicationPoco);
        }

        // POST: ApplicantJobApplicationPoco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicantJobApplicationPoco = await _context.ApplicantJobApplication.FindAsync(id);
            _context.ApplicantJobApplication.Remove(applicantJobApplicationPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantJobApplicationPocoExists(Guid id)
        {
            return _context.ApplicantJobApplication.Any(e => e.Id == id);
        }
    }
}
