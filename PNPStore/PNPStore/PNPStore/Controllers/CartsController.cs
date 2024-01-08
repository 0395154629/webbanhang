using PNPStore.Interfaces;
using PNPStore.Models;
using PNPStore.ModelView.Carts;
using Microsoft.AspNetCore.Mvc;
using static PNPStore.ModelView.Response;

namespace PNPStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class CartsController : ControllerBase
    {
        private readonly ICarts service;
        public CartsController(ICarts carts)
        {
            service = carts;
        }

        /// <summary>
        /// Lấy danh sách cart.
        /// </summary>
        /// <param name="Ten_San_Pham"></param>
        /// <param name="Model"></param>
        /// <param name="Ten_Hang_San_Xuat"></param>
        /// <returns> </returns>
        [HttpGet("Get_Product")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get_Product(string IdUser)
        {
            ResponseAPI<List<Carts>> responseAPI = new ResponseAPI<List<Carts>>();
            try
            {
                var data = new List<Carts>();
                data = await service.Get_Carts(IdUser);
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
        /// Thêm sản phẩm vào giỏ hàng
        /// </summary>
        /// <param name="IdUser"></param>
        /// <param name="Model"></param>
        /// <returns> </returns>
        [HttpPost("Create_Carts")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create_Carts(string IdUser, CreateCarts model)
        {
            ResponseAPI<List<Carts>> responseAPI = new ResponseAPI<List<Carts>>();
            try
            {
                var data = new  Carts();
                data = await service.Create_Carts(IdUser, model);
                responseAPI.Data = data;
                responseAPI.Count = 1;
                responseAPI.Message = " Thêm giỏ hàng thành công";
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }

        }
        /// <summary>
        /// Cập nhật sản phẩm vào giỏ hàng.
        /// </summary>
        /// <param name="Model"></param>
        /// <returns> </returns>
        [HttpPut("Update_Carts")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update_Carts(List<UpdateCart> model)
        {
            ResponseAPI<string> responseAPI = new ResponseAPI<string>();
            try
            {
                responseAPI.Count = 1;
                responseAPI.Message = await service.Update_Carts(model);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }

        }
        /// <summary>
        /// Xóa sản phẩm khỏi giỏ hàng.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> </returns>
        [HttpDelete("Delete_Carts/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete_Carts(int id)
        {
            ResponseAPI<string> responseAPI = new ResponseAPI<string>();
            try
            {
                responseAPI.Count = 1;
                responseAPI.Message = await service.Delete_Carts(id);
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
