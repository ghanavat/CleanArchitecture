using Asp.Versioning;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Shared.Query;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.AspNetCore;
using NSwag.Generation.Processors.Security;
using CleanArchitecture.Api.Middlewares;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using CleanArchitecture.Core.PlayerAggregate;
using CleanArchitecture.Infrastructure.DependencyInjection;
using CleanArchitecture.UseCases.DependencyInjection;
using CleanArchitecture.UseCases.PlayerFeature.GetSomeDataForSomeId;
using Ghanavats.Domain.Factory.DependencyInjection;
using Ghanavats.Domain.Primitives.DependencyInjection;
using CleanArchitecture.Api.CustomOpenApiProcessors;

const string sampleOriginsName = "_sampleOriginsPolicyName";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(opt => ConfigureJsonSerializers(opt.JsonSerializerOptions));

builder.Services.ConfigureHttpJsonOptions(opt => ConfigureJsonSerializers(opt.SerializerOptions));

builder.Services.AddCors(corsOptions =>
{
    /* Add your policies here */

    corsOptions.AddPolicy(sampleOriginsName, policy =>
    {
        policy.WithOrigins("https://some.domain.com")
            .AllowAnyHeader() /* To allow all headers in the CORS requests. */
            //.WithHeaders(HeaderNames.ContentType, "some-other-header") /* To allow specific header(s) in the CORS requests */
            .WithMethods("GET", "POST", "PUT"); /* This can be configured with what HTTP Methods we want the CORS policy to be applied for */
    });

    /* Use this only for default policy configs, if needed */
    //corsOptions.AddDefaultPolicy(defaultOptions =>
    //{
    //});
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// TODO Add validations for Options (AppSettings)

builder.Services.AddHealthChecks().AddCheck<CustomHealthCheckMiddleware>("Name_For_Your_HealthCheck");

builder.Services.AddExceptionHandler<ExceptionHandlerMiddleware>();
builder.Services.AddProblemDetails();

builder.Services.AddApiVersioning(
    options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddApiExplorer(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

ConfigureMediatR();

builder.Services.AddValidations();
builder.Services.AddSqlDb(builder.Configuration.GetSection("SqlServerSettings"), builder.Environment.IsDevelopment());
builder.Services.AddRepository();
builder.Services.AddDomainEventPublisher();
builder.Services.AddDomainFactory();

/* Authentication is configured as an example to show what it may look like.
 * Here we used AddJwtBearer (package: 'Microsoft.AspNetCore.Authentication.JwtBearer') scheme to validate the token. 
 * It is possible to use other schemes, such as Microsoft Identity WebApi (AddMicrosoftIdentityWebApi), 
 * when Microsoft Identity is used for authentication.
 * Require to install 'Microsoft.Identity.Web' package
 */
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOptions =>
{
    jwtOptions.Authority = "https://some-authority.com";
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "TheValidIssuer",

        /* Default is true for this, but in this template we are disabling it.
         * Setting this flag to false will skip the Audience validation. Some providers don't send 'aud' in Access Token, 
         * although it can be configured.
         */
        ValidateAudience = false,

        ValidateIssuerSigningKey = true,
        RoleClaimType = "RoleClaimType_UsuallyGroups"
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("SamplePolicy", policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireRole("SampleRole");
        policy.RequireAuthenticatedUser();
    });

builder.Services
    .AddOpenApiDocument(settings =>
    {
        CommonApiDocumentGeneratorSettings(settings, "v1");

        settings.PostProcess = document =>
        {
            document.Info = ConfigureOpenApiInfo(settings.DocumentName);
        };

        settings.SchemaSettings.SchemaNameGenerator = new CustomSchemaNameGenerator();
        settings.SchemaSettings.TypeNameGenerator = new CustomTypeNameGenerator();

        settings.OperationProcessors.Add(new CustomOperationProcessor());
    })
    .AddOpenApiDocument(settings =>
    {
        CommonApiDocumentGeneratorSettings(settings, "v2");
   
        settings.PostProcess = document =>
        {
            document.Info = ConfigureOpenApiInfo(settings.DocumentName);            
        };

        settings.AddSecurity("SecuritySchemeName", new OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.Http,

            /* Require installing package: 'Microsoft.AspNetCore.Authentication.JwtBearer' */
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            BearerFormat = "JWT",
            Name = "Authorization",
            Description = "Place your valid JWT Bearer Token into the below Value box."
        });

        settings.SchemaSettings.SchemaNameGenerator = new CustomSchemaNameGenerator();
        settings.SchemaSettings.TypeNameGenerator = new CustomTypeNameGenerator();

        settings.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
        settings.OperationProcessors.Add(new CustomOperationProcessor());
    })
    .AddOpenApiDocument(settings =>
    {
        CommonApiDocumentGeneratorSettings(settings, "v3");

        settings.PostProcess = document =>
        {
            document.Info = ConfigureOpenApiInfo(settings.DocumentName);
        };

        settings.SchemaSettings.SchemaNameGenerator = new CustomSchemaNameGenerator();
        settings.SchemaSettings.TypeNameGenerator = new CustomTypeNameGenerator();

        settings.OperationProcessors.Add(new CustomOperationProcessor());
    });

builder.Services.AddRouting((options) =>
{
    options.LowercaseUrls = true;
});

var app = builder.Build();

/* This is one way to 'enable' CORS. 
 * If you configure more than one policy, do not pass the 'policyName' argument. 
 * You can also apply finer controls and limit endpoints that support CORS by using the 'EnableCors' attribute on your endpoints.
 */
app.UseCors(sampleOriginsName);

// TODO For Strict-Transport-Security header
app.UseHsts();

//TODO Add GET minimal endpoints for your health check (i.e., /health)

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseStatusCodePages();
app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
return;

void ConfigureJsonSerializers(JsonSerializerOptions options)
{
    options.PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower;
    options.WriteIndented = true;
    options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.KebabCaseLower, false));
}

void ConfigureMediatR()
{
    var mediatRAssemblies = new[]
      {
        Assembly.GetAssembly(typeof(Player)), // Core
        Assembly.GetAssembly(typeof(GetPlayerByIdQuery)), // UseCases
        Assembly.GetAssembly(typeof(PlayGroundDbContext)), // Infrastructure
        Assembly.GetAssembly(typeof(IQuery<>)) // Shared
      };

    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssemblies(mediatRAssemblies!);
        //cfg.AddOpenBehavior(typeof(ValidatorBehaviour<,>));
    });
}

OpenApiInfo ConfigureOpenApiInfo(string apiVersion)
{
    return new OpenApiInfo
    {
        Version = apiVersion,
        Title = "Clean Architecture template",
        Description = "A template based on Clean Architecture rules. This template aims to show/help developers with setting up a solution based on Clean Architecture rules and guidelines."
    };
}

void CommonApiDocumentGeneratorSettings(AspNetCoreOpenApiDocumentGeneratorSettings settings, string version)
{
    settings.DocumentName = version;
    settings.ApiGroupNames = [version];
    settings.UseHttpAttributeNameAsOperationId = false;
    settings.UseControllerSummaryAsTagDescription = true;
    settings.DefaultResponseReferenceTypeNullHandling = NJsonSchema.Generation.ReferenceTypeNullHandling.Null;
}
