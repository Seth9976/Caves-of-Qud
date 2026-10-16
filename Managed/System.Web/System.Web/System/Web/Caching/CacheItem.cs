using System;

namespace System.Web.Caching
{
	// Token: 0x02000679 RID: 1657
	internal class CacheItem
	{
		// Token: 0x04002569 RID: 9577
		public object Value;

		// Token: 0x0400256A RID: 9578
		public string Key;

		// Token: 0x0400256B RID: 9579
		public CacheDependency Dependency;

		// Token: 0x0400256C RID: 9580
		public DateTime AbsoluteExpiration;

		// Token: 0x0400256D RID: 9581
		public TimeSpan SlidingExpiration;

		// Token: 0x0400256E RID: 9582
		public CacheItemPriority Priority;

		// Token: 0x0400256F RID: 9583
		public CacheItemRemovedCallback OnRemoveCallback;

		// Token: 0x04002570 RID: 9584
		public CacheItemUpdateCallback OnUpdateCallback;

		// Token: 0x04002571 RID: 9585
		public DateTime LastChange;

		// Token: 0x04002572 RID: 9586
		public long ExpiresAt;

		// Token: 0x04002573 RID: 9587
		public bool Disabled;

		// Token: 0x04002574 RID: 9588
		public bool IsTimedItem;

		// Token: 0x04002575 RID: 9589
		public int PriorityQueueIndex = -1;
	}
}
