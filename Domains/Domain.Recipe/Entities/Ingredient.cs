using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Recipe.Entities
{
    public class Ingredient
    {
        [BsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public int? Quantity { get; set; }
        public string Unit { get; set; }
        [Required]
        public string Name { get; set; }
        [BsonIgnore]
        public Recipe Recipe { get; set; }

        public Ingredient()
        {

        }
        public Ingredient(Domain.Recipe.Payloads.Ingredient ingredient)
        {
            this.Quantity = ingredient?.Amount?.Quantity;
            this.Unit = ingredient?.Amount?.Unit;
            this.Name = ingredient.Name;
        }
    }
}
