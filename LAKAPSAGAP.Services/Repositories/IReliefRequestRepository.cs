using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Services.Repositories
{
	public interface IReliefRequestRepository
	{
		Task<List<string>> GetAllBarangayAsync();
		Task<List<StockItem>> GetAllStockItemAsync();
		Task<List<Kit>> GetAllKitAsync();
		Task<string?> CreateRequestAsync(ReliefRequestDetailViewModel reliefRequestVM);
		Task<List<ReliefRequestDetail>> GetAllRequestsAsync();
		Task<List<ReliefRequestDetail>> GetOnGoingRequests();
		Task<List<ReliefRequestDetail>> GetDoneRequests(); //history
		Task<bool> CancelRequest(string requestId);
		Task<bool> RejectRequest(string requestId);
		Task<bool> ApproveRequest(string requestId);
		Task<bool> SendRelief(ReliefSentViewModel reliefSentVM);
	}
}
