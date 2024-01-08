using Microsoft.AspNetCore.Mvc;
using static PNPStore.ModelView.Response;
using PNPStore.Interfaces;
using PNPStore.Models;
using PNPStore.ModelView.Product;

namespace PNPStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _IProduct;
        public ProductController(IProduct Product)
        {
            _IProduct = Product;
        }

        /// <summary>
        /// Lấy danh sách sản phẩm.
        /// </summary>
        /// <param name="Ten_San_Pham"></param>
        /// <param name="Model"></param>
        /// <param name="Ten_Hang_San_Xuat"></param>
        /// <returns> </returns>
        [HttpGet("Get_Product")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GGet_Product(string? NameProduct, int? IdCate, string? CPU, string? RAM, string? Graphics, string? Disk)
        {
            ResponseAPI<List<Products>> responseAPI = new ResponseAPI<List<Products>>();
            try
            {
                var data = new List<Products>();
                data = await _IProduct.Get_Product(NameProduct, IdCate, CPU, RAM, Graphics, Disk);
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
        /// Lấy danh sách sản phẩm theo id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns> </returns>
        [HttpGet("Get_ProductById/{Id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get_ProductById(int Id)
        {
            ResponseAPI<Products> responseAPI = new ResponseAPI<Products>();
            try
            {
                var data = new Products();
                data = await _IProduct.Get_ProductById(Id);
                responseAPI.Data = data;
                responseAPI.Count = 1;
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
        /// Tạo sản phẩm
        /// </summary>
        /// <param name="model"></param>
        /// <returns> </returns>
        [HttpPost("Create_Product")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create_Product([FromForm] Product model)
        {
            ResponseAPI<Products> responseAPI = new ResponseAPI<Products>();
            try
            {
                var data = new Products();
                data = await _IProduct.Create_Product(model);
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
        /// cập nhật danh sách sản phẩm
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Id"></param>
        /// <returns> </returns>
        [HttpPut("Update_Product/{Id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update_Product([FromForm] Product model, int Id)
        {
            ResponseAPI<Products> responseAPI = new ResponseAPI<Products>();
            try
            {
                var data = new Products();
                data = await _IProduct.Update_Product(model, Id);
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
        /// Xóa sản phẩm
        /// </summary>
        /// <param name="Id"></param>
        /// <returns> </returns>
        [HttpDelete("Delete_Product/{Id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete_Product(int Id)
        {
            ResponseAPI<string> responseAPI = new ResponseAPI<string>();
            try
            {
                var data = new Categorys();
                responseAPI.Count = 1;
                responseAPI.Message = await _IProduct.Delete_Product(Id);
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
