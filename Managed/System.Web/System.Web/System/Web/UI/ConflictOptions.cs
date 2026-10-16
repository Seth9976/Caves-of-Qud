using System;

namespace System.Web.UI
{
	/// <summary>Determines how ASP.NET data source controls handle data conflicts when updating or deleting data.</summary>
	// Token: 0x02000150 RID: 336
	public enum ConflictOptions
	{
		/// <summary>A data source control overwrites all values in a data row with its own values for the row.</summary>
		// Token: 0x04001223 RID: 4643
		OverwriteChanges,
		/// <summary>A data source control uses the <paramref name="oldValues" /> collection of the Update and Delete methods to determine whether the data has been changed by another process.</summary>
		// Token: 0x04001224 RID: 4644
		CompareAllValues
	}
}
