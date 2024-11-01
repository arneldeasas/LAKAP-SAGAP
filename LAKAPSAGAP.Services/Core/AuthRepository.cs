using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Services.Core
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MyDbContext _context;
        public AuthRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task Authenticate(LoginViewModel login)
        {
            var user = await _context.UserAuth.FirstOrDefaultAsync(x => x.UserName == login.Username);
            if (user == null)
            {
                throw new Exception("Invalid username or password");
            }
            if (user.PasswordHash != login.Password)
            {
                throw new Exception("Invalid username or password");
            }
        }
    }
}
