using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NET1814_MilkShop.API.CoreHelpers.ActionFilters;
using NET1814_MilkShop.API.CoreHelpers.Extensions;
using NET1814_MilkShop.Repositories.Models.CartModels;
using NET1814_MilkShop.Services.Services.Interfaces;
using ILogger = Serilog.ILogger;

namespace NET1814_MilkShop.API.Controllers;

[ApiController]
public class CartController : Controller
{
    private readonly ILogger _logger;
    private readonly ICartService _cartService;

    public CartController(ILogger logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _cartService = serviceProvider.GetRequiredService<ICartService>();
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Access", Roles = "3")]
    [ServiceFilter(typeof(UserExistsFilter))]
    [Route("api/user/{userId}/cart")]
    public async Task<IActionResult> GetCartAsync(Guid userId, [FromQuery] CartQueryModel model)
    {
        var current = (HttpContext.Items["UserId"] as Guid?)!.Value;
        _logger.Information($"Get cart of customer {userId}");
        if (current != userId)
        {
            return Unauthorized();
        }

        var response = await _cartService.GetCartAsync(userId, model);
        return ResponseExtension.Result(response);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Access", Roles = "3")]
    [ServiceFilter(typeof(UserExistsFilter))]
    [Route("api/user/{userId}/cart")]
    public async Task<IActionResult> AddToCartAsync(Guid userId, [FromBody] AddToCartModel model)
    {
        var current = (HttpContext.Items["UserId"] as Guid?)!.Value;
        if (current != userId)
        {
            return Unauthorized();
        }

        _logger.Information($"Add product {model.ProductId} to cart of customer {userId}");
        var response = await _cartService.AddToCartAsync(userId, model);
        return ResponseExtension.Result(response);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = "Access", Roles = "3")]
    [ServiceFilter(typeof(UserExistsFilter))]
    [Route("api/user/{userId}/cart")]
    public async Task<IActionResult> UpdateCartAsync(Guid userId)
    {
        var current = (HttpContext.Items["UserId"] as Guid?)!.Value;
        if (current != userId)
        {
            return Unauthorized();
        }

        _logger.Information($"Update cart of customer {userId} with new product data");
        var response = await _cartService.UpdateCartAsync(userId);
        return ResponseExtension.Result(response);
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = "Access", Roles = "3")]
    [Route("api/user/{userId}/cart")]
    public async Task<IActionResult> ClearCartAsync(Guid userId)
    {
        var current = (HttpContext.Items["UserId"] as Guid?)!.Value;
        if (current != userId)
        {
            return Unauthorized();
        }

        _logger.Information($"Clear cart of customer {userId}");
        var response = await _cartService.ClearCartAsync(userId);
        return ResponseExtension.Result(response);
    }

    [HttpPatch]
    [Authorize(AuthenticationSchemes = "Access", Roles = "3")]
    [ServiceFilter(typeof(UserExistsFilter))]
    [Route("api/user/{userId}/cart/{productId}")]
    public async Task<IActionResult> UpdateCartItemAsync(Guid userId, Guid productId, [FromBody] UpdateCartItemModel model)
    {
        var current = (HttpContext.Items["UserId"] as Guid?)!.Value;
        if (current != userId)
        {
            return Unauthorized();
        }

        _logger.Information($"Update product {productId} in cart of customer {userId}");
        var response = await _cartService.UpdateCartItemAsync(userId, productId, model);
        return ResponseExtension.Result(response);
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = "Access", Roles = "3")]
    [ServiceFilter(typeof(UserExistsFilter))]
    [Route("api/user/{userId}/cart/{productId}")]
    public async Task<IActionResult> DeleteCartItemAsync(Guid userId, Guid productId)
    {
        var current = (HttpContext.Items["UserId"] as Guid?)!.Value;
        if (current != userId)
        {
            return Unauthorized();
        }

        _logger.Information($"Delete product {productId} from cart of customer {userId}");
        var response = await _cartService.DeleteCartItemAsync(userId, productId);
        return ResponseExtension.Result(response);
    }
}