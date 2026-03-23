using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NET1814_MilkShop.Repositories.Data.Interfaces;

namespace NET1814_MilkShop.Repositories.Data.Entities;

public class User : IAuditableEntity
{
    [Key][Column("Id")] public Guid Id { get; set; }

    [Column("Username", TypeName = "nvarchar(50)")]
    public string Username { get; set; } = null!;

    [Column("Password", TypeName = "nvarchar(255)")]
    public string Password { get; set; } = null!;

    [Column("first_name", TypeName = "nvarchar(50)")]
    public string? FirstName { get; set; }

    [Column("last_name", TypeName = "nvarchar(50)")]
    public string? LastName { get; set; }

    [Column("verification_code", TypeName = "nvarchar(6)")]
    public string? VerificationCode { get; set; }

    [Column("reset_password_code", TypeName = "nvarchar(6)")]
    public string? ResetPasswordCode { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [DefaultValue(false)] 
    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("is_banned")]
    [DefaultValue(false)]
    public bool IsBanned { get; set; }

    [Column("created_at", TypeName = "datetime2")]
    public DateTime CreatedAt { get; set; }

    [Column("modified_at", TypeName = "datetime2")]
    public DateTime? ModifiedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime2")]
    public DateTime? DeletedAt { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = [];
}