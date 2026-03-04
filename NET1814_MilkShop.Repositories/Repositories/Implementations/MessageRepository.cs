using Microsoft.EntityFrameworkCore;
using NET1814_MilkShop.Repositories.Data;
using NET1814_MilkShop.Repositories.Data.Entities;
using NET1814_MilkShop.Repositories.Repositories.Interfaces;

namespace NET1814_MilkShop.Repositories.Repositories.Implementations;

public class MessageRepository : IMessageRepository
{
    private readonly AppDbContext _context;

    public MessageRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Message> GetByConversationIdQuery(Guid conversationId)
    {
        return _context.Messages
            .Include(m => m.Sender)
            .Where(m => m.ConversationId == conversationId && m.DeletedAt == null)
            .AsNoTracking();
    }

    public async Task<int> GetUnreadCountAsync(Guid receiverId, Guid conversationId)
    {
        return await _context.Messages
            .CountAsync(m =>
                m.ConversationId == conversationId &&
                m.SenderId != receiverId &&
                !m.IsRead &&
                m.DeletedAt == null);
    }

    public void Add(Message message)
    {
        _context.Messages.Add(message);
    }

    public void UpdateRange(IEnumerable<Message> messages)
    {
        _context.Messages.UpdateRange(messages);
    }
}
