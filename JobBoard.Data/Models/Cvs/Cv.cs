using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static JobBoard.Data.Models.Constants.Constraints;

namespace JobBoard.Data.Models.Cvs
{

    public class Cv
    {
        public ObjectId Id { get; set; }

        [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("userId")]
        public ObjectId UserId { get; set; }

        [BsonElement("personalInfo")]
        public PersonalInfo PersonalInfo { get; set; }

        [BsonElement("works")]
        public List<Work> Works { get; set; } = new List<Work>();

        [BsonElement("educations")]
        public List<Education> Educations { get; set; } = new List<Education>();

        [BsonElement("languages")]
        public List<Language> Languages { get; set; } = new List<Language>();

        [BsonElement("skills")]
        public List<Skill> Skills { get; set; } = new List<Skill>();
    }

}
