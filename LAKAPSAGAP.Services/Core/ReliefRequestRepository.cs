using System.Text.Json;
using LAKAPSAGAP.Models.Models;
namespace LAKAPSAGAP.Services.Core
{
	public class ReliefRequestRepository : IReliefRequestRepository
	{
		readonly HttpClient _httpClient;
		readonly MyDbContext _context;
		public ReliefRequestRepository(HttpClient httpClient, MyDbContext context)
		{
			_httpClient = httpClient;
			_context = context;
		}

		public async Task<List<string>> GetAllBarangayAsync()
		{
			List<string> Barangay = new List<string>();
			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync("https://psgc.gitlab.io/api/cities/137504000/barangays/");

				string json = await response.Content.ReadAsStringAsync();
				var barangays = JsonSerializer.Deserialize<List<Barangay>>(json, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

				Barangay = barangays.Select(x => x.Name).ToList();
				return Barangay;
			}
			catch (Exception)
			{
				return Barangay;
				
			}
		}

		public async Task<List<Kit>> GetAllKitAsync()
		{
			List<Kit> kitList = new();
			try
			{
				kitList = await _context.Kits.WhereIsNotArchivedAndDeleted().Include(x => x.KitComponentList).ThenInclude(x => x.StockItem).ToListAsync();
				return kitList;
			}
			catch (Exception)
			{

				return kitList;
			}
		}

		public async Task<List<StockItem>> GetAllStockItemAsync()
		{
			List<StockItem> stockItemList = new();
			try
			{
				stockItemList = await _context.StockItems.WhereIsNotArchivedAndDeleted().ToListAsync();
				return stockItemList;
			}
			catch (Exception)
			{

				return stockItemList;
			}
		}
	}
}
