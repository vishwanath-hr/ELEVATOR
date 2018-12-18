using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Elevator.Domain.Entities;
using Elevator.Domain.Interfaces;
using Elevator.Infrastructure.Data.Mapping;


namespace Elevator.Infrastructure.Data.Repositories
{
    public class ElevatorRepository : IElevatorRepository
    {


        public ElevatorRepository()
        {
            
        }

        /// <summary>
        /// Get All Elevator as Iquerable
        /// </summary>
        /// <returns></returns>
        public IQueryable<Domain.Entities.Elevator> All()
        {
            using (var context = new ElevatorContext())
            {
                var elevators = context.Elevators;

              
                return elevators;

            }
        }

        /// <summary>
        /// Get All Elevator as List
        /// </summary>
        /// <returns></returns>
        public List<Domain.Entities.Elevator> AllList()
        {
            using (var context = new ElevatorContext())
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var elevators = context.Elevators.ToList();
                stopWatch.Stop();
                          return elevators;

            }

        }

        /// <summary>
        /// Get All Elevator with search func expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public List<Domain.Entities.Elevator> AllList(Expression<Func<Domain.Entities.Elevator, bool>> expression)
        {
            using (var context = new ElevatorContext())
            {
                var elevators = context.Elevators.Where(expression).ToList();

                            return elevators;

            }
            
        }

        /// <summary>
        /// Get Elevator by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.Elevator Get(int id)
        {
            using (var context = new ElevatorContext())
            {
                var elevator = context.Elevators.Find(id);

                return elevator;

            }

        }

        /// <summary>
        /// Update current Elevator to db
        /// </summary>
        /// <param name="elevator"></param>
        /// <returns></returns>
        public bool Update(Domain.Entities.Elevator elevator)
        {
            using (var context = new ElevatorContext())
            {
                context.Elevators.Attach(elevator);

                var entry = context.Entry(elevator);
                entry.State = EntityState.Modified;
                context.SaveChanges();

                      return true;

            }
        }
    }

}
