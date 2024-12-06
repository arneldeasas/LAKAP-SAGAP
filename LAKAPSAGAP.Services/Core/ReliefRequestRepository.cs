using System.IO.Enumeration;
using System.Security.Claims;
using System.Text.Json;
using LAKAPSAGAP.Models.Models;
using LAKAPSAGAP.Services.Core.Helpers;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
namespace LAKAPSAGAP.Services.Core
{
	public class ReliefRequestRepository : IReliefRequestRepository
	{
		readonly HttpClient _httpClient;
		readonly MyDbContext _context;
		readonly HttpContextAccessor _httpContext;
		public ReliefRequestRepository(HttpClient httpClient, MyDbContext context,HttpContextAccessor httpContextAccessor)
		{
			_httpClient = httpClient;
			_context = context;
			_httpContext = httpContextAccessor;
		}

		public async Task<string?> CreateRequestAsync(ReliefRequestDetailViewModel reliefRrequestVM)
		{
			string? _id =null;
			var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				string? userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
				int count = await _context.GetCount<ReliefRequestDetail>();
				int countRequest = await _context.GetCount<Request>();

				string Id = IdGenerator.GenerateId(IdGenerator.PFX_REQUEST, count);
				ReliefRequestDetail requestDetail = new ReliefRequestDetail
				{
					Id = Id,
					RequestedById = userId ?? "",
					Reason = reliefRrequestVM.Reason,
					Status = reliefRrequestVM.Status,
					SpecificReason = reliefRrequestVM.SpecificReason,
					AdditionalNotes = reliefRrequestVM.AdditionalNotes,
					NumberOfRecipients = reliefRrequestVM.NumberOfRecipients,
					Organization = reliefRrequestVM.Organization,
					TargetDateToReceive = reliefRrequestVM.TargetDateToReceive,
					ReceiverAddress = reliefRrequestVM.ReceiverAddress,
					ReceiverName = reliefRrequestVM.ReceiverName,
					ContactNumber = int.Parse(reliefRrequestVM.ContactNumber),

					RequestList = reliefRrequestVM.RequestList.Select(x =>
					{
						countRequest++;
						return new Request
						{
							Id = IdGenerator.GenerateId(IdGenerator.PFX_REQUEST, countRequest),
							ReliefRequestId = Id,
							UnitId = x.UnitId,
							RequestType = x.RequestType,
							Quantity = x.Quantity,

						};
					}).ToList(),

				};

				_id = (await _context.Create<ReliefRequestDetail>(requestDetail)).Id;

				await _context.SaveChangesAsync();

				List<RequestFileMetadata> fileMetadataList = UploadAttachment(reliefRrequestVM.FileList);


				int countAttachment = await _context.GetCount<RequestAttachment>();
				List<RequestAttachment> requestAttachments = fileMetadataList.Select(x => new RequestAttachment
				{
					Id = IdGenerator.GenerateId(IdGenerator.PFX_REQUESTATTACHMENT, countAttachment),
					ReliefRequestId = _id,
					Url = x.FilePath,

				}).ToList();

				await _context.AddRangeAsync(requestAttachments);

				await _context.SaveChangesAsync();

				return _id;
			}
			catch (Exception)
			{
				transaction.Rollback();
				return _id;
			}
		}

		 List<RequestFileMetadata> UploadAttachment(List<IBrowserFile> files)
		{
			List<RequestFileMetadata> fileMetadataList = new();
			string path = Path.Combine("wwwroot","request_attachments");
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			fileMetadataList = files.Select(x => new RequestFileMetadata
			{
				FilePath = Path.Combine(path, x.Name)
			}).ToList();
			return fileMetadataList;
		}

		class RequestFileMetadata
		{
			public string FilePath { get; set; }
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
