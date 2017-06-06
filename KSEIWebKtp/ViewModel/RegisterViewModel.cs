using System.ComponentModel.DataAnnotations;

namespace KSEIWebKtp.ViewModel
{
    public class RegisterViewModel
    {
        [Required,EmailAddress,MaxLength(256), Display(Name = "Alamat Email")]
        public string Email { get; set; }
        [Required,MinLength(6),MaxLength(25),DataType(DataType.Password),Display(Name ="Password")]
        public string Password { get; set; }
        [Required, MinLength(6), MaxLength(50), DataType(DataType.Password), Display(Name = "Konfirmasi Password")]
        [Compare("Password", ErrorMessage="Konfirmasi password tidak sesuai !")]
        public string ConfirmPassword { get; set; }
        [Required, Display(Name = "Nama")]
        public string Nama { get; set; }
    }
}
