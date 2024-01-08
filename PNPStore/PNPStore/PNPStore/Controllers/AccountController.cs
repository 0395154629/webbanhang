using PNPStore.Interfaces;
using PNPStore.Models;
using PNPStore.ModelView.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static PNPStore.ModelView.Response;

namespace PNPStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount service;
        public AccountController(IAccount account)
        {
            service = account;
        }
        /// <summary>
        /// Lấy danh sách cart.
        /// </summary>
        /// <param name="Ten_San_Pham"></param>
        /// <param name="Model"></param>
        /// <param name="Ten_Hang_San_Xuat"></param>
        /// <returns> </returns>
        [HttpGet("Get_Account")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get_Account(string? name, string? role)
        {
            ResponseAPI<List<Accounts>> responseAPI = new ResponseAPI<List<Accounts>>();
            try
            {
                var data = new List<Accounts>();
                data = await service.Get_Account(name, role);
                responseAPI.Data = data;
                responseAPI.Count = data.Count();
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
        /// Lấy danh sách cart.
        /// </summary>
        /// <param name="Ten_San_Pham"></param>
        /// <param name="Model"></param>
        /// <param name="Ten_Hang_San_Xuat"></param>
        /// <returns> </returns>
        [HttpPost("login")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> login(Login_model model)
        {
            ResponseAPI<JsonLogin> responseAPI = new ResponseAPI<JsonLogin>();
            try
            {
                var data = new JsonLogin();
                data = await service.login(model);
                responseAPI.Data = data;
                responseAPI.Count = 1;
                responseAPI.Message = "Đăng nhập thành công";
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }

        }
        /// <summary>
        /// Lấy danh sách cart.
        /// </summary>
        /// <param name="Ten_San_Pham"></param>
        /// <param name="Model"></param>
        /// <param name="Ten_Hang_San_Xuat"></param>
        /// <returns> </returns>
        [HttpPost("Register")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            ResponseAPI<string> responseAPI = new ResponseAPI<string>();
            try
            {
                responseAPI.Count = 1;
                responseAPI.Message = await service.Register(model);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }

        }
        /// <summary>
        /// Lấy danh sách cart.
        /// </summary>
        /// <param name="Ten_San_Pham"></param>
        /// <param name="Model"></param>
        /// <param name="Ten_Hang_San_Xuat"></param>
        /// <returns> </returns>
        [HttpPost("ChangePassword")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangePassword(string Username, string newPass)
        {
            ResponseAPI<string> responseAPI = new ResponseAPI<string>();
            try
            {
                responseAPI.Count = 1;
                responseAPI.Message = await service.ChangePassword(Username, newPass);
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
