using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Domain.Recipe.Payloads
{
    public class Recipe
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public ICollection<string> Categories { get; set; }
        [Required]
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Direction> Directions { get; set; }

        public Recipe()
        {

        }
        public Recipe(Domain.Recipe.Entities.Recipe recipe)
        {
            this.Title = recipe.Title;
            this.Categories = recipe?.Categories?.Select(c => c.Title)?.ToArray();
            this.Ingredients =  recipe?.Ingredients?.ToList()?.ConvertAll(i => new Ingredient(i));
            this.Directions = recipe?.Directions?.ToList()?.ConvertAll(d => new Direction(d));
        }
    }
}
