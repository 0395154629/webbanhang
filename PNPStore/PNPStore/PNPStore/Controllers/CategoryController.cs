using PNPStore.Interfaces;
using PNPStore.Models;
using PNPStore.ModelView.Categorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static PNPStore.ModelView.Response;

namespace PNPStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory service;
        public CategoryController(ICategory Category)
        {
            service = Category;
        }
        /// <summary>
        /// Lấy danh sách loại sản phẩm.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Ten_Loai"></param>
        /// <returns> </returns>
        [HttpGet("Get_Categorys")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get_Categorys(int? id, string? Ten_Loai)
        {
            ResponseAPI<List<Categorys>> responseAPI = new ResponseAPI<List<Categorys>>();
            try
            {
                var data = new List<Categorys>();
                data = await service.Get_Categorys(id, Ten_Loai);
                responseAPI.Data = data;
                responseAPI.Count = data.Count;
                responseAPI.Message = "Lấy danh sách thành công";
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }

        }
        /// <summary>
        /// Tạo danh sách loại sản phẩm
        /// </summary>
        /// <param name="Ten_Loai_San_Pham"></param>
        /// <param name="Hinh_Anh"></param>
        /// <returns> </returns>
        [HttpPost("Create_Category")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create_Category([FromForm] CreateCategory model)
        {
            ResponseAPI<Categorys> responseAPI = new ResponseAPI<Categorys>();
            try
            {
                var data = new Categorys();
                data = await service.Create_Category(model);
                responseAPI.Data = data;
                responseAPI.Count = 1;
                responseAPI.Message = "Tạo thành công";
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }

        }
        /// <summary>
        /// cập nhật danh sách loại sản phẩm
        /// </summary>
        /// <param name="Ten_Loai_San_Pham"></param>
        /// <param name="Hinh_Anh"></param>
        /// <returns> </returns>
        [HttpPut("Update_Category/{Id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update_Category([FromForm] UpdateCategory model, int Id)
        {
            ResponseAPI<Categorys> responseAPI = new ResponseAPI<Categorys>();
            try
            {
                var data = new Categorys();
                data = await service.Update_Category(model, Id);
                responseAPI.Data = data;
                responseAPI.Count = 1;
                responseAPI.Message = "Cập nhật thành công";
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }

        }
        /// <summary>
        /// cập nhật danh sách loại sản phẩm
        /// </summary>
        /// <param name="Id"></param>
        /// <returns> </returns>
        [HttpDelete("Delete_Category/{Id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete_Category(int Id)
        {
            ResponseAPI<string> responseAPI = new ResponseAPI<string>();
            try
            {
                var data = new Categorys();
                responseAPI.Count = 1;
                responseAPI.Message = await service.Delete_Category(Id);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }

        }
    }
}
