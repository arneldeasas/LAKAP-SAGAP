using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Services.Repositories
{
	public interface IKittingRepository
	{
		public Task<Kit> CreateKit(KitViewModel kitViewModel);
		public Task<Kit> UpdateKit(KitViewModel kitViewModel);
		public Task<Kit> DeleteKit(string Id);
		public Task<Kit> ArchiveKit(string Id);
		public Task<Kit> GetKitById(string Id);
		public Task<List<Kit>> GetAllKit();

	}
}
