using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using StroyToday.API.Extensions;
using StroyToday.Application.Interfaces.IServices;
using StroyToday.Application.Interfaces;
using StroyToday.Application.Services;
using StroyToday.Common.Auth;
using StroyToday.Common.Azure;
using StroyToday.Core.IRepositories;
using StroyToday.DataAccess.Repositories;
using StroyToday.DataAccess;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        builder => builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
services.Configure<AzureOptions>(builder.Configuration.GetSection(nameof(AzureOptions)));

// Настройка Redis-кэша
services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "RedisInstance";
});

services.AddDbContext<StroyTodayDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(StroyTodayDbContext)));
});

//Repository for DI
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IUserCvRepository, UserCvRepository>();
services.AddScoped<ISkillCategoryRepository, SkillCategoryRepository>();
services.AddScoped<IUserToSkillCategoryRepository, UserToSkillCategoryRepository>();
services.AddScoped<IPortfolioForUserRepository, PortfolioForUserRepository>();
services.AddScoped<IOrderRepository, OrderRepository>();

//Services for DI
services.AddScoped<IUserService, UserService>();
services.AddScoped<IUserCvService, UserCvService>();
services.AddScoped<ISkillCategoryService, SkillCategoryService>();
services.AddScoped<IPortfolioForUserService, PortfolioForUserService>();
services.AddScoped<IOrderService, OrderService>();

services.AddScoped<AuthenticationHelper>();
services.AddScoped<IAzureProvider, AzureProvider>();

services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();

services.AddApiAuthentication(configuration);

var app = builder.Build();

app.UseCors("AllowLocalhost3000");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
