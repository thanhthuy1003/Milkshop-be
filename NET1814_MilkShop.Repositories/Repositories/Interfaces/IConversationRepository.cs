using NET1814_MilkShop.Repositories.Data.Entities;

namespace NET1814_MilkShop.Repositories.Repositories.Interfaces;

public interface IConversationRepository
{
    Task<Conversation?> GetByIdAsync(Guid id);
    Task<Conversation?> GetByParticipantsAsync(Guid buyerId, Guid sellerId);
    IQueryable<Conversation> GetByUserIdQuery(Guid userId);
    void Add(Conversation conversation);
}
