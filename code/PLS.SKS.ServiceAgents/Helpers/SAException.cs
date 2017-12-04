using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.ServiceAgents
{
    public class SAException : Exception
    {
		public SAException()
		{

		}

		public SAException(string message) : base(message)
		{

		}

		public SAException(string message, Exception inner) : base(message, inner)
		{

		}
	}
}
