namespace NET1814_MilkShop.Repositories.Models.ConversationModels;

public class ConversationModel
{
    public Guid Id { get; set; }
    public Guid BuyerId { get; set; }
    public string BuyerName { get; set; } = null!;
    public Guid SellerId { get; set; }
    public string SellerName { get; set; } = null!;
    public int UnreadCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
