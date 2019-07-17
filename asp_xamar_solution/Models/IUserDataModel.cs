using System.Linq;

namespace asp_xamar_solution.Models
{
    public interface IUserDataModel
    {
        IQueryable<UserDataModel> QUserData { get; }
    }
}
