using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Elevator.Domain.Entities;
using Elevator.Domain.Interfaces;
using Elevator.Service.Interfaces;
using Elevator.Web.Models;

namespace Elevator.Web.Controllers
{
    public class FloorApiController : ApiController
    {
        private readonly IFloorRepository _floorRepository;
        private readonly IElevatorRepository _elevatorRepository;
        private readonly IElevatorManagerService _elevatorManagerService;

        public FloorApiController(IFloorRepository floorRepository, IElevatorRepository elevatorRepository, IElevatorManagerService elevatorManagerService)
        {
            _floorRepository = floorRepository;
            _elevatorRepository = elevatorRepository;
            _elevatorManagerService = elevatorManagerService;
        }

        public IList<FloorModel> Get()
        {
            var floors = _floorRepository.AllList().OrderByDescending(f=>f.CurrentFloor);
            var elevators = _elevatorRepository.AllList();

            var floorModels = new List<FloorModel>();

            foreach (var floor in floors)
            {
                floorModels.Add(new FloorModel(floor.CurrentFloor, floor.TotalPeople, floor.DestinationFloor, elevators));
            }

            return floorModels;
        }

        public HttpResponseMessage Update(Floor floor)
        {
            var response = Validate(floor);
            if (response.StatusCode != HttpStatusCode.OK)
                return response;

            _floorRepository.Update(floor);

            var worker = new Thread(() => _elevatorManagerService.Operate(floor)) { IsBackground = true };
            worker.Start();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private HttpResponseMessage Validate(Floor floor)
        {
            if (floor.CurrentFloor == floor.DestinationFloor)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Invalid Destination")
                };

            if (floor.CurrentFloor > floor.TotalFloor || floor.DestinationFloor > floor.TotalFloor)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Invalid floor")
                };

            if (floor.TotalPeople > 20)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Only allowed upto 20 people")
                };
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

    }
}
