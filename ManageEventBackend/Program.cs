using ManageEventBackend.Applications.Responses;
using ManageEventBackend.Applications.Services;
using ManageEventBackend.Domains.Interfaces;
using ManageEventBackend.Infrastructures.Data;
using ManageEventBackend.Infrastructures.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(
    option => option.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"))
    );

var odataBuilder = new ODataConventionModelBuilder();
odataBuilder.EntitySet<UserResponse>("user");
odataBuilder.EntitySet<EventResponse>("event");
odataBuilder.EntitySet<EventMemberResponse>("participant");

builder.Services.AddControllers()
    .AddOData(options => options
        .SetMaxTop(100)
        .Filter()
        .OrderBy()
        .Count()
        .Expand()
        .Select()
        .AddRouteComponents("api", odataBuilder.GetEdmModel())
        )
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Event Management",
        Description = "An event management website is an online platform that allows organizers to easily create, manage, and promote events, while helping attendees conveniently register, track information, and participate in activities.",
    });
    options.EnableAnnotations();
    var jwtSecuritySchema = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Enter your JWT Access Token",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition("Bearer", jwtSecuritySchema);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecuritySchema, Array.Empty<string>() }
    });

    //options.OperationFilter<RemoveODataMediaTypesFilter>();
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventMemberRepository, EventMemberRepository>();


// Confiure cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Config JWT Authentication
var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"]!);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),

        ClockSkew = TimeSpan.Zero
    };
});

// Add Authorization
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Management V1");
        c.DocumentTitle = "Event Management API Documentation";
        c.DefaultModelsExpandDepth(-1);
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    });
}

//using (var scrope = app.Services.CreateScope())
//{
//    var dbContext = scrope.ServiceProvider.GetRequiredService<AppDbContext>();
//    //dbContext.Database.EnsureCreated();
//    dbContext.Database.GetMigrations();
//    dbContext.Database.Migrate();
//}

app.UseHttpsRedirection();

app.UseODataRouteDebug();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
