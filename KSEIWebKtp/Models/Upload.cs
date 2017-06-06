using System;
using System.ComponentModel.DataAnnotations;

namespace KSEIWebKtp.Models
{
    public class Upload
    {
        public int ID { get; set; }
        [Display(Name = "Nama Petugas")]
        public string User_ID { get; set; }
        [Display(Name = "Waktu Upload")]
        public DateTime Tgl_Upload { get; set; }
        [Display(Name = "Nama File")]
        public string File_Upload { get; set; }
        public string File_Saved { get; set; }
    }
}
