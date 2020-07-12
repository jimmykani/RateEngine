using RateEngine.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RateEngine.Core.Helper;
using RateEngine.Core.Model;

namespace RateEngine.Infrastructure.Repositories
{
    public class SpecialRateRepository : ISpecialRateRepository
    {
       
        public IEnumerable<SpecialRate> SelectAllAsync()
        {
            var path = "specialrate.json";
            var data = FileHelper.Read(path);
            var result = JsonHelper.Deserialise<IEnumerable<SpecialRate>>(data);

            return result;
        }
    }
}
