using System.ComponentModel.DataAnnotations;

namespace asp_xamar_solution.Models
{
    public class LoginModel
    {
        [Required (ErrorMessage = "This field is requaired")]
        public string Email { get; set; }
        [Required (ErrorMessage = "This field is requaired")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public string LoginError { get; set; }
    }
}
