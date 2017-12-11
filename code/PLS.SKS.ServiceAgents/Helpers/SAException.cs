using System;

namespace PLS.SKS.ServiceAgents.Helpers
{
    public class SaException : Exception
    {
		public SaException()
		{

		}

		public SaException(string message) : base(message)
		{

		}

		public SaException(string message, Exception inner) : base(message, inner)
		{

		}
	}
}
