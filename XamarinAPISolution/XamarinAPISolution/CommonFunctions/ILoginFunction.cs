using System.Threading.Tasks;
using XamarinAPISolution.Models;

namespace XamarinAPISolution.CommonFunctions
{
    public interface ILoginFunction
    {
        Task<bool> Login(LoginDataInput data);
    }
}
