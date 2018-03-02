namespace JobBoard.Data.Models.MongoDB.Identity
{
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization.Attributes;

	public class MongoRole
	{
		public MongoRole()
		{
			Id = ObjectId.GenerateNewId().ToString();
		}

		public MongoRole(string roleName) : this()
		{
			Name = roleName;
		}

		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		public string Name { get; set; }

		public string NormalizedName { get; set; }

		public override string ToString() => Name;
	}
}