using System;

namespace System.Web.UI
{
	/// <summary>Provides an interface that designer developers can use to evaluate device filters by name.</summary>
	// Token: 0x0200016F RID: 367
	public interface IFilterResolutionService
	{
		/// <summary>Returns a value indicating whether the specified filter is a type of the current filter object.</summary>
		/// <returns>true if the specified filter is a type applicable to the current filter object; otherwise, false.</returns>
		/// <param name="filterName">The name of a device filter.</param>
		// Token: 0x06000F32 RID: 3890
		bool EvaluateFilter(string filterName);

		/// <summary>Returns a value indicating whether a parent-child relationship exists between two specified device filters. </summary>
		/// <returns>1 if the device filter identified by <paramref name="filter1" /> is a parent of the filter identified by <paramref name="filter2" />, -1 if the device filter identified by <paramref name="filter2" /> is a parent of the filter identified by <paramref name="filter1" />, and 0 if there is no parent-child relationship between the two filters.</returns>
		/// <param name="filter1">A device filter name.</param>
		/// <param name="filter2">A device filter name</param>
		// Token: 0x06000F33 RID: 3891
		int CompareFilters(string filter1, string filter2);
	}
}
