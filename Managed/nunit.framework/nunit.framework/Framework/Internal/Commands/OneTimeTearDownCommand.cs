using System;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Commands
{
	// Token: 0x020000A9 RID: 169
	public class OneTimeTearDownCommand : TestCommand
	{
		// Token: 0x0600063D RID: 1597 RVA: 0x0000EFD2 File Offset: 0x0000D1D2
		public OneTimeTearDownCommand(TestSuite suite, List<SetUpTearDownItem> setUpTearDownItems, List<TestActionItem> actions)
			: base(suite)
		{
			this._setUpTearDownItems = setUpTearDownItems;
			this._actions = actions;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000EFEC File Offset: 0x0000D1EC
		public override TestResult Execute(ITestExecutionContext context)
		{
			TestResult currentResult = context.CurrentResult;
			try
			{
				int i = this._actions.Count;
				while (i > 0)
				{
					this._actions[--i].AfterTest(base.Test);
				}
				if (this._setUpTearDownItems != null)
				{
					foreach (SetUpTearDownItem setUpTearDownItem in this._setUpTearDownItems)
					{
						setUpTearDownItem.RunTearDown(context);
					}
				}
				IDisposable disposable = context.TestObject as IDisposable;
				if (disposable != null && base.Test is IDisposableFixture)
				{
					if (Reflect.MethodCallWrapper != null)
					{
						Reflect.MethodCallWrapper(delegate
						{
							disposable.Dispose();
							return null;
						});
					}
					else
					{
						disposable.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				currentResult.RecordTearDownException(ex);
			}
			return currentResult;
		}

		// Token: 0x0400014D RID: 333
		private List<SetUpTearDownItem> _setUpTearDownItems;

		// Token: 0x0400014E RID: 334
		private List<TestActionItem> _actions;
	}
}
