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
    public class OrderDetailsRepository : RepositoryBase<OrderDetails>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(ApplicationDbContext ePlastDBContext) : base(ePlastDBContext)
        {
        }
    }
}
