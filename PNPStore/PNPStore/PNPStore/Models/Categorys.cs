namespace PNPStore.Models
{
    public class Categorys
    {
        public int Id { get; set; }
        public string NameCategory { get; set; }
        public string? Images { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Products> Products { get; set; }
    }
}
