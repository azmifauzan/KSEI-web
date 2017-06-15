﻿using KSEIWebKtp.Models;
using KSEIWebKtp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReflectionIT.Mvc.Paging;
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
    public class PemadananController : Controller
    {
        private readonly KseiContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IHostingEnvironment _environment;

        public PemadananController(KseiContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(string filter, int page = 1, string sortExpression = "Tgl_Upload")
        {
            var dataktp =
                    from d in _context.Dataktp
                    join ju in _context.Upload on d.Upload_ID equals ju.ID into bp
                    from c in bp.DefaultIfEmpty()
                    select new InquiryViewModel
                    {
                        NIK = d.NIK,
                        Nama = d.Nama,
                        Tgl_Upload = c.Tgl_Upload,
                        User_ID = c.User_ID,
                        File_Upload = c.File_Upload,
                        Tempat_lahir = d.Tempat_lahir,
                        Tanggal_lahir = d.Tanggal_lahir,
                        Jk = d.Jk,
                        Alamat = d.Alamat,
                        RtRw = d.RtRw,
                        KelDesa = d.KelDesa,
                        Kecamatan = d.Kecamatan,
                        Agama = d.Agama,
                        Status = d.Status,
                        Pekerjaan = d.Pekerjaan,
                        Kewarganegaraan = d.Kewarganegaraan,
                        Berlaku = d.Berlaku,
                        Provinsi = d.Provinsi,
                        ID = c.ID
                    };

            if (!string.IsNullOrWhiteSpace(filter))
            {
                dataktp = dataktp.Where(p => p.NIK.Contains(filter) || p.Nama.Contains(filter) || p.User_ID.Contains(filter)
                || p.Alamat.Contains(filter) || p.Tempat_lahir.Contains(filter) || p.Tanggal_lahir.Contains(filter)
                || p.Kecamatan.Contains(filter) || p.KelDesa.Contains(filter) || p.Pekerjaan.Contains(filter)
                || p.Provinsi.Contains(filter));
            }

            var qry = dataktp.OrderByDescending(p => p.Tgl_Upload);
            var model = await PagingList<InquiryViewModel>.CreateAsync(qry, 20, page, sortExpression, "Tgl_Upload");
            model.Action = "Index";
            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Cek()
        {
            var pilih = HttpContext.Request.Form["Pilih"].ToArray();
            var user = await _userManager.GetUserAsync(User);
            var ip_address = user.IP_ADDRESS_WS;
            var password = user.PASSWORD_WS;
            var user_id = user.USER_WS;
            var client = new HttpClient();
            List<PemadananViewModel> listValue = new List<PemadananViewModel>();

            foreach (var id in pilih)
            {
                var did = Convert.ToInt32(id);
                var dataktp = await _context.Dataktp
                                .SingleOrDefaultAsync(m => m.ID == did);
                var nik = dataktp.NIK;
                var upload = await _context.Upload
                                .SingleOrDefaultAsync(m => m.ID == dataktp.Upload_ID);
                try
                {
                    string json = "{\"NIK\": \"" + nik + "\"," +
                                         "\"ip_address\": \"" + ip_address + "\"," +
                                          "\"password\": \"" + password + "\"," +
                                          "\"user_id\": \"" + user_id + "\"}";
                    //var js = await client.PostAsync(new Uri("http://172.16.160.25:8000/dukcapil/get_json/KSEI/CALL_NIK"), new StringContent(json, Encoding.UTF8, "application/json"));
                    var js = await client.PostAsync(new Uri("http://localhost/ksei"), new StringContent(json, Encoding.UTF8, "application/json"));
                    var result = await js.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        Webservice dt = JsonConvert.DeserializeObject<Webservice>(result);                        
                        if (dt.content[0].RESPON != null)
                        {
                            var dtk = new PemadananViewModel
                            {
                                File_Upload = upload.File_Upload,
                                Tgl_Upload = upload.Tgl_Upload,
                                User_ID = upload.User_ID,
                                dtws = null,
                                idktp = dataktp.ID,
                                dtktp = dataktp,
                                idupload = upload.ID,
                                idws = 0
                            };
                            listValue.Add(dtk);
                        }
                        else
                        {
                            var ws = new Webservice
                            {
                                PETUGAS_CEK = user_id,
                                TGL_CEK = DateTime.Now
                            };

                            _context.Webservice.Add(ws);
                            await _context.SaveChangesAsync();
                            var iddtws = ws.ID;

                            Kontenws dbws = dt.content[0];
                            dbws.WebserviceID = iddtws;
                            _context.Kontenws.Add(dbws);
                            await _context.SaveChangesAsync();

                            var dtws = new Dataktp
                            {
                                NIK = dt.content[0].NIK,
                                Nama = dt.content[0].NAMA_LGKP,
                                Alamat = dt.content[0].ALAMAT,
                                Agama = dt.content[0].AGAMA,
                                RtRw = dt.content[0].NO_RT + "/" + dt.content[0].NO_RW,
                                KelDesa = dt.content[0].KEL_NAME,
                                Kecamatan = dt.content[0].KEC_NAME,
                                Status = dt.content[0].STATUS_KAWIN,
                                Pekerjaan = dt.content[0].JENIS_PKRJN,
                                Tempat_lahir = dt.content[0].TMPT_LHR,
                                Tanggal_lahir = dt.content[0].TGL_LHR,
                                Jk = dt.content[0].JENIS_KLMIN,
                                Goldarah = dt.content[0].GOL_DARAH,
                                Provinsi = dt.content[0].PROP_NAME
                            };
                            var dtk = new PemadananViewModel
                            {
                                File_Upload = upload.File_Upload,
                                Tgl_Upload = upload.Tgl_Upload,
                                User_ID = upload.User_ID,
                                idupload = upload.ID,
                                idktp = dataktp.ID,
                                dtktp = dataktp,
                                dtws = dtws,
                                idws = dbws.ID
                            };
                            listValue.Add(dtk);
                        }
                    }
                }
                catch (Exception)
                {
                    ViewData["msg"] = "Koneksi ke server Dukcapil gagal";
                    return View("Error");
                }
            }

            return View(listValue);
        }

        public async Task<IActionResult> Download()
        {
            var ext = HttpContext.Request.Form["ext"];
            string idkw = HttpContext.Request.Form["idkw"];
            var uploads = Path.Combine(_environment.WebRootPath, "ws");
            var user = await _userManager.GetUserAsync(User);
            var iduser = user.Nama;
            string waktu = String.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
            var flnmres = "pemadanan_" + waktu + "_" + iduser + ".txt";
            var fl = System.IO.File.Create(Path.Combine(uploads, flnmres));

            var dtid = idkw.Split('|');
            var msg = "";
            if (ext.Equals("txt"))
            {                
                using (var flwr = new StreamWriter(fl))
                {
                    var konten = "DATA-KTP{NIK|NAMA|TEMPAT_LAHIR|TANGGAL_LAHIR|JENIS_KELAMIN|ALAMAT|RT/RW|KEL/DESA|" +
                        "KECAMATAN|AGAMA|STATUS_PERKAWINAN|PEKERJAAN|PROVINSI|GOL_DARAH} DATA-WS{NIK|NAMA|TEMPAT_LAHIR|" +
                        "TANGGAL_LAHIR|JENIS_KELAMIN|ALAMAT|RT/RW|KEL/DESA| KECAMATAN|AGAMA|STATUS_PERKAWINAN|PEKERJAAN|" +
                        "PROVINSI|GOL_DARAH}";
                    flwr.WriteLine(konten);
                    var jumdt = dtid.Count();
                    int i = 1;
                    foreach (var idgab in dtid)
                    {
                        if (i < jumdt)
                        {
                            var idktp = idgab.Split(',');
                            //msg += idktp[0] + ";";
                            var dtktp = _context.Dataktp.Single(d => d.ID == Convert.ToInt32(idktp[0]));
                            Kontenws dtws;
                            var konten2 = "";
                            if (idktp[1].Equals("0"))
                            {
                                dtws = null;
                                konten2 = "DATA-KTP{" + dtktp.NIK + "|" + dtktp.Nama + "|" + dtktp.Tempat_lahir + "|" +
                                    dtktp.Tanggal_lahir + "|" + dtktp.Jk + "|" + dtktp.Alamat + "|" + dtktp.RtRw + "|" +
                                    dtktp.KelDesa + "|" + dtktp.Kecamatan + "|" + dtktp.Agama + "|" + dtktp.Status + "|" +
                                    dtktp.Pekerjaan + "|" + dtktp.Provinsi + "|" + dtktp.Goldarah + "|" + "} DATA-WS{Data tidak ditemukan}";
                            }
                            else
                            {
                                dtws = _context.Kontenws.Single(d => d.ID == Convert.ToInt32(idktp[1]));
                                konten2 = "DATA-KTP{" + dtktp.NIK + "|" + dtktp.Nama + "|" + dtktp.Tempat_lahir + "|" +
                                    dtktp.Tanggal_lahir + "|" + dtktp.Jk + "|" + dtktp.Alamat + "|" + dtktp.RtRw + "|" +
                                    dtktp.KelDesa + "|" + dtktp.Kecamatan + "|" + dtktp.Agama + "|" + dtktp.Status + "|" +
                                    dtktp.Pekerjaan + "|" + dtktp.Provinsi + "|" + dtktp.Goldarah + "|" + "} DATA-WS{" +
                                    dtws.NIK + "|" + dtws.NAMA_LGKP + "|" + dtws.TMPT_LHR + "|" + dtws.TGL_LHR + "|" +
                                    dtws.JENIS_KLMIN + "|" + dtws.ALAMAT + "|" + dtws.NO_RT + "/" + dtws.NO_RW + "|" + dtws.KEL_NAME +
                                    "|" + dtws.KEC_NAME + "|" + dtws.AGAMA + "|" + dtws.STATUS_KAWIN + "|" + dtws.JENIS_PKRJN + "|" +
                                    dtws.PROP_NAME + "|" + dtws.GOL_DARAH + "}";
                            }
                            flwr.WriteLine(konten2);
                            //var dtws = _context.Kontenws.Single(d => d.ID == Convert.ToInt32(idktp[1]));                            
                        }

                        i++;
                    }
                }

                var bukafile = Path.Combine(uploads, flnmres);
                var fileContentResult = new FileContentResult(System.IO.File.ReadAllBytes(bukafile), "text/plain")
                {
                    FileDownloadName = flnmres
                };
                return fileContentResult;
                //ViewData["msg"] = msg;
                //return View();
            }
            else
            {
                ViewData["msg"] = ext;
                return View();
            }
            
        }
    }
}
