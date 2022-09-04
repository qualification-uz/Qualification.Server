using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Qualification.Api.Extensions;
using Qualification.Api.Helpers;
using Qualification.Data.Contexts;
using Qualification.Domain.Entities.Users;
using Qualification.Domain.Enums;
using Qualification.Service.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration
        .GetConnectionString("PostgreSqlConnection"),
        sqlServerOption => sqlServerOption.EnableRetryOnFailure()));

builder.Services
    .AddIdentity<User, Role>(option =>
    {
        option.Password.RequiredLength = 8;
        option.Password.RequireUppercase = false;
        option.Password.RequireDigit = false;
        option.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SchoolPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.SuperAdmin),
        Enum.GetName(UserRole.School)));

    options.AddPolicy("ApplicationPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.SuperAdmin),
        Enum.GetName(UserRole.School),
        Enum.GetName(UserRole.Admin)));

    options.AddPolicy("All", policy => policy.RequireRole(
        Enum.GetName(UserRole.SuperAdmin),
        Enum.GetName(UserRole.Admin),
        Enum.GetName(UserRole.Tester),
        Enum.GetName(UserRole.Teacher),
        Enum.GetName(UserRole.School)));

    options.AddPolicy("TestPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.SuperAdmin),
        Enum.GetName(UserRole.Tester)));
});

builder.Services.AddMvc(options =>
{
    // add custom model binders to beginning of collection
    options.ModelBinderProviders.Insert(0, new FormDataJsonBinderProvider());
});

// Jwt services
builder.Services.AddJwtService(builder.Configuration);
builder.Services.AddHttpContextAccessor();
// Add cors services
builder.Services.AddCorsService();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerService();
builder.Services.AddCustomServices();
builder.Services.AddHttpClientServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<CustomExceptionMiddleware>();

app.UseStaticFiles();
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
