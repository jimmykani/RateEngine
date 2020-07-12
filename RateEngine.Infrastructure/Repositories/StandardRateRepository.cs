using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RateEngine.Core.Helper;
using RateEngine.Core.Interfaces.Repositories;
using RateEngine.Core.Model;

namespace RateEngine.Infrastructure.Repositories
{
    public class StandardRateRepository :IStandardRateRepository
    {
      

        public IEnumerable<StandardRate> SelectAllAsync()
        {
            var path = "standardrate.json";
            var data = FileHelper.Read(path);
            var result = JsonHelper.Deserialise<IEnumerable<StandardRate>>(data);

            return result;
        }
    }
}
