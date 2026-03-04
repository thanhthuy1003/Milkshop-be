using Microsoft.EntityFrameworkCore;
using NET1814_MilkShop.Repositories.CoreHelpers.Enum;
using NET1814_MilkShop.Repositories.Data;
using NET1814_MilkShop.Repositories.Data.Entities;

namespace NET1814_MilkShop.API.Infrastructure;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Roles.Any())
        {
            return;
        }

        var roles = new Role[]
        {
            new Role { Id = (int)RoleId.Admin, Name = RoleId.Admin.ToString(), CreatedAt = DateTime.UtcNow },
            new Role { Id = (int)RoleId.Staff, Name = RoleId.Staff.ToString(), CreatedAt = DateTime.UtcNow },
            new Role { Id = (int)RoleId.Buyer, Name = RoleId.Buyer.ToString(), CreatedAt = DateTime.UtcNow },
            new Role { Id = (int)RoleId.Seller, Name = RoleId.Seller.ToString(), CreatedAt = DateTime.UtcNow }
        };

        using (var transaction = context.Database.BeginTransaction())
        {
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT roles ON");
            context.Roles.AddRange(roles);
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT roles OFF");
            transaction.Commit();
        }
    }
}
