using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Elevator.Domain.Entities;
using Newtonsoft.Json;

namespace Elevator.Web.Models
{
    /// <summary>
    /// View Model class for the Elevator and Floor details
    /// </summary>
    public class FloorModel
    {
        public int CurrentFloor { get; set; }
        public int TotalPeople { get; set; }
        public int DestinationFloor { get; set; }
        public List<Domain.Entities.Elevator> Elevators { get; set; }

        public Domain.Entities.Elevator ElevatorA { get { return Elevators.SingleOrDefault(e => e.ElevatorId == 1 && e.CurrentFloor == CurrentFloor); } }
        public Domain.Entities.Elevator ElevatorB { get { return Elevators.SingleOrDefault(e => e.ElevatorId == 2 && e.CurrentFloor == CurrentFloor); } }
        public Domain.Entities.Elevator ElevatorC { get { return Elevators.SingleOrDefault(e => e.ElevatorId == 3 && e.CurrentFloor == CurrentFloor); } }
      //  public Domain.Entities.Elevator ElevatorD { get { return Elevators.SingleOrDefault(e => e.ElevatorId == 4 && e.CurrentFloor == CurrentFloor); } }

        public FloorModel() { }

        public FloorModel(int currentFloor, int totalPeople, int destinationFloor,
                          List<Domain.Entities.Elevator> elevators)
        {
            CurrentFloor = currentFloor;
            TotalPeople = totalPeople;
            DestinationFloor = destinationFloor;
            Elevators = elevators;
        }
    }
}