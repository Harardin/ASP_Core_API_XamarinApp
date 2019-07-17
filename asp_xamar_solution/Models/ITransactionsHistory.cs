using System.Linq;

namespace asp_xamar_solution.Models
{
    public interface ITransactionsHistory
    {
        IQueryable<TransactionsHistoryModel> QTransactionHistory { get; }
    }
}
