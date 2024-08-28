using ValidKitchenControl.Application.Services;
using ValidKitchenControl.Domain.Repositories;
using ValidKitchenControl.Domain.Services;
using ValidKitchenControl.Infrastructure.Repositories;
using ValidKitchenControl.WebApi.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configura o Newtonsoft.Json ou System.Text.Json conforme necessário
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Exemplo: manter a política de nomenclatura padrão
    }); 

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddSingleton<IAreaRepository, AreaRepository>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura o CORS para permitir requisições de qualquer origem
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
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

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configura o pipeline de requisição HTTP
app.UseRouting();

// Adiciona middleware CORS
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
