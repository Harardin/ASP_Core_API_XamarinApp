using System.ComponentModel.DataAnnotations;

namespace asp_xamar_solution.Models.NonQuaryableModels
{
    public class RegDataInput
    {
        [Required(ErrorMessage = "This field is requaired")]
        public string UserName { get; set; }
        public string UserNameLable = "Enter your name";

        [Required(ErrorMessage = "This field is requaired")]
        public string Email { get; set; }
        public string EmailLable = "Enter your Email";

        [Required(ErrorMessage = "This field is requaired")]
        public string Password { get; set; }
        public string PasswordLable = "Enter Password";

        [Required(ErrorMessage = "This field is requaired")]
        [Compare("Password", ErrorMessage ="Passwords do not match")]
        public string ConfPassword { get; set; }
        public string ConfPasswordLable = " Confirm Password";
    }
}
