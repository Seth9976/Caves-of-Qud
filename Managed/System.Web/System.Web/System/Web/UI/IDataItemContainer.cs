using System;

namespace System.Web.UI
{
	/// <summary>Enables data-bound control containers to identify a data item object for simplified data-binding operations.</summary>
	// Token: 0x0200016A RID: 362
	public interface IDataItemContainer : INamingContainer
	{
		/// <summary>When implemented, gets an object that is used in simplified data-binding operations.</summary>
		/// <returns>An object that represents the value to use when data-binding operations are performed.</returns>
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000F25 RID: 3877
		object DataItem { get; }

		/// <summary>When implemented, gets the index of the data item bound to a control.</summary>
		/// <returns>An Integer representing the index of the data item in the data source.</returns>
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06000F26 RID: 3878
		int DataItemIndex { get; }

		/// <summary>When implemented, gets the position of the data item as displayed in a control.</summary>
		/// <returns>An Integer representing the position of the data item as displayed in a control.</returns>
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000F27 RID: 3879
		int DisplayIndex { get; }
	}
}
