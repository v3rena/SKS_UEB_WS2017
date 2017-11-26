using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic
{
	public class BLException : Exception
	{
		public BLException()
		{

		}

		public BLException(string message) : base(message)
		{

		}

		public BLException(string message, Exception inner) : base(message, inner)
		{

		}
	}
}
