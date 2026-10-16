using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Collections.ObjectModel
{
	// Token: 0x0200049A RID: 1178
	internal static class EventArgsCache
	{
		// Token: 0x040014CF RID: 5327
		internal static readonly PropertyChangedEventArgs CountPropertyChanged = new PropertyChangedEventArgs("Count");

		// Token: 0x040014D0 RID: 5328
		internal static readonly PropertyChangedEventArgs IndexerPropertyChanged = new PropertyChangedEventArgs("Item[]");

		// Token: 0x040014D1 RID: 5329
		internal static readonly NotifyCollectionChangedEventArgs ResetCollectionChanged = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
	}
}
