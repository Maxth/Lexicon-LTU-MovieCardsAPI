using API.Extensions;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieCardsDbContext>();
builder.Services.ConfigureControllers();
builder.Services.AddAutoMapper(typeof(Infrastructure.AssemblyReference).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();

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
