using Microsoft.IdentityModel.Tokens;
using StayStop.DAL.Context;
using StayStop.Model;

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
                 RoleName = "Admin"
             },
             new Role()
             {
                 RoleName = "Manager"
             },
             new Role()
             {
                 RoleName = "User"
             }
         };
            return roles;
        }
    }
}
