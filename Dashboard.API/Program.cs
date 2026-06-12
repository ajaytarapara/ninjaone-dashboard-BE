using Dashboard.API.Clients;
using Dashboard.API.Interfaces;
using Dashboard.API.Services;
using Dashboard.API.Middleware;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Add In-Memory Caching
builder.Services.AddMemoryCache();

builder.Services.AddScoped<IDashboardService, DashboardService>();

// Configure Polly HttpClient with a retry policy for transient HTTP errors
builder.Services.AddHttpClient<INinjaOneClient, NinjaOneClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MockApi:BaseUrl"] ?? "http://localhost:5052");
})
.AddTransientHttpErrorPolicy(policy =>
    policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(1)));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
