using System;

namespace NUnit.Framework.Internal.Commands
{
	// Token: 0x020000B3 RID: 179
	public abstract class TestCommand
	{
		// Token: 0x06000658 RID: 1624 RVA: 0x0000F7C2 File Offset: 0x0000D9C2
		public TestCommand(Test test)
		{
			this.Test = test;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0000F7D1 File Offset: 0x0000D9D1
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x0000F7D9 File Offset: 0x0000D9D9
		public Test Test { get; private set; }

		// Token: 0x0600065B RID: 1627
		public abstract TestResult Execute(ITestExecutionContext context);
	}
}
