using Domain.Contracts.Interfaces;
using dotenv.net;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Presentation.CustomMiddleware;
using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddControllers(options =>
    {
        //This server only supports JSON and XML so we return a 406 Not Acceptable for requests
        //demanding other formats
        options.ReturnHttpNotAcceptable = true;
    })
    .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MovieCardsDbContext>();
builder.Services.AddScoped<IMovieInfoRepository, MovieInfoRepository>();
builder.Services.AddScoped<IDirectorInfoRepository, DirectorInfoRepository>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
