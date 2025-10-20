using ManageEventBackend.Applications.Responses;
using ManageEventBackend.Applications.Services;
using ManageEventBackend.Domains.Interfaces;
using ManageEventBackend.Infrastructures.Data;
using ManageEventBackend.Infrastructures.Repositories;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(
    option => option.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"))
    );

var odataBuilder = new ODataConventionModelBuilder();
odataBuilder.EntitySet<UserResponse>("user");
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
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
