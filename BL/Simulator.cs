using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using DO;
using DalApi;
using System.Threading;
using static BL.BL;
using NPOI.SS.Formula.Functions;
using Newtonsoft.Json.Serialization;

namespace BL
{
    /// <summary>
    /// 
    /// </summary>
    class Simulator
    {
        const int DELAY = 1000;
        const double KMS = 2.0;

       public Simulator(BL bl, Action SimulatorProgress, Func<bool> IsRun, int DroneId)
        {
         
        }

    }
}
