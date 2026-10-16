using System;
using System.Collections.Generic;
using NUnit.Framework.Internal;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000D9 RID: 217
	public interface ITestBuilder
	{
		// Token: 0x060006DC RID: 1756
		IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite);
	}
}
