using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Commands
{
	// Token: 0x020000AA RID: 170
	public class ApplyChangesToContextCommand : DelegatingTestCommand
	{
		// Token: 0x0600063F RID: 1599 RVA: 0x0000F0EC File Offset: 0x0000D2EC
		public ApplyChangesToContextCommand(TestCommand innerCommand, IEnumerable<IApplyToContext> changes)
			: base(innerCommand)
		{
			this._changes = changes;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0000F0FC File Offset: 0x0000D2FC
		public void ApplyChanges(ITestExecutionContext context)
		{
			foreach (IApplyToContext applyToContext in this._changes)
			{
				applyToContext.ApplyToContext(context);
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0000F148 File Offset: 0x0000D348
		public override TestResult Execute(ITestExecutionContext context)
		{
			try
			{
				this.ApplyChanges(context);
				context.CurrentResult = this.innerCommand.Execute(context);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException)
				{
					Thread.ResetAbort();
				}
				context.CurrentResult.RecordException(ex);
			}
			return context.CurrentResult;
		}

		// Token: 0x0400014F RID: 335
		private IEnumerable<IApplyToContext> _changes;
	}
}
