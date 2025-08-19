var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure HTTP endpoints
app.MapCarter();

app.Run();