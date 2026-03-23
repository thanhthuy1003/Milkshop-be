using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NET1814_MilkShop.Repositories.Data.Interfaces;

namespace NET1814_MilkShop.Repositories.Data.Entities;

public class Conversation : IAuditableEntity
{
    [Key]
    public Guid Id { get; set; }

    [Column("buyer_id")]
    [ForeignKey("Buyer")]
    public Guid BuyerId { get; set; }

    [Column("seller_id")]
    [ForeignKey("Seller")]
    public Guid SellerId { get; set; }

    [Column("created_at", TypeName = "datetime2")]
    public DateTime CreatedAt { get; set; }

    [Column("modified_at", TypeName = "datetime2")]
    public DateTime? ModifiedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime2")]
    public DateTime? DeletedAt { get; set; }

    public virtual User? Buyer { get; set; }
    public virtual User? Seller { get; set; }
    public virtual ICollection<Message> Messages { get; set; } = [];
}
