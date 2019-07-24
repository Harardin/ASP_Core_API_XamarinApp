## APS.NET CORE + XamarinApp Coins Recieve and send Solution

Current Application demonstrates a Demmo of a WebWallet wher people can send Coins and recieve Coins from another users.

In this solution I didn't put to much effort in fromnt part of Web. Instead focused more on Mobile version of the App.

All closed api methods uses a Token Authentification system.

Here is the excample of ASP.NET API Controller code:

```
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
        
        // And the Transaction excecution
        
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
        
```

## About Xamarin Application

Here is some screenshot how the application appearence looks like:

![alt text](https://raw.githubusercontent.com/Harardin/ASP_Core_API_XamarinApp/ASPNETCORE_API_WithTokenAuth/XamarinAPISolution/XamarinAPISolution/ScreenShots/MainWalletScreen.jpg) ![alt text](https://raw.githubusercontent.com/Harardin/ASP_Core_API_XamarinApp/ASPNETCORE_API_WithTokenAuth/XamarinAPISolution/XamarinAPISolution/ScreenShots/TransactionScreen.jpg)
