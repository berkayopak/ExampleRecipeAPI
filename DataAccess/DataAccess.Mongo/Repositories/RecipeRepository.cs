using Domain.Recipe.Entities;
using Domain.Recipe.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Mongo.Repositories
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationContext context) : base(context)
        {
        }
        public IEnumerable<Recipe> GetAllWithPagination(int currentPage, int itemPerPage)
        {
            if (currentPage >= 0 && itemPerPage > 0)
            {
                FilterDefinition<Recipe> filter = Builders<Recipe>.Filter.Empty;

                return _dbCollection.
                                    Find(filter).
                                    SortByDescending(r => r.CreatedOn).
                                    Skip(currentPage * itemPerPage).
                                    Limit(itemPerPage).
                                    ToList();
            }
            else
                return null;
        }
        public IEnumerable<string> GetAllRecipeCategories()
        {
            var filter = new BsonDocument();
            return _dbCollection.Distinct<Category>(nameof(Recipe.Categories), filter).ToList().Select(c=>c.Title).Distinct();
        }

        public IEnumerable<Recipe> FindWithTitle(string title)
        {
            FilterDefinition<Recipe> filter = Builders<Recipe>.Filter.Where(r => r.Title == title);
            return _dbCollection.Find(filter).ToList();
        }

        public long? GetAllRecipeCount()
        {
            FilterDefinition<Recipe> filter = Builders<Recipe>.Filter.Empty;

            return _dbCollection?.CountDocuments(filter);
        }
    }
}
