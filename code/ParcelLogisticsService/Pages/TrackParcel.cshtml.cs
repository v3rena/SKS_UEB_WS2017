using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using IO.Swagger.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using PLS.SKS.Package.Services.Helpers;

namespace PLS.SKS.Package.Services.Pages
{
    public class TrackParcelModel : PageModel
    {
	    readonly ILogger<TrackParcelModel> _logger;

		[BindProperty]
		public TrackingInformation TrackingInformation { get; set; }
		[BindProperty]
		public string TrackingNumber { get; set; }

		public TrackParcelModel(ILogger<TrackParcelModel> logger)
		{
			_logger = logger;
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var client = new HttpClient
			{
#if DEBUG
				BaseAddress = new Uri("http://localhost:56172")
#else
				BaseAddress = new Uri("http://parcellogisticsservice.azurewebsites.net")
#endif
			};
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			try
			{
				var stringTask = client.GetAsync($"/api/parcel/{TrackingNumber}");
				var msg = await stringTask;
				if (msg.IsSuccessStatusCode)
				{
					TrackingInformation = msg.Content.ReadAsAsync<TrackingInformation>().Result;
				}
				return Page();
			}
			catch (Exception ex)
			{
				_logger.LogError("Api call failed", ex);
				throw new ServiceException("Api call failed", ex);
			}
		}
	}
}