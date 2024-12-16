using Mapster;

namespace LAKAPSAGAP.BlazorServer.Pages.Requesting
{
	public partial class RequestingForm
	{
		[Inject] IJSRuntime _jSRuntime { get; set; } = default!;
		[Inject] DialogService _dialogService { get; set; }
		[Inject] IReliefRequestRepository ReliefRequestRepository { get; set; }
		[Inject] NavigationManager _navManager { get; set; }
		//Form Initializations
		List<string> BarangayList { get; set; } = new();
		List<Kit> KitList { get; set; } = new();
		List<StockItem> StockItemList { get; set; } = new();

		//Form State
		ReliefRequestDetailViewModel ReliefRequestVM { get; set; } = new();
		UnitFormViewModel KitVM { get; set; } = new();
		UnitFormViewModel ItemVM { get; set; } = new();

		bool _isBusy = false;
		protected override async Task OnInitializedAsync()
		{
			Task<List<string>> BarangayListTask = ReliefRequestRepository.GetAllBarangayAsync();

			KitList = await ReliefRequestRepository.GetAllKitAsync();
			StockItemList = await ReliefRequestRepository.GetAllStockItemAsync();
			BarangayList = await BarangayListTask;
		}

		void AddItem(UnitFormViewModel unitVM)
		{
			ReliefRequestVM.RequestList.Add(new RequestViewModel
			{
				RequestType = RequestType.Item,
				UnitId = unitVM.UnitId,
				Quantity = unitVM.Quantity,
				UnitName = StockItemList.FirstOrDefault(x => x.Id == unitVM.UnitId)?.Name ?? ""

			});
			unitVM.resetForm();
		}
		void AddKit(UnitFormViewModel unitVM)
		{

			ReliefRequestVM.RequestList.Add(new RequestViewModel
			{
				RequestType = RequestType.Kit,
				UnitId = unitVM.UnitId,
				Quantity = unitVM.Quantity,
				UnitName = KitList.FirstOrDefault(x => x.Id == unitVM.UnitId)?.Name ?? ""
			});
			unitVM.resetForm();
		}

		async void HandleFileChange (InputFileChangeEventArgs e)
		{
			try
			{
				ValidateFiles(e.GetMultipleFiles().ToList());

				ReliefRequestVM.FileList.AddRange(e.GetMultipleFiles());
			}
			catch (Exception x)
			{

				await _jSRuntime.InvokeVoidAsync("Toast", "error", x.Message);
			}
		
			

		}
		void ValidateFiles(List<IBrowserFile> files)
		{
			int maxFileSize = 2097152;
			string[] allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".pdf" };
			foreach (var file in files)
			{
				string extension = Path.GetExtension(file.Name)?.ToLowerInvariant();
				if (!allowedExtensions.Contains(extension)) throw new Exception("Please upload only supported images or pdf files");
				if(file.Size > maxFileSize) throw new Exception("File size is too large. Maximum file size is 2MB");
			}
		}
		void ValidateForm()
		{
			if (ReliefRequestVM.RequestList.Count <= 0)
			{
				throw new Exception("Please add at least one item or kit to request");
			}
			if (ReliefRequestVM.FileList.Count <= 0)
			{
				throw new Exception("Please attach a required document.");
			}
		}
		async Task SubmitRequest()
		{
			ValidateForm();
			

			if (!(await _jSRuntime.InvokeAsync<bool>("Confirmation"))) return;
			try
			{
				_isBusy = true;
				StateHasChanged();
				string id = await ReliefRequestRepository.CreateRequestAsync(ReliefRequestVM);

				if(string.IsNullOrEmpty(id))
				{
					await _jSRuntime.InvokeVoidAsync("Toast", "error", "An error occured. Something went wrong!");
				}
				else
				{
					await _jSRuntime.InvokeVoidAsync("Toast", "success", "Request submitted successfully!");
					_navManager.NavigateTo("/barangay-rep/requests");
				}
			}
			catch (Exception e)
			{
				
				await _jSRuntime.InvokeVoidAsync("Toast", "error", e.Message);
                _isBusy = false;
                StateHasChanged();
            }
		
		
		}
	}

	public class UnitFormViewModel
	{
		public string UnitId { get; set; }
		public string SearchString { get; set; }
		public int Quantity { get; set; }

		public void resetForm()
		{
			UnitId = "";
			Quantity = 0;
		}
	}
}