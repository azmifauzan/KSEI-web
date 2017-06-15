using KSEIWebKtp.Models;
using System;

namespace KSEIWebKtp.ViewModel
{
    public class PemadananViewModel
    {
        public int idktp { get; set; }
        public Dataktp dtktp { get; set; }
        public int idws { get; set; }
        public Dataktp dtws { get; set; }
        public DateTime Tgl_Upload { get; set; }
        public int idupload { get; set; }
        public string User_ID { get; set; }
        public string File_Upload { get; set; }
    }
}
