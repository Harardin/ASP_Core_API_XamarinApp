using System;
using System.Linq;
using System.Threading.Tasks;
using asp_xamar_solution.Models;
using asp_xamar_solution.Models.NonQuaryableModels;

namespace asp_xamar_solution.CommonFunctions
{
    public class RegistrationFunction
    {
        public bool Registration(ApplicationDBContext context, RegDataInput data)
        {
            if (context.UserData.Any(o => o.Email == data.Email) != true)
            {
                context.UserData.Add(new UserDataModel { UserName = data.UserName, Email = data.Email, Paswword = data.Password });
                CreateCoinDataAsync createCoin = new CreateCoinDataAsync();
                createCoin.CreateCoinData(context, data);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
