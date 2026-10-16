using System;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000D4 RID: 212
	public interface ITestCaseData : ITestData
	{
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060006D0 RID: 1744
		object ExpectedResult { get; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060006D1 RID: 1745
		bool HasExpectedResult { get; }
	}
}
