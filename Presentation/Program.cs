using Domain.Contracts.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using Presentation.CustomMiddleware;
using Presentation.Extensions;
using Service;
using Service.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieCardsDbContext>();
builder
    .Services.AddControllers(options =>
    {
        //This server only supports JSON so we return a 406 Not Acceptable for requests
        //demanding other formats
        options.ReturnHttpNotAcceptable = true;
    })
    .AddNewtonsoftJson();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.SeedDataAsync();
}

app.UseMiddleware<CustomExceptionHandler>();
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
