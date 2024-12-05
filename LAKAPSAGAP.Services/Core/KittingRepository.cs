using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAKAPSAGAP.Services.Core.Helpers;

namespace LAKAPSAGAP.Services.Core
{
	public class KittingRepository : IKittingRepository
	{
		public readonly MyDbContext _context;
		public KittingRepository(MyDbContext context)
		{
			_context = context;
		}
		public async Task<Kit> CreateKit(KitViewModel kitViewModel)
		{
			try
			{
				int kitCount = await _context.GetCount<Kit>();
				string Id = IdGenerator.GenerateId(IdGenerator.PFX_KIT, kitCount);
				var newKit = new Kit
				{
					Id = Id,
					Name = kitViewModel.Name,
					Description = kitViewModel.Description,
					
				};

				int kitCtCount = await _context.GetCount<KitComponent>();

				List<KitComponent> KitComponentList = kitViewModel.KitComponentList.Select((kitComponent,index) =>
				{
					kitCtCount++;
					string Id = IdGenerator.GenerateId(IdGenerator.PFX_KITCOMPONENT, kitCtCount-1);
					return new KitComponent
					{
						Id = Id,
						KitId = newKit.Id,
						StockItemId = kitComponent.StockItemId,
						Quantity = kitComponent.Quantity
					};
				}).ToList();

				newKit.KitComponentList = KitComponentList;

				await _context.Create<Kit>(newKit);

				await _context.SaveChangesAsync();
				return newKit;
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<Kit> UpdateKit(KitViewModel kitViewModel)
		{
			try
			{
				var kit = await _context.Kits.FindAsync(kitViewModel.Id);
				if (kit != null)
				{
					kit.Name = kitViewModel.Name;
					kit.Description = kitViewModel.Description;
					kit.KitType = kitViewModel.KitType;
	
					await _context.SaveChangesAsync();
				}
				return kit;
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<Kit> DeleteKit(string Id)
		{
			try
			{
				var kit = await _context.Kits.FindAsync(Id);
				if (kit != null)
				{
					kit.IsDeleted = true;
					await _context.SaveChangesAsync();
				}
				return kit;
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<Kit> ArchiveKit(string Id)
		{
			try
			{
				var kit = await _context.Kits.FindAsync(Id);
				if (kit != null)
				{
					kit.isArchived = true;
					 await _context.SaveChangesAsync();
				}
				return kit;
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<Kit> GetKitById(string Id)
		{
			try
			{
				return await _context.GetById<Kit>(Id);
			}
			catch (Exception)
			{

				throw;
			}
			
		}
		public  async Task<List<Kit>> GetAllKit()
		{
			try
			{
				return await _context.GetAll<Kit>();
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<KitComponent> CreateKitComponent(KitComponentViewModel kitComponentViewModel)
		{
			try
			{
				int count = await _context.GetCount<KitComponent>();
				string Id = IdGenerator.GenerateId(IdGenerator.PFX_KITCOMPONENT, count);
				var newKitComponent = new KitComponent
				{
					Id = Id,
					KitId = kitComponentViewModel.KitId,
					StockItemId = kitComponentViewModel.StockItemId,
					Quantity = kitComponentViewModel.Quantity
				};

				await _context.Create<KitComponent>(newKitComponent);
				return newKitComponent;
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<KitComponent> UpdateKitComponent(KitComponentViewModel kitComponentViewModel)
		{
			try
			{
				var kitComponent = await _context.KitComponents.FindAsync(kitComponentViewModel.Id);
				if (kitComponent != null)
				{
					kitComponent.StockItemId = kitComponentViewModel.StockItemId;
					kitComponent.Quantity = kitComponentViewModel.Quantity;
					await _context.SaveChangesAsync();
				}
				return kitComponent;
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<KitComponent> DeleteKitComponent(string Id)
		{
			try
			{
				var kitComponent = await _context.KitComponents.FindAsync(Id);
				if (kitComponent != null)
				{
					kitComponent.IsDeleted = true;
					await _context.SaveChangesAsync();
				}
				return kitComponent;
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<KitComponent> ArchiveKitComponent(string Id)
		{
			try
			{
				var kitComponent = await _context.KitComponents.FindAsync(Id);
				if (kitComponent != null)
				{
					kitComponent.isArchived = true;
					await _context.SaveChangesAsync();
				}
				return kitComponent;
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<KitComponent> GetKitComponentById(string Id)
		{
			try
			{
				return await _context.GetById<KitComponent>(Id);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<List<KitComponent>> GetAllKitComponent()
		{
			try
			{
				return await _context.GetAll<KitComponent>();
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<List<StockItem>> GetAllStockItemsAsync()
		{
			List<StockItem> stockItems = new();
			try
			{
				stockItems = await _context.StockItems.WhereIsNotArchivedAndDeleted().ToListAsync();
				return stockItems;
			}
			catch (Exception)
			{

				return stockItems;
			}
		}

		public async Task<List<Kit>> GetAllKitsAsync()
		{
			List<Kit> kits = new();
			try
			{

				kits = await _context.Kits.WhereIsNotArchivedAndDeleted().Include(x=>x.KitComponentList).ThenInclude(x=>x.StockItem).ToListAsync();
				return kits;
			}
			catch (Exception)
			{

				return kits;
			}
		}
	}
}
