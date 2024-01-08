
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Security.Policy;
using PNPStore.Interfaces;
using PNPStore.Models;
using PNPStore.Utilities;
using PNPStore.ModelView.Categorys;
using PNPStore.Data;

namespace PNPStore.Services
{
    public class CategoryService : ICategory
    {
        private readonly PNPStoreContext db_;
        private readonly ImageService hinhAnh;
        public CategoryService(PNPStoreContext _ApiContext, ImageService hinhAnh)
        {
            db_ = _ApiContext;
            this.hinhAnh = hinhAnh;
        }
        public async Task<List<Categorys>> Get_Categorys(int? id, string? nameCate)
        {
            try
            {
                var data = db_.Categorys.OrderBy(a => a.NameCategory).ToList();
                if (id != null)
                {
                    data = data.Where(a => a.Id == id).ToList();
                }
                if (nameCate != null && nameCate != "")
                {
                    data = data.Where(a => a.NameCategory.ToUpper().Contains(nameCate!.ToUpper())).ToList();
                }

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn" + ex.Message);
            }
        }
        public async Task<Categorys> Create_Category(CreateCategory model)
        {
            try
            {
                string url = "https://localhost:44388/images/";
                var checkName = db_.Categorys.Where(a => a.NameCategory.ToUpper() == model.NameCategory.ToUpper()).ToList();
                if (checkName.Count() > 0)
                {
                    throw new Exception("Tên loại sản phẩm tồn tại");
                }
                string nameImage = "";
                if (model.Image != null)
                {
                    nameImage = FormatString.RemoveAccents(model.NameCategory).ToLower().Replace(" ", "");
                    bool IsHinhAnh = await hinhAnh.UploadImage(model.Image!, nameImage);
                    if (IsHinhAnh== false) {
                        throw new Exception("Thêm hình thất bại");
                    }
                }
                var data = new Categorys()
                {
                    NameCategory = model.NameCategory,
                    Images = model.Image == null ? null : url + nameImage + ".png",
                };
                db_.Categorys.Add(data);
                await db_.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn " + ex.Message);
            }
        }
        public async Task<Categorys> Update_Category(UpdateCategory model, int Id)
        {
            try
            {
                string url = "https://localhost:44388/images/";
                string nameImage = "";
                if (model.Images != null)
                {
                    nameImage = FormatString.RemoveAccents(model.NameCategory).ToLower().Replace(" ", "");
                    bool IsHinhAnh = await hinhAnh.UploadImage(model.Images!, nameImage);
                }
                var data = db_.Categorys.Where(a => a.Id == Id).FirstOrDefault();
                data.NameCategory = model.NameCategory;
                data.Images = model.Images == null ? null : url + nameImage + ".png";
                db_.Entry(data).State = EntityState.Modified;
                await db_.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn " + ex.Message);
            }
        }
        public async Task<string> Delete_Category(int Id)
        {
            try
            {
                var sanpham = db_.Products.Where(a => a.IdCate == Id).ToList();
                if (sanpham.Count() > 0)
                {
                    throw new Exception("Không thể xóa loại sản phẩm này. Đang tồn tại sản phẩm");
                }
                var data = db_.Categorys.Where(a => a.Id == Id).FirstOrDefault();
                if(data != null)
                {
                    db_.Categorys.Remove(data!);
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
