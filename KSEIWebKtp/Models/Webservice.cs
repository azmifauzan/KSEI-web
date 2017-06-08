using System;
using System.Collections.Generic;

namespace KSEIWebKtp.Models
{
    public class Webservice
    {
        public int ID { get; set; }
        public string FILE_UPLOAD { get; set; }
        public string FILE_GENERATE { get; set; }
        public DateTime TGL_CEK { get; set; }
        public string PETUGAS_CEK { get; set; }
        public List<Kontenws> content { get; set; }
    }
}
