using FluentValidation;
using FluentValidation.AspNetCore;
using EmpleadosApi.Application.Validators;
using EmpleadosApi.Domain.Interfaces;
using EmpleadosApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<EmpleadoValidator>();
        fv.DisableDataAnnotationsValidation = true; 
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IEmpleadoRepository, EmpleadoRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
