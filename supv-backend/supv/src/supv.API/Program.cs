using supv.src.supv.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// DbContext
var connectionString = builder.Configuration.GetConnectionString("ApplicationDatabase")
        ?? throw new InvalidOperationException(
        $"Connection string was not found.");

builder.Services.AddDatabase(builder.Configuration, connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();

