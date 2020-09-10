using DataAccess.Mongo.Repositories;
using Domain.Recipe.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Mongo.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
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
            return 1;
        }
        public void Dispose()
        {
            //_context.Session.Dispose();
        }
    }
}
