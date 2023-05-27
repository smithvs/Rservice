
using RService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RService.Data;
using RService.Repositories.Interfaces;
using RService.Repositories;

var d = new Record();
Console.WriteLine(d.DescriptionOffice );
Console.WriteLine(d.DescriptionClient == null ? "null" : "i dont no");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RServiceContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("RServiceContext") ?? throw new InvalidOperationException("Connection string 'RServiceContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

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
