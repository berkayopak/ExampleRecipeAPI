using DataAccess.EFCore.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EFCore.UnitOfWorks
{
    public class UnitOfWork : Domain.Recipe.Interfaces.IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Recipes = new RecipeRepository(_context);
        }
        public Domain.Recipe.Interfaces.IRecipeRepository Recipes { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
