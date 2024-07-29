using Microsoft.EntityFrameworkCore;
using SeverBooks.Data;
using SeverBooks.Models;
using SeverBooks.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//Conexión
builder.Services.AddDbContext<DataContext> (options => 
                            options.UseMySql(
                                builder.Configuration.GetConnectionString("MySqlConnection"),
                                Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));

// CORS
builder.Services.AddCors(options =>
    options.AddPolicy("politica", service => {
        service.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    })
);                           

builder.Services.Configure<Email>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddTransient<MailRepository>();

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

