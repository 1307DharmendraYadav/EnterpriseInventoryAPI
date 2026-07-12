using EnterpriseInventory.API.Middleware;
using EnterpriseInventory.Application.Interfaces;
using EnterpriseInventory.Application.Mappings;
using EnterpriseInventory.Application.Services;
using EnterpriseInventory.Application.Validators;
using EnterpriseInventory.Infrastructure.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);


// ============================================================
// HOST & LOGGING CONFIGURATION
// ============================================================

// Option 1: Configure Serilog directly in Program.cs.
// Use this approach when Serilog configuration is not defined
// in appsettings.json.
//
//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Information()
//    .WriteTo.Console()
//    .WriteTo.File(
//        "logs/log-.txt",
//        rollingInterval: RollingInterval.Day)
//    .CreateLogger();

// Option 2: Read Serilog configuration from appsettings.json.
// This keeps logging configuration outside the application code
// and allows log levels and sinks to be changed through configuration.
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Replace the default ASP.NET Core logging provider with Serilog.
builder.Host.UseSerilog();


// ============================================================
// SERVICE REGISTRATION (DEPENDENCY INJECTION CONTAINER)
// ============================================================

// MVC / API Controllers
builder.Services.AddControllers();


// FluentValidation
// Enables automatic validation of incoming request models.
builder.Services.AddFluentValidationAutoValidation();

// Registers validators from the assembly containing
// CreateProductRequestValidator.
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();


// AutoMapper
// Registers AutoMapper profiles from the assembly containing ProductProfile.
builder.Services.AddAutoMapper(
    cfg => { },
    typeof(ProductProfile).Assembly);


// Application Services
// Registers application service implementations with the DI container.
builder.Services.AddScoped<IProductService, ProductService>();


// Infrastructure Services
// Registers infrastructure dependencies such as DbContext and repositories.
builder.Services.AddInfrastructure(builder.Configuration);


// Swagger / OpenAPI Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ============================================================
// BUILD THE APPLICATION
// ============================================================

var app = builder.Build();


// ============================================================
// HTTP REQUEST PIPELINE (MIDDLEWARE)
// Order is important: requests flow from top to bottom.
// Responses flow back from bottom to top.
// ============================================================

// Swagger Middleware
// Swagger is enabled only in the Development environment.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "EnterpriseInventory API v1");

        c.RoutePrefix = "swagger";//https://localhost:7047/swagger/v1/swagger.json
        //c.RoutePrefix = "api-docs"; //https://localhost:7047/api-docs
    });
}


// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();


// Global Exception Middleware
// Placed before Authorization and endpoint execution so that exceptions
// thrown by downstream middleware and controllers can be handled centrally.
app.UseMiddleware<ExceptionMiddleware>();


// Authorization Middleware
// Evaluates authorization policies for protected endpoints.
app.UseAuthorization();


// ============================================================
// ENDPOINT MAPPING
// ============================================================

// Maps controller actions to HTTP endpoints.
app.MapControllers();


// ============================================================
// START THE APPLICATION
// ============================================================

app.Run();

