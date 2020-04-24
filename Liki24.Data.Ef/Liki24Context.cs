using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Liki24.Data.Ef
{
    public class Liki24Context : DbContext
    {
        public Liki24Context(DbContextOptions<Liki24Context> options)
            : base(options) { }

        public DbSet<DeliveryWindow> DeliveryWindows { get; set; }
    }
}
