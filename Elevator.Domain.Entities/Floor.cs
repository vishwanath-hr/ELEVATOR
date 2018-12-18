using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Domain.Entities
{
    /// <summary>
    /// Floor Domain object mapped with Floor table
    /// </summary>
    public class Floor
    {
        public virtual Int16 CurrentFloor { get; set; }
        public virtual Int16 DestinationFloor { get; set; }
        public virtual Int16 TotalPeople { get; set; }

        private readonly int _totalFloor;

        public virtual Direction Direction { get
        {
            return DestinationFloor == 0 || CurrentFloor == DestinationFloor
                       ? Direction.Idle
                       : CurrentFloor > DestinationFloor ? Direction.Down : Direction.Up;
        } } 

        public Floor()
        {
            _totalFloor = 13;
        }

        public virtual int TotalFloor { get { return _totalFloor; } }
    }
}
