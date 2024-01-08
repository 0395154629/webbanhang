using PNPStore.Models;
using PNPStore.ModelView.Product;

namespace PNPStore.Interfaces
{
    public interface IProduct
    {
        Task<List<Products>> Get_Product(string? NameProduct, int? IdCate, string? CPU, string? RAM, string? Graphics, string? Disk);
        Task<Products> Get_ProductById(int Id);
        Task<Products> Create_Product(Product model);
        Task<Products> Update_Product(Product model, int Id);
        Task<string> Delete_Product(int Id);
    }
}
