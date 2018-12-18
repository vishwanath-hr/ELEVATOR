using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevator.Domain.Entities;

namespace Elevator.Service.Interfaces
{
    public interface IElevatorManagerService
    {
        int WaitTime { get; set; }
        void Operate(Floor floor);
        
    }
}
