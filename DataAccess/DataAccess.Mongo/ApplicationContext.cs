using DataAccess.Mongo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace DataAccess.Mongo
{
    public class ApplicationContext : IMongoDbContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        public ApplicationContext(MongoDatabaseSettings settings)
        {
            _mongoClient = new MongoClient(settings.ConnectionString);
            _db = _mongoClient.GetDatabase(settings.DatabaseName);
        }
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}
