using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLS.SKS.Package.Services
{
	public class ServiceException : Exception
	{
		ILogger<ServiceException> logger;

		public ServiceException()
		{

		}

		public ServiceException(string message) : base(message)
		{

		}

		public ServiceException(string message, Exception inner) : base(message, inner)
		{

		}
	}
}
