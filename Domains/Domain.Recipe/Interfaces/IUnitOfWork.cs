using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Recipe.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRecipeRepository Recipes { get; }
        int Complete();
    }
}
