using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using UdemyRabbitMQ.WatermarkWebApp.BackgroundServices;
using UdemyRabbitMQ.WatermarkWebApp.Models;
using UdemyRabbitMQ.WatermarkWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add your DbContext configuration here
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase(databaseName: "productDb");
});

// Retrieve RabbitMQ URI from appsettings
builder.Services.AddSingleton(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var rabbitMqUri = new Uri(configuration.GetConnectionString("RabbitMQ"));
    return new ConnectionFactory() { Uri = rabbitMqUri, DispatchConsumersAsync=true };
});

builder.Services.AddSingleton<RabbitMQClientService>();
builder.Services.AddSingleton<RabbitMQPublisher>();
builder.Services.AddHostedService<ImageWatermarkProcessBackgroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
