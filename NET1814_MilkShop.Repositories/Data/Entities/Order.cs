using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NET1814_MilkShop.Repositories.Data.Interfaces;

namespace NET1814_MilkShop.Repositories.Data.Entities;

public class Order : IAuditableEntity
{
    [Key] public Guid Id { get; set; }

    [Column("customer_id")] public Guid? CustomerId { get; set; }

    [Column("total_price")] public int TotalPrice { get; set; }

    [Column("voucher_amount")]
    public int VoucherAmount { get; set; }

    [Column("point_amount")]
    public int PointAmount { get; set; }

    [Column("shipping_fee")] public int ShippingFee { get; set; }

    [Column("total_amount")] public int TotalAmount { get; set; } // TotalPrice + ShippingFee

    [Column("total_gram")] public int TotalGram { get; set; }

    [Column("receiver_name", TypeName = "nvarchar(255)")]
    public string ReceiverName { get; set; } = null!;

    [Column("address", TypeName = "nvarchar(2000)")]
    public string Address { get; set; } = null!;

    [Column("district_id")] public int DistrictId { get; set; }

    [Column("ward_code", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    public string WardCode { get; set; } = null!;

    [Column("phone_number", TypeName = "nvarchar(20)")]
    public string PhoneNumber { get; set; } = null!;

    [Column("note")]
    public string? Note { get; set; }

    [Column("payment_method", TypeName = "varchar(255)")]
    public string? PaymentMethod { get; set; }
    /// <summary>
    /// Order code 
    /// </summary>
    [Column("transaction_code")] public long? TransactionCode { get; set; }

    [Column("shipping_code", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    public string? ShippingCode { get; set; }

    [Column("payment_date", TypeName = "datetime2")]
    public DateTime? PaymentDate { get; set; }

    [Column("status_id")] public int StatusId { get; set; }

    [Column("email", TypeName = "nvarchar(255)")]
    public string? Email { get; set; }

    [Column("is_preorder")] public bool IsPreorder { get; set; }

    [Column("created_at", TypeName = "datetime2")]
    public DateTime CreatedAt { get; set; }

    [Column("modified_at", TypeName = "datetime2")]
    public DateTime? ModifiedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime2")]
    public DateTime? DeletedAt { get; set; }

    [Column("version")] [Timestamp] public byte[] Version { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = [];

    public virtual OrderStatus? Status { get; set; }

    public virtual ICollection<OrderLog> OrderLogs { get; set; } = [];
}