using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RateEngine.Core.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> SelectAllAsync();
    }
}
