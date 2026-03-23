using NET1814_MilkShop.Repositories.Models;

namespace NET1814_MilkShop.Services.Services.Interfaces;

public interface IPaymentService
{
    public Task<ResponseModel> CreatePaymentLink(long orderCode);
    public Task<ResponseModel> GetPaymentLinkInformation(Guid orderId);
    public Task<ResponseModel> CancelPaymentLink(Guid orderId);
}