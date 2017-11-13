/*
 * Parcel Logistics Service
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 2.2.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.SwaggerGen.Annotations;
using IO.Swagger.Models;
using PLS.SKS.Package;
using PLS.SKS.Package.BusinessLogic;

namespace IO.Swagger.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    public class DefaultApiController : Controller
    {
		PLS.SKS.Package.BusinessLogic.BusinessLogic bl;

		public DefaultApiController(BusinessLogic bl) //ItrackingLogic, IMapper
		{
			this.bl = bl;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>Exports the hierarchy of &#x60;Warehouse&#x60; and &#x60;Truck&#x60; objects. </remarks>
		/// <response code="200">Successful response</response>
		/// <response code="404">No hierarchy loaded yet.</response>
		/// <response code="500">An error occurred loading.</response>
		[HttpGet]
        [Route("/api/warehouse")]
        [SwaggerOperation("ExportWarehouses")]
        [SwaggerResponse(200, type: typeof(Warehouse))]
        public virtual IActionResult ExportWarehouses()
        {
			Warehouse warehouse = bl.ExportWarehouses();
            string exampleJson = null;
           
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<Warehouse>(exampleJson)
            : default(Warehouse);
            return new ObjectResult(example);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Imports a hierarchy of &#x60;Warehouse&#x60; and &#x60;Truck&#x60; objects. </remarks>
        /// <param name="warehouseRoot"></param>
        /// <response code="200">Successfully loaded.</response>
        /// <response code="500">The operation failed due to an error.</response>
        [HttpPost]
        [Route("/api/warehouse")]
        [SwaggerOperation("ImportWarehouses")]
        public virtual void ImportWarehouses([FromBody]Warehouse warehouseRoot)
        {
			string exampleJson = null;

			var example = exampleJson != null
			? JsonConvert.DeserializeObject<InlineResponse200>(exampleJson)
			: default(InlineResponse200);

		}


        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Report that a &#x60;Parcel&#x60; has arrived at a certain hop either &#x60;Warehouse&#x60; or &#x60;Truck&#x60;. </remarks>
        /// <param name="trackingId">The tracking ID of the parcel.</param>
        /// <param name="code">The &#x60;Code&#x60; of the hop (&#x60;Warehouse&#x60; or &#x60;Truck&#x60;).</param>
        /// <response code="200">Successfully reported hop.</response>
        /// <response code="500">The operation failed due to an error.</response>
        [HttpPost]
        [Route("/api/parcel/{trackingId}/reportHop/{code}")]
        [SwaggerOperation("ReportParcelHop")]
        public virtual void ReportParcelHop([FromRoute]string trackingId, [FromRoute]string code)
        {
			string exampleJson = null;

			var example = exampleJson != null
			? JsonConvert.DeserializeObject<InlineResponse200>(exampleJson)
			: default(InlineResponse200);
		}


        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Submit a new parcel to the logistics service. </remarks>
        /// <param name="newParcel"></param>
        /// <response code="200">Successfully submitted the new parcel</response>
        /// <response code="500">The operation failed due to an error.</response>
        [HttpPost]
        [Route("/api/parcel")]
        [SwaggerOperation("SubmitParcel")]
        [SwaggerResponse(200, type: typeof(InlineResponse200))]
        public virtual IActionResult SubmitParcel([FromBody]Parcel newParcel)
        {
			string trNr = bl.addParcel(newParcel);
			//Test it!!
			InlineResponse200 inlineR = new InlineResponse200(trNr);
            return new ObjectResult(inlineR);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Track a parcel with its ID. </remarks>
        /// <param name="trackingId">The tracking ID of the parcel.</param>
        /// <response code="200">Parcel exists, here&#39;s the tracking information.</response>
        /// <response code="404">Parcel does not exist with this tracking ID.</response>
        [HttpGet]
        [Route("/api/parcel/{trackingId}")]
        [SwaggerOperation("TrackParcel")]
        [SwaggerResponse(200, type: typeof(TrackingInformation))]
        public virtual IActionResult TrackParcel([FromRoute]string trackingId)
        { 
			TrackingInformation trInfo = bl.trackParcel(trackingId);

			//string exampleJson = null;
			//var example = exampleJson != null
            //? JsonConvert.DeserializeObject<Parcel>(sParcel.ToString())
            //: default(Parcel);
			return new ObjectResult(trInfo);

		}

        [HttpGet]
        [Route("/test")]
        [SwaggerOperation("TestFunktion")]
        [SwaggerResponse(200)]
        public virtual IActionResult TestFunktion()
        {
            

            string exampleJson = null;
            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<string>(exampleJson)
                : default(string);
            return new ObjectResult(example);
        }
    }
}
