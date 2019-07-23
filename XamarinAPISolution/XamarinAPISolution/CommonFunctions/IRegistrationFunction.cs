using System.Threading.Tasks;
using XamarinAPISolution.Models;

namespace XamarinAPISolution.CommonFunctions
{
    interface IRegistrationFunction
    {
        Task<bool> Registration(RegDataInput data);
    }
}
