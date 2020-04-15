using Domain.Contracts;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class AbonoRepository : GenericRepository<Abono>, IAbonoRepository
    {
        public AbonoRepository(IDbContext context)
              : base(context)
        {

        }
    }
}
