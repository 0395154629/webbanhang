

using PNPStore.Models;
using PNPStore.ModelView.Account;

namespace PNPStore.Interfaces
{
    public interface IAccount
    {
        Task<List<Accounts>> Get_Account(string? name, string? role);
        Task<JsonLogin> login(Login_model model);
        Task<string> Register(RegisterModel model);
        Task<string> ChangePassword(string Username, string newPass);

    }
}
