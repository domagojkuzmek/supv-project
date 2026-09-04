using System.Text.Json.Serialization;
using Supv.Src.Supv.Data;
using Supv.Src.Supv.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddApiVersioning();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
});

// DbContext
var connectionString = builder.Configuration.GetConnectionString("ApplicationDatabase")
        ?? throw new InvalidOperationException("Connection string was not found.");

builder.Services.AddDatabase(connectionString);

builder.Services.AddAutoMapper();

builder.Services.AddFluentValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHsts();
app.UseHttpsRedirection();

app.Run();
