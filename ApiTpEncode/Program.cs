using ApiTpEncode.Data;
using ApiTpEncode.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ApiTpEncode.Servicios.AutoMaper;
using ApiTpEncode.Servicios;


var builder = WebApplication.CreateBuilder(args);
//agragar la conexion a la base de datos
var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(connectionString));
// agregar el servicio de repositorio
builder.Services.AddScoped<IUsuarioRepository,UsuarioRepository>();


// agregar el servicio de srvicios
builder.Services.AddScoped<IUsuarioServicio,UsuarioService >();

//agrego el automapper

builder.Services.AddAutoMapper(typeof(MappingProfile));
// Add services to the container.



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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
