using System.ComponentModel.DataAnnotations;

namespace NET1814_MilkShop.Repositories.Models.ConversationModels;

public class SendMessageModel
{
    [Required(ErrorMessage = "Content is required")]
    public string Content { get; set; } = null!;
}
