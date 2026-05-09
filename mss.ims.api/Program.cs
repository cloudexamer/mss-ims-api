using mss.ims.application;
using mss.ims.infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUi", policy =>
    {
        policy.WithOrigins("https://ashy-sky-0cef6b510.7.azurestaticapps.net")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//    app.MapOpenApi();
//}

app.MapGet("/", () => "MSS IMS API is running");
app.MapGet("/health", () => Results.Ok(new
{
    status = "healthy",
    timestamp = DateTimeOffset.UtcNow
}));

app.MapGet("/internal/version", (HttpContext context) =>
{
    var expectedKey = Environment.GetEnvironmentVariable("INTERNAL_DIAGNOSTICS_KEY");
    var providedKey = context.Request.Headers["X-Internal-Diagnostics-Key"].FirstOrDefault();

    if (!app.Environment.IsDevelopment())
    {
        if (string.IsNullOrWhiteSpace(expectedKey) || providedKey != expectedKey)
        {
            return Results.NotFound();
        }
    }

    return Results.Ok(new
    {
        app = "mss-ims-api",
        buildNumber = Environment.GetEnvironmentVariable("BUILD_NUMBER") ?? "local",
        imageTag = Environment.GetEnvironmentVariable("IMAGE_TAG") ?? "local",
        environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "unknown"
    });
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowUi");
app.UseAuthorization();
app.MapControllers();

app.Run();



//using IMS.Infrastructure;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddInfrastructure(builder.Configuration);

//var app = builder.Build();

//app.UseSwagger();
//app.UseSwaggerUI();

//app.MapControllers();

//app.Run();