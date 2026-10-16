using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Commands
{
	// Token: 0x020000B2 RID: 178
	public class TestActionItem
	{
		// Token: 0x06000655 RID: 1621 RVA: 0x0000F788 File Offset: 0x0000D988
		public TestActionItem(ITestAction action)
		{
			this._action = action;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0000F797 File Offset: 0x0000D997
		public void BeforeTest(ITest test)
		{
			this._beforeTestWasRun = true;
			this._action.BeforeTest(test);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0000F7AC File Offset: 0x0000D9AC
		public void AfterTest(ITest test)
		{
			if (this._beforeTestWasRun)
			{
				this._action.AfterTest(test);
			}
		}

		// Token: 0x0400015C RID: 348
		private readonly ITestAction _action;

		// Token: 0x0400015D RID: 349
		private bool _beforeTestWasRun;
	}
}
