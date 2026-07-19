using EnterpriseInventory.API.Middleware;
using EnterpriseInventory.Application.Common.Settings;
using EnterpriseInventory.Application.DependencyInjection;
using EnterpriseInventory.Application.Mappings;
using EnterpriseInventory.Application.Validators;
using EnterpriseInventory.Infrastructure.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Serilog;
using System.Text;
var builder = WebApplication.CreateBuilder(args);


// ============================================================
// HOST & LOGGING CONFIGURATION
// ============================================================

// Option 1: Configure Serilog directly in Program.cs.
// Use this approach when Serilog configuration is not defined
// in appsettings.json.
//
// Log.Logger = new LoggerConfiguration()
//     .MinimumLevel.Information()
//     .WriteTo.Console()
//     .WriteTo.File(
//         "logs/log-.txt",
//         rollingInterval: RollingInterval.Day)
//     .CreateLogger();

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
// NOTE:
// FluentValidation integrates with ASP.NET Core MVC,
// therefore its registration belongs in the API project.
builder.Services.AddFluentValidationAutoValidation();

// Registers validators from the Application assembly.
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();


// AutoMapper
// Registers all AutoMapper profiles from the Application assembly.
// NOTE:
// The mapping profiles live in the Application project,
// but the DI registration belongs to the API composition root.
builder.Services.AddAutoMapper(
    cfg => { },
    typeof(ProductProfile).Assembly);


// Application Layer
// Registers all application services
// (ProductService, AuthService, etc.)
builder.Services.AddApplicationServices();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

/*---------------------------------------------------------------------
  Read the strongly typed JWT configuration from appsettings.json.
 
  Program.cs is the application's composition root, so we read the
  configuration directly from IConfiguration.
 
  Application services should use IOptions<JwtSettings> instead.
  ---------------------------------------------------------------------
*/

var jwtSettings = builder.Configuration
    .GetSection("JwtSettings")
    .Get<JwtSettings>()!;

// Infrastructure Layer
// Registers DbContext, repositories, password hasher,
// and other infrastructure services.
builder.Services.AddInfrastructure(builder.Configuration);


builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = 
              new SymmetricSecurityKey(Encoding.UTF8
              .GetBytes(jwtSettings.Key)),

            ClockSkew = TimeSpan.Zero
        };
    });

// Swagger / OpenAPI Services
builder.Services.AddSwaggerGen(options =>
{
    // ------------------------------------------------------------
    // Swagger document
    // ------------------------------------------------------------
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Enterprise Inventory API",
        Version = "v1",
        Description = "REST API for Enterprise Inventory Management System"
    });

    // ------------------------------------------------------------
    // JWT Bearer definition
    // ------------------------------------------------------------
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });

    // ------------------------------------------------------------
    // JWT requirement (Swashbuckle 10.x)
    // ------------------------------------------------------------
    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [
                new OpenApiSecuritySchemeReference("Bearer", document)
            ] = []
        });
});



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


// Authenticates the incoming JWT and creates HttpContext.User.
app.UseAuthentication();

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

