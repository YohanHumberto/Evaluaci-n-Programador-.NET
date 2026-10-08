using PruebaTecnicaApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();


builder.Services.ConfigureAutoMapper();
builder.Services.AddServices();
builder.Services.AddExternalServices();

builder.Services.ConfigureDbContext(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var loggerFactory = LoggerFactory
    .Create(builder =>
    {
        builder.ClearProviders();
        builder.AddConsole();
    });
var logger = loggerFactory.CreateLogger<Program>();

app.ConfigureExceptionHandler(logger);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
