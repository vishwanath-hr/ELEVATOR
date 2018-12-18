using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevator.Domain.Entities;
using Elevator.Domain.Interfaces;
using Elevator.Infrastructure.Data.Mapping;


namespace Elevator.Infrastructure.Data.Repositories
{
    public class FloorRepository : IFloorRepository
    {
       

        public FloorRepository()
        {
          
        }

        /// <summary>
        /// Get All Floors as IQueryable
        /// </summary>
        /// <returns></returns>
        public IQueryable<Floor> All()
        {
            using (var context = new FloorContext())
            {
                var floors = context.Floors;

           
                return floors;

            }
        }

        /// <summary>
        /// Get All Floors as List
        /// </summary>
        /// <returns></returns>
        public List<Floor> AllList()
        {
            using (var context = new FloorContext())
            {
                var floors = context.Floors.ToList();

              
                return floors;

            }
        }

        /// <summary>
        /// Get floor by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Floor Get(int id)
        {
            using (var context = new FloorContext())
            {
                var floor = context.Floors.Find(id);

           
                return floor;

            }
        }

        /// <summary>
        /// Update Floor to db
        /// </summary>
        /// <param name="floor"></param>
        /// <returns></returns>
        public bool Update(Floor floor)
        {
            using (var context = new FloorContext())
            {
                context.Floors.Attach(floor);

                var entry = context.Entry(floor);
                entry.State = EntityState.Modified;
                context.SaveChanges();

    
                return true;

            }
        }

    }
}
