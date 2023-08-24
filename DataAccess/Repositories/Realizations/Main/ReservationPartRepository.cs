using DataAccess.Entities;
using DataAccess.Repositories.Realizations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Realizations.Main
{
    internal class ReservationPartRepository : RepositoryBase<ReservationPart>, IReservationPartRepository
    {
        public ReservationPartRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
