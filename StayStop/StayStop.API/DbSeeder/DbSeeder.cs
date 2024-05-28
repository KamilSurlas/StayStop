using Microsoft.IdentityModel.Tokens;
using StayStop.DAL.Context;
using StayStop.Model;
using StayStop.Model.Constants;

namespace StayStop.API.DbSeeder
{
    public class DbSeeder : IDbSeeder
    {
        private readonly StayStopDbContext _context;

        public DbSeeder(StayStopDbContext context)
        {
            _context = context;
        }
        
        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.Roles.Any())
                {
                    var roles = GetRoles();
                    _context.Roles.AddRange(roles);
                    _context.SaveChanges();
                }
            }
         }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
         {
             new Role()
             {
                 RoleName = UserRole.Admin
             },
             new Role()
             {
                 RoleName = UserRole.Manager
             },
             new Role()
             {
                 RoleName = UserRole.User
             },
             new Role()
             {
                 RoleName = UserRole.HotelOwner
             }
         };
            return roles;
        }
    }
}
