using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IMongoConfig
    {
        string ConnectionString { get; init; }
        string DatabaseName { get; init; }
    }
}
