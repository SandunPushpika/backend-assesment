using LiquidLabAssesment.Configurations.Middlewares;
using LiquidLabAssesment.Core.Helpers;
using LiquidLabAssesment.Core.Interfaces;
using LiquidLabAssesment.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppConfig>(builder.Configuration.GetSection("AppConfig"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<HttpHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();