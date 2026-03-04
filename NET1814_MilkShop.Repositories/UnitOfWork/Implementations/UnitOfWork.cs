using System.Data;
using Microsoft.EntityFrameworkCore;
using NET1814_MilkShop.Repositories.Data;
using NET1814_MilkShop.Repositories.Data.Interfaces;
using NET1814_MilkShop.Repositories.UnitOfWork.Interfaces;

namespace NET1814_MilkShop.Repositories.UnitOfWork.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        int result;
        await using var dbContextTransaction = await _context.Database.BeginTransactionAsync();
        try
        {
            UpdateAuditableEntities();
            result = await _context.SaveChangesAsync();
            await dbContextTransaction.CommitAsync();
        }
        catch (Exception)
        {
            result = -1;
            await dbContextTransaction.RollbackAsync();
        }

        return result;
    }

    private void UpdateAuditableEntities()
    {
        var entries = _context.ChangeTracker.Entries<IAuditableEntity>();

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(a => a.CreatedAt).CurrentValue = DateTime.UtcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(a => a.ModifiedAt).CurrentValue = DateTime.UtcNow;
            }
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public void Detach<TEntity>(TEntity entity) where TEntity : class
    {
        _context.Entry(entity).State = EntityState.Detached;
    }
}