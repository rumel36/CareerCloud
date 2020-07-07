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
    public class SecurityLoginPocoController : Controller
    {
        private readonly CareerCloudContext _context;

        public SecurityLoginPocoController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: SecurityLoginPoco
        public async Task<IActionResult> Index()
        {
            return View(await _context.SecurityLogin.ToListAsync());
        }

        // GET: SecurityLoginPoco/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginPoco = await _context.SecurityLogin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityLoginPoco == null)
            {
                return NotFound();
            }

            return View(securityLoginPoco);
        }

        // GET: SecurityLoginPoco/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SecurityLoginPoco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,Password,Created,PasswordUpdate,AgreementAccepted,IsLocked,IsInactive,EmailAddress,PhoneNumber,FullName,ForceChangePassword,PrefferredLanguage")] SecurityLoginPoco securityLoginPoco)
        {
            if (ModelState.IsValid)
            {
                securityLoginPoco.Id = Guid.NewGuid();
                _context.Add(securityLoginPoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(securityLoginPoco);
        }

        // GET: SecurityLoginPoco/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginPoco = await _context.SecurityLogin.FindAsync(id);
            if (securityLoginPoco == null)
            {
                return NotFound();
            }
            return View(securityLoginPoco);
        }

        // POST: SecurityLoginPoco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Login,Password,Created,PasswordUpdate,AgreementAccepted,IsLocked,IsInactive,EmailAddress,PhoneNumber,FullName,ForceChangePassword,PrefferredLanguage")] SecurityLoginPoco securityLoginPoco)
        {
            if (id != securityLoginPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(securityLoginPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecurityLoginPocoExists(securityLoginPoco.Id))
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
            return View(securityLoginPoco);
        }

        // GET: SecurityLoginPoco/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginPoco = await _context.SecurityLogin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityLoginPoco == null)
            {
                return NotFound();
            }

            return View(securityLoginPoco);
        }

        // POST: SecurityLoginPoco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var securityLoginPoco = await _context.SecurityLogin.FindAsync(id);
            _context.SecurityLogin.Remove(securityLoginPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecurityLoginPocoExists(Guid id)
        {
            return _context.SecurityLogin.Any(e => e.Id == id);
        }
    }
}
