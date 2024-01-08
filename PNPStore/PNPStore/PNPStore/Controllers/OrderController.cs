using PNPStore.Interfaces;
using PNPStore.Models;
using PNPStore.ModelView.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static PNPStore.ModelView.Response;

namespace PNPStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder service;
        public OrderController(IOrder order)
        {
            service = order;
        }
        [HttpGet("GetOrderAsync")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderAsync(string? status)
        {
            ResponseAPI<List<OrdersList>> responseAPI = new ResponseAPI<List<OrdersList>>();
            try
            {
                var data = new List<OrdersList>();
                data = await service.GetOrderAsync(status);
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
        [HttpGet("GetOrderAsyncById/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderAsyncById(int Id)
        {
            ResponseAPI<OrdersList> responseAPI = new ResponseAPI<OrdersList>();
            try
            {
                var data = new OrdersList();
                data = await service.GetOrderAsyncById(Id);
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
        [HttpPost("CreateOrder")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateOrder(string IdUser)
        {
            ResponseAPI<OrdersList> responseAPI = new ResponseAPI<OrdersList>();
            try
            {
                responseAPI.Count = 1;
                responseAPI.Message = await service.CreateOrder(IdUser);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }

        }
        [HttpPost("UpdateStatus")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int orderId, string newStatus)
        {
            ResponseAPI<OrdersList> responseAPI = new ResponseAPI<OrdersList>();
            try
            {
                responseAPI.Count = 1;
                responseAPI.Message = await service.UpdateStatus(orderId, newStatus);
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
