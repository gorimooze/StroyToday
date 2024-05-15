using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using StroyToday.API.Extensions;
using StroyToday.Application.Interfaces;
using StroyToday.Application.Services;
using StroyToday.Common.Auth;
using StroyToday.Common.Azure;
using StroyToday.Core.IRepositories;
using StroyToday.DataAccess;
using StroyToday.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddDbContext<StroyTodayDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(StroyTodayDbContext)));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCvRepository, UserCvRepository>();
builder.Services.AddScoped<ISkillCategoryRepository, SkillCategoryRepository>();
builder.Services.AddScoped<IUserToSkillCategoryRepository, UserToSkillCategoryRepository>();
builder.Services.AddScoped<IPortfolioForUserCVRepository, PortfolioForUserCVRepository>();

builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddScoped<AuthenticationHelper>();
builder.Services.AddScoped<AzureHelper>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddApiAuthentication(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions()
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
