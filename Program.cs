using Microsoft.EntityFrameworkCore;
using System.Net;
using Transportes_API_gen13.Models;
using Transportes_API_gen13.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//a�adimos el Contexto a las configuraciones del proyecto y le pasamos la Cadena de Conexi�n para que inicialice con nuestra BD
builder.Services.AddDbContext<TransportesContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("sql_connection_remote"), sqlServerOptionsAction: sqloption =>
{
    sqloption.EnableRetryOnFailure(
        maxRetryCount: 20,
        maxRetryDelay: TimeSpan.FromSeconds(15),
        errorNumbersToAdd: null
        );
}));
//Inyecci�n de dependencias
//a�adimos al scope para conectar la interfaz con el servicio
builder.Services.AddScoped<ICamiones, CamionesService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS
//CORS, o Cross-Origin Resource Sharing (Compartir recursos entre diferentes or�genes), 
//es un mecanismo de seguridad utilizado en navegadores web para permitir que las solicitudes de recursos 
//(como im�genes, scripts, estilos, etc.) se realicen desde un dominio (origen) diferente al dominio 
//en el que se encuentra la p�gina web actual. En otras palabras, CORS es un conjunto de reglas y 
//pol�ticas que permiten o restringen las solicitudes HTTP entre diferentes dominios.

builder.Services.AddCors(option => option.AddPolicy(
    "AllowAnyOrigin", //A�adimos una nueva pol�tica de CORS
    builder => builder.AllowAnyOrigin() //Permitimos las peticiones desde cualquier origen
                      .AllowAnyMethod()//permitiomos cualquier petici�n de cualquier m�todo
                      .AllowAnyHeader()//permitimos cualquier cabecera
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//indicamos que la aplicaci�n utilice nuestra pol�tica de CORS
app.UseCors("AllowAnyOrigin");
app.UseAuthorization();

app.MapControllers();

app.Run();
