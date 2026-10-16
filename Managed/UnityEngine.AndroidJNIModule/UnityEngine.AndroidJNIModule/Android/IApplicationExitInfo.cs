using System;

namespace UnityEngine.Android
{
	// Token: 0x02000017 RID: 23
	public interface IApplicationExitInfo
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000251 RID: 593
		string description { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000252 RID: 594
		int describeContents { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000253 RID: 595
		int definingUid { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000254 RID: 596
		ProcessImportance importance { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000255 RID: 597
		int packageUid { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000256 RID: 598
		int pid { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000257 RID: 599
		string processName { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000258 RID: 600
		sbyte[] processStateSummary { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000259 RID: 601
		long pss { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600025A RID: 602
		int realUid { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600025B RID: 603
		ExitReason reason { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600025C RID: 604
		long rss { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600025D RID: 605
		int status { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600025E RID: 606
		long timestamp { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600025F RID: 607
		byte[] trace { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000260 RID: 608
		string traceAsString { get; }
	}
}
