namespace PNPStore.Models
{
    public class Carts
    {
        public int Id { get; set; }
        public string IdAccount { get; set; }
        public virtual Accounts? Account { get; set; }
        public int IdProduct { get; set; }
        public virtual Products? Products { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public DateTime TimeCreate { get; set; }
        public bool Status { get; set; }

    }
}
