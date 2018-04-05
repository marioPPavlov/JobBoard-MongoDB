using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobBoard.Data.Models.Employers
{
    public class JobApplication
    {
        [BsonElement("appliedCvId")]
        public ObjectId AppliedCvId { get; set; }

        [BsonElement("motivationalLetter")]
        public string MotivationalLetter { get; set; }
    }
}
