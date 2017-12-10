using System;

namespace PLS.SKS.Package.BusinessLogic.Helpers
{
	public class BlException : Exception
	{
		public BlException()
		{

		}

		public BlException(string message) : base(message)
		{

		}

		public BlException(string message, Exception inner) : base(message, inner)
		{

		}
	}
}
