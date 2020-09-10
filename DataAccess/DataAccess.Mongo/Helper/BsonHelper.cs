using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Mongo.Helper
{
    public static class BsonHelper<T>
    {
        public static BsonArray ToObjectIdArray(IEnumerable<T> entities)
        {
            var array = new BsonArray();
            foreach (var entity in entities)
            {
                var idValue = entity?.GetType()?.GetProperty("Id")?.GetValue(entity);
                if (idValue != null)
                {
                    var objectId = new ObjectId(idValue.ToString());
                    array.Add(objectId);
                }
            }
            return array;
        }
    }
}
