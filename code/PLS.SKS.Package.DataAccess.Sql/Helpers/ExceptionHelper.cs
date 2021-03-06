﻿using System.Data.SqlClient;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Sql.Helpers
{
    public static class ExceptionHelper
    {
		public static string BuildSqlExceptionMessage(SqlException ex)
		{
			StringBuilder errorMessages = new StringBuilder();
			for (int i = 0; i < ex.Errors.Count; i++)
			{
				errorMessages.Append("Index #" + i + "\n" +
					"Message: " + ex.Errors[i].Message + "\n" +
					"LineNumber: " + ex.Errors[i].LineNumber + "\n" +
					"Source: " + ex.Errors[i].Source + "\n" +
					"Procedure: " + ex.Errors[i].Procedure + "\n");
			}
			return errorMessages.ToString();
		}
	}
}
