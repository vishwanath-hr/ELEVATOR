using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevator.Domain.Entities;

namespace Elevator.Infrastructure.Data.Mapping
{
    public class ElevatorContext : DbContext
    {
        public ElevatorContext()
            : base("name=DefaultConnection")
        {
            
        }

        public DbSet<Domain.Entities.Elevator> Elevators { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Elevator>()
                        .HasKey(e => e.ElevatorId);

        } 
    }
}
