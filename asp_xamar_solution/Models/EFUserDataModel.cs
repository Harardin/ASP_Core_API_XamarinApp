using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_xamar_solution.Models
{
    public class EFUserDataModel : IUserDataModel
    {
        private ApplicationDBContext context;
        public EFUserDataModel(ApplicationDBContext _context)
        {
            context = _context;
        }

        public IQueryable<UserDataModel> QUserData => context.UserData;
    }
}
