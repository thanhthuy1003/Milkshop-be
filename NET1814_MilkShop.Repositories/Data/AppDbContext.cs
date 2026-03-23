using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NET1814_MilkShop.Repositories.Data.Entities;
using NET1814_MilkShop.Repositories.Data.Interfaces;

namespace NET1814_MilkShop.Repositories.Data;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
    public virtual DbSet<Cart> Carts { get; set; }
    public virtual DbSet<CartDetail> CartDetails { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<PreorderProduct> PreorderProducts { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Brand> Brands { get; set; }
    public virtual DbSet<Unit> Units { get; set; }
    public virtual DbSet<ProductReview> ProductReviews { get; set; }
    public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
    public virtual DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
    public virtual DbSet<ProductImage> ProductImages { get; set; }
    public virtual DbSet<ProductStatus> ProductStatuses { get; set; }
    public virtual DbSet<Post> Posts { get; set; }
    public virtual DbSet<OrderLog> OrderLogs { get; set; }
    public virtual DbSet<Voucher> Vouchers { get; set; }
    public virtual DbSet<Report> Reports { get; set; }
    public virtual DbSet<ReportType> ReportTypes { get; set; }
    public virtual DbSet<Conversation> Conversations { get; set; }
    public virtual DbSet<Message> Messages { get; set; }

    // Thêm phương thức này để tự động quản lý CreatedAt và ModifiedAt
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            // Xử lý cho các entity implement IAuditableEntity
            if (entry.Entity is IAuditableEntity auditableEntity)
            {
                if (entry.State == EntityState.Added)
                {
                    auditableEntity.CreatedAt = DateTime.UtcNow;
                    auditableEntity.ModifiedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    auditableEntity.ModifiedAt = DateTime.UtcNow;
                }
            }

            // Chuyển đổi tất cả các thuộc tính DateTime sang UTC
            foreach (var property in entry.Properties)
            {
                if (property.CurrentValue is DateTime dateTime && dateTime.Kind == DateTimeKind.Unspecified)
                {
                    property.CurrentValue = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //optionsBuilder.UseSqlServer(
            //    _configuration.GetConnectionString("DefaultConnection")
            optionsBuilder.UseNpgsql(
            _configuration.GetConnectionString("DefaultConnection")
            );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Map all properties to snake_case for both SQL Server and PostgreSQL compatibility
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (string.IsNullOrEmpty(tableName)) continue;

            // Apply snake_case to the table name (this handles pluralization naturally if GetTableName() provides it)
            entity.SetTableName(ToSnakeCase(tableName));

            if (Database.IsNpgsql())
            {
                foreach (var property in entity.GetProperties())
                {
                    // Map all properties to snake_case for PostgreSQL
                    property.SetColumnName(ToSnakeCase(property.Name));
                }
            }
            // For SQL Server, we rely on attributes or default naming (PascalCase) 
            // to avoid mismatches like ParentId -> parent_id.
        }

        ////////////////////////////////////////////////////////////////
        modelBuilder.Entity<Brand>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            if (Database.IsNpgsql())
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("deleted_at");

                b.Property<string>("Description")
                    .HasColumnName("Description")
                    .HasColumnType("nvarchar(2000)");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<string>("Logo")
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("logo");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("modified_at");
            }
            else
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnName("deleted_at");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(2000)")
                    .HasColumnName("description");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<string>("Logo")
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("logo");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnName("modified_at");
            }

            b.Property<string>("Name")
                .IsRequired()
                .HasColumnType("nvarchar(255)")
                .UseCollation("Latin1_General_CI_AS");

            b.HasKey("Id");

            b.HasIndex("Name");
        });

        modelBuilder.Entity<Cart>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("created_at");

            b.Property<Guid>("CustomerId")
                .HasColumnType("uniqueidentifier")
                .HasColumnName("customer_id");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("deleted_at");

            b.Property<DateTime?>("ModifiedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("modified_at");

            b.HasKey("Id");

            b.HasIndex("CustomerId")
                .IsUnique();
        });

        modelBuilder.Entity<CartDetail>(b =>
        {
            b.Property<int>("CartId")
                .HasColumnType("int")
                .HasColumnName("cart_id");

            b.Property<Guid>("ProductId")
                .HasColumnType("uniqueidentifier")
                .HasColumnName("product_id");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("created_at");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("deleted_at");

            b.Property<DateTime?>("ModifiedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("modified_at");

            b.Property<int>("Quantity")
                .HasColumnType("int")
                .HasColumnName("quantity");

            b.HasKey("CartId", "ProductId");

            b.HasIndex("ProductId");
        });

        modelBuilder.Entity<Category>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            if (Database.IsNpgsql())
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("deleted_at");

                b.Property<string>("Description")
                    .HasColumnName("Description")
                    .HasColumnType("nvarchar(2000)");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("modified_at");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("nvarchar(255)")
                    .UseCollation("Latin1_General_CI_AI");

                b.Property<int?>("ParentId")
                    .HasColumnName("parent_id")
                    .HasColumnType("int");
            }
            else
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnName("deleted_at");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(2000)")
                    .HasColumnName("description");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnName("modified_at");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("name")
                    .UseCollation("Latin1_General_CI_AI");

                b.Property<int?>("ParentId")
                    .HasColumnName("parent_id");
            }

            b.HasKey("Id");

            b.HasIndex("Name");

            b.HasIndex("ParentId");
        });

        modelBuilder.Entity<Customer>(b =>
        {
            b.Property<Guid>("UserId")
                .HasColumnType("uniqueidentifier")
                .HasColumnName("user_id");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("created_at");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("deleted_at");

            b.Property<string>("Email")
                .HasColumnType("nvarchar(255)")
                .HasColumnName("email");

            b.Property<string>("GoogleId")
                .HasColumnType("nvarchar(255)")
                .HasColumnName("google_id");

            b.Property<DateTime?>("ModifiedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("modified_at");

            b.Property<string>("PhoneNumber")
                .HasColumnType("nvarchar(20)")
                .HasColumnName("phone_number");

            b.Property<int>("Point")
                .HasColumnType("int")
                .HasColumnName("point");

            b.Property<string>("ProfilePictureUrl")
                .HasColumnType("nvarchar(255)")
                .HasColumnName("profile_picture_url");

            b.HasKey("UserId");

            b.HasIndex("Email")
                .IsUnique();

            b.HasIndex("GoogleId")
                .IsUnique();

            b.HasIndex("PhoneNumber")
                .IsUnique();
        });

        modelBuilder.Entity<CustomerAddress>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            b.Property<string>("Address")
                .HasColumnType("nvarchar(2000)")
                .HasColumnName("address");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("created_at");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("deleted_at");

            b.Property<int>("DistrictId")
                .HasColumnType("int")
                .HasColumnName("district_id");

            b.Property<string>("DistrictName")
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)")
                .HasColumnName("district_name");

            b.Property<bool>("IsDefault")
                .HasColumnName("is_default");

            b.Property<DateTime?>("ModifiedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("modified_at");

            b.Property<string>("PhoneNumber")
                .HasColumnType("nvarchar(20)")
                .HasColumnName("phone_number");

            b.Property<int>("ProvinceId")
                .HasColumnType("int")
                .HasColumnName("province_id");

            b.Property<string>("ProvinceName")
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)")
                .HasColumnName("province_name");

            b.Property<string>("ReceiverName")
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)")
                .HasColumnName("receiver_name");

            b.Property<Guid?>("UserId")
                .HasColumnType("uniqueidentifier")
                .HasColumnName("user_id");

            b.Property<string>("WardCode")
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)")
                .HasColumnName("ward_code");

            b.Property<string>("WardName")
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)")
                .HasColumnName("ward_name");

            b.HasKey("Id");

            b.HasIndex("UserId");
        });

        modelBuilder.Entity<Order>(b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uniqueidentifier")
                .HasColumnName("id");

            if (Database.IsNpgsql())
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("created_at");

                b.Property<Guid?>("CustomerId")
                    .HasColumnType("uniqueidentifier")
                    .HasColumnName("customer_id");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("deleted_at");

                b.Property<int>("DistrictId")
                    .HasColumnType("int")
                    .HasColumnName("district_id");

                b.Property<string>("Email")
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("email");

                b.Property<bool>("IsPreorder")
                    .HasColumnName("is_preorder");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("modified_at");

                b.Property<int?>("TransactionCode")
                    .HasColumnType("int")
                    .HasColumnName("transaction_code");

                b.Property<DateTime?>("PaymentDate")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("payment_date");

                b.Property<string>("PaymentMethod")
                    .HasColumnType("varchar(255)")
                    .HasColumnName("payment_method");

                b.Property<string>("PhoneNumber")
                    .IsRequired()
                    .HasColumnType("nvarchar(20)")
                    .HasColumnName("phone_number");

                b.Property<int>("PointAmount")
                    .HasColumnType("int")
                    .HasColumnName("point_amount");

                b.Property<string>("ReceiverName")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("receiver_name");

                b.Property<string>("ShippingCode")
                    .HasMaxLength(255)
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("shipping_code");

                b.Property<int>("ShippingFee")
                    .HasColumnType("int")
                    .HasColumnName("shipping_fee");

                b.Property<int>("StatusId")
                    .HasColumnType("int")
                    .HasColumnName("status_id");

                b.Property<int>("TotalAmount")
                    .HasColumnType("int")
                    .HasColumnName("total_amount");

                b.Property<int>("TotalGram")
                    .HasColumnType("int")
                    .HasColumnName("total_gram");

                b.Property<int>("TotalPrice")
                    .HasColumnType("int")
                    .HasColumnName("total_price");

                b.Property<int>("VoucherAmount")
                    .HasColumnType("int")
                    .HasColumnName("voucher_amount");

                b.Property<string>("WardCode")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("ward_code");

                b.Property<byte[]>("Version")
                    .HasColumnName("version")
                    .IsRowVersion();
            }
            else
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnName("created_at");

                b.Property<Guid?>("CustomerId")
                    .HasColumnName("customer_id");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnName("deleted_at");

                b.Property<int>("DistrictId")
                    .HasColumnName("district_id");

                b.Property<string>("Email")
                    .HasColumnName("email");

                b.Property<bool>("IsPreorder")
                    .HasColumnName("is_preorder");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnName("modified_at");

                b.Property<long?>("TransactionCode")
                    .HasColumnName("transaction_code");

                b.Property<DateTime?>("PaymentDate")
                    .HasColumnName("payment_date");

                b.Property<string>("PaymentMethod")
                    .HasColumnType("varchar(255)")
                    .HasColumnName("payment_method");

                b.Property<string>("PhoneNumber")
                    .IsRequired()
                    .HasColumnType("nvarchar(20)")
                    .HasColumnName("phone_number");

                b.Property<int>("PointAmount")
                    .HasColumnName("point_amount");

                b.Property<string>("ReceiverName")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("receiver_name");

                b.Property<string>("ShippingCode")
                    .HasColumnName("shipping_code");

                b.Property<int>("ShippingFee")
                    .HasColumnName("shipping_fee");

                b.Property<int>("StatusId")
                    .HasColumnName("status_id");

                b.Property<int>("TotalAmount")
                    .HasColumnName("total_amount");

                b.Property<int>("TotalGram")
                    .HasColumnName("total_gram");

                b.Property<int>("TotalPrice")
                    .HasColumnName("total_price");

                b.Property<int>("VoucherAmount")
                    .HasColumnName("voucher_amount");

                b.Property<string>("WardCode")
                    .HasColumnName("ward_code");

                b.Property<string>("Address")
                    .IsRequired()
                    .HasColumnType("nvarchar(2000)")
                    .HasColumnName("address");

                b.Property<string>("Note")
                    .HasColumnType("nvarchar(max)")
                    .HasColumnName("note");

                b.Property<byte[]>("Version")
                    .IsRowVersion()
                    .HasColumnName("version");
            }

            b.HasKey("Id");

            b.HasIndex("CustomerId");

            b.HasIndex("StatusId");
        });

        modelBuilder.Entity<OrderDetail>(b =>
        {
            b.Property<Guid>("OrderId")
                .HasColumnType("uniqueidentifier")
                .HasColumnName("order_id");

            b.Property<Guid>("ProductId")
                .HasColumnType("uniqueidentifier")
                .HasColumnName("product_id");

            if (Database.IsNpgsql())
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("deleted_at");

                b.Property<int>("ItemPrice")
                    .HasColumnType("int")
                    .HasColumnName("item_price");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("modified_at");

                b.Property<string>("ProductName")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("product_name");

                b.Property<string>("Thumbnail")
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("thumbnail");

                b.Property<int>("UnitPrice")
                    .HasColumnType("int")
                    .HasColumnName("unit_price");
            }
            else
            {
                b.Property<string>("ProductName")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)");
            }

            b.HasKey("OrderId", "ProductId");

            b.HasIndex("ProductId");
        });

        modelBuilder.Entity<OrderLog>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("created_at");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("deleted_at");

            b.Property<DateTime?>("ModifiedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("modified_at");

            b.Property<int>("NewStatusId")
                .HasColumnType("int")
                .HasColumnName("new_status_id");

            b.Property<Guid>("OrderId")
                .HasColumnType("uniqueidentifier")
                .HasColumnName("order_id");

            b.Property<string>("StatusName")
                .IsRequired()
                .HasColumnType("nvarchar(50)")
                .HasColumnName("status_name");

            b.HasKey("Id");

            b.HasIndex("OrderId");
        });

        modelBuilder.Entity<OrderStatus>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            if (Database.IsNpgsql())
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("deleted_at");

                b.Property<string>("Description")
                    .HasColumnName("description")
                    .HasColumnType("nvarchar(2000)");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("modified_at");
            }
            else
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnName("deleted_at");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(2000)")
                    .HasColumnName("description");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnName("modified_at");
            }

            b.Property<string>("Name")
                .IsRequired()
                .HasColumnType("nvarchar(255)")
                .UseCollation("Latin1_General_CI_AS");

            b.HasKey("Id");
        });

        modelBuilder.Entity<Post>(b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uniqueidentifier");

            if (Database.IsNpgsql())
            {
                b.Property<Guid>("AuthorId")
                    .HasColumnType("uniqueidentifier")
                    .HasColumnName("author_id");

                b.Property<string>("Content")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)")
                    .HasColumnName("content");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("deleted_at");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<string>("MetaDescription")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)")
                    .HasColumnName("meta_description");

                b.Property<string>("MetaTitle")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("meta_title");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("modified_at");

                b.Property<string>("Thumbnail")
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("thumbnail");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("title");
            }
            else
            {
                b.Property<Guid>("AuthorId")
                    .HasColumnName("author_id");

                b.Property<string>("Content")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)")
                    .HasColumnName("content");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnName("deleted_at");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<string>("MetaDescription")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)")
                    .HasColumnName("meta_description");

                b.Property<string>("MetaTitle")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("meta_title");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnName("modified_at");

                b.Property<string>("Thumbnail")
                    .HasColumnName("thumbnail");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("title");
            }

            b.HasKey("Id");

            b.HasIndex("AuthorId");
        });

        modelBuilder.Entity<PreorderProduct>(b =>
        {
            b.Property<Guid>("ProductId")
                .HasColumnType("uniqueidentifier")
                .HasColumnName("product_id");

            if (Database.IsNpgsql())
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("deleted_at");

                b.Property<DateTime>("EndDate")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("end_date");

                b.Property<int>("ExpectedPreOrderDays")
                    .HasColumnType("int")
                    .HasColumnName("expected_preorder_days");

                b.Property<int>("MaxPreOrderQuantity")
                    .HasColumnType("int")
                    .HasColumnName("max_preorder_quantity");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("modified_at");

                b.Property<DateTime>("StartDate")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("start_date");
            }

            b.HasKey("ProductId");
        });

        modelBuilder.Entity<Product>(b =>
        {
            // row version
            b.Property<byte[]>("Version")
                .HasColumnName("version")
                .IsRowVersion();

            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uniqueidentifier");

            if (Database.IsNpgsql())
            {
                b.Property<int>("BrandId")
                    .HasColumnType("int")
                    .HasColumnName("brand_id");

                b.Property<int>("CategoryId")
                    .HasColumnType("int")
                    .HasColumnName("category_id");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("deleted_at");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(MAX)")
                    .HasColumnName("description")
                    .UseCollation("Latin1_General_CI_AI");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("modified_at");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("name")
                    .UseCollation("Latin1_General_CI_AI");

                b.Property<int>("OriginalPrice")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasDefaultValue(0)
                    .HasColumnName("original_price");

                b.Property<int>("Quantity")
                    .HasColumnType("int")
                    .HasColumnName("quantity");

                b.Property<int>("SalePrice")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasDefaultValue(0)
                    .HasColumnName("sale_price");

                b.Property<int>("StatusId")
                    .HasColumnType("int")
                    .HasColumnName("status_id");

                b.Property<string>("Thumbnail")
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("thumbnail");

                b.Property<int>("UnitId")
                    .HasColumnType("int")
                    .HasColumnName("unit_id");
            }
            else
            {
                b.Property<int>("BrandId")
                    .HasColumnName("brand_id");

                b.Property<int>("CategoryId")
                    .HasColumnName("category_id");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnName("deleted_at");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(MAX)")
                    .HasColumnName("description")
                    .UseCollation("Latin1_General_CI_AI");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnName("modified_at");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("name")
                    .UseCollation("Latin1_General_CI_AI");

                b.Property<int>("OriginalPrice")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasDefaultValue(0)
                    .HasColumnName("original_price");

                b.Property<int>("Quantity")
                    .HasColumnName("quantity");

                b.Property<int>("SalePrice")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasDefaultValue(0)
                    .HasColumnName("sale_price");

                b.Property<int>("StatusId")
                    .HasColumnName("status_id");

                b.Property<string>("Thumbnail")
                    .HasColumnName("thumbnail");

                b.Property<int>("UnitId")
                    .HasColumnName("unit_id");
            }

            b.HasKey("Id");

            b.HasIndex("BrandId");

            b.HasIndex("CategoryId");

            b.HasIndex("StatusId");

            b.HasIndex("UnitId");
        });

        modelBuilder.Entity<ProductAttribute>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            if (Database.IsNpgsql())
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("deleted_at");

                b.Property<string>("Description")
                    .HasColumnName("description")
                    .HasColumnType("nvarchar(2000)");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("modified_at");
            }
            else
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnName("deleted_at");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(2000)")
                    .HasColumnName("description");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnName("modified_at");
            }

            b.Property<string>("Name")
                .IsRequired()
                .HasColumnType("nvarchar(255)")
                .HasColumnName("name")
                .UseCollation("Latin1_General_CI_AS");

            b.HasKey("Id");
        });

        modelBuilder.Entity<ProductAttributeValue>(b =>
        {
            if (Database.IsNpgsql())
            {
                // Existing Npgsql mapping
            }
            else
            {
                b.Property<Guid>("ProductId")
                    .HasColumnName("product_id");

                b.Property<int>("AttributeId")
                    .HasColumnName("attribute_id");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnName("deleted_at");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnName("modified_at");

                b.Property<string>("Value")
                    .IsRequired()
                    .HasColumnType("nvarchar(2000)")
                    .HasColumnName("value");
            }

            b.HasKey("ProductId", "AttributeId");

            b.HasIndex("AttributeId");
        });

        modelBuilder.Entity<ProductImage>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            if (Database.IsNpgsql())
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("deleted_at");

                b.Property<string>("ImageUrl")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("image_url");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("modified_at");

                b.Property<Guid?>("ProductId")
                    .HasColumnType("uniqueidentifier")
                    .HasColumnName("product_id");
            }
            else
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnName("deleted_at");

                b.Property<string>("ImageUrl")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("image_url");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnName("modified_at");

                b.Property<Guid?>("ProductId")
                    .HasColumnName("product_id");
            }

            b.HasKey("Id");

            b.HasIndex("ProductId");
        });

        modelBuilder.Entity<ProductReview>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            if (Database.IsNpgsql())
            {
                // Existing Npgsql mapping
            }
            else
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnName("created_at");

                b.Property<Guid>("CustomerId")
                    .HasColumnName("customer_id");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnName("deleted_at");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnName("modified_at");

                b.Property<Guid>("OrderId")
                    .HasColumnName("order_id");

                b.Property<Guid>("ProductId")
                    .HasColumnName("product_id");

                b.Property<int>("Rating")
                    .HasColumnName("rating");

                b.Property<string>("Review")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("review");
            }

            b.HasKey("Id");

            b.HasIndex("CustomerId");

            b.HasIndex("OrderId");

            b.HasIndex("ProductId");
        });

        modelBuilder.Entity<ProductStatus>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            if (Database.IsNpgsql())
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("deleted_at");

                b.Property<string>("Description")
                    .HasColumnName("Description")
                    .HasColumnType("nvarchar(2000)");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("modified_at");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)");
            }
            else
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnName("deleted_at");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(2000)")
                    .HasColumnName("description");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnName("modified_at");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("name");
            }

            b.HasKey("Id");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.Report", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uniqueidentifier");

            if (Database.IsNpgsql())
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("created_at");

                b.Property<Guid>("CustomerId")
                    .HasColumnType("uniqueidentifier")
                    .HasColumnName("customer_id");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("deleted_at");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("modified_at");

                b.Property<Guid>("ProductId")
                    .HasColumnType("uniqueidentifier")
                    .HasColumnName("product_id");

                b.Property<int>("ReportTypeId")
                    .HasColumnType("int")
                    .HasColumnName("report_type_id");

                b.Property<DateTime?>("ResolvedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("resolved_at");

                b.Property<Guid>("ResolvedBy")
                    .HasColumnType("uniqueidentifier")
                    .HasColumnName("resolved_by");
            }
            else
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnName("created_at");

                b.Property<Guid>("CustomerId")
                    .HasColumnName("customer_id");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnName("deleted_at");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnName("modified_at");

                b.Property<Guid>("ProductId")
                    .HasColumnName("product_id");

                b.Property<int>("ReportTypeId")
                    .HasColumnName("report_type_id");

                b.Property<DateTime?>("ResolvedAt")
                    .HasColumnName("resolved_at");

                b.Property<Guid>("ResolvedBy")
                    .HasColumnName("resolved_by");
            }

            b.HasKey("Id");

            b.HasIndex("CustomerId");

            b.HasIndex("ProductId");

            b.HasIndex("ReportTypeId");
        });

        modelBuilder.Entity<ReportType>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            if (Database.IsNpgsql())
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("deleted_at");

                b.Property<string>("Description")
                    .HasColumnName("description")
                    .HasColumnType("nvarchar(2000)");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("modified_at");
            }
            else
            {
                b.Property<string>("Description")
                    .HasColumnType("nvarchar(2000)");
            }

            b.Property<string>("Name")
                .IsRequired()
                .HasColumnType("nvarchar(255)");

            b.HasKey("Id");
        });

        modelBuilder.Entity<Role>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("created_at");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("deleted_at");

            b.Property<string>("Description")
                .HasColumnName("description")
                .HasColumnType("nvarchar(2000)");

            b.Property<DateTime?>("ModifiedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("modified_at");

            b.Property<string>("Name")
                .IsRequired()
                .HasColumnType("nvarchar(255)")
                .HasColumnName("name");

            b.HasKey("Id");
        });

        modelBuilder.Entity<Unit>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            if (Database.IsNpgsql())
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("deleted_at");

                b.Property<string>("Description")
                    .HasColumnName("Description")
                    .HasColumnType("nvarchar(2000)");

                b.Property<int>("Gram")
                    .HasColumnType("int")
                    .HasColumnName("gram");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("modified_at");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)")
                    .UseCollation("Latin1_General_CI_AS");
            }
            else
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnName("deleted_at");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(2000)")
                    .HasColumnName("description");

                b.Property<int>("Gram")
                    .HasColumnName("gram");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnName("modified_at");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("name")
                    .UseCollation("Latin1_General_CI_AS");
            }

            b.HasKey("Id");

            b.HasIndex("Name");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.User", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

            if (Database.IsNpgsql())
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("deleted_at");

                b.Property<string>("FirstName")
                    .HasColumnType("nvarchar(50)")
                    .HasColumnName("first_name");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<bool>("IsBanned")
                    .HasColumnName("is_banned");

                b.Property<string>("LastName")
                    .HasColumnType("nvarchar(50)")
                    .HasColumnName("last_name");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("modified_at");

                b.Property<string>("ResetPasswordCode")
                    .HasColumnType("nvarchar(6)")
                    .HasColumnName("reset_password_code");

                b.Property<int>("RoleId")
                    .HasColumnType("int")
                    .HasColumnName("role_id");

                b.Property<string>("VerificationCode")
                    .HasColumnType("nvarchar(6)")
                    .HasColumnName("verification_code");

                b.Property<string>("Password")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("password");

                b.Property<string>("Username")
                    .IsRequired()
                    .HasColumnType("nvarchar(50)")
                    .HasColumnName("username")
                    .UseCollation("Latin1_General_CS_AS");
            }
            else
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnName("deleted_at");

                b.Property<string>("FirstName")
                    .HasColumnType("nvarchar(50)")
                    .HasColumnName("first_name");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<bool>("IsBanned")
                    .HasColumnName("is_banned");

                b.Property<string>("LastName")
                    .HasColumnType("nvarchar(50)")
                    .HasColumnName("last_name");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnName("modified_at");

                b.Property<string>("ResetPasswordCode")
                    .HasColumnType("nvarchar(6)")
                    .HasColumnName("reset_password_code");

                b.Property<int>("RoleId")
                    .HasColumnName("role_id");

                b.Property<string>("VerificationCode")
                    .HasColumnType("nvarchar(6)")
                    .HasColumnName("verification_code");

                b.Property<string>("Password")
                    .IsRequired()
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("password");

                b.Property<string>("Username")
                    .IsRequired()
                    .HasColumnType("nvarchar(50)")
                    .HasColumnName("username")
                    .UseCollation("Latin1_General_CS_AS");
            }

            b.HasKey("Id");

            b.HasIndex("RoleId");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.Voucher", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uniqueidentifier");

            b.Property<string>("Code")
                .IsRequired()
                .HasColumnType("nvarchar(10)");

            if (Database.IsNpgsql())
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("deleted_at");

                b.Property<DateTime>("EndDate")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("end_date");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<int>("MaxDiscount")
                    .HasColumnType("int")
                    .HasColumnName("max_discount");

                b.Property<int>("MinPriceCondition")
                    .HasColumnType("int")
                    .HasColumnName("min_price_condition");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("modified_at");

                b.Property<DateTime>("StartDate")
                    .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                    .HasColumnName("start_date");
            }
            else
            {
                b.Property<DateTime>("CreatedAt")
                    .HasColumnName("created_at");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnName("deleted_at");

                b.Property<DateTime>("EndDate")
                    .HasColumnName("end_date");

                b.Property<bool>("IsActive")
                    .HasColumnName("is_active");

                b.Property<int>("MaxDiscount")
                    .HasColumnName("max_discount");

                b.Property<int>("MinPriceCondition")
                    .HasColumnName("min_price_condition");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnName("modified_at");

                b.Property<DateTime>("StartDate")
                    .HasColumnName("start_date");
            }

            b.Property<string>("Description")
                .HasColumnName("description")
                .IsRequired()
                .HasColumnType("nvarchar(2000)");

            b.Property<int>("Percent")
                .HasColumnType("int")
                .HasColumnName("percent");

            b.Property<int>("Quantity")
                .HasColumnType("int")
                .HasColumnName("quantity");

            b.HasKey("Id");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.Cart", b =>
        {
            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Customer", "Customer")
                .WithMany("Carts")
                .HasForeignKey("CustomerId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Customer");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.CartDetail", b =>
        {
            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Cart", "Cart")
                .WithMany("CartDetails")
                .HasForeignKey("CartId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Product", "Product")
                .WithMany("CartDetails")
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Cart");

            b.Navigation("Product");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.Category", b =>
        {
            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Category", "Parent")
                .WithMany("SubCategories")
                .HasForeignKey("ParentId");

            b.Navigation("Parent");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.Customer", b =>
        {
            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.User", "User")
                .WithOne("Customer")
                .HasForeignKey("NET1814_MilkShop.Repositories.Data.Entities.Customer", "UserId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("User");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.CustomerAddress", b =>
        {
            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Customer", "User")
                .WithMany("CustomerAddresses")
                .HasForeignKey("UserId");

            b.Navigation("User");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.Order", b =>
        {
            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Customer", "Customer")
                .WithMany("Orders")
                .HasForeignKey("CustomerId");

            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.OrderStatus", "Status")
                .WithMany("Orders")
                .HasForeignKey("StatusId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Customer");

            b.Navigation("Status");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.OrderDetail", b =>
        {
            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Order", "Order")
                .WithMany("OrderDetails")
                .HasForeignKey("OrderId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Product", "Product")
                .WithMany("OrderDetails")
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Order");

            b.Navigation("Product");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.OrderLog", b =>
        {
            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Order", "Order")
                .WithMany("OrderLogs")
                .HasForeignKey("OrderId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Order");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.Post", b =>
        {
            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.User", "Author")
                .WithMany("Posts")
                .HasForeignKey("AuthorId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Author");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.PreorderProduct", b =>
        {
            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Product", "Product")
                .WithOne("PreorderProduct")
                .HasForeignKey("NET1814_MilkShop.Repositories.Data.Entities.PreorderProduct", "ProductId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Product");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.Product", b =>
        {
            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Brand", "Brand")
                .WithMany("Products")
                .HasForeignKey("BrandId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Category", "Category")
                .WithMany("Products")
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.ProductStatus", "ProductStatus")
                .WithMany()
                .HasForeignKey("StatusId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Unit", "Unit")
                .WithMany("Products")
                .HasForeignKey("UnitId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Brand");

            b.Navigation("Category");

            b.Navigation("ProductStatus");

            b.Navigation("Unit");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.ProductAttributeValue", b =>
        {
            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.ProductAttribute", "Attribute")
                .WithMany("ProductAttributeValues")
                .HasForeignKey("AttributeId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Product", "Product")
                .WithMany("ProductAttributeValues")
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Attribute");

            b.Navigation("Product");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.ProductImage", b =>
        {
            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Product", "Product")
                .WithMany("ProductImages")
                .HasForeignKey("ProductId");

            b.Navigation("Product");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.ProductReview", b =>
        {
            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Customer", "Customer")
                .WithMany()
                .HasForeignKey("CustomerId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Order", "Order")
                .WithMany()
                .HasForeignKey("OrderId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Product", "Product")
                .WithMany("ProductReviews")
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Customer");

            b.Navigation("Order");

            b.Navigation("Product");
        });

        modelBuilder.Entity<Report>(b =>
        {
            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Customer", "Customer")
                .WithMany()
                .HasForeignKey("CustomerId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Product", "Product")
                .WithMany()
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.ReportType", "ReportType")
                .WithMany("Reports")
                .HasForeignKey("ReportTypeId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Customer");

            b.Navigation("Product");

            b.Navigation("ReportType");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.User", b =>
        {
            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Role", "Role")
                .WithMany("Users")
                .HasForeignKey("RoleId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Role");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.Brand", b => { b.Navigation("Products"); });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.Cart", b => { b.Navigation("CartDetails"); });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.Category", b =>
        {
            b.Navigation("Products");

            b.Navigation("SubCategories");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.Customer", b =>
        {
            b.Navigation("Carts");

            b.Navigation("CustomerAddresses");

            b.Navigation("Orders");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.Order", b =>
        {
            b.Navigation("OrderDetails");

            b.Navigation("OrderLogs");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.OrderStatus",
            b => { b.Navigation("Orders"); });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.Product", b =>
        {
            b.Navigation("CartDetails");

            b.Navigation("OrderDetails");

            b.Navigation("PreorderProduct");

            b.Navigation("ProductAttributeValues");

            b.Navigation("ProductImages");

            b.Navigation("ProductReviews");
        });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.ProductAttribute",
            b => { b.Navigation("ProductAttributeValues"); });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.ReportType",
            b => { b.Navigation("Reports"); });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.Role", b => { b.Navigation("Users"); });

        modelBuilder.Entity("NET1814_MilkShop.Repositories.Data.Entities.Unit", b => { b.Navigation("Products"); });

        modelBuilder.Entity<User>(b =>
        {
            b.Navigation("Customer");

            b.Navigation("Posts");
        });

        modelBuilder.Entity<Conversation>(b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uniqueidentifier");

            b.Property<Guid>("BuyerId")
                .HasColumnType("uniqueidentifier")
                .HasColumnName("buyer_id");

            b.Property<Guid>("SellerId")
                .HasColumnType("uniqueidentifier")
                .HasColumnName("seller_id");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("created_at");

            b.Property<DateTime?>("ModifiedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("modified_at");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("deleted_at");

            b.HasKey("Id");

            b.HasIndex("BuyerId");
            b.HasIndex("SellerId");

            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.User", "Buyer")
                .WithMany()
                .HasForeignKey("BuyerId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.User", "Seller")
                .WithMany()
                .HasForeignKey("SellerId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            b.Navigation("Buyer");
            b.Navigation("Seller");
            b.Navigation("Messages");

            b.ToTable("conversations");
        });

        modelBuilder.Entity<Message>(b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uniqueidentifier");

            b.Property<Guid>("ConversationId")
                .HasColumnType("uniqueidentifier")
                .HasColumnName("conversation_id");

            b.Property<Guid>("SenderId")
                .HasColumnType("uniqueidentifier")
                .HasColumnName("sender_id");

            b.Property<string>("Content")
                .IsRequired()
                .HasColumnType("nvarchar(max)")
                .HasColumnName("content");

            b.Property<bool>("IsRead")
                .HasColumnName("is_read")
                .HasDefaultValue(false);

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("created_at");

            b.Property<DateTime?>("ModifiedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("modified_at");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp with time zone") // Chỉ định kiểu dữ liệu cho PostgreSQL
                .HasColumnName("deleted_at");

            b.HasKey("Id");

            b.HasIndex("ConversationId");
            b.HasIndex("SenderId");

            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.Conversation", "Conversation")
                .WithMany("Messages")
                .HasForeignKey("ConversationId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("NET1814_MilkShop.Repositories.Data.Entities.User", "Sender")
                .WithMany()
                .HasForeignKey("SenderId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            b.Navigation("Sender");
        });
    }

    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input)) { return input; }
        var startUnderscores = System.Text.RegularExpressions.Regex.Match(input, @"^_+");
        return startUnderscores + System.Text.RegularExpressions.Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }
}