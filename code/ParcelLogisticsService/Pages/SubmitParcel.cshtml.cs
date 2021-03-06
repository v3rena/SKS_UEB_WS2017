﻿using System;
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
	    readonly ILogger<SubmitParcelModel> _logger;

		[BindProperty]
		public Parcel Parcel { get; set; }
		[BindProperty]
		public InlineResponse200 InlineResponse200 { get; set; }
	    [BindProperty]
	    public string Message { get; set; }

		public SubmitParcelModel(ILogger<SubmitParcelModel> logger)
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
                BaseAddress = new Uri("http://localhost:59408")
                //BaseAddress = new Uri("http://localhost:50074")
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
				else
				{
					Message = "Could not submit parcel, please check for correct input!";
				}
				return Page();
			}
			catch(Exception ex)
			{
				_logger.LogError("Api call failed", ex);
				throw new ServiceException("Api call failed", ex);
			}
		}
	}
}