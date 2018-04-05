using JobBoard.Common.DTO;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static JobBoard.Data.Models.Constants.Constraints;

namespace JobBoard.Data.Models.Cvs
{
    public class Education : IData
    {
        [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        [BsonElement("subject")]
        public string Subject { get; set; }

        [Required]
        [BsonElement("qualification")]
        [StringLength(NameMax, MinimumLength = NameMin)]
        public string Qualification { get; set; }

        [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        [BsonElement("organisation")]
        public string Organisation { get; set; }

        //[Required]
        [BsonElement("dateFrom")]
        public DateTime DateFrom { get; set; }
        //[Required]
        [BsonElement("dateTo")]
        public DateTime DateTo { get; set; }
    }
}
