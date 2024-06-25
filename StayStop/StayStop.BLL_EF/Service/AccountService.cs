using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StayStop.BLL.Authentication;
using StayStop.BLL.Dtos.User;
using StayStop.BLL.Exceptions;
using StayStop.BLL.IService;
using StayStop.DAL.Context;
using StayStop.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
        private readonly IUserContextService _userContextService;

        public AccountService(StayStopDbContext context, IMapper mapper, IPasswordHasher<User> passwordHasher, 
            AuthenticationSettings authenticationSettings, IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _userContextService = userContextService;
        }
        private string GetToken(User user)
        {
           

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Name}"),
                new Claim(ClaimTypes.Surname, $"{user.LastName}"),
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
        private string GetRefreshToken()
        {
            var randomNumber = new byte[64];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
               
            return Convert.ToBase64String(randomNumber);
        }   
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParamters = new TokenValidationParameters()
            {
                ValidIssuer = _authenticationSettings.JwtIssuer,
                ValidAudience = _authenticationSettings.JwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParamters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.
                Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)) 
            {
                throw new SecurityTokenException("Invalid token");

            }
            return principal;
        }
        private UserTokenResponse CreateToken(User user, bool populateExp)
        {
            var refreshToken = GetRefreshToken();

            user.RefreshToken = refreshToken;

            if (populateExp)
            {
                user.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(7);
            }


            _context.Update(user);
            _context.SaveChanges();
            var token = GetToken(user);
            var response = new UserTokenResponse(token, refreshToken);



            return response;
        }
        public UserTokenResponse LoginUser(UserLoginDto dto, bool populateExp = true)
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
           
            return CreateToken(user, populateExp);
        }      
        public void RegisterUser(UserRegisterDto dto)
        {
            var newUser = _mapper.Map<User>(dto);
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public UserTokenResponse RefreshToken(UserTokenResponse token)
        {
            var principal = GetPrincipalFromExpiredToken(token.AccessToken);
            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
            {
                throw new SecurityTokenException("UserId was not found");
            }

            var userId = int.Parse(userIdClaim.Value);
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserId == userId);

            if (user == null || user.RefreshToken != token.RefreshToken || user.RefreshTokenExpiryDate <= DateTime.UtcNow)
            {
                throw new SecurityTokenException();
            }

            return CreateToken(user, false);
        }

        public void UpdateUser(UserUpdateRequestDto dto)
        {
            var user = _userContextService.User;
            Debug.WriteLine("XD");
            //user.Claims.
        }
    }
}
