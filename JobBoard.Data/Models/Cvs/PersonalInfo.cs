using JobBoard.Data.Models.Constants;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static JobBoard.Data.Models.Constants.Constraints;

namespace JobBoard.Data.Models.Cvs
{
    public class PersonalInfo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        [BsonElement("name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(NameMax, MinimumLength = NameMin)]
        [BsonElement("email")]
        public string Email { get; set; }

        [StringLength(PictureMax, MinimumLength = PictureMin)]
        [BsonElement("picture")]
        public string Picture { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [BsonElement("birthDate")]
        public DateTime BirhtDate { get; set; }

        [Required]
        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }

        [Required]
        [BsonElement("gender")]
        public Gender Gender { get; set; }

    }
}
