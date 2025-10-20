using MarcasApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("Default")
           ?? Environment.GetEnvironmentVariable("ConnectionStrings__Default");

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(conn));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Crear la base de datos, aplicar migraciones y aplicar seed mínimo
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await db.Database.MigrateAsync();

    if (!await db.MarcasAutos.AsNoTracking().AnyAsync())
    {
        db.MarcasAutos.AddRange(
            new MarcasApi.Models.MarcaAuto { Id = 1, Nombre = "Toyota" },
            new MarcasApi.Models.MarcaAuto { Id = 2, Nombre = "Ford" },
            new MarcasApi.Models.MarcaAuto { Id = 3, Nombre = "Honda" }
        );
        await db.SaveChangesAsync();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();