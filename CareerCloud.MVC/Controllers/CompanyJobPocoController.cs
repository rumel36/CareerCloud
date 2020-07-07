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
    public class CompanyJobPocoController : Controller
    {
        private readonly CareerCloudContext _context;

        public CompanyJobPocoController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: CompanyJobPoco
        public async Task<IActionResult> Index(Guid? id)
        {

            List<CreateJobVM> companyJobsVM = new List<CreateJobVM>();
           
            if (id == null)
                return BadRequest();
           
            var careerCloudContext = _context.CompanyJob.Where(a => a.Company == id).Include(a => a.CompanyJobDescription);
            foreach (CompanyJobPoco companyJob in careerCloudContext)
            {
                companyJobsVM.Add( new CreateJobVM()
                {
                    CompanyId = companyJob.Id,
                    JobTitle = companyJob.CompanyJobDescription.FirstOrDefault().JobName,
                    ProfileCreated = companyJob.ProfileCreated,
                    IsInactive = companyJob.IsInactive,
                    IsCompanyHidden = companyJob.IsCompanyHidden
    
                });
            }
            //response = Client.GetAsync(GetApiUriString($"company/v1/profile/{companyId}")).Result;
            ViewBag.CompanyName =   _context.CompanyDescription.Where(a=>a.Company == id)
                                             .Where(cd => cd.LanguageId.Trim() == "EN")
                                            .FirstOrDefault()
                                            .CompanyName;
            if (careerCloudContext.Count() == 0)
                return NotFound();



            return View(companyJobsVM);
            //var careerCloudContext = _context.CompanyJob.Include(c => c.CompanyProfile);
            //return View(await careerCloudContext.ToListAsync());

            

        }

        // GET: CompanyJobPoco/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobPoco = await _context.CompanyJob
                .Include(c => c.CompanyProfile)
                .Include(c=>c.CompanyJobDescription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobPoco == null)
            {
                return NotFound();
            }

            return View(companyJobPoco);
        }
        public ActionResult PostedJob(Guid id)
        {
            var companyJobs = _context.CompanyJob.Include(c => c.CompanyJobDescription).Where(cj => cj.Company == id).ToList();
            // var companyJobs = db.CompanyProfiles.Include(cp => cp.CompanyJobs).Where(cp => cp.Id == id).ToList();
            ViewBag.CompanyName = _context.CompanyProfile.Include(Cp => Cp.CompanyDescription)
                .SingleOrDefault(cp => cp.Id == id).CompanyDescription.Where(cd => cd.Company == id)
                .FirstOrDefault()?.CompanyName;
            TempData["CompanyId"] = id;
            List<JobPostedVM> jobPostedVMs = new List<JobPostedVM>();
            if (companyJobs == null)
            {
                return View(jobPostedVMs);
            }

            foreach (var companyJob in companyJobs)
            {
                jobPostedVMs.Add(
                    new JobPostedVM
                    {
                        JobId = companyJob.Id,
                        JobTitle = companyJob.CompanyJobDescription.FirstOrDefault().JobName,
                        JobDescription = companyJob.CompanyJobDescription.FirstOrDefault().JobDescriptions,
                        JobCreated = companyJob.ProfileCreated
                    });

            }

            return View(jobPostedVMs);
        }
        // GET: CompanyJobPoco/Create
        public IActionResult Create(Guid? id )
        {
           // ViewData["Company"] = new SelectList(_context.CompanyProfile, "Id", "Id");
            //ViewBag.CompanyName = _context.CompanyDescription.Where(a => a.Company == id)
            //                             .FirstOrDefault()
            //                             .CompanyName;
            TempData["CompanyId"] = id;
            TempData.Keep();
            
            //TempData.Keep();
            var createJobVM = new CreateJobVM();
            createJobVM.ProfileCreated = DateTime.Now;
            return View(createJobVM);
        }

        // POST: CompanyJobPoco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Company,ProfileCreated,IsInactive,IsCompanyHidden")] CompanyJobPoco companyJobPoco)
        public async Task<IActionResult> Create([Bind("Id,Company,ProfileCreated,IsInactive,IsCompanyHidden")] CompanyJobPoco companyJobPoco, [Bind("Id,Job,JobName,JobDescriptions")]CompanyJobDescriptionPoco companyJobDescriptionPoco)
     
        {
            if (ModelState.IsValid)
            {
            
                companyJobPoco.Id = Guid.NewGuid();
                companyJobPoco.Company = (Guid)TempData["CompanyId"];
                companyJobDescriptionPoco.Id = Guid.NewGuid();
                companyJobDescriptionPoco.Job = companyJobPoco.Id;
                TempData["CompanyId"] = companyJobPoco.Company;
                TempData.Keep();

                _context.Add(companyJobPoco);
                _context.Add(companyJobDescriptionPoco);
                
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", new { Controller = "CompanyJobEducationPoco", job = companyJobPoco.Id });
            }
            ViewData["Company"] = new SelectList(_context.CompanyProfile, "Id", "CompanyWebsite", companyJobPoco.Company);
            return View(companyJobPoco);

        }

        // GET: CompanyJobPoco/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobPoco = await _context.CompanyJob.FindAsync(id);
            if (companyJobPoco == null)
            {
                return NotFound();
            }
            ViewData["Company"] = new SelectList(_context.CompanyProfile, "Id", "Id", companyJobPoco.Company);
            return View(companyJobPoco);
        }

        // POST: CompanyJobPoco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Company,ProfileCreated,IsInactive,IsCompanyHidden")] CompanyJobPoco companyJobPoco)
        {
            if (id != companyJobPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyJobPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyJobPocoExists(companyJobPoco.Id))
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
            ViewData["Company"] = new SelectList(_context.CompanyProfile, "Id", "Id", companyJobPoco.Company);
            return View(companyJobPoco);
        }

        // GET: CompanyJobPoco/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobPoco = await _context.CompanyJob
                .Include(c => c.CompanyProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobPoco == null)
            {
                return NotFound();
            }

            return View(companyJobPoco);
        }

        // POST: CompanyJobPoco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyJobPoco = await _context.CompanyJob.FindAsync(id);
            _context.CompanyJob.Remove(companyJobPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyJobPocoExists(Guid id)
        {
            return _context.CompanyJob.Any(e => e.Id == id);
        }
    }
}
