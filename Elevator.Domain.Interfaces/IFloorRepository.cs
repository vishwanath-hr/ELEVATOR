using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevator.Domain.Entities;

namespace Elevator.Domain.Interfaces
{
    public interface IFloorRepository
    {
        IQueryable<Floor> All();
        List<Floor> AllList();
        Floor Get(int id);
        bool Update(Floor floor);
    }

}
