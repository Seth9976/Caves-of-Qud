using System;
using System.Collections.Generic;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000DA RID: 218
	public interface ITestResult : IXmlNodeBuilder
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060006DD RID: 1757
		ResultState ResultState { get; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060006DE RID: 1758
		string Name { get; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060006DF RID: 1759
		string FullName { get; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060006E0 RID: 1760
		double Duration { get; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060006E1 RID: 1761
		DateTime StartTime { get; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060006E2 RID: 1762
		DateTime EndTime { get; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060006E3 RID: 1763
		string Message { get; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060006E4 RID: 1764
		string StackTrace { get; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060006E5 RID: 1765
		int AssertCount { get; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060006E6 RID: 1766
		int FailCount { get; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060006E7 RID: 1767
		int PassCount { get; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060006E8 RID: 1768
		int SkipCount { get; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060006E9 RID: 1769
		int InconclusiveCount { get; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060006EA RID: 1770
		bool HasChildren { get; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060006EB RID: 1771
		IEnumerable<ITestResult> Children { get; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060006EC RID: 1772
		ITest Test { get; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060006ED RID: 1773
		string Output { get; }
	}
}
