using CdService.AutoMappers;
using CdService.DTOs;
using CdService.Models;
using CdService.Repository;
using CdService.Services;
using CdService.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CdContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CdDBConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
});

// Configure security
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(builder.Configuration["JWTKey"]))
               });

// Setting up security in Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
        "JWT Authentication Using Bearer Scheme. \r\n\r " +
        "Enter the word 'Bearer' followed by a space and the authentication token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

});

//CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Validators
builder.Services.AddScoped<IValidator<GroupInsertDTO>, GroupInsertValidator>();
builder.Services.AddScoped<IValidator<GroupUpdateDTO>, GroupUpdateValidator>();
builder.Services.AddScoped<IValidator<MusicGenreInsertDTO>, MusicGenreInsertValidator>();
builder.Services.AddScoped<IValidator<MusicGenreUpdateDTO>, MusicGenreUpdateValidator>();
builder.Services.AddScoped<IValidator<RecordInsertDTO>, RecordInsertValidator>();
builder.Services.AddScoped<IValidator<RecordUpdateDTO>, RecordUpdateValidator>();

// Services
builder.Services.AddTransient<IFileManagerService, FileManagerService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IMusicGenreService, MusicGenreService>();
builder.Services.AddScoped<IRecordService, RecordService>();

string userServiceBaseUrl = Environment.GetEnvironmentVariable("UserServiceBaseUrl")
    ?? "http://userservice:8080";

builder.Services.AddHttpClient("UserService", client =>
{
    client.BaseAddress = new Uri(userServiceBaseUrl);
});

// Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Repositories
builder.Services.AddScoped<IGroupRepository<Group>, GroupRepository>();
builder.Services.AddScoped<IMusicGenreRepository<MusicGenre>, MusicGenreRepository>();
builder.Services.AddScoped<IRecordRepository<Record>, RecordRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CdContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while creating the database.");
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
