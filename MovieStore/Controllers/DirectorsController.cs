using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieStore.Data;
using MovieStore.Models;

namespace MovieStore.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _UserManager;

        public DirectorsController(ApplicationDbContext context, UserManager<IdentityUser> UserManager)
        {
            _context = context;
            _UserManager = UserManager;
        }

        // GET: Directors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Director.Include(d => d.Genre).Include(d => d.IdentityUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Directors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Director
                .Include(d => d.Genre)
                .Include(d => d.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // GET: Directors/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name");
            ViewData["Created_By"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: Directors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,GenreId")] Director director)
        {
            if (ModelState.IsValid)
            {
                director.Created_Date = DateTime.Now;
                var identityUser = await _UserManager.GetUserAsync(User);
                director.Created_By = identityUser.Id;

                _context.Add(director);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name", director.GenreId);
            ViewData["Created_By"] = new SelectList(_context.Users, "Id", "Name", director.Created_By);
            return View(director);
        }

        // GET: Directors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Director.FindAsync(id);
            if (director == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name", director.GenreId);
            ViewData["Created_By"] = new SelectList(_context.Users, "Id", "Name", director.Created_By);
            return View(director);
        }

        // POST: Directors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,GenreId,Created_Date,Created_By,Updated_Date")] Director director)
        {
            if (id != director.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var edt = await _context.Director.FindAsync(id);
                    edt.Updated_Date = DateTime.Now;
                    var identityUser = await _UserManager.GetUserAsync(User);
                    edt.Created_By = identityUser.Id;
                    edt.GenreId = director.GenreId;

                    _context.Update(edt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorExists(director.Id))
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
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name", director.GenreId);
            ViewData["Created_By"] = new SelectList(_context.Users, "Id", "Name", director.Created_By);
            return View(director);
        }

        // GET: Directors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Director
                .Include(d => d.Genre)
                .Include(d => d.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // POST: Directors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var director = await _context.Director.FindAsync(id);
            _context.Director.Remove(director);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectorExists(int id)
        {
            return _context.Director.Any(e => e.Id == id);
        }
    }
}
