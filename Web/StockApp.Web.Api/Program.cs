using StockApp.Core.Application.Mappers;
using StockApp.Core.Application.UseCases;
using StockApp.Core.Application.Validators;
using StockApp.Core.Infraestructure.Persistence;
using StockApp.Web.Api.Extensions;
using StockApp.Web.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddOpenApi()
    .AddSwagger()
    .AddJwtAuthentication(builder.Configuration)
    .AddPersistence(builder.Configuration)
    .AddMappers()
    .AddValidators()
    .AddUseCases();

// Configuración de aplicación
var app = builder.Build();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();


app.UseCors(options =>
{
    options.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionMiddleware();
app.MapControllers();

app.Run();