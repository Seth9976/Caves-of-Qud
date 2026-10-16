using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework.Internal.Execution;

namespace NUnit.Framework.Internal.Commands
{
	// Token: 0x020000AE RID: 174
	public class SetUpTearDownCommand : DelegatingTestCommand
	{
		// Token: 0x06000646 RID: 1606 RVA: 0x0000F25C File Offset: 0x0000D45C
		public SetUpTearDownCommand(TestCommand innerCommand)
			: base(innerCommand)
		{
			Guard.ArgumentValid(innerCommand.Test is TestMethod, "SetUpTearDownCommand may only apply to a TestMethod", "innerCommand");
			Guard.OperationValid(base.Test.TypeInfo != null, "TestMethod must have a non-null TypeInfo");
			this._setUpTearDownItems = CommandBuilder.BuildSetUpTearDownList(base.Test.TypeInfo.Type, typeof(SetUpAttribute), typeof(TearDownAttribute));
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
		public override TestResult Execute(ITestExecutionContext context)
		{
			try
			{
				int i = this._setUpTearDownItems.Count;
				while (i > 0)
				{
					this._setUpTearDownItems[--i].RunSetUp(context);
				}
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
			finally
			{
				if (context.ExecutionStatus != TestExecutionStatus.AbortRequested)
				{
					for (int j = 0; j < this._setUpTearDownItems.Count; j++)
					{
						this._setUpTearDownItems[j].RunTearDown(context);
					}
				}
			}
			return context.CurrentResult;
		}

		// Token: 0x04000157 RID: 343
		private IList<SetUpTearDownItem> _setUpTearDownItems;
	}
}
