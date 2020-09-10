using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Recipe.Entities
{
    public class Direction
    {
        [BsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string Step { get; set; }
        [BsonIgnore]
        public Recipe Recipe { get; set; }


        public Direction()
        {

        }
        public Direction(Domain.Recipe.Payloads.Direction direction)
        {
            this.Step = direction.Step;
        }
    }
}
