using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StayStop.API.DbSeeder;
using StayStop.BLL.Authentication;
using StayStop.BLL.Authorization;
using StayStop.BLL.Dtos.Hotel;
using StayStop.BLL.Dtos.Room;
using StayStop.BLL.Dtos.User;
using StayStop.BLL.IService;
using StayStop.BLL.Middleware;
using StayStop.BLL.Pagination;
using StayStop.BLL.Validators;
using StayStop.BLL_EF.Service;
using StayStop.DAL.Context;
using StayStop.Model;
using System;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme,
        securityScheme: new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter the Baerer Authorization : `Bearer Generated-JWT-Token`",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            }
        }, new string[] {  }
        }
    });
});
builder.Services.AddDbContext<StayStopDbContext>();
builder.Services.AddScoped<IValidator<HotelPagination>, HotelPaginationValidator>();
builder.Services.AddScoped<IValidator<ReservationPagination>, ReservationPaginationValidator>();
builder.Services.AddScoped<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();
builder.Services.AddScoped<IValidator<HotelRequestDto>,HotelRequestDtoValidator>();
builder.Services.AddScoped<IValidator<HotelUpdateRequestDto>,HotelUpdateRequestDtoValidator>();
builder.Services.AddScoped<IValidator<RoomRequestDto>, RoomRequestDtoValidator>();
builder.Services.AddScoped<IValidator<RoomUpdateRequestDto>, RoomUpdateRequestDtoValidator>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IOpinionService, OpinionService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IReservationPositionService, ReservationPositionService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<DbSeeder>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IUserContextService,UserContextService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IAuthorizationHandler, HotelOperationRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, RoomOperationRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, OpinionOperationRequirementHandler>();
builder.Services.AddFluentValidationAutoValidation();
var authSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authSettings);
builder.Services.AddSingleton(authSettings);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authSettings.JwtIssuer,
        ValidAudience = authSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.JwtKey))
    };
});

var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
seeder.Seed();
app.UseMiddleware<ErrorHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseCors(optBuilder => optBuilder
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowAnyOrigin()
                                .Build());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
