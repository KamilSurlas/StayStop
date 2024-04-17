using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Cors.Infrastructure;
using StayStop.BLL.Dtos.Hotel;
using StayStop.BLL.Dtos.User;
using StayStop.BLL.IService;
using StayStop.BLL.Middleware;
using StayStop.BLL.Pagination;
using StayStop.BLL.Validators;
using StayStop.BLL_EF.Service;
using StayStop.DAL.Context;
using StayStop.Model;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StayStopDbContext>();
builder.Services.AddScoped<IValidator<HotelPagination>, HotelPaginationValidator>();
builder.Services.AddScoped<IValidator<ReservationPagination>, ReservationPaginationValidator>();
builder.Services.AddScoped<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();
builder.Services.AddScoped<IValidator<HotelRequestDto>,HotelRequestDtoValidator>();
builder.Services.AddScoped<IValidator<HotelUpdateRequestDto>,HotelUpdateRequestDtoValidator>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IOpinionService, OpinionService>();
//builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IReservationPositionService, ReservationPositionService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
