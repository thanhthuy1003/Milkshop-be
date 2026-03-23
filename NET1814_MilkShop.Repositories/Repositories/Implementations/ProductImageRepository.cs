using Microsoft.EntityFrameworkCore;
using NET1814_MilkShop.Repositories.Data;
using NET1814_MilkShop.Repositories.Data.Entities;
using NET1814_MilkShop.Repositories.Models.ProductModels;
using NET1814_MilkShop.Repositories.Repositories.Interfaces;

namespace NET1814_MilkShop.Repositories.Repositories.Implementations;

public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
{
    public ProductImageRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<List<ProductImageModel>> GetByProductIdAsync(Guid id)
    {
        var query = _query.Where(x => x.ProductId == id);
        return await query.Select(x => new ProductImageModel
            {
                Id = x.Id,
                ProductId = id,
                ImageUrl = x.ImageUrl
            })
            .ToListAsync();
    }
}