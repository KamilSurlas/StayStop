using FluentValidation;
using Microsoft.AspNetCore.Cors.Infrastructure;
using StayStop.BLL.Dtos.User;
using StayStop.BLL.IService;
using StayStop.BLL.Pagination;
using StayStop.BLL.Validators;
using StayStop.DAL.Context;
using StayStop.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator<HotelPagination>, HotelPaginationValidator>();
builder.Services.AddScoped<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();
//builder.Services.AddScoped<IReservationService, >();
//builder.Services.AddScoped<IHotelService, >();
//builder.Services.AddScoped<IRoomService, >();
//builder.Services.AddScoped<IAccountService, >();
//builder.Services.AddScoped<IReservationPositionService, >();
//builder.Services.AddScoped<IOpinionService, >();
builder.Services.AddDbContext<StayStopDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
