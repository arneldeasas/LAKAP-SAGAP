using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Services.Core
{
    public class KittingRepository : IKittingRepository
    {
        public Task<Kit> CreateKit(KitViewModel kitViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Kit> UpdateKit(KitViewModel kitViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
