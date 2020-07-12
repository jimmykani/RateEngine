using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using RateEngine.Application.Features.Rate.Get;
using RateEngine.Core.Model;


namespace RateEngine.Api.Functions.Functions.ParkingRates
{
    public class GetRateFunction : BaseFunction<Query, ParkingRateResponse>
    {
        public GetRateFunction(IMediator mediator) : base(mediator) { }

        [FunctionName(nameof(GetRate))]
        public async Task<IActionResult> GetRate([HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Rates")] Query req, ILogger log)
        {
            return await Execute(req);
        }
    }
}
