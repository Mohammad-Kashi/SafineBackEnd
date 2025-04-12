using Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Repository
{
    public record MongoConfig : IMongoConfig
    {
        public string ConnectionString { get; init; }
        public string DatabaseName { get; init; }
        public MongoConfig() { }
        public MongoConfig(string connectionString, string dbName)
        {
            this.ConnectionString = connectionString;
            this.DatabaseName = dbName;
        }
    }
}
