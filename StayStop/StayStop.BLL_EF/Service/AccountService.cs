using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StayStop.BLL.Authentication;
using StayStop.BLL.Dtos.User;
using StayStop.BLL.Exceptions;
using StayStop.BLL.IService;
using StayStop.DAL.Context;
using StayStop.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL_EF.Service
{
    public class AccountService : IAccountService
    {
        private readonly StayStopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        public AccountService(StayStopDbContext context, IMapper mapper, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public string LoginUser(UserLoginDto dto)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == dto.EmailAddress);
            if (user is null)
            {
                throw new BadEmailOrPassword("Invalid email or password");
            }
            var verifyPassword = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, dto.Password);
            if (verifyPassword == PasswordVerificationResult.Failed)
            {
                throw new BadEmailOrPassword("Invalid email or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Name}{user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.RoleName}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiredDate = DateTime.Now.AddDays(_authenticationSettings.JwtExpiredDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expiredDate, signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void RegisterUser(UserRegisterDto dto)
        {
            var newUser = _mapper.Map<User>(dto);
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
    }
}
