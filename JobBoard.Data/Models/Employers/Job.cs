using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobBoard.Data.Models.Employers
{
    public class Job
    {
        public ObjectId Id { get; set; }

        [BsonElement("userId")]
        public ObjectId UserId { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("picture")]
        public string Picture { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("applications")]
        public IEnumerable<JobApplication> Applications { get; set; } = new List<JobApplication>();

        [BsonElement("tags")]
        public string Tags { get; set; }

    }
}
