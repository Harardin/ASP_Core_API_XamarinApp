using System.ComponentModel.DataAnnotations;

namespace asp_xamar_solution.Models
{
    public class UserDataModel
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Paswword { get; set; }
    }
}
