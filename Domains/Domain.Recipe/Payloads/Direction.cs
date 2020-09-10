using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Recipe.Payloads
{
    public class Direction
    {
        [Required]
        public string Step { get; set; }

        public Direction()
        {

        }
        public Direction(Domain.Recipe.Entities.Direction direction)
        {
            this.Step = direction.Step;
        }
    }
}
