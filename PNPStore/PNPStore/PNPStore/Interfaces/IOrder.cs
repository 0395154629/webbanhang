using PNPStore.ModelView.Orders;

namespace PNPStore.Interfaces
{
    public interface IOrder
    {
        Task<List<OrdersList>> GetOrderAsync(string? status);
        Task<OrdersList> GetOrderAsyncById(int Id);
        Task<string> CreateOrder(string IdUser);
        Task<string> UpdateStatus(int orderId, string newStatus);
    }
}
