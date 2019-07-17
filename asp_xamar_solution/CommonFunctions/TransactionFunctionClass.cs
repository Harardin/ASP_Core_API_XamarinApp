using System;
using System.Linq;
using asp_xamar_solution.Models;

namespace asp_xamar_solution.CommonFunctions
{
    public class TransactionFunctionClass
    {
        public bool SendCoins(ApplicationDBContext context, string UserSender, string UserReciever, decimal CoinAmount)
        {
            if(CoinAmount > context.WalletData.Where(where => where.UserName == UserSender).Select(select => select.Coins).FirstOrDefault())
            {
                return false;
            }
            else
            {
                // Adding coins to recipient
                var userRecipient = context.WalletData.SingleOrDefault(recipient => recipient.UserName == UserReciever);
                userRecipient.Coins = userRecipient.Coins + CoinAmount;
                // Decreasing amount of Coins from Sender User
                var userSender = context.WalletData.SingleOrDefault(sender => sender.UserName == UserSender);
                userSender.Coins = userSender.Coins - CoinAmount;

                // Saving changes
                context.SaveChanges();

                return true;
            }
        }
    }
}
