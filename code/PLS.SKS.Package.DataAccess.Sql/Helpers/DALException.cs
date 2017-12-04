using Microsoft.Extensions.Logging;
using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Sql
{
    public class DALException : Exception
    {
		IExceptionHelper exceptionHelper = new ExceptionHelper();

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

        }
    }
}
