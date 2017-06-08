using KSEIWebKtp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KSEIWebKtp.Controllers
{
    [Authorize]
    public class WebserviceController : Controller
    {
        private IHostingEnvironment _environment;
        private UserManager<ApplicationUser> _userManager;
        private readonly KseiContext _context;

        public WebserviceController(IHostingEnvironment environment, UserManager<ApplicationUser> userManager, KseiContext context)
        {
            _environment = environment;
            _userManager = userManager;
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile files)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "ws");
            var user = await _userManager.GetUserAsync(User);
            var iduser = user.Nama;
            string waktu = String.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
            var flnm = waktu + "_" + user + "_" + files.FileName;
            using (var fileStream = new FileStream(Path.Combine(uploads, flnm), FileMode.Create))
            {
                await files.CopyToAsync(fileStream);
            }

            var client = new HttpClient();

            var ip_address = "";
            var password = "";
            var user_id = "";            
            var fileLines = System.IO.File.ReadAllLines(Path.Combine(uploads, flnm));
            var result = "";

            if (fileLines[0].Equals("NIK"))
            {
                List<Kontenws> listValue = new List<Kontenws>();
                int i = 0;
                foreach (var nik in fileLines)
                {
                    i++;
                    if (i > 1)
                    {
                        if (nik != null)
                        {
                            try
                            {
                                ViewData["NIK"] = nik;
                                string json = "{\"NIK\": \"" + nik + "\"," +
                                 "\"ip_address\": \"" + ip_address + "\"," +
                                  "\"password\": \"" + password + "\"," +
                                  "\"user_id\": \"" + user_id + "\"}";
                                //var js = await client.PostAsync(new Uri("http://172.16.160.25:8000/dukcapil/get_json/KSEI/CALL_NIK"), new StringContent(json, Encoding.UTF8, "application/json"));
                                var js = await client.PostAsync(new Uri("http://localhost/ksei"), new StringContent(json, Encoding.UTF8, "application/json"));
                                result = await js.Content.ReadAsStringAsync();
                                if (result != null)
                                {
                                    Webservice dt = JsonConvert.DeserializeObject<Webservice>(result);
                                    if (dt.content[0].RESPON != null)
                                    {
                                        var dtk = new Kontenws
                                        {
                                            NIK = nik,
                                            RESPON = dt.content[0].RESPON
                                        };
                                        listValue.Add(dtk);
                                    }
                                    else
                                    {
                                        foreach (var dtk in dt.content)
                                        {
                                            dtk.RESPON = "Ditemukan";
                                            listValue.Add(dtk);
                                        }
                                    }
                                }
                                else
                                {
                                    ViewData["msg"] = "Koneksi ke server Dukcapil gagal";
                                    return View("Error");
                                }
                            }
                            catch (Exception)
                            {
                                ViewData["msg"] = "Koneksi ke server Dukcapil gagal";
                                return View("Error");
                            }                           
                        }
                    }
                }

                var iddtws = 0;
                if (listValue != null)
                {
                    //generate file txt
                    var flnmres = "hasilcekws_" + waktu + "_" + iduser+".txt";
                    var fl = System.IO.File.Create(Path.Combine(uploads, flnmres));
                    using (var flwr = new System.IO.StreamWriter(fl))
                    {
                        var konten = "NIK|STATUS|NAMA|TEMPAT_LAHIR|TANGGAL_LAHIR|JENIS_KELAMIN|ALAMAT|RT/RW|KEL/DESA|KECAMATAN|AGAMA|STATUS_PERKAWINAN|PEKERJAAN|PROVINSI|GOL_DARAH";
                        flwr.WriteLine(konten);
                        foreach (var dtlst in listValue)
                        {
                            flwr.WriteLine(
                                dtlst.NIK + "|" +
                                dtlst.RESPON + "|" +
                                dtlst.NAMA_LGKP + "|" +
                                dtlst.TMPT_LHR + "|" +
                                dtlst.TGL_LHR + "|" +
                                dtlst.JENIS_KLMIN + "|" +
                                dtlst.ALAMAT + "|" +
                                dtlst.NO_RT + "/" + dtlst.NO_RW +
                                dtlst.KEL_NAME + "|" +
                                dtlst.KEC_NAME + "|" +
                                dtlst.AGAMA + "|" +
                                dtlst.STATUS_KAWIN + "|" +
                                dtlst.JENIS_PKRJN + "|" +
                                dtlst.PROP_NAME + "|" +
                                dtlst.GOL_DARAH
                            );
                        }                        
                    }

                    //simpan ke db

                    var dtws = new Webservice
                    {
                        FILE_GENERATE = flnmres,
                        FILE_UPLOAD = flnm,
                        PETUGAS_CEK = iduser,
                        TGL_CEK = DateTime.Now                       
                    };

                    _context.Webservice.Add(dtws);
                    await _context.SaveChangesAsync();
                    iddtws = dtws.ID;

                    foreach (var dtls in listValue)
                    {
                        dtls.WebserviceID = iddtws;
                        _context.Kontenws.Add(dtls);
                        await _context.SaveChangesAsync();
                    }
                }

                ViewData["iddtws"] = iddtws;
                return View();
            }
            else
            {
                ViewData["msg"] = "Format File tidak Sesuai";
                return View("Error");
            }
        }

        public IActionResult Unduh(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upload = _context.Webservice
               .Single(m => m.ID == id);
            if (upload == null)
            {
                return NotFound();
            }
            else
            {
                var pth = Path.Combine(_environment.WebRootPath, "ws");
                var flnm = Path.Combine(pth, upload.FILE_GENERATE);
                var fileContentResult = new FileContentResult(System.IO.File.ReadAllBytes(flnm), "text/plain")
                {
                    FileDownloadName = upload.FILE_GENERATE
                };
                return fileContentResult;
            }
        }
    }    
}
