using System;

namespace System.Collections.Specialized
{
	/// <summary>Describes the action that caused a <see cref="E:System.Collections.Specialized.INotifyCollectionChanged.CollectionChanged" /> event. </summary>
	// Token: 0x020004B7 RID: 1207
	public enum NotifyCollectionChangedAction
	{
		/// <summary>One or more items were added to the collection.</summary>
		// Token: 0x04001538 RID: 5432
		Add,
		/// <summary>One or more items were removed from the collection.</summary>
		// Token: 0x04001539 RID: 5433
		Remove,
		/// <summary>One or more items were replaced in the collection.</summary>
		// Token: 0x0400153A RID: 5434
		Replace,
		/// <summary>One or more items were moved within the collection.</summary>
		// Token: 0x0400153B RID: 5435
		Move,
		/// <summary>The content of the collection changed dramatically.</summary>
		// Token: 0x0400153C RID: 5436
		Reset
	}
}
