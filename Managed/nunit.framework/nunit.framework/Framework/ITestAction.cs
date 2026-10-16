using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework
{
	// Token: 0x02000055 RID: 85
	public interface ITestAction
	{
		// Token: 0x060002C8 RID: 712
		void BeforeTest(ITest test);

		// Token: 0x060002C9 RID: 713
		void AfterTest(ITest test);

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060002CA RID: 714
		ActionTargets Targets { get; }
	}
}
