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
   public partial class BL :IBL
    {
        public void StartSimulator(Action SimulatorProgress, Func<bool> IsRun, int DroneId, TimeSpan time)
        {
            new Simulator(this, SimulatorProgress, IsRun, DroneId);

           
        }
    }
}
