using Microsoft.EntityFrameworkCore;
using Server_Books.Data;
using Server_Books.Services.Interfaces;
using Server_Books.Services.Repositories;
using Server_Books.Models;

using Server_Books.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IExcelRepository, ExcelRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();

// Conexión
builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySqlConnection"),
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));

// CORS
builder.Services.AddCors(options =>
    options.AddPolicy("politica", service =>
    {
        service.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    })
);

var app = builder.Build();

app.MapControllers();
app.UseCors("politica");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
