using Challenge.Backend.DBContext;
using Challenge.Backend.Logs;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SQLServer");

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ChallengeDBContext>(
    options=>options.UseSqlServer(connectionString)
    );

builder.Services.AddScoped<ILogServices<LogServices>,LogServices>();

LogConfig.ConfigureLogs();

builder.Host.UseSerilog();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
