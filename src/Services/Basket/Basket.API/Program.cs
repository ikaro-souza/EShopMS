var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

builder.Services.AddCarter();
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// var connectionString = builder.Configuration.GetConnectionString("Database")!;
// builder.Services.AddMarten(config => { config.Connection(connectionString); })
//     .UseLightweightSessions();
//
// builder.Services.AddHealthChecks()
//     .AddNpgSql(connectionString);

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { });

// app.MapHealthChecks("/health", new HealthCheckOptions
// {
//     ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
// });

app.Run();