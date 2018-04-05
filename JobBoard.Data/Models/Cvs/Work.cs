using JobBoard.Common.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using static JobBoard.Data.Models.Constants.Constraints;

namespace JobBoard.Data.Models.Cvs
{
    public class Work : IData
    {
        [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        [BsonElement("position")]
        public string Position { get; set; }

        [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        [BsonElement("employer")]
        public string Employer { get; set; }

        //[Required]
        [BsonElement("dateFrom")]
        public DateTime DateFrom { get; set; }
        //[Required]
        [BsonElement("dateTo")]
        public DateTime DateTo { get; set; }
    }
}
