using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Restaurant.Payment.Data.Model;

public class OrderData
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public DateTime? Data { get; set; }

    public string? Status { get; set; }

    public string? Cliente { get; set; }

    public List<OrderItemData> Items { get; set; } = new();

}
