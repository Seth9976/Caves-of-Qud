using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Extends the <see cref="T:System.ComponentModel.IBindingList" /> interface by providing advanced sorting and filtering capabilities.</summary>
	// Token: 0x020003B3 RID: 947
	public interface IBindingListView : IBindingList, IList, ICollection, IEnumerable
	{
		/// <summary>Sorts the data source based on the given <see cref="T:System.ComponentModel.ListSortDescriptionCollection" />.</summary>
		/// <param name="sorts">The <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> containing the sorts to apply to the data source.</param>
		// Token: 0x06001EF7 RID: 7927
		void ApplySort(ListSortDescriptionCollection sorts);

		/// <summary>Gets or sets the filter to be used to exclude items from the collection of items returned by the data source</summary>
		/// <returns>The string used to filter items out in the item collection returned by the data source. </returns>
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001EF8 RID: 7928
		// (set) Token: 0x06001EF9 RID: 7929
		string Filter { get; set; }

		/// <summary>Gets the collection of sort descriptions currently applied to the data source.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> currently applied to the data source.</returns>
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001EFA RID: 7930
		ListSortDescriptionCollection SortDescriptions { get; }

		/// <summary>Removes the current filter applied to the data source.</summary>
		// Token: 0x06001EFB RID: 7931
		void RemoveFilter();

		/// <summary>Gets a value indicating whether the data source supports advanced sorting. </summary>
		/// <returns>true if the data source supports advanced sorting; otherwise, false. </returns>
		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001EFC RID: 7932
		bool SupportsAdvancedSorting { get; }

		/// <summary>Gets a value indicating whether the data source supports filtering. </summary>
		/// <returns>true if the data source supports filtering; otherwise, false. </returns>
		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001EFD RID: 7933
		bool SupportsFiltering { get; }
	}
}
