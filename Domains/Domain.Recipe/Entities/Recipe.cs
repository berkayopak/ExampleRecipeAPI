using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Domain.Recipe.Entities
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [NotNull]
        [Required]
        public string Title { get; set; }
        [Required]
        public ICollection<Category> Categories { get; set; }
        [Required]
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Direction> Directions { get; set; }

        public DateTime CreatedOn { get; set; }

        public Recipe()
        {

        }
        public Recipe(Domain.Recipe.Payloads.Recipe recipe)
        {
            this.Title = recipe.Title;
            this.Categories = recipe?.Categories?.ToList()?.ConvertAll(c => new Category(c));
            this.Ingredients = recipe?.Ingredients?.ToList()?.ConvertAll(i => new Ingredient(i));
            this.Directions = recipe?.Directions?.ToList()?.ConvertAll(d => new Direction(d));
        }
    }
}
