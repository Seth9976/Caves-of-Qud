using System;

namespace System.Web.UI
{
	/// <summary>Allows a type converter to access schema information stored on an object.</summary>
	// Token: 0x0200016D RID: 365
	public interface IDataSourceViewSchemaAccessor
	{
		/// <summary>When implemented, gets or sets the schema associated with the object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the schema.</returns>
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06000F2E RID: 3886
		// (set) Token: 0x06000F2F RID: 3887
		object DataSourceViewSchema { get; set; }
	}
}
