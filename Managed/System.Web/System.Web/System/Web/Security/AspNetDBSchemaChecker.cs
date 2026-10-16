using System;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;

namespace System.Web.Security
{
	// Token: 0x020004B4 RID: 1204
	internal static class AspNetDBSchemaChecker
	{
		// Token: 0x0600363A RID: 13882 RVA: 0x0008BEE0 File Offset: 0x0008A0E0
		private static DbConnection CreateConnection(DbProviderFactory factory, string connStr)
		{
			DbConnection dbConnection = factory.CreateConnection();
			dbConnection.ConnectionString = connStr;
			dbConnection.Open();
			return dbConnection;
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x0008BEF8 File Offset: 0x0008A0F8
		public static bool CheckMembershipSchemaVersion(DbProviderFactory factory, string connStr, string feature, string compatibleVersion)
		{
			bool flag;
			using (DbConnection dbConnection = AspNetDBSchemaChecker.CreateConnection(factory, connStr))
			{
				DbCommand dbCommand = factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandText = "aspnet_CheckSchemaVersion";
				dbCommand.CommandType = CommandType.StoredProcedure;
				AspNetDBSchemaChecker.AddParameter(factory, dbCommand, "@Feature", ParameterDirection.Input, feature);
				AspNetDBSchemaChecker.AddParameter(factory, dbCommand, "@CompatibleSchemaVersion", ParameterDirection.Input, compatibleVersion);
				DbParameter dbParameter = AspNetDBSchemaChecker.AddParameter(factory, dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, null);
				try
				{
					dbCommand.ExecuteNonQuery();
				}
				catch (Exception)
				{
					throw new ProviderException("ASP.NET Membership schema not installed.");
				}
				if ((int)(dbParameter.Value ?? (-1)) == 0)
				{
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x0008BFB8 File Offset: 0x0008A1B8
		private static DbParameter AddParameter(DbProviderFactory factory, DbCommand command, string parameterName, ParameterDirection direction, object parameterValue)
		{
			DbParameter dbParameter = command.CreateParameter();
			dbParameter.ParameterName = parameterName;
			dbParameter.Value = parameterValue;
			dbParameter.Direction = direction;
			command.Parameters.Add(dbParameter);
			return dbParameter;
		}
	}
}
