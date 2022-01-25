using BlApi;
using DalApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;


namespace BL
{
    sealed partial class BL :IBL
    {
        public void StartSimulator(Action SimulatorViewProgress, Func<bool> IsRun, int DroneId)
        {
            new Simulator(this, SimulatorViewProgress, IsRun, DroneId); 
        }
    }
}
