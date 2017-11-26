using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Sql
{
    public class DALException : Exception
    {
		ILogger<DALException> logger;
		ExceptionHelper exceptionHelper = new ExceptionHelper();

		public DALException()
		{

		}

		public DALException(string message) : base(message)
		{

		}

		public DALException(string message, Exception inner) : base(message, inner)
		{

		}

		public DALException(string message, SqlException inner) : base(message, inner)
		{
			//logger.LogError(exceptionHelper.BuildSqlExceptionMessage(inner));
		}
	}
}
