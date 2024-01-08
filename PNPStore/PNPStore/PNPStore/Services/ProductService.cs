using PNPStore.Interfaces;
using PNPStore.Models;
using PNPStore.ModelView.Product;
using PNPStore.Utilities;
using PNPStore.Data;
using Microsoft.EntityFrameworkCore;

namespace PNPStore.Services
{
    public class ProductService : IProduct
    {
        private readonly PNPStoreContext db_;
        private readonly ImageService hinhAnh;
        public ProductService(PNPStoreContext _ApiContext, ImageService hinhAnh)
        {
            db_ = _ApiContext;
            this.hinhAnh = hinhAnh;
        }
        public async Task<List<Products>> Get_Product(string? NameProduct, int? IdCate,string?CPU, string? RAM, string? Graphics, string? Disk)
        {
            try
            {
                var data = db_.Products.Where(a =>
                (NameProduct == null || NameProduct == "" || a.NameProduct.ToUpper().Contains(NameProduct.ToUpper()))&&
                (IdCate == null || IdCate == 0 || a.IdCate==IdCate)&&
                (CPU == null || CPU == "" || a.CPU.ToUpper().Contains(CPU.ToUpper())) &&
                (RAM == null || RAM == "" || a.RAM.ToUpper().Contains(RAM.ToUpper())) &&
                (Graphics == null || Graphics == "" || a.Graphics.ToUpper().Contains(Graphics.ToUpper())) &&
                (Disk == null || Disk == "" || a.Disk.ToUpper().Contains(Disk.ToUpper())) 
                ).Select(a => new Products
                {
                    Id = a.Id,
                    NameProduct = a.NameProduct,
                    IdCate = a.IdCate,
                    Categories = db_.Categorys.Where(v => v.Id == a.IdCate).FirstOrDefault(),
                    Color=a.Color,
                    Price =a.Price,
                    Image = a.Image,
                    CPU = a.CPU,
                    RAM = a.RAM,
                    Graphics = a.Graphics,
                    Disk = a.Disk,
                    description = a.description,
                    Quantity = a.Quantity,
                    Time = a.Time,
                    IdUserUpdate = a.IdUserUpdate,
                    Accounts = db_.Accounts.Where(z=>z.Id == a.IdUserUpdate).FirstOrDefault(),
                }).OrderBy(a => a.IdCate).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn" + ex.Message);
            }
        }
        public async Task<Products> Get_ProductById(int Id)
        {
            try
            {
                var data = db_.Products.Where(a => a.Id == Id).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn" + ex.Message);
            }
        }
        public async Task<Products> Create_Product(Product model)
        {
            try
            {
                string url = "https://localhost:44388/images/";
                var checkName = db_.Products.Where(a => a.NameProduct.ToUpper() == model.NameProduct.ToUpper()).ToList();
                if (checkName.Count() > 0)
                {
                    throw new Exception("Tên loại sản phẩm tồn tại");
                }
                string nameImage = "";
                if (model.Image != null)
                {
                    nameImage = FormatString.RemoveAccents(model.NameProduct).ToLower().Replace(" ", "");
                    bool IsHinhAnh = await hinhAnh.UploadImage(model.Image!, nameImage);
                }
                var data = new Products()
                {
                    NameProduct = model.NameProduct,
                    IdCate = model.IdCate,
                    Color = model.Color,
                    Price = model.Price,
                    CPU = model.CPU,
                    RAM = model.RAM,
                    Graphics = model.Graphics,
                    Disk = model.Disk,
                    description = model.description,
                    Quantity = model.Quantity,
                    Time = DateTime.UtcNow.AddHours(7),
                    Image = model.Image == null ? null : url + nameImage + ".png",
                };
                db_.Products.Add(data);
                await db_.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn " + ex.Message);
            }
        }
        public async Task<Products> Update_Product(Product model, int Id)
        {
            try
            {
                string url = "https://localhost:44388/images/";
                string nameImage = "";
                if (model.Image != null)
                {
                    nameImage = FormatString.RemoveAccents(model.NameProduct).ToLower().Replace(" ", "");
                    bool IsHinhAnh = await hinhAnh.UploadImage(model.Image!, nameImage);
                    if (IsHinhAnh== false)
                    {
                        throw new Exception("Lỗi lưu hình");
                    }
                }
                var data = db_.Products.Where(a => a.Id == Id).FirstOrDefault();
                data.NameProduct = model.NameProduct;
                data.IdCate = model.IdCate;
                data.Color = model.Color;
                data.Price = model.Price;
                data.CPU = model.CPU;
                data.RAM = model.RAM;
                data.Graphics = model.Graphics;
                data.Disk = model.Disk;
                data.description = model.description;
                data.Quantity = model.Quantity;
                data.Time = DateTime.UtcNow.AddHours(7);
                data.Image = model.Image == null ? null : url + nameImage + ".png";
                db_.Entry(data).State = EntityState.Modified;
                await db_.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn " + ex.Message);
            }
        }
        public async Task<string> Delete_Product(int Id)
        {
            try
            {
                var Product = db_.Carts.Where(a => a.IdProduct == Id).ToList();
                if (Product.Count() > 0)
                {
                    throw new Exception("Không thể xóa loại sản phẩm này. Giỏ hàng đang tồn tại sản phẩm");
                }
                var Hoa_Don = db_.Orders.Where(a => a.IdProduct == Id).ToList();
                if (Hoa_Don.Count() > 0)
                {
                    throw new Exception("Không thể xóa loại sản phẩm này. Hóa đơn đang tồn tại sản phẩm");
                }
                var data = db_.Products.Where(a => a.Id == Id).FirstOrDefault();
                if (data != null)
                {
                    db_.Products.Remove(data!);
                    await db_.SaveChangesAsync();
                }
                return "Xóa thành công";
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn " + ex.Message);
            }
        }
    }
}
