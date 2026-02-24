using Application;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Infrastructure;
using Infrastructure.BackgroundServices;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region HttpClientFactories Configuration
ApiClientConfiguration sendgridResilienceConfiguration = new()
{
    RetryCount = 3,
    RetryAttemptInSeconds = 2,
    DurationOfBreakInSeconds = 60,
    HandledEventsAllowedBeforeBreaking = 5
};

Console.WriteLine("APP ARRANCANDO...");

builder.Services.AddHttpClient(
    "SendGrid",
    client =>
    {
        client.BaseAddress = new Uri("https://api.sendgrid.com/v3/");
    })
    .AddPolicyHandler(PollyResiliencePolicies.GetRetryPolicy(sendgridResilienceConfiguration))
    .AddPolicyHandler(PollyResiliencePolicies.GetCircuitBreakerPolicy(sendgridResilienceConfiguration));

#endregion
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Dependency Injections
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAccountRepository<Account>, AccountRepository<Account>>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IRequesterService, RequesterService>();
builder.Services.AddScoped<IBloodRequestRepository, BloodRequestRepository>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IDonorRepository, DonorRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IModeratorService, ModeratorService>();
builder.Services.AddScoped<IRequesterRepository, RequesterRepository>();
builder.Services.AddHostedService<BloodRequestExpirationWorker>();
#endregion

builder.Services.AddDbContext<DonationsDbContext>(options =>
    options.UseNpgsql(builder.Configuration["ConnectionStrings:DbConnectionString"]));

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("DonationsAPI", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Ac� pegar el token generado al loguearse."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "DonationsAPI" }
                }, new List<string>() }
    });
});


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]!))
        };
    }
);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ModeratorPolicy", policy =>
        policy.RequireRole("Admin", "Moderator"));

    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllowOrigin");

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DonationsDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

app.Run();
