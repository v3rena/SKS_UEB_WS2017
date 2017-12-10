using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using IO.Swagger.Models;
using Microsoft.Extensions.Logging;
using PLS.SKS.Package.Services.Helpers;

namespace PLS.SKS.Package.Services.Pages
{
	public class SubmitParcelModel : PageModel
    {
		ILogger<SubmitParcelModel> logger;

		[BindProperty]
		public Parcel Parcel { get; set; }
		[BindProperty]
		public InlineResponse200 InlineResponse200 { get; set; }

		public SubmitParcelModel(ILogger<SubmitParcelModel> logger)
		{
			this.logger = logger;
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
				var stringTask = client.PostAsJsonAsync("/api/parcel", Parcel);
				var msg = await stringTask;
				if (msg.IsSuccessStatusCode)
				{
					InlineResponse200 = msg.Content.ReadAsAsync<InlineResponse200>().Result;
				}
				return Page();
			}
			catch(Exception ex)
			{
				logger.LogError("Api call failed", ex);
				throw new ServiceException("Api call failed", ex);
			}
		}
	}
}