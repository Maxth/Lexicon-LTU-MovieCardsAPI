using dotenv.net;
using Microsoft.EntityFrameworkCore;
using MovieCardsApi.Data;
using MovieCardsAPI.Extensions;
using MovieCardsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddControllers(options =>
    {
        //This server only supports JSON and XML so we return a 406 Not Acceptable for requests
        //demanding other formats
        options.ReturnHttpNotAcceptable = true;
    })
    .AddNewtonsoftJson()
    //Add XML output support
    .AddXmlDataContractSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MovieCardsDbContext>(options =>
{
    DotEnv.Load();
    string? dbUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
    options.UseNpgsql(dbUrl ?? throw new ArgumentNullException(dbUrl));
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
