using PNPStore.Models;
using PNPStore.ModelView.Categorys;

namespace PNPStore.Interfaces
{
    public interface ICategory
    {
        Task<List<Categorys>> Get_Categorys(int? id, string? Ten_Loai);
        Task<Categorys> Create_Category(CreateCategory model);
        Task<Categorys> Update_Category(UpdateCategory model, int Id);
        Task<string> Delete_Category(int Id);
    }
}
