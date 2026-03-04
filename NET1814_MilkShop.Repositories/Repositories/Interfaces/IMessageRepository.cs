using NET1814_MilkShop.Repositories.Data.Entities;

namespace NET1814_MilkShop.Repositories.Repositories.Interfaces;

public interface IMessageRepository
{
    IQueryable<Message> GetByConversationIdQuery(Guid conversationId);
    Task<int> GetUnreadCountAsync(Guid receiverId, Guid conversationId);
    void Add(Message message);
    void UpdateRange(IEnumerable<Message> messages);
}
