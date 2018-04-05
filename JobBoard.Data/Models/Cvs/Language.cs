using JobBoard.Common.DTO;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using static JobBoard.Data.Models.Constants.Constraints;

namespace JobBoard.Data.Models.Cvs
{
    public class Language : IData
    {
      //  [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        [BsonElement("name")]
        public string Name { get; set; }

      //  [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        [BsonElement("level")]
        public string Level { get; set; }
    }
}
