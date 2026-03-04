using Microsoft.EntityFrameworkCore;
using NET1814_MilkShop.Repositories.CoreHelpers.Constants;
using NET1814_MilkShop.Repositories.Data.Entities;
using NET1814_MilkShop.Repositories.Models;
using NET1814_MilkShop.Repositories.Models.ConversationModels;
using NET1814_MilkShop.Repositories.Repositories.Interfaces;
using NET1814_MilkShop.Repositories.UnitOfWork.Interfaces;
using NET1814_MilkShop.Services.CoreHelpers;
using NET1814_MilkShop.Services.Services.Interfaces;

namespace NET1814_MilkShop.Services.Services.Implementations;

public class ConversationService : IConversationService
{
    private readonly IConversationRepository _conversationRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ConversationService(
        IConversationRepository conversationRepository,
        IMessageRepository messageRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _conversationRepository = conversationRepository;
        _messageRepository = messageRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseModel> GetMyConversationsAsync(Guid userId, ConversationQueryModel query)
    {
        var conversationQuery = _conversationRepository.GetByUserIdQuery(userId)
            .OrderByDescending(c => c.ModifiedAt ?? c.CreatedAt)
            .Select(c => new ConversationModel
            {
                Id = c.Id,
                BuyerId = c.BuyerId,
                BuyerName = (c.Buyer!.FirstName ?? "") + " " + (c.Buyer.LastName ?? ""),
                SellerId = c.SellerId,
                SellerName = (c.Seller!.FirstName ?? "") + " " + (c.Seller.LastName ?? ""),
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.ModifiedAt
            });

        var result = await PagedList<ConversationModel>.CreateAsync(
            conversationQuery, query.Page, query.PageSize);

        return ResponseModel.Success(ResponseConstants.Get("cuộc trò chuyện", result.TotalCount > 0), result);
    }

    public async Task<ResponseModel> GetOrCreateConversationAsync(Guid myUserId, Guid otherUserId)
    {
        var me = await _userRepository.GetByIdAsync(myUserId);
        if (me == null)
            return ResponseModel.BadRequest(ResponseConstants.NotFound("Người dùng"));

        var other = await _userRepository.GetByIdAsync(otherUserId);
        if (other == null)
            return ResponseModel.BadRequest(ResponseConstants.NotFound("Người dùng kia"));

        if (myUserId == otherUserId)
            return ResponseModel.BadRequest("Không thể tạo hội thoại với chính mình");

        Guid buyerId, sellerId;
        if (me.RoleId == 3)
        {
            buyerId = myUserId;
            sellerId = otherUserId;
        }
        else
        {
            buyerId = otherUserId;
            sellerId = myUserId;
        }

        var existing = await _conversationRepository.GetByParticipantsAsync(buyerId, sellerId);
        if (existing != null)
        {
            return ResponseModel.Success(ResponseConstants.Get("cuộc trò chuyện", true),
                MapToConversationModel(existing));
        }

        var conversation = new Conversation
        {
            Id = Guid.NewGuid(),
            BuyerId = buyerId,
            SellerId = sellerId
        };
        _conversationRepository.Add(conversation);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result > 0)
        {
            var created = await _conversationRepository.GetByIdAsync(conversation.Id);
            return ResponseModel.Success(ResponseConstants.Create("cuộc trò chuyện", true),
                MapToConversationModel(created!));
        }

        return ResponseModel.Error(ResponseConstants.Create("cuộc trò chuyện", false));
    }

    public async Task<ResponseModel> GetMessagesAsync(Guid userId, Guid conversationId, ConversationQueryModel query)
    {
        var conversation = await _conversationRepository.GetByIdAsync(conversationId);
        if (conversation == null)
            return ResponseModel.BadRequest(ResponseConstants.NotFound("Cuộc trò chuyện"));

        if (conversation.BuyerId != userId && conversation.SellerId != userId)
            return ResponseModel.BadRequest(ResponseConstants.NotEnoughPermission);

        var messageQuery = _messageRepository.GetByConversationIdQuery(conversationId)
            .OrderBy(m => m.CreatedAt)
            .Select(m => new MessageModel
            {
                Id = m.Id,
                ConversationId = m.ConversationId,
                SenderId = m.SenderId,
                SenderName = (m.Sender!.FirstName ?? "") + " " + (m.Sender.LastName ?? ""),
                Content = m.Content,
                IsRead = m.IsRead,
                CreatedAt = m.CreatedAt
            });

        var result = await PagedList<MessageModel>.CreateAsync(
            messageQuery, query.Page, query.PageSize);

        return ResponseModel.Success(ResponseConstants.Get("tin nhắn", result.TotalCount > 0), result);
    }

    public async Task<ResponseModel> SendMessageAsync(Guid senderId, Guid conversationId, SendMessageModel model)
    {
        var conversation = await _conversationRepository.GetByIdAsync(conversationId);
        if (conversation == null)
            return ResponseModel.BadRequest(ResponseConstants.NotFound("Cuộc trò chuyện"));

        if (conversation.BuyerId != senderId && conversation.SellerId != senderId)
            return ResponseModel.BadRequest(ResponseConstants.NotEnoughPermission);

        if (string.IsNullOrWhiteSpace(model.Content))
            return ResponseModel.BadRequest("Nội dung tin nhắn không được để trống");

        var message = new Message
        {
            Id = Guid.NewGuid(),
            ConversationId = conversationId,
            SenderId = senderId,
            Content = model.Content.Trim(),
            IsRead = false
        };
        _messageRepository.Add(message);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result > 0)
        {
            return ResponseModel.Success(ResponseConstants.Create("tin nhắn", true), null);
        }

        return ResponseModel.Error(ResponseConstants.Create("tin nhắn", false));
    }

    public async Task<ResponseModel> MarkMessagesReadAsync(Guid userId, Guid conversationId)
    {
        var conversation = await _conversationRepository.GetByIdAsync(conversationId);
        if (conversation == null)
            return ResponseModel.BadRequest(ResponseConstants.NotFound("Cuộc trò chuyện"));

        if (conversation.BuyerId != userId && conversation.SellerId != userId)
            return ResponseModel.BadRequest(ResponseConstants.NotEnoughPermission);

        var unreadMessages = await _messageRepository
            .GetByConversationIdQuery(conversationId)
            .Where(m => m.SenderId != userId && !m.IsRead)
            .ToListAsync();

        if (!unreadMessages.Any())
            return ResponseModel.Success("Không có tin nhắn chưa đọc", null);

        foreach (var msg in unreadMessages)
        {
            msg.IsRead = true;
        }
        _messageRepository.UpdateRange(unreadMessages);
        await _unitOfWork.SaveChangesAsync();
        return ResponseModel.Success("Đã đánh dấu tin nhắn là đã đọc", null);
    }

    private static ConversationModel MapToConversationModel(Conversation c) => new ConversationModel
    {
        Id = c.Id,
        BuyerId = c.BuyerId,
        BuyerName = (c.Buyer?.FirstName ?? "") + " " + (c.Buyer?.LastName ?? ""),
        SellerId = c.SellerId,
        SellerName = (c.Seller?.FirstName ?? "") + " " + (c.Seller?.LastName ?? ""),
        CreatedAt = c.CreatedAt,
        UpdatedAt = c.ModifiedAt
    };
}
