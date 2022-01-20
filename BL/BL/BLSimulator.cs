using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public partial class BL :IBL
    {
        public void StartSimulator(Action SimulatorProgress, Func<bool> IsRun, int DroneId)
        {
            new Simulator(this, SimulatorProgress, IsRun, DroneId); 
        }
    }
}
