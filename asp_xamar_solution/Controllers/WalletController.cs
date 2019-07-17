using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using asp_xamar_solution.Models;
using asp_xamar_solution.CommonFunctions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace asp_xamar_solution.Controllers
{
    [Authorize]
    public class WalletController : Controller
    {
        private IUserDataModel qUser;
        private IUserWalletData qWallet;
        private ITransactionsHistory qTransHistory;
        public WalletController(IUserDataModel _qUser, IUserWalletData _qWallet, ITransactionsHistory _qTrans)
        {
            qUser = _qUser;
            qWallet = _qWallet;
            qTransHistory = _qTrans;
        }

        private MainWalletModel DataModel = new MainWalletModel();

        [HttpGet]
        public IActionResult Main()
        {
            // Getting User Data from cookie
            string UserName = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Name).Value;
            string Email = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Email).Value;

            DataModel.WalletData = qWallet.QUserWalletData.Where(q => q.Email == Email).FirstOrDefault();
            DataModel.TransactionsHistory = qTransHistory.QTransactionHistory.Where(i => i.UserEmail == Email).ToList();

            return View(DataModel);
        }
        [HttpPost]
        public IActionResult Main(MainWalletModel mod)
        {
            return RedirectToAction("SendCoins", "Transaction", new { recipient = mod.UserName });
        }

        public async Task<JsonResult> AutoComplete(string Prefix)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();


            List<MainWalletModel> mainWalletModels = new List<MainWalletModel>();
            foreach(var zx in qUser.QUserData.ToList())
            {
                mainWalletModels.Add(new MainWalletModel { UserName = zx.UserName });
            }

            var UsersList = from N in mainWalletModels where N.UserName.StartsWith(Prefix) select new { N.UserName };

            return Json(UsersList, settings);
        }
    }
}