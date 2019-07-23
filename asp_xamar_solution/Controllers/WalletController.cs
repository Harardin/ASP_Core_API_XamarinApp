using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using asp_xamar_solution.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace asp_xamar_solution.Controllers
{
    [Authorize]
    public class WalletController : Controller
    {
        private IUserWalletData qWallet;
        private ITransactionsHistory qTransHistory;
        private readonly UserManager<IdentityUser> userManager;
        public WalletController(IUserWalletData _qWallet, ITransactionsHistory _qTrans, UserManager<IdentityUser> _userManager)
        {
            qWallet = _qWallet;
            qTransHistory = _qTrans;
            userManager = _userManager;
        }

        private MainWalletModel DataModel = new MainWalletModel();

        [HttpGet]
        public async Task<IActionResult> Main()
        {
            // Getting User Data from cookie
            // This throws NullReference now don't work with ASP.NET Identity
            string UserName = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Name).Value;
            System.Diagnostics.Debug.WriteLine("This is the email of the user " + UserName);
            IdentityUser UserData = await userManager.FindByNameAsync(UserName);
            string Email = UserData.Email;

            DataModel.WalletData = qWallet.QUserWalletData.Where(q => q.UserName == UserName).FirstOrDefault();
            DataModel.TransactionsHistory = qTransHistory.QTransactionHistory.Where(i => i.UserEmail == Email).OrderByDescending(d => d.trDate).ToList();

            return View(DataModel);
        }
        [HttpPost]
        public IActionResult Main(MainWalletModel mod)
        {
            return RedirectToAction("SendCoins", "Transaction", new { recipient = mod.UserName });
        }


        // Data for JS autocompeat form
        public async Task<JsonResult> AutoComplete(string Prefix)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();


            List<MainWalletModel> mainWalletModels = new List<MainWalletModel>();
            foreach(var zx in userManager.Users.ToList())
            {
                mainWalletModels.Add(new MainWalletModel { UserName = zx.UserName });
            }

            var UsersList = from N in mainWalletModels where N.UserName.StartsWith(Prefix) select new { N.UserName };

            return Json(UsersList, settings);
        }
    }
}