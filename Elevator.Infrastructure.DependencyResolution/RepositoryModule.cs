using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevator.Domain.Interfaces;
using Elevator.Infrastructure.Data.Repositories;
using Ninject.Modules;

namespace Elevator.Infrastructure.DependencyResolution
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IElevatorRepository>().To<ElevatorRepository>();
            Bind<IFloorRepository>().To<FloorRepository>();
        }
    }
}
