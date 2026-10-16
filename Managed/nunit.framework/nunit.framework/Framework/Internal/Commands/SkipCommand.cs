using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Commands
{
	// Token: 0x020000B0 RID: 176
	public class SkipCommand : TestCommand
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x0000F4F5 File Offset: 0x0000D6F5
		public SkipCommand(Test test)
			: base(test)
		{
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0000F500 File Offset: 0x0000D700
		public override TestResult Execute(ITestExecutionContext context)
		{
			TestResult currentResult = context.CurrentResult;
			switch (base.Test.RunState)
			{
			case RunState.NotRunnable:
				currentResult.SetResult(ResultState.NotRunnable, this.GetSkipReason(), this.GetProviderStackTrace());
				break;
			default:
				currentResult.SetResult(ResultState.Skipped, this.GetSkipReason());
				break;
			case RunState.Explicit:
				currentResult.SetResult(ResultState.Explicit, this.GetSkipReason());
				break;
			case RunState.Ignored:
				currentResult.SetResult(ResultState.Ignored, this.GetSkipReason());
				break;
			}
			return currentResult;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0000F58B File Offset: 0x0000D78B
		private string GetSkipReason()
		{
			return (string)base.Test.Properties.Get("_SKIPREASON");
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0000F5A7 File Offset: 0x0000D7A7
		private string GetProviderStackTrace()
		{
			return (string)base.Test.Properties.Get("_PROVIDERSTACKTRACE");
		}
	}
}
