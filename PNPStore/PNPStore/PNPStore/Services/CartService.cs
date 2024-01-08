using PNPStore.Models;
using PNPStore.Data;
using PNPStore.Interfaces;
using PNPStore.ModelView.Carts;
using Microsoft.EntityFrameworkCore;

namespace PNPStore.Services
{
    public class CartService:ICarts
    {
        private readonly PNPStoreContext db_;
        private readonly ImageService hinhAnh;
        public CartService(PNPStoreContext _ApiContext, ImageService hinhAnh)
        {
            db_ = _ApiContext;
            this.hinhAnh = hinhAnh;
        }
        public async Task<List<Carts>> Get_Carts(string IdUser)
        {
            try
            {
                var data = db_.Carts.Where(a => a.IdAccount == IdUser).Select(a => new Carts
                {
                    Id = a.Id,
                    IdAccount = a.IdAccount,
                    Account = db_.Accounts.Where(z => z.Id == a.IdAccount).FirstOrDefault(),
                    IdProduct = a.IdProduct,
                    Products = db_.Products.Where(t => t.Id == a.IdProduct).FirstOrDefault(),
                    Quantity = a.Quantity,
                    Price = a.Price,
                    TimeCreate = DateTime.UtcNow.AddHours(7),
                    Status = true,
                }).OrderBy(a => a.TimeCreate).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn" + ex.Message);
            }
        }
        public async Task<Carts> Create_Carts(string IdUser,CreateCarts model)
        {
            try
            {
                if (model.Quantity < 0)
                {
                    throw new Exception("Số lượng không bé hơn 0");
                }
                if (model.Price < 0)
                {
                    throw new Exception("Số lượng không bé hơn 0");
                }
                var data = new Carts()
                {
                    IdAccount = IdUser,
                    IdProduct = model.IdProduct,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    Status = true,
                    TimeCreate = DateTime.UtcNow.AddHours(7),
                };
                db_.Carts.Add(data);
                db_.SaveChanges();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn" + ex.Message);
            }
        }
        public async Task<string> Update_Carts(List<UpdateCart> model)
        {
            try
            {
                foreach (var item in model)
                {
                    if (item.Quantity < 0)
                    {
                        throw new Exception("Số lượng không bé hơn 0");
                    }
                    if (item.Price < 0)
                    {
                        throw new Exception("Số lượng không bé hơn 0");
                    }
                    var data = db_.Carts.Where(a=>a.Id == item.IdCart).FirstOrDefault();
                    data.Price = item.Price;
                    data.Quantity = item.Quantity;
                    data.TimeCreate = DateTime.UtcNow.AddHours(7);
                    db_.Entry(data).State = EntityState.Modified;
                }
                await db_.SaveChangesAsync();
                return "Thành công";
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn" + ex.Message);
            }
        }
        public async Task<string> Delete_Carts(int id)
        {
            try
            {
                var data = db_.Carts.Where(a => a.Id == id).FirstOrDefault();
                db_.Carts.Remove(data);
                await db_.SaveChangesAsync();
                return "Thành công";
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn" + ex.Message);
            }
        }


    }
}
