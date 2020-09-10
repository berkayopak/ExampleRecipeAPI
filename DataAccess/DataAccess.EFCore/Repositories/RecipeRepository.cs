using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Recipe.Entities;
using Domain.Recipe.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore.Repositories
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationContext context) : base(context)
        {
        }
        public IEnumerable<Recipe> GetAllWithPagination(int currentPage, int itemPerPage)
        {
            if (currentPage >= 0 && itemPerPage > 0)
                return _context.Set<Recipe>()?.
                OrderByDescending(r => r.CreatedOn)?.
                Skip(currentPage * itemPerPage)?.
                Take(itemPerPage)?.
                Include(nameof(Recipe.Categories))?.
                Include(nameof(Recipe.Directions))?.
                Include(nameof(Recipe.Ingredients))?.
                ToList();
            else
                return null;
        }

        public long? GetAllRecipeCount()
        {
            return _context.Set<Recipe>()?.Count();
        }

        public IEnumerable<string> GetAllRecipeCategories()
        {
            return _context.Set<Category>().Select(c => c.Title).Distinct().ToList();
        }

        public IEnumerable<Recipe> FindWithTitle(string title)
        {
            return _context.Set<Recipe>().Where(r => r.Title == title).ToList();
        }
    }
}
