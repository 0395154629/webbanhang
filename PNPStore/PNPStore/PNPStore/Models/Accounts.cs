using Microsoft.AspNetCore.Identity;

namespace PNPStore.Models
{
    public class Accounts : IdentityUser
    {
        public string Fullname { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Distrist { get; set; }
        public string? Ward { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
        public DateTime DateCreate { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carts> Carts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
