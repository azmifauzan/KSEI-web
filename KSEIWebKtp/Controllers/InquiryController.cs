using KSEIWebKtp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using System.Linq;
using System.Threading.Tasks;

namespace KSEIWebKtp.Controllers
{
    [Authorize]
    public class InquiryController : Controller
    {
        private readonly Models.KseiContext _context;

        public InquiryController(Models.KseiContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> All(int page = 1)
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

            var qry = dataktp.OrderByDescending(p => p.Tgl_Upload);
            var model = await PagingList<KSEIWebKtp.ViewModel.InquiryViewModel>.CreateAsync(qry, 20, page);
            model.Action = "All";
            return View(model);
        }

        [HttpPost]
        public IActionResult Filter(InquiryViewModel ivm)
        {
            if (ModelState.IsValid)
            {
                var dataktp =
                    from d in _context.Dataktp
                    join ju in _context.Upload on d.Upload_ID equals ju.ID into bp
                    from c in bp.DefaultIfEmpty()
                    where c.Tgl_Upload >= ivm.Tgl_Upload && c.Tgl_Upload <= ivm.Sd_upload  
                    orderby c.Tgl_Upload descending
                    select new InquiryViewModel
                    {
                        NIK = d.NIK, Nama = d.Nama, Tgl_Upload = c.Tgl_Upload, User_ID = c.User_ID, File_Upload = c.File_Upload,
                        Tempat_lahir = d.Tempat_lahir, Tanggal_lahir = d.Tanggal_lahir, Jk = d.Jk, Alamat = d.Alamat, RtRw = d.RtRw,
                        KelDesa = d.KelDesa, Kecamatan = d.Kecamatan, Agama = d.Agama, Status = d.Status, Pekerjaan = d.Pekerjaan,
                        Kewarganegaraan = d.Kewarganegaraan, Berlaku = d.Berlaku, Provinsi = d.Provinsi, ID = c.ID
                    };

                if (ivm.NIK != null)
                {
                    dataktp = dataktp.Where(d => d.NIK.Contains(ivm.NIK));
                }
                if (ivm.Nama != null)
                {
                    dataktp = dataktp.Where(d => d.Nama.Contains(ivm.Nama));
                }

                if (dataktp != null)
                {
                    return View(dataktp.ToList());
                }
                else
                {
                    ViewBag.msg = "Data Tidak ditemukan !";
                    return View();
                }
            }

            return View("Index",ivm);            
        }
    }

}
