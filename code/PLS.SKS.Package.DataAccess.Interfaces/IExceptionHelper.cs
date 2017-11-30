using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Interfaces
{
    public interface IExceptionHelper
    {
		string BuildSqlExceptionMessage(SqlException ex);
	}
}
