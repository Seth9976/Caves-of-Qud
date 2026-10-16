using System;

namespace System.Collections.Specialized
{
	/// <summary>Notifies listeners of dynamic changes, such as when items get added and removed or the whole list is refreshed.</summary>
	// Token: 0x020004B6 RID: 1206
	public interface INotifyCollectionChanged
	{
		/// <summary>Occurs when the collection changes.</summary>
		// Token: 0x14000051 RID: 81
		// (add) Token: 0x060026F2 RID: 9970
		// (remove) Token: 0x060026F3 RID: 9971
		event NotifyCollectionChangedEventHandler CollectionChanged;
	}
}
