using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Recipe.Entities;
using Domain.Recipe.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RecipeAPI.Controllers
{
    [Route("services/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RecipeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("all")]
        [HttpPost]
        public IActionResult GetAllRecipes(int currentPage=0, int itemPerPage=1)
        {

            var recipes = _unitOfWork.Recipes.GetAllWithPagination(currentPage, itemPerPage)?.ToList();

            int? resultCount = recipes?.Count();

            if (recipes == null || resultCount <= 0)
                return NoContent();

            var _recipes = recipes.ConvertAll(r => new Domain.Recipe.Payloads.Recipe(r));

            long? totalCount = _unitOfWork.Recipes.GetAllRecipeCount();
            
            return Ok(new { 
                results = resultCount,
                total = totalCount,
                recipes = _recipes
            });
        }

        [Route("filter/categories")]
        [HttpGet]
        public IActionResult GetAllRecipeCategories()
        {
            var categories = _unitOfWork.Recipes.GetAllRecipeCategories()?.ToList();
            int? resultCount = categories?.Count();

            if (categories == null || resultCount <= 0)
                return NoContent();

            return Ok(new {
                results = resultCount,
                categories
            });
        }

        [Route("add")]
        [HttpPost]
        public IActionResult AddRecipe(Domain.Recipe.Payloads.Recipe recipe)
        {

            Domain.Recipe.Entities.Recipe _recipe;

            if(_unitOfWork.Recipes.FindWithTitle(recipe.Title).Count() > 0)
            {
                return StatusCode(409, new Domain.Recipe.Payloads.Error("Recipe duplicated", "409"));
            }

            try
            {
                _recipe = new Domain.Recipe.Entities.Recipe(recipe);
            }
            catch
            {
                return BadRequest(new Domain.Recipe.Payloads.Error("Wrong JSON Object", "400"));
            }

            if (_recipe == null)
            {
                return BadRequest(new Domain.Recipe.Payloads.Error("Wrong JSON Object", "400"));
            }

            _recipe.CreatedOn = DateTime.UtcNow;

            _unitOfWork.Recipes.Add(_recipe);
            _unitOfWork.Complete();
            return CreatedAtAction(nameof(this.AddRecipe), recipe);

        }
    }
}
