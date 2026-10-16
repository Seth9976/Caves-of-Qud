using System;
using NUnit.Framework.Internal;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000D3 RID: 211
	public interface ITestCaseBuilder
	{
		// Token: 0x060006CE RID: 1742
		bool CanBuildFrom(IMethodInfo method, Test suite);

		// Token: 0x060006CF RID: 1743
		Test BuildFrom(IMethodInfo method, Test suite);
	}
}
