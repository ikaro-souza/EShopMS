var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddMarten(config => { config.Connection(builder.Configuration.GetConnectionString("Database")!); })
    .UseLightweightSessions();
if (builder.Environment.IsDevelopment()) builder.Services.InitializeMartenWith<CatalogInitialData>();
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure HTTP endpoints
app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();