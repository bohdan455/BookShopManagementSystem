using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using DataAccess.Repositories.Realizations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Realizations.Main
{
    public class OrderPartRepository : RepositoryBase<OrderPart>, IOrderPartRepository
    {
        public OrderPartRepository(ApplicationDbContext ePlastDBContext) : base(ePlastDBContext)
        {
        }
    }
}
