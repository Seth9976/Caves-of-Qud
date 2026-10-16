using System;
using System.Collections.Generic;
using System.Threading;

namespace NUnit.Framework.Internal.Commands
{
	// Token: 0x020000B1 RID: 177
	public class TestActionCommand : DelegatingTestCommand
	{
		// Token: 0x06000653 RID: 1619 RVA: 0x0000F5C3 File Offset: 0x0000D7C3
		public TestActionCommand(TestCommand innerCommand)
			: base(innerCommand)
		{
			Guard.ArgumentValid(innerCommand.Test is TestMethod, "TestActionCommand may only apply to a TestMethod", "innerCommand");
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0000F5F4 File Offset: 0x0000D7F4
		public override TestResult Execute(ITestExecutionContext context)
		{
			if (base.Test.Fixture == null)
			{
				base.Test.Fixture = context.TestObject;
			}
			foreach (ITestAction testAction in context.UpstreamActions)
			{
				this._actions.Add(new TestActionItem(testAction));
			}
			foreach (ITestAction testAction2 in ActionsHelper.GetActionsFromAttributeProvider(((TestMethod)base.Test).Method.MethodInfo))
			{
				if (testAction2.Targets == ActionTargets.Default || (testAction2.Targets & ActionTargets.Test) == ActionTargets.Test)
				{
					this._actions.Add(new TestActionItem(testAction2));
				}
			}
			try
			{
				for (int j = 0; j < this._actions.Count; j++)
				{
					this._actions[j].BeforeTest(base.Test);
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
					for (int k = this._actions.Count - 1; k >= 0; k--)
					{
						this._actions[k].AfterTest(base.Test);
					}
				}
			}
			return context.CurrentResult;
		}

		// Token: 0x0400015B RID: 347
		private IList<TestActionItem> _actions = new List<TestActionItem>();
	}
}
