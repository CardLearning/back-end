using CardLearning.Application;
using CardLearning.Infrastructure.DI;
using CardLearningAPI.Exceptions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo{ Title = "CardLearning.Api", Version = "v1" });
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();

app.Use(async (context, next) =>
{
    var started = DateTime.UtcNow;
    await next();
    var finished = DateTime.UtcNow;
    var duration = finished - started;
    app.Logger.LogInformation("Request {RequestPath} started {started} and finished {finished} took {Duration}ms. Status code {StatusCode}",
        context.Request.Path, started.ToString(), finished.ToString(), duration.TotalMilliseconds, context.Response.StatusCode);
});
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapGet("/healthz", () => Results.Ok(new { status = "Healthy" }))
    .WithName("HealthCheck")
    .WithOpenApi();

app.Use(async (context, next) =>
{
    await next();
});

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.Run();

public partial class Program;
