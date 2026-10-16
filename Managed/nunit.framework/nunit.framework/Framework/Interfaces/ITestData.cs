using System;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000D5 RID: 213
	public interface ITestData
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060006D2 RID: 1746
		string TestName { get; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060006D3 RID: 1747
		RunState RunState { get; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060006D4 RID: 1748
		object[] Arguments { get; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060006D5 RID: 1749
		IPropertyBag Properties { get; }
	}
}
