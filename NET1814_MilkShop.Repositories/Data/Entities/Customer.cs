using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NET1814_MilkShop.Repositories.Data.Interfaces;

namespace NET1814_MilkShop.Repositories.Data.Entities;

public class Customer : IAuditableEntity
{
    [Key] [Column("user_id")] public Guid UserId { get; set; }

    [Column("phone_number", TypeName = "nvarchar(20)")] public string? PhoneNumber { get; set; }

    [Column("point")] public int Point { get; set; }

    [Column("email", TypeName = "nvarchar(255)")] public string? Email { get; set; }

    [Column("google_id", TypeName = "nvarchar(255)")] public string? GoogleId { get; set; }

    [Column("profile_picture_url", TypeName = "nvarchar(255)")] public string? ProfilePictureUrl { get; set; }

    [Column("created_at", TypeName = "datetime2")]
    public DateTime CreatedAt { get; set; }

    [Column("modified_at", TypeName = "datetime2")]
    public DateTime? ModifiedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime2")]
    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = [];

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = [];

    public virtual ICollection<Order> Orders { get; set; } = [];

    public virtual User User { get; set; } = null!;
}