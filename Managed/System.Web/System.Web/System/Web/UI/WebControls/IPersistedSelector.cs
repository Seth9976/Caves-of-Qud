using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides a property that is used by the <see cref="T:System.Web.DynamicData.DynamicDataManager" /> control to enable selecting data in a data-bound control through the query string.</summary>
	// Token: 0x020002D1 RID: 721
	public interface IPersistedSelector
	{
		/// <summary>Gets or sets the data-key value for the selected record in a data-bound control.</summary>
		/// <returns>The data-key value for the selected record in a data-bound control.</returns>
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001B38 RID: 6968
		// (set) Token: 0x06001B39 RID: 6969
		DataKey DataKey { get; set; }
	}
}
