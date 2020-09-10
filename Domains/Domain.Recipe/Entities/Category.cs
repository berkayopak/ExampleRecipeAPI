using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Recipe.Entities
{
    public class Category
    {
        [BsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [BsonIgnore]
        public ICollection<Recipe> Recipes { get; set; }

        public Category()
        {

        }
        public Category(string categoryTitle)
        {
            this.Title = categoryTitle;
        }

    }
}
