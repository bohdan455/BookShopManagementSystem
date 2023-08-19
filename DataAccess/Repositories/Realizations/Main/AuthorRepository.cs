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
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext ePlastDBContext) : base(ePlastDBContext)
        {
        }
    }
}
