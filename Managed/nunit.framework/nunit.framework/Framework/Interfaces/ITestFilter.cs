using System;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000D6 RID: 214
	public interface ITestFilter : IXmlNodeBuilder
	{
		// Token: 0x060006D6 RID: 1750
		bool Pass(ITest test);

		// Token: 0x060006D7 RID: 1751
		bool IsExplicitMatch(ITest test);
	}
}
