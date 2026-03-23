using Microsoft.EntityFrameworkCore;
using NET1814_MilkShop.Repositories.CoreHelpers.Enum;
using NET1814_MilkShop.Repositories.Data;
using NET1814_MilkShop.Repositories.Data.Entities;
using NET1814_MilkShop.Repositories.Repositories.Interfaces;

namespace NET1814_MilkShop.Repositories.Repositories.Implementations;

public sealed class AuthenticationRepository : Repository<User>, IAuthenticationRepository
{
    public AuthenticationRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<User?> GetUserByUserNameNPassword(string username, string password, bool isCustomer)
    {
        var user = isCustomer
            ? await _query.Include(u => u.Role)
                .FirstOrDefaultAsync(x => username.ToLower().Equals(x.Username.ToLower()) &&
                    (x.RoleId == (int)RoleId.Buyer || x.RoleId == (int)RoleId.Seller))
            : await _query.Include(u => u.Role)
                .FirstOrDefaultAsync(x => username.ToLower().Equals(x.Username.ToLower()) &&
                    x.RoleId != (int)RoleId.Buyer && x.RoleId != (int)RoleId.Seller);
        if (user != null)
        {
            var isPasswordMatched = BCrypt.Net.BCrypt.Verify(password, user.Password);
            Console.WriteLine($"[DEBUG] Login attempt for user '{username}': User found. Password match: {isPasswordMatched}");
            if (isPasswordMatched)
            {
                return user;
            }
        }
        else
        {
            Console.WriteLine($"[DEBUG] Login attempt for user '{username}': User NOT found.");
        }

        return null;
    }
}