using System.ComponentModel.DataAnnotations;

namespace KSEIWebKtp.ViewModel
{
    public class ConfigViewModel
    {
        [Required, Display(Name = "IP Address")]
        public string IP_ADDRESS_WS { get; set; }
        [Required, Display(Name = "Password")]
        public string PASSWORD_WS { get; set; }
        [Required, Display(Name = "User ID")]
        public string USER_WS { get; set; }
    }
}
