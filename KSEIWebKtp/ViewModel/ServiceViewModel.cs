using System.ComponentModel.DataAnnotations;

namespace KSEIWebKtp.ViewModel
{
    public class ServiceViewModel
    {
        [Required, Display(Name = "File yang akan diupload")]
        public string File_Upload { get; set; }
    }
}
