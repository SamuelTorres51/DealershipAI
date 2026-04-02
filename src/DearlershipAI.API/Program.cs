using DearlershipAI.API;
using DearlershipAI.API.Services.UseCases.Cars.Search;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

// UI (wwwroot) will call the API from the same origin.
builder.Services.AddRouting();

builder.Services.AddInjection(builder.Configuration);
builder.Services.AddScoped<SearchCarUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
    app.UseSwaggerUI();
    app.UseSwagger();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Fallback for SPA-like navigation (serves wwwroot/index.html)
app.MapFallbackToFile("index.html");

app.Run();
