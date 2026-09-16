using SlowVibes.Api.DependencyInjection;
using SlowVibes.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSlowVibesServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/openapi/v1.json", "SlowVibes API v1"));
}

app.UseHttpsRedirection();
app.UseMiddleware<DomainExceptionMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();
