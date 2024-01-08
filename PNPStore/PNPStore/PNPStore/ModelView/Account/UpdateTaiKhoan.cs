using System.ComponentModel.DataAnnotations;

namespace PNPStore.ModelView.Account
{
    public class UpdateTaiKhoan
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Phone]
        [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Distrist { get; set; }
        public string? Ward { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
    }
}
