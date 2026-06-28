using EnterpriseInventory.Application.Interfaces;
using EnterpriseInventory.Application.Services;
using EnterpriseInventory.Infrastructure.DependencyInjection;
using EnterpriseInventory.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// MVC
builder.Services.AddControllers();

// Dependency Injection
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddInfrastructure(builder.Configuration);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnterpriseInventory API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();

