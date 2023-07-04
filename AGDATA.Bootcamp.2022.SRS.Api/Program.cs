using Agdata.Bootcamp._2022.SRS.Domain;
using Agdata.Bootcamp._2022.SRS.Domain.AutoMapper;
using Agdata.Bootcamp._2022.SRS.Domain.Repositories;
using Agdata.Bootcamp._2022.SRS.Services.Contract;
using Agdata.Bootcamp._2022.SRS.Services.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SRSDbContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddScoped<ISRSDbContext, SRSDbContext>();
builder.Services.AddAutoMapper(typeof(Mapper));

builder.Services.AddScoped<ISeatManagementService, SeatManagementService>();
builder.Services.AddScoped<IReservationManagementService, ReservationManagementService>();

builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowOrigin",
        builder => {
            builder.AllowAnyOrigin()
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

app.UseCors("AllowOrigin");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();