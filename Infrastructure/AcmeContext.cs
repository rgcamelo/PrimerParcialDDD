using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AcmeContext: DbContext
    {
        public AcmeContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Credito> Creditos { get; set; }
        public DbSet<Cuota> Cuotas { get; set; }
        public DbSet<Abono> Abonos { get; set; }
        

    }
}
