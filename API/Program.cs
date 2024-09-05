using API.Extensions;
using Domain.Contracts.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using Service;
using Service.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieCardsDbContext>();
builder.Services.ConfigureControllers();
builder.Services.AddAutoMapper(typeof(Infrastructure.AssemblyReference).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();

var app = builder.Build();
app.ConfigureExceptionHandler();

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
