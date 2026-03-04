using NET1814_MilkShop.Repositories.Models;
using NET1814_MilkShop.Repositories.Models.ConversationModels;

namespace NET1814_MilkShop.Services.Services.Interfaces;

public interface IConversationService
{
    Task<ResponseModel> GetMyConversationsAsync(Guid userId, ConversationQueryModel query);
    Task<ResponseModel> GetOrCreateConversationAsync(Guid myUserId, Guid otherUserId);
    Task<ResponseModel> GetMessagesAsync(Guid userId, Guid conversationId, ConversationQueryModel query);
    Task<ResponseModel> SendMessageAsync(Guid senderId, Guid conversationId, SendMessageModel model);
    Task<ResponseModel> MarkMessagesReadAsync(Guid userId, Guid conversationId);
}
