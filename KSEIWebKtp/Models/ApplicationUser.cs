using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KSEIWebKtp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nama { get; set; }
    }
}
