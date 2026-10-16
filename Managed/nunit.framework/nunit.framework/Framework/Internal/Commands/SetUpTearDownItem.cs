using System;
using System.Collections.Generic;
using System.Reflection;

namespace NUnit.Framework.Internal.Commands
{
	// Token: 0x020000AF RID: 175
	public class SetUpTearDownItem
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x0000F390 File Offset: 0x0000D590
		public SetUpTearDownItem(IList<MethodInfo> setUpMethods, IList<MethodInfo> tearDownMethods)
		{
			this._setUpMethods = setUpMethods;
			this._tearDownMethods = tearDownMethods;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0000F3A6 File Offset: 0x0000D5A6
		public bool HasMethods
		{
			get
			{
				return this._setUpMethods.Count > 0 || this._tearDownMethods.Count > 0;
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0000F3C8 File Offset: 0x0000D5C8
		public void RunSetUp(ITestExecutionContext context)
		{
			this._setUpWasRun = true;
			foreach (MethodInfo methodInfo in this._setUpMethods)
			{
				this.RunSetUpOrTearDownMethod(context, methodInfo);
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0000F420 File Offset: 0x0000D620
		public void RunTearDown(ITestExecutionContext context)
		{
			if (this._setUpWasRun)
			{
				try
				{
					int num = this._tearDownMethods.Count;
					while (--num >= 0)
					{
						this.RunSetUpOrTearDownMethod(context, this._tearDownMethods[num]);
					}
				}
				catch (Exception ex)
				{
					context.CurrentResult.RecordTearDownException(ex);
				}
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0000F480 File Offset: 0x0000D680
		private void RunSetUpOrTearDownMethod(ITestExecutionContext context, MethodInfo method)
		{
			if (AsyncInvocationRegion.IsAsyncOperation(method))
			{
				this.RunAsyncMethod(method, context);
				return;
			}
			this.RunNonAsyncMethod(method, context);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0000F49C File Offset: 0x0000D69C
		private void RunAsyncMethod(MethodInfo method, ITestExecutionContext context)
		{
			using (AsyncInvocationRegion asyncInvocationRegion = AsyncInvocationRegion.Create(method))
			{
				asyncInvocationRegion.WaitForPendingOperationsToComplete(this.RunNonAsyncMethod(method, context));
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0000F4DC File Offset: 0x0000D6DC
		private object RunNonAsyncMethod(MethodInfo method, ITestExecutionContext context)
		{
			return Reflect.InvokeMethod(method, method.IsStatic ? null : context.TestObject);
		}

		// Token: 0x04000158 RID: 344
		private IList<MethodInfo> _setUpMethods;

		// Token: 0x04000159 RID: 345
		private IList<MethodInfo> _tearDownMethods;

		// Token: 0x0400015A RID: 346
		private bool _setUpWasRun;
	}
}
