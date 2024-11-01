using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Services.Repositories
{
    public interface IAuthRepository
    {
        public Task Authenticate(LoginViewModel login);
    }
}
