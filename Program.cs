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

builder
    .Services.AddControllers(options =>
    {
        //This server only supports JSON and XML so we return a 406 Not Acceptable for requests
        //demanding other formats
        options.ReturnHttpNotAcceptable = true;
    })
    //Add XML output support
    .AddXmlDataContractSerializerFormatters();

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
