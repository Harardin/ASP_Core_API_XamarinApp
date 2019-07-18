using System.Collections.Generic;

namespace asp_xamar_solution.Models
{
    public class MainWalletModel
    {
        public string UserName { get; set; }

        public UserWalletData WalletData { get; set; }
        public List<TransactionsHistoryModel> TransactionsHistory { get; set; }
    }
}
