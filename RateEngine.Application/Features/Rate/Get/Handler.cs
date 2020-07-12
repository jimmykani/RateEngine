using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RateEngine.Core.Interfaces.Service;
using RateEngine.Core.Model;

namespace RateEngine.Application.Features.Rate.Get
{
    public class Handler : IRequestHandler<Query, ParkingRateResponse>
    {
        private readonly ISpecialRateService _specialRateService;
        private readonly IStandardRateService _standardRateService;
        public Handler(ISpecialRateService specialRateService,IStandardRateService standardRateService)
        {
            _specialRateService = specialRateService;
            _standardRateService = standardRateService;
        }

        public Task<ParkingRateResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var result = new ParkingRateResponse();
                var resultSpecial = _specialRateService.Calculate(request.EntryTime, request.ExitTime);
                result = resultSpecial;

                var resultStandard = _standardRateService.Calculate(request.EntryTime, request.ExitTime);

                // choose for either normal vs special
                if (resultStandard.Price > 0 && (result.Price == 0 || result.Price > resultStandard.Price))
                {
                    result.Name = resultStandard.Name;
                    result.Price = resultStandard.Price;
                }

                return result;
            },cancellationToken);
        }
    }
}

