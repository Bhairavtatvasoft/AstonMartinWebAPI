using AstonMartin.Application.Interfaces;
using AstonMartin.Application.Services;
using AstonMartin.Domain.Interfaces;
using AstonMartin.Infrastructure.Contentful;
using AstonMartin.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyProject.Infrastructure.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("DemoDB"));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.Configure<ContentfulConfigs>(builder.Configuration.GetSection("ContentfulSettings"));
builder.Services.AddSingleton<ContentfulClientFactory>();
builder.Services.AddScoped<IContentfulService, ContentfulService>();
builder.Services.AddControllers();
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
