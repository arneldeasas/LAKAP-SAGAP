﻿using LAKAPSAGAP.BlazorServer.Pages.UserManagement;
using Radzen;

namespace LAKAPSAGAP.BlazorServer.Pages.Stocks
{
	public partial class Stocks
	{
		[Parameter] public static string? id { get; set; }
		[Inject] DialogService _dialogService { get; set; }

		private List<BreadcrumbViewModel> Breadcrumbs = new()
		{
			new BreadcrumbViewModel { Path = "javascript:void(0);", Text = "Warehouse" },
			new BreadcrumbViewModel { Path = $@"/Warehouse/{id}/Stocks", Text = "Stocks" },
		};

	}
}
