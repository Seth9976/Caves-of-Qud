using System;
using NUnit.Framework.Internal;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000D0 RID: 208
	public interface ISimpleTestBuilder
	{
		// Token: 0x060006BC RID: 1724
		TestMethod BuildFrom(IMethodInfo method, Test suite);
	}
}
