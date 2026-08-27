using Supv.Src.Supv.Data;
using Supv.Src.Supv.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddApiVersioning();

// DbContext
var connectionString = builder.Configuration.GetConnectionString("ApplicationDatabase")
        ?? throw new InvalidOperationException("Connection string was not found.");

builder.Services.AddDatabase(connectionString);

builder.Services.AddAutoMapper();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHsts();
app.UseHttpsRedirection();

app.Run();
