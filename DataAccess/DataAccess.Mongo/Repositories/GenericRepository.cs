﻿using DataAccess.Mongo.Helper;
using Domain.Recipe.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Mongo.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        protected IMongoCollection<T> _dbCollection;
        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            _dbCollection = _context.GetCollection<T>(typeof(T).Name);
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(T).Name + " object is null");
            }
            _dbCollection.InsertOne(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbCollection.InsertMany(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Where(expression);
            return _dbCollection.Find(filter).ToList();
        }

        public IEnumerable<T> GetAll()
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Empty;
            return _dbCollection.Find(filter).ToList();
        }


        public T GetById(string id)
        {
            var objectId = new ObjectId(id);

            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", objectId);

            return _dbCollection.Find(filter).FirstOrDefault();
        }

        public void Remove(T entity)
        {
            string id = entity?.GetType()?.GetProperty("Id")?.GetValue(entity)?.ToString();
            if(id != null)
            {
                var objectId = new ObjectId(id);
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", objectId);

                _dbCollection.DeleteOne(filter);
            }
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            var objectIdArray = BsonHelper<T>.ToObjectIdArray(entities);
            if(objectIdArray != null && objectIdArray.Count() > 0)
            {
                FilterDefinition<T> filter = Builders<T>.Filter.In("_id", objectIdArray);

                _dbCollection.DeleteMany(filter);
            }
        }

        
    }
}
