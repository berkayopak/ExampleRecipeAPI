using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Recipe.Payloads
{
    public class Ingredient
    {
        public Amount Amount;
        [Required]
        public string Name { get; set; }
        public Ingredient()
        {

        }
        public Ingredient(Domain.Recipe.Entities.Ingredient ingredient)
        {
            this.Name = ingredient.Name;
            this.Amount = new Amount(ingredient.Quantity, ingredient.Unit);
        }
    }
}
