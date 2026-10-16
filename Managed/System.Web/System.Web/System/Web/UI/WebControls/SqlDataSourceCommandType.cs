using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Describes the type of SQL command used by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> and <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> controls when performing a database operation.</summary>
	// Token: 0x02000309 RID: 777
	public enum SqlDataSourceCommandType
	{
		/// <summary>The text contained in a corresponding text property is a SQL query or command.</summary>
		// Token: 0x0400175D RID: 5981
		Text,
		/// <summary>The text contained in a corresponding text property is the name of a stored procedure.</summary>
		// Token: 0x0400175E RID: 5982
		StoredProcedure
	}
}
