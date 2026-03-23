using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NET1814_MilkShop.Repositories.Data.Interfaces;

namespace NET1814_MilkShop.Repositories.Data.Entities;

public class Message : IAuditableEntity
{
    [Key]
    public Guid Id { get; set; }

    [Column("conversation_id")]
    [ForeignKey("Conversation")]
    public Guid ConversationId { get; set; }

    [Column("sender_id")]
    [ForeignKey("Sender")]
    public Guid SenderId { get; set; }

    [Column("content", TypeName = "nvarchar(max)")]
    public string Content { get; set; } = null!;

    [Column("is_read")]
    public bool IsRead { get; set; }

    [Column("created_at", TypeName = "datetime2")]
    public DateTime CreatedAt { get; set; }

    [Column("modified_at", TypeName = "datetime2")]
    public DateTime? ModifiedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime2")]
    public DateTime? DeletedAt { get; set; }

    public virtual Conversation? Conversation { get; set; }
    public virtual User? Sender { get; set; }
}
