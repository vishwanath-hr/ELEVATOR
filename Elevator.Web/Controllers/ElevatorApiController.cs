using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using Elevator.Domain.Entities;
using Elevator.Domain.Interfaces;
using Elevator.Service.Interfaces;
using Elevator.Web.utils;

namespace Elevator.Web.Controllers
{
   
    public class ElevatorApiController : ApiController
    {
        private readonly IElevatorRepository _elevatorRepository;

        public ElevatorApiController(IElevatorRepository elevatorRepository)
        {
            _elevatorRepository = elevatorRepository;
        }

        public IList<Domain.Entities.Elevator> Get()
        {
            return _elevatorRepository.AllList();
        }

    }

}
