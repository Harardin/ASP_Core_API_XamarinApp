using System;
using System.Linq;
using asp_xamar_solution.Models;

namespace asp_xamar_solution.CommonFunctions
{
    public class TransactionHistoryRecordClass
    {
        private ApplicationDBContext dbContext;
        private string userSender;
        private string userReciever;
        private decimal coinAmount;
        public void SaveHistory(ApplicationDBContext context, string Sender, string Reciever, decimal Amount)
        {
            dbContext = context;
            userSender = Sender;
            userReciever = Reciever;
            coinAmount = Amount;
            dbContext.TransHistory.Add(new TransactionsHistoryModel
            {
                trDate = DateTime.Now,
                CorrespondentName = userReciever,
                CorrespondentEmail = dbContext.UserData.Where(corEm => corEm.UserName == userReciever).Select(em => em.Email).FirstOrDefault(),
                trAmount = coinAmount,
                RestBalance = dbContext.WalletData.Where(usr => usr.UserName == userSender).Select(am => am.Coins).FirstOrDefault(),
                UserEmail = dbContext.UserData.Where(u => u.UserName == userSender).Select(ml => ml.Email).FirstOrDefault()
            });
            dbContext.SaveChanges();
        }
    }
}