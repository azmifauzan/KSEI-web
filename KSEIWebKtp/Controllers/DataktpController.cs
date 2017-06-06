using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KSEIWebKtp.Models;

namespace KSEIWebKtp.Controllers
{
    public class DataktpController : Controller
    {
        private readonly Models.KseiContext _context;

        public DataktpController(Models.KseiContext context)
        {
            _context = context;    
        }

        // GET: Dataktps
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dataktp.ToListAsync());
        }

        // GET: Dataktps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataktp = await _context.Dataktp
                .SingleOrDefaultAsync(m => m.ID == id);
            if (dataktp == null)
            {
                return NotFound();
            }

            return View(dataktp);
        }

        // GET: Dataktps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dataktps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NIK,Nama,Ttl,Alamat,RtRw,KelDesa,Kecamatan,Agama,Pekerjaan,Jk,Status,Kewarganegaraan,Berlaku,Goldarah")] Dataktp dataktp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataktp);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dataktp);
        }

        // GET: Dataktps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataktp = await _context.Dataktp.SingleOrDefaultAsync(m => m.ID == id);
            if (dataktp == null)
            {
                return NotFound();
            }
            return View(dataktp);
        }

        // POST: Dataktps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NIK,Nama,Ttl,Alamat,RtRw,KelDesa,Kecamatan,Agama,Pekerjaan,Jk,Status,Kewarganegaraan,Berlaku,Goldarah")] Dataktp dataktp)
        {
            if (id != dataktp.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataktp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataktpExists(dataktp.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(dataktp);
        }

        // GET: Dataktps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataktp = await _context.Dataktp
                .SingleOrDefaultAsync(m => m.ID == id);
            if (dataktp == null)
            {
                return NotFound();
            }

            return View(dataktp);
        }

        // POST: Dataktps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataktp = await _context.Dataktp.SingleOrDefaultAsync(m => m.ID == id);
            _context.Dataktp.Remove(dataktp);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DataktpExists(int id)
        {
            return _context.Dataktp.Any(e => e.ID == id);
        }
    }
}
