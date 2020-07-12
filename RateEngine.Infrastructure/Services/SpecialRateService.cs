using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RateEngine.Core.Interfaces.Repositories;
using RateEngine.Core.Interfaces.Service;
using RateEngine.Core.Model;

namespace RateEngine.Infrastructure.Services
{
    public class SpecialRateService : ISpecialRateService
    {
        private readonly IRepository<SpecialRate> _specialRateRepository;

        public SpecialRateService(ISpecialRateRepository specialRateRepository)
        {
            _specialRateRepository = specialRateRepository;
        }

        public ParkingRateResponse Calculate(DateTime start, DateTime end)
        {
            var result = new ParkingRateResponse();
            var specialRates = _specialRateRepository.SelectAllAsync();

            foreach (var specialRate in specialRates)
            {
                bool isSpecial = (specialRate.Entry.Startime <= start.TimeOfDay &&
                                  start.TimeOfDay <= specialRate.Entry.EndTime) ||
                                 (specialRate.MaxDays > 0 &&
                                  (specialRate.Entry.Startime <= start.TimeOfDay &&
                                   start.TimeOfDay <= specialRate.Entry.EndTime.Add(TimeSpan.FromDays(1))) ||
                                  (specialRate.Entry.Startime.Subtract(TimeSpan.FromDays(1)) <= start.TimeOfDay &&
                                   start.TimeOfDay <= specialRate.Entry.EndTime));


                if (
                    !specialRate.Entry.Days.Any(
                        d => string.Equals(d, start.DayOfWeek.ToString(), StringComparison.InvariantCultureIgnoreCase)))
                {
                    isSpecial = false;
                }

                var maxExitDay = start.AddDays(specialRate.MaxDays);
                var maxExit = new DateTime(maxExitDay.Year, maxExitDay.Month, maxExitDay.Day,
                    specialRate.Exit.EndTime.Hours,
                    specialRate.Exit.EndTime.Minutes, 0);
                if (end > maxExit)
                {
                    isSpecial = false;
                }

                if (!specialRate.Exit.Days.Any(
                    d => string.Equals(d, end.DayOfWeek.ToString(), StringComparison.InvariantCultureIgnoreCase)))
                {
                    isSpecial = false;
                }

                if ((end - start).Days > specialRate.MaxDays)
                {
                    isSpecial = false;
                }

                if (isSpecial)
                {
                    if (result.Price == 0 || result.Price > specialRate.TotalPrice)
                    {
                        result.Name = specialRate.Name;
                        result.Price = specialRate.TotalPrice;
                    }
                }
            }

            return result;
        }
    }
}
