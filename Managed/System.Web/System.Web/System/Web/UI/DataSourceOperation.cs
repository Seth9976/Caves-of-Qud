using System;

namespace System.Web.UI
{
	/// <summary>Specifies a data operation performed by a data source control.</summary>
	// Token: 0x02000159 RID: 345
	public enum DataSourceOperation
	{
		/// <summary>The operation deletes records from a data source.</summary>
		// Token: 0x04001234 RID: 4660
		Delete,
		/// <summary>The operation inserts one or more records into a data source.</summary>
		// Token: 0x04001235 RID: 4661
		Insert,
		/// <summary>The operation retrieves records from a data source.</summary>
		// Token: 0x04001236 RID: 4662
		Select,
		/// <summary>The operation updates records in a data source.</summary>
		// Token: 0x04001237 RID: 4663
		Update,
		/// <summary>The operation retrieves the total number of records for a query from the data source.</summary>
		// Token: 0x04001238 RID: 4664
		SelectCount
	}
}
