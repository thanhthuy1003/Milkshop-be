using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NET1814_MilkShop.API.CoreHelpers.ActionFilters;
using NET1814_MilkShop.API.CoreHelpers.Extensions;
using NET1814_MilkShop.Repositories.Models.AddressModels;
using NET1814_MilkShop.Repositories.Models.OrderModels;
using NET1814_MilkShop.Repositories.Models.UserModels;
using NET1814_MilkShop.Services.Services.Interfaces;
using ILogger = Serilog.ILogger;

namespace NET1814_MilkShop.API.Controllers;

[ApiController]
[Route("api")]
public class UserController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IUserService _userService;
    private readonly ICustomerService _customerService;
    private readonly IAddressService _addressService;
    private readonly IOrderService _orderService;

    public UserController(ILogger logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _userService = serviceProvider.GetRequiredService<IUserService>();
        _customerService = serviceProvider.GetRequiredService<ICustomerService>();
        _addressService = serviceProvider.GetRequiredService<IAddressService>();
        _orderService = serviceProvider.GetRequiredService<IOrderService>();
    }

    #region User

    [HttpPost("users")]
    [Authorize(AuthenticationSchemes = "Access", Roles = "1")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserModel model)
    {
        _logger.Information("Create user");
        var response = await _userService.CreateUserAsync(model);
        return ResponseExtension.Result(response);
    }

    [HttpGet("users")]
    [Authorize(AuthenticationSchemes = "Access", Roles = "1")]
    [ServiceFilter(typeof(UserExistsFilter))]
    public async Task<IActionResult> GetUsers([FromQuery] UserQueryModel request)
    {
        _logger.Information("Get all users");
        var response = await _userService.GetUsersAsync(request);
        return ResponseExtension.Result(response);
    }

    [HttpPatch("users/{id}")]
    [Authorize(AuthenticationSchemes = "Access", Roles = "1")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserModel model)
    {
        var response = await _userService.UpdateUserAsync(id, model);
        return ResponseExtension.Result(response);
    }

    #endregion

    #region Customer

    [HttpGet]
    [Route("customers")]
    [Authorize(AuthenticationSchemes = "Access", Roles = "1,2")]
    [ServiceFilter(typeof(UserExistsFilter))]
    public async Task<IActionResult> GetCustomers([FromQuery] CustomerQueryModel request)
    {
        _logger.Information("Get all customers");
        var response = await _customerService.GetCustomersAsync(request);
        return ResponseExtension.Result(response);
    }

    [HttpGet]
    [Route("customers/{id}")]
    [Authorize(AuthenticationSchemes = "Access", Roles = "1,2")]
    [ServiceFilter(typeof(UserExistsFilter))]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        _logger.Information("Get customer by id");
        var response = await _customerService.GetByIdAsync(id);
        return ResponseExtension.Result(response);
    }

    #endregion

    #region Account

    [HttpGet]
    [Route("user/account/profile")]
    [Authorize(AuthenticationSchemes = "Access")]
    [ServiceFilter(typeof(UserExistsFilter))]
    public async Task<IActionResult> GetCurrentAuthUser()
    {
        _logger.Information("Get current user");
        var userId = (HttpContext.Items["UserId"] as Guid?)!.Value;
        var response = await _customerService.GetByIdAsync(userId);
        return ResponseExtension.Result(response);
    }

    [HttpPatch]
    [Route("user/account/profile")]
    [Authorize(AuthenticationSchemes = "Access", Roles = "3,4")]
    [ServiceFilter(typeof(UserExistsFilter))]
    public async Task<IActionResult> ChangeInfo([FromBody] ChangeUserInfoModel model)
    {
        _logger.Information("Change user info");
        var userId = (HttpContext.Items["UserId"] as Guid?)!.Value;
        var response = await _customerService.ChangeInfoAsync(userId, model);
        return ResponseExtension.Result(response);
    }

    [HttpPatch]
    [Route("user/account/change-password")]
    [Authorize(AuthenticationSchemes = "Access")]
    [ServiceFilter(typeof(UserExistsFilter))]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
    {
        _logger.Information("Change user password");
        var userId = (HttpContext.Items["UserId"] as Guid?)!.Value;
        var response = await _userService.ChangePasswordAsync(userId, model);
        return ResponseExtension.Result(response);
    }

    [HttpPatch]
    [Route("user/account/change-username")]
    [Authorize(AuthenticationSchemes = "Access")]
    [ServiceFilter(typeof(UserExistsFilter))]
    public async Task<IActionResult> ChangeUsername([FromBody] ChangeUsernameModel model)
    {
        _logger.Information("Change user username");
        var userId = (HttpContext.Items["UserId"] as Guid?)!.Value;
        var response = await _userService.ChangeUsernameAsync(userId, model);
        return ResponseExtension.Result(response);
    }

    [HttpGet]
    [Route("users/{userId}/addresses")]
    [Authorize(AuthenticationSchemes = "Access")]
    [ServiceFilter(typeof(UserExistsFilter))]
    public async Task<IActionResult> GetCustomerAddresses(Guid userId)
    {
        _logger.Information("Get customer addresses");
        var response = await _addressService.GetAddressesByCustomerId(userId);
        return ResponseExtension.Result(response);
    }

    [HttpPost]
    [Route("user/account/addresses")]
    [Authorize(AuthenticationSchemes = "Access", Roles = "3,4")]
    [ServiceFilter(typeof(UserExistsFilter))]
    public async Task<IActionResult> CreateCustomerAddress([FromBody] CreateAddressModel request)
    {
        _logger.Information("Create customer address");
        var customerId = (HttpContext.Items["UserId"] as Guid?)!.Value;
        var response = await _addressService.CreateAddressAsync(customerId, request);
        return ResponseExtension.Result(response);
    }

    [HttpPatch]
    [Route("user/account/addresses/{id}")]
    [Authorize(AuthenticationSchemes = "Access", Roles = "3,4")]
    [ServiceFilter(typeof(UserExistsFilter))]
    public async Task<IActionResult> UpdateCustomerAddress(int id, [FromBody] UpdateAddressModel request)
    {
        _logger.Information("Update Customer Address");
        var customerId = (HttpContext.Items["UserId"] as Guid?)!.Value;
        var response = await _addressService.UpdateAddressAsync(customerId, id, request);
        return ResponseExtension.Result(response);
    }

    [HttpDelete]
    [Route("user/account/addresses/{id}")]
    [Authorize(AuthenticationSchemes = "Access", Roles = "3,4")]
    [ServiceFilter(typeof(UserExistsFilter))]
    public async Task<IActionResult> DeleteCustomerAddress(int id)
    {
        _logger.Information("Delete Customer Address");
        var customerId = (HttpContext.Items["UserId"] as Guid?)!.Value;
        var response = await _addressService.DeleteAddressAsync(customerId, id);
        return ResponseExtension.Result(response);
    }

    #endregion

    #region OrderHistory

    [HttpGet]
    [Route("users/{userId}/orders")]
    [Authorize(AuthenticationSchemes = "Access")]
    [ServiceFilter(typeof(UserExistsFilter))]
    public async Task<IActionResult> GetOrderHistory(Guid userId, [FromQuery] OrderHistoryQueryModel model)
    {
        _logger.Information("Get order history");
        var res = await _orderService.GetOrderHistoryAsync(userId, model);
        return ResponseExtension.Result(res);
    }

    [HttpGet("users/{userId}/orders/{id}")]
    [Authorize(AuthenticationSchemes = "Access")]
    [ServiceFilter(typeof(UserExistsFilter))]
    public async Task<IActionResult> GetOrderHistoryDetail(Guid userId, Guid id)
    {
        _logger.Information("Get order detail history");
        var res = await _orderService.GetOrderHistoryDetailAsync(userId, id);
        return ResponseExtension.Result(res);
    }

    [HttpPatch("user/orders/{id}/cancel")]
    [Authorize(AuthenticationSchemes = "Access", Roles = "3")]
    [ServiceFilter(typeof(UserExistsFilter))]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        _logger.Information("Cancel order");
        var userId = (HttpContext.Items["UserId"] as Guid?)!.Value;
        var res = await _orderService.CancelOrderAsync(userId, id);
        return ResponseExtension.Result(res);
    }

    #endregion
}