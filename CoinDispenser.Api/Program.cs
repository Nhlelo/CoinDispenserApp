using CoinDispenser.Api.Data;
using CoinDispenser.Api.Middleware;
using CoinDispenser.Core.Interfaces;
using CoinDispenser.Core.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI
builder.Services.AddSingleton<ICoinChangeService, CoinChangeService>();

// Use SQL Server
builder.Services.AddDbContext<CoinChangeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CoinChangeDbContext>();
    db.Database.Migrate();
}

app.UseMiddleware<ExceptionMiddleware>();
//app.UseMiddleware<ApiKeyMiddleware>();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoinDispenser API v1"));

}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();