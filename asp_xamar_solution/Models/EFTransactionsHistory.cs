using System;
using System.Linq;

namespace asp_xamar_solution.Models
{
    public class EFTransactionsHistory : ITransactionsHistory
    {
        private ApplicationDBContext context;
        public EFTransactionsHistory(ApplicationDBContext _context)
        {
            context = _context;
        }

        public IQueryable<TransactionsHistoryModel> QTransactionHistory => context.TransHistory;
    }
}
