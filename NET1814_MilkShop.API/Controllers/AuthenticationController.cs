using Microsoft.AspNetCore.Mvc;
using NET1814_MilkShop.API.CoreHelpers.Extensions;
using NET1814_MilkShop.Repositories.Models.UserModels;
using NET1814_MilkShop.Services.Services.Interfaces;
using ILogger = Serilog.ILogger;

namespace NET1814_MilkShop.API.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : Controller
{
    private readonly ILogger _logger;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(
        ILogger logger,
        IWebHostEnvironment webHostEnvironment,
        IServiceProvider serviceProvider
    )
    {
        _logger = logger;
        _authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
    {
        _logger.Information("Sign up");
        var response = await _authenticationService.SignUpAsync(model);
        return ResponseExtension.Result(response);
    }

    [HttpGet("verify")]
    public async Task<IActionResult> VerifyAccount([FromQuery] string token)
    {
        _logger.Information("Verify Account");
        var response = await _authenticationService.VerifyAccountAsync(token);
        return ResponseExtension.Result(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(RequestLoginModel model)
    {
        _logger.Information("Login");
        var res = await _authenticationService.LoginAsync(model);
        return ResponseExtension.Result(res);
    }

    [HttpPost("dashboard/login")]
    public async Task<IActionResult> AdminLogin(RequestLoginModel model)
    {
        _logger.Information("Login");
        var response = await _authenticationService.DashBoardLoginAsync(model);
        return ResponseExtension.Result(response);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel request)
    {
        _logger.Information("Forgot Password");
        var response = await _authenticationService.ForgotPasswordAsync(request);
        return ResponseExtension.Result(response);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel request)
    {
        _logger.Information("Reset Password");
        var response = await _authenticationService.ResetPasswordAsync(request);
        return ResponseExtension.Result(response);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromQuery] string token)
    {
        _logger.Information("Refresh Token");
        var response = await _authenticationService.RefreshTokenAsync(token);
        return ResponseExtension.Result(response);
    }

    [HttpPost("activate-account")]
    public async Task<IActionResult> ActivateAccount([FromQuery] string email)
    {
        _logger.Information("Activate Account");
        var response = await _authenticationService.ActivateAccountAsync(email);
        return ResponseExtension.Result(response);
    }

    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromQuery] string token)
    {
        _logger.Information("Google Login");
        var response = await _authenticationService.GoogleLoginAsync(token);
        return ResponseExtension.Result(response);
    }
}