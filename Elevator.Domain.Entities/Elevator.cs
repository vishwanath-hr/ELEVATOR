using System;

namespace Elevator.Domain.Entities
{
    /// <summary>
    /// Enum for Direction Idle: 0, Up:1, Down:2
    /// </summary>
    public enum Direction : short
    {
        Idle,
        Up,
        Down
        
    }

    /// <summary>
    /// Elevator Domain object mapped with Elevator Table
    /// </summary>
    public class Elevator
    {
        public virtual int ElevatorId { get; set; }
        public virtual string Name { get; set; }
        public virtual Int16 CurrentFloor { get; set; }
        public virtual Direction Direction { get; set; }
        public virtual Int16 TotalPeople { get; set; }

    }

}
