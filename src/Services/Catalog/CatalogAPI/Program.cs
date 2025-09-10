using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddMarten(config => { config.Connection(builder.Configuration.GetConnectionString("Database")!); })
    .UseLightweightSessions();
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure HTTP endpoints
app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();