using supv.src.supv.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// DbContext
var connectionString = builder.Configuration.GetConnectionString("ApplicationDatabase")
        ?? throw new InvalidOperationException($"Connection string was not found.");

builder.Services.AddDatabase(connectionString);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHsts();
app.UseHttpsRedirection();


app.Run();

