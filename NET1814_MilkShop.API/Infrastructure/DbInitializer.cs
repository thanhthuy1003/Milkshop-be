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

        // Look for any roles.
        if (context.Roles.Any())
        {
            return; // DB has been seeded
        }

        var roles = new Role[]
        {
            new Role { Id = (int)RoleId.Admin, Name = RoleId.Admin.ToString(), CreatedAt = DateTime.UtcNow },
            new Role { Id = (int)RoleId.Staff, Name = RoleId.Staff.ToString(), CreatedAt = DateTime.UtcNow },
            new Role { Id = (int)RoleId.Customer, Name = RoleId.Customer.ToString(), CreatedAt = DateTime.UtcNow }
        };

        foreach (Role r in roles)
        {
            context.Roles.Add(r);
        }

        // Enable identity insert if needed, but for now try simple add.
        // If Id is identity column, setting it explicitly without Identity_Insert ON might fail.
        // However, EF Core with SQL Server often handles this if formatted correctly. 
        // A safer way for identity columns is to let DB generate IDs, but we need specific IDs.
        // Let's try raw SQL for Identity Insert if regular add fails, but let's try regular first.
        // Actually, for explicit IDs on identity column, we need:
        // context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT roles ON");
        // ... save ...
        // context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT roles OFF");
        
        // Wait, if I just add them, they get 1, 2, 3 automatically since table is empty.
        // So I will REMOVE the explicit Id assignment to be safe and let auto-increment handle it, 
        // ASSUMING they are inserted in order.
        
        /*
        var rolesAuto = new Role[]
        {
            new Role { Name = RoleId.Admin.ToString(), CreatedAt = DateTime.UtcNow },
            new Role { Name = RoleId.Staff.ToString(), CreatedAt = DateTime.UtcNow },
            new Role { Name = RoleId.Customer.ToString(), CreatedAt = DateTime.UtcNow }
        };
        foreach (Role r in rolesAuto)
        {
            context.Roles.Add(r);
        }
        */
        
        // BETTER APPROACH: Force IDs with Identity Insert
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
