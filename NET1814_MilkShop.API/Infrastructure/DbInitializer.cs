using Microsoft.EntityFrameworkCore;
using NET1814_MilkShop.Repositories.CoreHelpers.Enum;
using NET1814_MilkShop.Repositories.Data;
using NET1814_MilkShop.Repositories.Data.Entities;

namespace NET1814_MilkShop.API.Infrastructure;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        Console.WriteLine("[DEBUG] DbInitializer.Initialize started.");
        // DB-first: KHÔNG tạo schema, chỉ làm việc với DB đã tồn tại
        if (!context.Database.CanConnect())
        {
            throw new InvalidOperationException(
                "Cannot connect to database. Make sure the database exists and the connection string is correct.");
        }

        // Seed Roles if missing
        if (!context.Roles.Any())
        {
            var utcNow = DateTime.UtcNow;
            var roles = new[]
            {
                new Role { Id = (int)RoleId.Admin,  Name = RoleId.Admin.ToString(),  CreatedAt = utcNow },
                new Role { Id = (int)RoleId.Staff,  Name = RoleId.Staff.ToString(),  CreatedAt = utcNow },
                new Role { Id = (int)RoleId.Buyer,  Name = RoleId.Buyer.ToString(),  CreatedAt = utcNow },
                new Role { Id = (int)RoleId.Seller, Name = RoleId.Seller.ToString(), CreatedAt = utcNow }
            };
            context.Roles.AddRange(roles);
            context.SaveChanges();
        }

        // Ensure 'admin' user exists and has the correct password
        var admin = context.Users.FirstOrDefault(u => u.Username == "admin");
        if (admin == null)
        {
            Console.WriteLine("[DEBUG] Seeding 'admin' user...");
            admin = new User
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                FirstName = "Admin",
                LastName = "System",
                RoleId = (int)RoleId.Admin,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            context.Users.Add(admin);
            Console.WriteLine("[DEBUG] 'admin' user seeded.");
        }
        else
        {
            Console.WriteLine("[DEBUG] Resetting existing 'admin' password...");
            admin.Password = BCrypt.Net.BCrypt.HashPassword("Admin@123");
            admin.IsActive = true;
            context.Users.Update(admin);
        }

        // Also ensure 'admin_test' has the correct password if it exists
        var adminTest = context.Users.FirstOrDefault(u => u.Username == "admin_test");
        if (adminTest != null)
        {
            Console.WriteLine("[DEBUG] Resetting 'admin_test' password...");
            adminTest.Password = BCrypt.Net.BCrypt.HashPassword("Admin@123");
            adminTest.IsActive = true;
            context.Users.Update(adminTest);
        }

        context.SaveChanges();
        Console.WriteLine("[DEBUG] DbInitializer: All admin passwords synchronized.");
        Console.WriteLine("[DEBUG] DbInitializer.Initialize finished.");
    }
}