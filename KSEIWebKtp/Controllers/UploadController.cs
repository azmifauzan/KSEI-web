using KSEIWebKtp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KSEIWebKtp.Controllers
{
    [Authorize]

    public class UploadController : Controller
    {
        private readonly Models.KseiContext _context;
        private IHostingEnvironment _environment;
        private UserManager<ApplicationUser> _userManager;

        public UploadController(Models.KseiContext context, IHostingEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
        }

        // GET: Upload
        public async Task<IActionResult> Index(int page = 1)
        {
            var qry = _context.Upload.AsNoTracking().OrderBy(p => p.Tgl_Upload);
            var model = await PagingList<Upload>.CreateAsync(qry, 20, page);
            return View(model);
            //return View(await _context.Upload.ToListAsync());
        }

        // GET: Upload/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upload = await _context.Upload
                .SingleOrDefaultAsync(m => m.ID == id);
            if (upload == null)
            {
                return NotFound();
            }

            return View(upload);
        }

        public IActionResult Unduh(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upload = _context.Upload
                .Single(m => m.ID == id);
            if (upload == null)
            {
                return NotFound();
            }
            else
            {
                var pth = Path.Combine(_environment.WebRootPath, "uploads");
                var flnm = Path.Combine(pth, upload.File_Saved);
                var fileContentResult = new FileContentResult(System.IO.File.ReadAllBytes(flnm), "text/plain")
                {
                    FileDownloadName = upload.File_Upload
                };
                return fileContentResult;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUp(ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            var user = await _userManager.GetUserAsync(User);
            var iduser = user.Nama;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string waktu = String.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
                    var flnm = waktu + "_" + iduser + "_" + file.FileName;

                    using (var fileStream = new FileStream(Path.Combine(uploads, flnm), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    var optionsBuilder = new DbContextOptionsBuilder<KseiContext>();
                    optionsBuilder.UseSqlite("Filename=kseiweb.db");
                    
                    var upl = new Upload { File_Upload = file.FileName, Tgl_Upload = DateTime.Now, User_ID = iduser, File_Saved = flnm };
                    _context.Upload.Add(upl);
                    _context.SaveChanges();                        
                    var id = upl.ID;

                    var fileLines = System.IO.File.ReadAllLines(Path.Combine(uploads, flnm));
                    foreach (var sln in fileLines)
                    {
                        string[] spl = sln.Split('|');
                        var dtktp = new Dataktp
                        {
                            NIK = spl[0],
                            Nama = spl[1],
                            Tempat_lahir = spl[2],
                            Tanggal_lahir = spl[3],
                            Jk = spl[4],
                            Alamat = spl[5],
                            RtRw = spl[6],
                            KelDesa = spl[7],
                            Kecamatan = spl[8],
                            Agama = spl[9],
                            Status = spl[10],
                            Pekerjaan = spl[11],
                            Kewarganegaraan = spl[12],
                            Berlaku = spl[13],
                            Provinsi = spl[14],
                            Goldarah = spl[15],
                            Upload_ID = id
                        };
                        _context.Dataktp.Add(dtktp);
                        _context.SaveChanges();
                    }                    
                }
            }
            return RedirectToAction("Index","Upload");
        }

        // GET: Upload/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Upload/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,User_ID,Tgl_Upload,File_Upload")] Upload upload)
        {
            if (ModelState.IsValid)
            {
                _context.Add(upload);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(upload);
        }

        // GET: Upload/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upload = await _context.Upload.SingleOrDefaultAsync(m => m.ID == id);
            if (upload == null)
            {
                return NotFound();
            }
            return View(upload);
        }

        // POST: Upload/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,User_ID,Tgl_Upload,File_Upload")] Upload upload)
        {
            if (id != upload.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(upload);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UploadExists(upload.ID))
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
            return View(upload);
        }

        // GET: Upload/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upload = await _context.Upload
                .SingleOrDefaultAsync(m => m.ID == id);
            if (upload == null)
            {
                return NotFound();
            }

            return View(upload);
        }

        // POST: Upload/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var upload = await _context.Upload.SingleOrDefaultAsync(m => m.ID == id);
            _context.Upload.Remove(upload);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UploadExists(int id)
        {
            return _context.Upload.Any(e => e.ID == id);
        }
    }
}
