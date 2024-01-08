namespace PNPStore.Models
{
    public enum Status
    {
        Pending,
        Processing,
        Delivered,
        Cancelled
    }
    public class Orders
    {
        public int Id { get; set; }
        public string IdAccount { get; set; }
        public virtual Accounts? Accounts { get; set; }
        public int IdProduct { get; set; }
        public virtual Products? Products { get; set; }
        public DateTime TimeCreate { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Status Status { get; set; }
    }
}
