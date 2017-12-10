using System;
using System.Data.SqlClient;

namespace PLS.SKS.Package.DataAccess.Sql.Helpers
{
    public class DalException : Exception
    {
		public DalException()
		{

		}

		public DalException(string message) : base(message)
		{

		}

		public DalException(string message, Exception inner) : base(message, inner)
		{

		}

        public DalException(string message, SqlException inner) : base(message, inner)
        {

        }
    }
}
