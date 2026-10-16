using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Commands;

namespace NUnit.Framework.Internal.Execution
{
	// Token: 0x020000A3 RID: 163
	public class SimpleWorkItem : WorkItem
	{
		// Token: 0x06000613 RID: 1555 RVA: 0x0000E564 File Offset: 0x0000C764
		public SimpleWorkItem(TestMethod test, ITestFilter filter)
			: base(test)
		{
			this._command = ((test.RunState == RunState.Runnable || (test.RunState == RunState.Explicit && filter.IsExplicitMatch(test))) ? CommandBuilder.MakeTestCommand(test) : CommandBuilder.MakeSkipCommand(test));
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0000E59C File Offset: 0x0000C79C
		protected override void PerformWork()
		{
			try
			{
				base.Result = this._command.Execute(base.Context);
			}
			finally
			{
				base.WorkItemComplete();
			}
		}

		// Token: 0x04000133 RID: 307
		private TestCommand _command;
	}
}
