using PNPStore.Data;
using PNPStore.Interfaces;
using PNPStore.Models;
using PNPStore.ModelView.Orders;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace PNPStore.Services
{
    public class OrderService: IOrder
    {
        private readonly PNPStoreContext db_;
        private readonly ImageService hinhAnh;
        public OrderService(PNPStoreContext _ApiContext, ImageService hinhAnh)
        {
            db_ = _ApiContext;
            this.hinhAnh = hinhAnh;
        }
        public async Task<List<OrdersList>> GetOrderAsync(string? status)
        {
            try
            {
                var data = await db_.Orders
                .Where(a => string.IsNullOrEmpty(status) || a.Status.Equals(status))
                .Select(a => new OrdersList
                {
                    Id = a.Id,
                    IdAccount = a.IdAccount,
                    Accounts = db_.Accounts.Where(n => n.Id.Equals(a.IdAccount)).FirstOrDefault(),
                    Price = a.Price,
                    IdProduct = a.IdProduct,
                    Products = db_.Products.Where(m => m.Id.Equals(a.IdProduct)).FirstOrDefault(),
                    Quantity = a.Quantity,
                    Status = a.Status,
                    TimeCreate = a.TimeCreate,
                    SumPrice = a.Price * a.Quantity
                })
                .OrderBy(a => a.TimeCreate)
                .ToListAsync();
                return data;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<OrdersList> GetOrderAsyncById(int Id)
        {
            try
            {
                var data = await db_.Orders
                .Where(a => a.Id == Id)
                .Select(a => new OrdersList
                {
                    Id = a.Id,
                    IdAccount = a.IdAccount,
                    Accounts = db_.Accounts.Where(n => n.Id.Equals(a.IdAccount)).FirstOrDefault(),
                    Price = a.Price,
                    IdProduct = a.IdProduct,
                    Products = db_.Products.Where(m => m.Id.Equals(a.IdProduct)).FirstOrDefault(),
                    Quantity = a.Quantity,
                    Status = a.Status,
                    TimeCreate = a.TimeCreate,
                    SumPrice = a.Price * a.Quantity
                }).FirstOrDefaultAsync();
                return data;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<string> CreateOrder(string IdUser)
        {
            try
            {
                var dataCart = db_.Carts.Where(a => a.IdAccount == IdUser).ToList();
                foreach (var item in dataCart)
                {
                    var data = new Orders()
                    {
                        IdAccount = item.IdAccount,
                        Price = item.Price,
                        IdProduct = item.IdProduct,
                        Quantity = item.Quantity,
                        Status = Status.Pending,
                        TimeCreate = item.TimeCreate,
                    };
                    db_.Orders.Add(data);
                }
                db_.SaveChanges();
                return "Tạo đơn hàng thành công";
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<string> UpdateStatus(int orderId, string newStatus)
        {
            try
            {

                var orderToUpdate = await db_.Orders.FindAsync(orderId);

                if (orderToUpdate != null)
                {
                    if (newStatus == Status.Cancelled.ToString()&& orderToUpdate.Status== Status.Pending)
                    {
                        orderToUpdate.Status = Status.Cancelled;
                        db_.Orders.Update(orderToUpdate);
                    }
                    else
                    {
                        if (newStatus == Status.Processing.ToString() && orderToUpdate.Status != Status.Delivered && orderToUpdate.Status != Status.Cancelled)
                        {
                            orderToUpdate.Status = Status.Processing;
                            db_.Orders.Update(orderToUpdate);
                        }
                        else if (newStatus == Status.Delivered.ToString() && orderToUpdate.Status != Status.Cancelled)
                        {
                            orderToUpdate.Status = Status.Delivered;
                            db_.Orders.Update(orderToUpdate);
                        } 
                    }
                    await db_.SaveChangesAsync();
                    return "Cập nhật thành công";
                }
                else
                {
                    throw new Exception("Không tìm thấy hóa đơn");
                }               
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }
}
