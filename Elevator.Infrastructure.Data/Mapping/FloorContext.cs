using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevator.Domain.Entities;

namespace Elevator.Infrastructure.Data.Mapping
{
    public class FloorContext : DbContext
    {
        public FloorContext()
            : base("name=DefaultConnection")
        {
        }

        public DbSet<Floor> Floors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Floor>()
                        .HasKey(e => e.CurrentFloor);
        } 

    }
}
