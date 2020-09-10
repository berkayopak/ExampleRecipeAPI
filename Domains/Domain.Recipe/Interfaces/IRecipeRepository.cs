using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Recipe.Interfaces
{
    public interface IRecipeRepository : IGenericRepository<Domain.Recipe.Entities.Recipe>
    {
        IEnumerable<Domain.Recipe.Entities.Recipe> GetAllWithPagination(int currentPage, int itemPerPage);
        IEnumerable<string> GetAllRecipeCategories();
        IEnumerable<Domain.Recipe.Entities.Recipe> FindWithTitle(string title);
        long? GetAllRecipeCount();
    }
}
