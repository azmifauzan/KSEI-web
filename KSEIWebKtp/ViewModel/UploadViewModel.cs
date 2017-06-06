using System.ComponentModel.DataAnnotations;

namespace KSEIWebKtp.ViewModel
{
    public class UploadViewModel
    {
        [Display(Name = "File Upload")]
        public string File_Upload { get; set; }
    }
}
