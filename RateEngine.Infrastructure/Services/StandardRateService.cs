using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RateEngine.Core.Interfaces.Repositories;
using RateEngine.Core.Interfaces.Service;
using RateEngine.Core.Model;

namespace RateEngine.Infrastructure.Services
{
    public class StandardRateService : IStandardRateService
    {
        private readonly IRepository<StandardRate> _standardRateRepository;
        public StandardRateService(IStandardRateRepository standardRateRepository)
        {
            _standardRateRepository = standardRateRepository;
        }
        public ParkingRateResponse Calculate(DateTime start, DateTime end)
        {
            var result = new ParkingRateResponse() { Name = "Standard" };
           var standardRates = _standardRateRepository.SelectAllAsync();
           var standardRate = 0.0;
           var isNormal = false;
           var standardRatesResult = 0.0;
           var duration = (end - start).TotalHours;
           var maxStandardRate = standardRates.OrderBy(nr => nr.MaxHours).LastOrDefault();
           if (duration >= maxStandardRate.MaxHours)
           {
               standardRatesResult = Math.Floor(duration / maxStandardRate.MaxHours) * maxStandardRate.Rate;
               duration = duration % maxStandardRate.MaxHours;
           }
           if (duration > 0)
           {
               foreach (var rate in standardRates)
               {
                   if (!isNormal && duration <= rate.MaxHours)
                   {
                       isNormal = true;
                       standardRate = rate.Rate;
                   }
               }
           }

           result.Price = standardRatesResult + standardRate;

           return result;

        }
    }
}
