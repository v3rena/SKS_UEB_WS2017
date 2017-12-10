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
    public class ReportHopModel : PageModel
    {
        readonly ILogger<ReportHopModel> _logger;

        [BindProperty]
        public string Code { get; set; }
        [BindProperty]
        public string TrackingNumber { get; set; }
        [BindProperty]
        public string Message { get; set; }

        public ReportHopModel(ILogger<ReportHopModel> logger)
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
                //BaseAddress = new Uri("http://localhost:50074")
#else
				BaseAddress = new Uri("http://parcellogisticsservice.azurewebsites.net")
#endif
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var stringContent = new StringContent("");
                var stringTask = client.PostAsync($"/api/parcel/{TrackingNumber}/reportHop/{Code}", stringContent);
                var msg = await stringTask;
                if (msg.IsSuccessStatusCode)
                {
                    Message = "Hop was successfully reported!";
                }
                else
                {
                    Message = "Hop was not reported, check for correct Input!";
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