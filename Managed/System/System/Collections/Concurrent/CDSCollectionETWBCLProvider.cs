using System;
using System.Diagnostics.Tracing;

namespace System.Collections.Concurrent
{
	// Token: 0x0200049F RID: 1183
	[EventSource(Name = "System.Collections.Concurrent.ConcurrentCollectionsEventSource", Guid = "35167F8E-49B2-4b96-AB86-435B59336B5E")]
	internal sealed class CDSCollectionETWBCLProvider : EventSource
	{
		// Token: 0x060025E1 RID: 9697 RVA: 0x0008489D File Offset: 0x00082A9D
		private CDSCollectionETWBCLProvider()
		{
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x000848A5 File Offset: 0x00082AA5
		[Event(1, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPushFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(1, spinCount);
			}
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x000848BA File Offset: 0x00082ABA
		[Event(2, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPopFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(2, spinCount);
			}
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x000848CF File Offset: 0x00082ACF
		[Event(3, Level = EventLevel.Warning)]
		public void ConcurrentDictionary_AcquiringAllLocks(int numOfBuckets)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(3, numOfBuckets);
			}
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x000848E4 File Offset: 0x00082AE4
		[Event(4, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryTakeSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(4);
			}
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x000848F8 File Offset: 0x00082AF8
		[Event(5, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryPeekSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(5);
			}
		}

		// Token: 0x040014E6 RID: 5350
		public static CDSCollectionETWBCLProvider Log = new CDSCollectionETWBCLProvider();

		// Token: 0x040014E7 RID: 5351
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x040014E8 RID: 5352
		private const int CONCURRENTSTACK_FASTPUSHFAILED_ID = 1;

		// Token: 0x040014E9 RID: 5353
		private const int CONCURRENTSTACK_FASTPOPFAILED_ID = 2;

		// Token: 0x040014EA RID: 5354
		private const int CONCURRENTDICTIONARY_ACQUIRINGALLLOCKS_ID = 3;

		// Token: 0x040014EB RID: 5355
		private const int CONCURRENTBAG_TRYTAKESTEALS_ID = 4;

		// Token: 0x040014EC RID: 5356
		private const int CONCURRENTBAG_TRYPEEKSTEALS_ID = 5;
	}
}
