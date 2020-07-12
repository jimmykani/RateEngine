using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RateEngine.Core.Model;

namespace RateEngine.Core.Interfaces.Service
{
    public interface IService
    {
        ParkingRateResponse Calculate( DateTime start, DateTime end);
    }
}
