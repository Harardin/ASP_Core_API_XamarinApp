using System;
using System.Linq;
using System.Threading.Tasks;
using asp_xamar_solution.Models;

namespace asp_xamar_solution.CommonFunctions
{
    public class TransactionHistoryRecordClass
    {
        public async Task SaveHistory(ApplicationDBContext context, string Sender, string Reciever, decimal Amount)
        {
            context.TransHistory.Add(new TransactionsHistoryModel
            {
                trDate = DateTime.Now,
                CorrespondentName = Reciever,
                CorrespondentEmail = context.Users.Where(corEm => corEm.UserName == Reciever).Select(em => em.Email).FirstOrDefault(),
                trAmount = Amount,
                RestBalance = context.WalletData.Where(usr => usr.UserName == Sender).Select(am => am.Coins).FirstOrDefault(),
                UserEmail = context.Users.Where(u => u.UserName == Sender).Select(ml => ml.Email).FirstOrDefault()
            });
            await context.SaveChangesAsync();
        }
    }
}