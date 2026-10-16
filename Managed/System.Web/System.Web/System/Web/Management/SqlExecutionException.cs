using System;
using System.Data.SqlClient;
using Unity;

namespace System.Web.Management
{
	/// <summary>Defines a class for SQL execution exceptions in the <see cref="N:System.Web.Management" /> namespace.</summary>
	// Token: 0x02000747 RID: 1863
	[Serializable]
	public sealed class SqlExecutionException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.SqlExecutionException" /> class using default values.</summary>
		// Token: 0x06004C8A RID: 19594 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public SqlExecutionException()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.SqlExecutionException" /> class using the passed message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06004C8B RID: 19595 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public SqlExecutionException(string message)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.SqlExecutionException" /> class using the passed message and exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> encountered.</param>
		// Token: 0x06004C8C RID: 19596 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public SqlExecutionException(string message, Exception innerException)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.SqlExecutionException" /> class.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="server">The SQL Server instance against which the SQL commands are run.</param>
		/// <param name="database">The database against which the SQL commands are run.</param>
		/// <param name="sqlFile">The name of the installation file containing the SQL commands being run.</param>
		/// <param name="commands">The SQL commands being run when the exception occurred.</param>
		/// <param name="sqlException">The <see cref="T:System.Data.SqlClient.SqlException" /> encountered when processing the SQL commands.</param>
		// Token: 0x06004C8D RID: 19597 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public SqlExecutionException(string message, string server, string database, string sqlFile, string commands, SqlException sqlException)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the SQL commands being run when the exception occurred.</summary>
		/// <returns>The SQL commands being run when the exception occurred.</returns>
		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x06004C8E RID: 19598 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string Commands
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the name of the database being accessed when the exception occurred.</summary>
		/// <returns>The name of the database being accessed when the exception occurred.</returns>
		// Token: 0x170017A1 RID: 6049
		// (get) Token: 0x06004C8F RID: 19599 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string Database
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the exception encountered when processing the SQL commands.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlException" /> encountered when processing the SQL commands.</returns>
		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x06004C90 RID: 19600 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public SqlException Exception
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the SQL Server instance being accessed when the exception occurred.</summary>
		/// <returns>The name of the SQL Server instance being accessed when the exception occurred.</returns>
		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x06004C91 RID: 19601 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string Server
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the path and name of the file containing the SQL commands being run when the exception occurred.</summary>
		/// <returns>The path and name of the file that contains the SQL commands being run when the exception occurred.</returns>
		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x06004C92 RID: 19602 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string SqlFile
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
