using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using RateEngine.Core.Model;


namespace RateEngine.Application.Features.Rate.Get
{
    public class Query :IRequest<ParkingRateResponse>
    {
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
    }
}
