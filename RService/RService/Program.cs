
using RService.Models;
using RService.Repositories;
using RService.Repositories.Interfaces;

var d = new Record();
Console.WriteLine(d.DescriptionOffice );
Console.WriteLine(d.DescriptionClient == null ? "null" : "i dont no");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IClientRepository, ClientRepository>();
builder.Services.AddSingleton<IOfficeRepository, OfficeRepository>();
builder.Services.AddSingleton<IOfficeTypeRepository, OfficeTypeRepository>();
builder.Services.AddSingleton<IRecordRepository, RecordRepository>();
builder.Services.AddSingleton<IServiceRepository, ServiceRepository>();
builder.Services.AddSingleton<IServiceOfficeRepository, ServiceOfficeRepository>();
builder.Services.AddSingleton<ISpecialistRepository, SpecialistRepository>();
builder.Services.AddSingleton<ISpecialistServiceRepository, SpecialistServiceRepository>();

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
