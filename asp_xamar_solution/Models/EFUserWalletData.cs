using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_xamar_solution.Models
{
    public class EFUserWalletData : IUserWalletData
    {
        private ApplicationDBContext context;
        public EFUserWalletData(ApplicationDBContext _context)
        {
            context = _context;
        }

        public IQueryable<UserWalletData> QUserWalletData => context.WalletData;
    }
}
