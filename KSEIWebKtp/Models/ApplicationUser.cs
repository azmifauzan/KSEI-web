using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KSEIWebKtp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nama { get; set; }
        public string IP_ADDRESS_WS { get; set; }
        public string PASSWORD_WS { get; set; }
        public string USER_WS { get; set; }
    }
}
