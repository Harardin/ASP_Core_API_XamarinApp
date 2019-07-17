using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using asp_xamar_solution.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_xamar_solution
{
    public static class SeedData
    {
        public static void FillData(IApplicationBuilder app)
        {
            ApplicationDBContext context = app.ApplicationServices.GetRequiredService<ApplicationDBContext>();
            
            if(!context.UserData.Any())
            {
                context.UserData.AddRange(
                    new UserDataModel { UserName = "Alan1", Email = "test@test.com", Paswword = "12345678" },
                    new UserDataModel { UserName = "Alan2", Email = "1test@test.com", Paswword = "12345678" },
                    new UserDataModel { UserName = "Alan3", Email = "2test@test.com", Paswword = "12345678" },
                    new UserDataModel { UserName = "Alan4", Email = "3test@test.com", Paswword = "12345678" },
                    new UserDataModel { UserName = "Alan5", Email = "4test@test.com", Paswword = "12345678" },
                    new UserDataModel { UserName = "Alan6", Email = "5test@test.com", Paswword = "12345678" },
                    new UserDataModel { UserName = "Alan7", Email = "6test@test.com", Paswword = "12345678" }
                    );
                context.WalletData.AddRange(
                    new UserWalletData { UserName = "Alan1", Email = "test@test.com", Coins = 500.0M },
                    new UserWalletData { UserName = "Alan2", Email = "1test@test.com", Coins = 500.0M },
                    new UserWalletData { UserName = "Alan3", Email = "2test@test.com", Coins = 500.0M },
                    new UserWalletData { UserName = "Alan4", Email = "3test@test.com", Coins = 500.0M },
                    new UserWalletData { UserName = "Alan5", Email = "4test@test.com", Coins = 500.0M },
                    new UserWalletData { UserName = "Alan6", Email = "5test@test.com", Coins = 500.0M },
                    new UserWalletData { UserName = "Alan7", Email = "6test@test.com", Coins = 500.0M }
                    );

                context.SaveChanges();
            }
        }

    }
}
