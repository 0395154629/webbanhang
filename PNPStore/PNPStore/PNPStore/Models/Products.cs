namespace PNPStore.Models
{
    public class Products
    {
        public int Id { get; set; }
        public int IdCate { get; set; }
        public virtual Categorys? Categories { get; set; }
        public string NameProduct { get; set; }
        public string? Color { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public string? CPU { get; set; }
        public string? RAM { get; set; }
        public string? Graphics { get; set; }
        public string? Disk { get; set; }
        public string? description { get; set; }
        public int Quantity { get; set; }
        public DateTime Time { get; set; }
        public string? IdUserUpdate { get; set; }
        public virtual Accounts? Accounts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carts> Carts { get; set; }
    }
}
