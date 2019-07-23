namespace XamarinAPISolution.Models
{
    public class LoginDataInput
    {
        public string Email { get; set; }
        public string Password { get; set; }
        // false while testing
        public bool RememberMe = false; // Always false we using an Access Token instead of a cookies
    }
}
