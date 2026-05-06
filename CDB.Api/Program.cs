using CDB.Application.Queries;
using CDB.Domain.Interfaces;
using CDB.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CalculateCdbQuery).Assembly));

// Domain Services
builder.Services.AddScoped<ITaxCalculator, TaxCalculator>();
builder.Services.AddScoped<ICdbCalculator, CdbCalculator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.MapPost("/api/cdb/calculate", async (CalculateCdbQuery query, IMediator mediator) =>
{
    if (query.InitialValue <= 0 || query.Months <= 1)
    {
        return Results.BadRequest(new { message = "O valor inicial deve ser maior que zero e o prazo maior que 1 mês." });
    }

    try
    {
        var result = await mediator.Send(query);
        return Results.Ok(result);
    }
    catch (System.ArgumentException ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
})
.WithName("CalculateCdb")
.WithOpenApi();

app.Run();
