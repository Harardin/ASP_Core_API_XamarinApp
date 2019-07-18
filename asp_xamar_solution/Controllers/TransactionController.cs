using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using asp_xamar_solution.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using asp_xamar_solution.CommonFunctions;
using Microsoft.AspNetCore.Identity;

namespace asp_xamar_solution.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private ApplicationDBContext context;
        private readonly UserManager<IdentityUser> userManager;

        public TransactionController(ApplicationDBContext ctx, UserManager<IdentityUser> _userManager)
        {
            context = ctx;
            userManager = _userManager;
        }
        private string UserSender;

        [HttpGet]
        public IActionResult SendCoins(string recipient)
        {
            TransactionDataModel TransactionDataModel = new TransactionDataModel();
            TransactionDataModel.WalletData = new UserWalletData() { UserName = recipient };
            return View(TransactionDataModel);
        }
        [HttpPost]
        public async Task<IActionResult> SendCoins(TransactionDataModel TrModel)
        {
            if (TrModel.WalletData.Coins < 1)
            {
                TrModel.CoinFieldErrorMessage = "You have to specify the amount of Coins to transfer";
                return View(TrModel);
            }
            UserSender = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Name).Value;

            TransactionFunctionClass transaction = new TransactionFunctionClass();
            if(transaction.SendCoins(context, UserSender, TrModel.WalletData.UserName, TrModel.WalletData.Coins))
            {
                // Saving Tr History
                TransactionHistoryRecordClass historyRecord = new TransactionHistoryRecordClass();
                historyRecord.SaveHistory(context, UserSender, TrModel.WalletData.UserName, TrModel.WalletData.Coins);
                return View("TransCompleat");
            }
            else
            {
                TrModel.CoinFieldErrorMessage = "You don't have enough Coins to send";
                return View(TrModel);
            }
        }
    }
}