using System.Linq;

namespace asp_xamar_solution.Models
{
    public interface IUserWalletData
    {
        IQueryable<UserWalletData> QUserWalletData { get; }
    }
}
