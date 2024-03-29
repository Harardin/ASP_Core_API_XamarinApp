﻿using System;
using asp_xamar_solution.Models;
using asp_xamar_solution.Models.NonQuaryableModels;
using System.Threading.Tasks;

namespace asp_xamar_solution.CommonFunctions
{
    public class CreateCoinDataAsync
    {
        // This is Starter Coin pack
        private decimal starter = 500.0M;

        public async Task CreateCoinData(ApplicationDBContext context, RegDataInput data)
        {
            context.WalletData.Add(new UserWalletData { Email = data.Email, UserName = data.UserName, Coins = starter });
            await context.SaveChangesAsync();
        }
    }
}
