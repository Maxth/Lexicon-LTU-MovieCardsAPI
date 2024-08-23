using dotenv.net;
using Microsoft.EntityFrameworkCore;
using MovieCardsApi.Data;
using MovieCardsAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MovieCardsDbContext>(options =>
{
    DotEnv.Load();
    options.UseNpgsql(
        Environment.GetEnvironmentVariable("DATABASE_URL")
            ?? throw new InvalidOperationException("Connection string resolved to null!")
    );
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.SeedDataAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
