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

    /// <summary>
    /// A class that implements the simulator functions declared at IBL.
    /// </summary>
    sealed partial class BL :IBL
    {
        /// <summary>
        /// Function to operate the simulator.
        /// </summary>
        /// <param name="SimulatorViewProgress">The process that changes the display in the drone window according to the simulator</param>
        /// <param name="IsRun">Checks if the simulator is still working</param>
        /// <param name="DroneId">The id of the drone</param>
        public void StartSimulator(Action SimulatorViewProgress, Func<bool> IsRun, int DroneId)
        {
            new Simulator(this, SimulatorViewProgress, IsRun, DroneId); 
        }
    }
}
