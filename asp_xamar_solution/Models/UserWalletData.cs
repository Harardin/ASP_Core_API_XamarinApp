using System.ComponentModel.DataAnnotations;

namespace asp_xamar_solution.Models
{
    public class UserWalletData
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public decimal Coins { get; set; }
    }
}
