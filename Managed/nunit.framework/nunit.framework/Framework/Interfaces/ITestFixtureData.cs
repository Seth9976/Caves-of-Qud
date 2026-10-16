using System;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000D7 RID: 215
	public interface ITestFixtureData : ITestData
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060006D8 RID: 1752
		Type[] TypeArgs { get; }
	}
}
