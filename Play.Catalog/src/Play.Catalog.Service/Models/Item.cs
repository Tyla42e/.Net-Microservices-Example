using MongoDB.Bson.Serialization.Attributes;
using Play.Common;

namespace Play.Catalog.Service.Models
{
    public class Item : IEntity {


        [BsonId]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

         public DateTimeOffset CreatedDate { get; set; }
    }
}