using PNPStore.Models;
using PNPStore.ModelView.Carts;

namespace PNPStore.Interfaces
{
    public interface ICarts
    {
        Task<List<Carts>> Get_Carts(string IdUser);
        Task<Carts> Create_Carts(string IdUser, CreateCarts model);
        Task<string> Update_Carts(List<UpdateCart> model);
        Task<string> Delete_Carts(int id);
    }
}
