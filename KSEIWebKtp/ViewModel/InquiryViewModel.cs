using System;
using System.ComponentModel.DataAnnotations;

namespace KSEIWebKtp.ViewModel
{
    public class InquiryViewModel
    {
        public int ID { get; set; }
        public string NIK { get; set; }
        public string Nama { get; set; }
        [Required, Display(Name = "Tanggal Upload")]
        public DateTime Tgl_upload { get; set; }
        [Required, Display(Name = "Sampai dengan")]
        public DateTime Sd_upload { get; set; }        
        public string Tempat_lahir { get; set; }
        public string Tanggal_lahir { get; set; }
        public string Alamat { get; set; }
        public string RtRw { get; set; }
        public string KelDesa { get; set; }
        public string Kecamatan { get; set; }
        public string Agama { get; set; }
        public string Pekerjaan { get; set; }
        public string Jk { get; set; }
        public string Status { get; set; }
        public string Kewarganegaraan { get; set; }
        public string Berlaku { get; set; }
        public string Goldarah { get; set; }
        public string Provinsi { get; set; }
        public int Upload_ID { get; set; }
        [Display(Name = "Nama Petugas")]
        public string User_ID { get; set; }
        [Display(Name = "Waktu Upload")]
        public DateTime Tgl_Upload { get; set; }
        [Display(Name = "Nama File")]
        public string File_Upload { get; set; }
        public string File_Saved { get; set; }
    }
}
