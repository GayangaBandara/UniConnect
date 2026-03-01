using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Repositories;
using backend.Repositories.Interfaces;
using backend.Services;
using backend.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to use a non-privileged port
builder.WebHost.UseUrls("http://localhost:8080");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IGuidanceRequestRepository, GuidanceRequestRepository>();
builder.Services.AddScoped<IGuidanceRequestService, GuidanceRequestService>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapGet("/", () => "uniconnect api running ✅ go to /swagger");

app.Run();
