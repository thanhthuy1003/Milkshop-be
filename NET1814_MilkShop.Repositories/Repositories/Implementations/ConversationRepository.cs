using Microsoft.EntityFrameworkCore;
using NET1814_MilkShop.Repositories.Data;
using NET1814_MilkShop.Repositories.Data.Entities;
using NET1814_MilkShop.Repositories.Repositories.Interfaces;

namespace NET1814_MilkShop.Repositories.Repositories.Implementations;

public class ConversationRepository : IConversationRepository
{
    private readonly AppDbContext _context;

    public ConversationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Conversation?> GetByIdAsync(Guid id)
    {
        return await _context.Conversations
            .Include(c => c.Buyer)
            .Include(c => c.Seller)
            .FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null);
    }

    public async Task<Conversation?> GetByParticipantsAsync(Guid buyerId, Guid sellerId)
    {
        return await _context.Conversations
            .FirstOrDefaultAsync(c =>
                c.BuyerId == buyerId && c.SellerId == sellerId && c.DeletedAt == null);
    }

    public IQueryable<Conversation> GetByUserIdQuery(Guid userId)
    {
        return _context.Conversations
            .Include(c => c.Buyer)
            .Include(c => c.Seller)
            .Where(c => (c.BuyerId == userId || c.SellerId == userId) && c.DeletedAt == null)
            .AsNoTracking();
    }

    public void Add(Conversation conversation)
    {
        _context.Conversations.Add(conversation);
    }
}
