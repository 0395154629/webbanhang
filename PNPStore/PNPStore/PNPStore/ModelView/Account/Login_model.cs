using System.ComponentModel.DataAnnotations;

namespace PNPStore.ModelView.Account
{
    public class Login_model
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
