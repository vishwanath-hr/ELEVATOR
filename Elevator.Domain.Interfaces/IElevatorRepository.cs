using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Elevator.Domain.Entities;

namespace Elevator.Domain.Interfaces
{
    public interface IElevatorRepository
    {
        IQueryable<Domain.Entities.Elevator> All();
        List<Domain.Entities.Elevator> AllList();
        List<Domain.Entities.Elevator> AllList(Expression<Func<Domain.Entities.Elevator, bool>> expression);
        Domain.Entities.Elevator Get(int id);
        bool Update(Domain.Entities.Elevator elevator);
    }
}
