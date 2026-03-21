using System.Reflection;
using System.Text;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NET1814_MilkShop.API.CoreHelpers.ActionFilters;
using NET1814_MilkShop.API.Infrastructure;
using NET1814_MilkShop.Repositories.Data;
using NET1814_MilkShop.Repositories.Models.MailModels;
using NET1814_MilkShop.Repositories.Repositories.Implementations;
using NET1814_MilkShop.Repositories.Repositories.Interfaces;
using NET1814_MilkShop.Repositories.UnitOfWork.Implementations;
using NET1814_MilkShop.Repositories.UnitOfWork.Interfaces;
using NET1814_MilkShop.Services.CoreHelpers.Extensions.Implementations;
using NET1814_MilkShop.Services.CoreHelpers.Extensions.Interfaces;
using NET1814_MilkShop.Services.Services.Implementations;
using NET1814_MilkShop.Services.Services.Interfaces;

namespace NET1814_MilkShop.API;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(WebApplicationBuilder builder, IWebHostEnvironment env)
    {
        _configuration = builder.Configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc(
                "v1",
                new OpenApiInfo { Title = "NET1814_MilkShop.API", Version = "v1" }
            );
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var apiXmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            o.IncludeXmlComments(apiXmlPath);
            var repoXmlFile = "NET1814_MilkShop.Repositories.xml";
            var repoXmlPath = Path.Combine(AppContext.BaseDirectory, repoXmlFile);
            o.IncludeXmlComments(repoXmlPath);
            o.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                }
            );
            o.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            );
        });

        services.Configure<RouteOptions>(options =>
        {
            options.AppendTrailingSlash = false;
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = false;
        });
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        if (connectionString == null)
        {
            throw new InvalidOperationException(
                "Could not find connection string 'DefaultConnection'"
            );
        }

        AddDI(services);
        QuartzExtenstionHosting.AddQuartzBackgroundJobs(services);
        try
        {
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromJson(_configuration["FIREBASE_CONFIG"])
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[WARNING] Firebase initialization failed: {ex.Message}. Continuing without Firebase.");
        }
        services.Configure<EmailSettingModel>(_configuration.GetSection("EmailSettings"));
        //services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        services.AddHttpClient();
        services.AddExceptionHandler<ExceptionLoggingHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowAll",
                builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }
            );
        });
        services
            .AddAuthentication()
            .AddJwtBearer(
                "Access",
                o =>
                {
                    o.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidIssuer = _configuration["Jwt:Issuer"],
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(_configuration["Jwt:AccessTokenKey"])
                            ),
                            ClockSkew = TimeSpan.FromMinutes(0)
                        };
                }
            )
            .AddJwtBearer(
                "Refresh",
                o =>
                {
                    o.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidIssuer = _configuration["Jwt:Issuer"],
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(_configuration["Jwt:RefreshTokenKey"])
                            ),
                            ClockSkew = TimeSpan.FromMinutes(0)
                        };
                }
            );
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
        }

        app.UseDeveloperExceptionPage();

        var isUserSwagger = _configuration.GetValue("UseSwagger", false);
        Console.WriteLine($"[DEBUG] UseSwagger config value: {isUserSwagger}");
        if (isUserSwagger)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DefaultModelsExpandDepth(-1);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NET1814_MilkShop.API v1");
            });
        }

        app.UseRouting();
        app.UseCors("AllowAll");
        app.UseAuthorization();
        app.UseExceptionHandler(_ => { });
        app.UseEndpoints(endpoint =>
        {
            endpoint.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
        });
        app.MapControllers();
    }


    private static void AddDI(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerService, CustomerService>();

        services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<IPreorderProductRepository, PreorderProductRepository>();

        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IBrandService, BrandService>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<IProductStatusRepository, ProductStatusRepository>();

        services.AddScoped<IUnitRepository, UnitRepository>();
        services.AddScoped<IUnitService, UnitService>();

        services.AddScoped<ICartDetailRepository, CartDetailRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICartService, CartService>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();

        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

        services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
        services.AddScoped<IProductAttributeService, ProductAttributeService>();

        services.AddScoped<IProductAttributeValueRepository, ProductAttributeValueRepository>();
        services.AddScoped<IProductAttributeValueService, ProductAttributeValueService>();

        services.AddScoped<IProductImageRepository, ProductImageRepository>();
        services.AddScoped<IProductImageService, ProductImageService>();

        services.AddScoped<IProductReviewRepository, ProductReviewRepository>();
        services.AddScoped<IProductReviewService, ProductReviewService>();

        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IAddressService, AddressService>();

        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IPostService, PostService>();
        
        services.AddScoped<IVoucherRepository, VoucherRepository>();
        services.AddScoped<IVoucherService, VoucherService>();
        
        services.AddScoped<IReportTypeRepository, ReportTypeRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<IReportService, ReportService>();

        services.AddScoped<IOrderLogRepository, OrderLogRepository>();

        services.AddScoped<ICheckoutService, CheckoutService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IEmailService, EmailService>();

        services.AddScoped<IImageService, ImageService>();

        services.AddScoped<IPaymentService, PaymentService>();

        services.AddScoped<IShippingService, ShippingService>();

        services.AddScoped<IConversationRepository, ConversationRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IConversationService, ConversationService>();

        services.AddScoped<IJwtTokenExtension, JwtTokenExtension>();
        services.AddScoped<UserExistsFilter>();
    }
}