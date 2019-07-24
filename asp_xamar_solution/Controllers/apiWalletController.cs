using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_xamar_solution.CommonFunctions;
using asp_xamar_solution.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XamarinAPISolution.Models;

namespace asp_xamar_solution.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class apiWalletController : ControllerBase
    {
        private readonly IUserWalletData walletData;
        private readonly ITransactionsHistory transactionsHistory;
        private ApplicationDBContext context;

        public apiWalletController(ApplicationDBContext _context, IUserWalletData _wallet, ITransactionsHistory _transactions)
        {
            walletData = _wallet;
            transactionsHistory = _transactions;
            context = _context;
        }

        // Returns UserWalletData
        [HttpGet]
        [Route("UserData")]
        public async Task<IActionResult> UserData(string email)
        {
            if(email == null || email == "")
            {
                return BadRequest();
            }
            UserWalletData userWalletData = new UserWalletData();
            userWalletData = await walletData.QUserWalletData.Where(em => em.Email == email).FirstOrDefaultAsync();
            return Ok(userWalletData);
        }

        // Return User TransactionsHistory
        [HttpGet]
        [Route("TransactionsHistory")]
        public async Task<IActionResult> TransactionsHistory(string email)
        {
            if (email == null || email == "")
            {
                return BadRequest(new { result = false });
            }
            List<TransactionsHistoryModel> historyModel = new List<TransactionsHistoryModel>();
            historyModel = await transactionsHistory.QTransactionHistory.Where(his => his.UserEmail == email).OrderByDescending(d => d.trDate).ToListAsync();

            return Ok(historyModel);
        }

        // Returns the List of Users ho can have the coins from MainUser
        // This will be used to make AutocompleatUserSearch work in Xamarin
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> ListOfUsers()
        {
            List<UsersList> users = new List<UsersList>();
            List<string> ex = new List<string>();
            users = await walletData.QUserWalletData.Select(sel => new UsersList { Name = sel.UserName, Email = sel.Email }).ToListAsync();
            return Ok(users);
        }

        public class UsersList
        {
            public string Name { get; set; }
            public string Email { get; set; }
        }

        // Making Transaction
        [HttpPost]
        [Route("sendCoins")]
        public async Task<IActionResult> SendCoins(Api_SendCoinsModel model)
        {
            TransactionFunctionClass transaction = new TransactionFunctionClass();
            if (transaction.SendCoins(context, model.Sender, model.Reciever, model.Amount))
            {
                // Saving Tr History
                TransactionHistoryRecordClass historyRecord = new TransactionHistoryRecordClass();
                await historyRecord.SaveHistory(context, model.Sender, model.Reciever, model.Amount);
                return Ok(new { result = true });
            }
            else
            {
                return Ok(new { result = false });
            }
        }
    }
}