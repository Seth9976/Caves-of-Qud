using System;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000D8 RID: 216
	public interface ITestListener
	{
		// Token: 0x060006D9 RID: 1753
		void TestStarted(ITest test);

		// Token: 0x060006DA RID: 1754
		void TestFinished(ITestResult result);

		// Token: 0x060006DB RID: 1755
		void TestOutput(TestOutput output);
	}
}
