using Microsoft.EntityFrameworkCore;
using UniConnect.Api.Data;
using UniConnect.Api.Repositories;
using UniConnect.Api.Repositories.Interfaces;
using UniConnect.Api.Services;
using UniConnect.Api.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapGet("/", () => "uniconnect api running ✅ go to /swagger");

app.Run();
