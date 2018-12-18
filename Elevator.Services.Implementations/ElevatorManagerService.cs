using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevator.Domain.Entities;
using Elevator.Domain.Interfaces;
using Elevator.Service.Interfaces;
using Elevator.Services.Implementations.SignalR;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace Elevator.Services.Implementations
{
    public class ElevatorManagerService : IElevatorManagerService
    {
        private IElevatorRepository _elevatorRepository;
        private IFloorRepository _floorRepository;
        private int _waitTime;

        public ElevatorManagerService(IElevatorRepository elevatorRepository, IFloorRepository floorRepository)
        {
            _elevatorRepository = elevatorRepository;
            _floorRepository = floorRepository;
            _waitTime = 1000;
        }

        /// <summary>
        /// Wait Time property of Elevetor Manager
        /// </summary>
        public int WaitTime
        {
            get
            {
                return this._waitTime;
            }
            set { this._waitTime = value; }
        }

        /// <summary>
        /// Operate Elevator on request
        /// </summary>
        /// <param name="floor"></param>
        public void Operate(Floor floor)
        {
            //No need to operate elevator if current floor and destination floor are same
            if (floor.CurrentFloor == floor.DestinationFloor)
                return;

            //loop until there is a elevator available
            while (true)
            {
                //find the closest elevator that are available
                var closestAvailableElevator = GetClosestElevator(floor);
                if(closestAvailableElevator == null)
                    continue;

                //Move elevator to the current floor
                GoToCurrentFloor(closestAvailableElevator, floor);

                //Pick People from the floor
                PickPeople(closestAvailableElevator, floor);

                //Move elevator to the destination
                GoToDestination(closestAvailableElevator, floor);

                //Stop elevator
                StayIdle(closestAvailableElevator, floor);
                break;
            }

        }

        /// <summary>
        /// Private method to move elevator to the requested floor
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="floor"></param>
        private void GoToCurrentFloor(Domain.Entities.Elevator elevator, Floor floor)
        {
            if (elevator.CurrentFloor < floor.CurrentFloor)
            {
                //if elevator is down, then go up to destination floor
                Ascend(elevator, floor, elevator.CurrentFloor, floor.CurrentFloor);
            }
            else
            {
                //if elevator is up, then go down to destination floor
                Descend(elevator, floor, elevator.CurrentFloor, floor.CurrentFloor);
            }

        }

        /// <summary>
        /// private method to move elevator to the destination floor
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="floor"></param>
        private void GoToDestination(Domain.Entities.Elevator elevator, Floor floor)
        {
            
            if (floor.CurrentFloor < floor.DestinationFloor)
            {
                //if elevator is down, then go up to destination floor
                Ascend(elevator, floor, floor.CurrentFloor, floor.DestinationFloor);
            }
            else
            {
                //if elevator is up, then go back to destination floor
                Descend(elevator, floor, floor.CurrentFloor, floor.DestinationFloor);
            }
            
        }

        /// <summary>
        /// Private method that will ascend elevator to the destination
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="floor"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private void Ascend(Domain.Entities.Elevator elevator, Floor floor, Int16 from, Int16 to)
        {
            elevator.Direction = Direction.Up;
            for (var i = from + 1; i <= to; i++)
            {
                elevator.CurrentFloor = (short) i;
                _elevatorRepository.Update(elevator);

                SendSignalToElevator(elevator);
                Wait();
            }
        }

        /// <summary>
        /// Private method that will descend elevator to the destination
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="floor"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private void Descend(Domain.Entities.Elevator elevator, Floor floor, Int16 from, Int16 to)
        {
            elevator.Direction = Direction.Down;
            for (var i = from - 1; i >= to; i--)
            {
                elevator.CurrentFloor = (short) i;
                _elevatorRepository.Update(elevator);
                SendSignalToElevator(elevator);
                Wait();
            }
            
        }

        /// <summary>
        /// Pick People from the floor
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="floor"></param>
        private void PickPeople(Domain.Entities.Elevator elevator, Floor floor)
        {
            elevator.TotalPeople += floor.TotalPeople;
            _elevatorRepository.Update(elevator);
        }

        /// <summary>
        /// Stop elevator in the destination
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="floor"></param>
        private void StayIdle(Domain.Entities.Elevator elevator, Floor floor)
        {
            elevator.Direction = Direction.Idle;
            elevator.TotalPeople = 0;
            _elevatorRepository.Update(elevator);
            SendSignalToElevator(elevator);
        }

        /// <summary>
        /// Send signal to the browser to current real time elevator movement
        /// </summary>
        /// <param name="elevator"></param>
        private void SendSignalToElevator(Domain.Entities.Elevator elevator)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ElevatorHub>();
            var elevatorStr = JsonConvert.SerializeObject(elevator);
            context.Clients.All.addMessage(elevatorStr);
            
        }

        /// <summary>
        /// Wait Elevator during the movement
        /// </summary>
        private void Wait()
        {
            System.Threading.Thread.Sleep(_waitTime);
        }

        /// <summary>
        /// Find the closest Elevator 
        /// </summary>
        /// <param name="floor"></param>
        /// <returns></returns>
        private Elevator.Domain.Entities.Elevator GetClosestElevator(Floor floor)
        {
            //get nearest elevator
            var elevators =
                _elevatorRepository.AllList(
                    e =>
                    e.Direction == Direction.Idle);

            if (!elevators.Any())
                return null;

            var closestElevators = (from e in elevators
                                    select new 
                                            { Elevator = e,
                                              Distance = Math.Abs(floor.CurrentFloor - e.CurrentFloor)
                                            }).OrderBy(e=>e.Distance).First();

            return closestElevators.Elevator;

        }
    }

}
