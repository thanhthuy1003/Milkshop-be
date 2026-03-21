using Microsoft.EntityFrameworkCore;
using NET1814_MilkShop.Repositories.CoreHelpers.Enum;
using NET1814_MilkShop.Repositories.Data;
using NET1814_MilkShop.Repositories.Data.Entities;

namespace NET1814_MilkShop.API.Infrastructure;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        // DB-first: KHÔNG tạo schema, chỉ làm việc với DB đã tồn tại
        if (!context.Database.CanConnect())
        {
            throw new InvalidOperationException(
                "Cannot connect to database. Make sure PostgreSQL database exists and the connection string is correct.");
        }

        // Nếu roles đã tồn tại (do restore từ db.zip) thì không làm gì thêm
        if (context.Roles.Any())
        {
            return;
        }

        var utcNow = DateTime.UtcNow;

        var roles = new[]
        {
            new Role { Id = (int)RoleId.Admin,  Name = RoleId.Admin.ToString(),  CreatedAt = utcNow },
            new Role { Id = (int)RoleId.Staff,  Name = RoleId.Staff.ToString(),  CreatedAt = utcNow },
            new Role { Id = (int)RoleId.Buyer,  Name = RoleId.Buyer.ToString(),  CreatedAt = utcNow },
            new Role { Id = (int)RoleId.Seller, Name = RoleId.Seller.ToString(), CreatedAt = utcNow }
        };

        // Không dùng IDENTITY_INSERT, PostgreSQL cho phép insert Id explicit
        context.Roles.AddRange(roles);
        context.SaveChanges();
    }
}