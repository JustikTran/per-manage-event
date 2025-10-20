using ManageEventBackend.Infrastructures.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(
    option => option.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"))
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scrope = app.Services.CreateScope())
{
    var dbContext = scrope.ServiceProvider.GetRequiredService<AppDbContext>();
    //dbContext.Database.EnsureCreated();
    dbContext.Database.GetMigrations();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
