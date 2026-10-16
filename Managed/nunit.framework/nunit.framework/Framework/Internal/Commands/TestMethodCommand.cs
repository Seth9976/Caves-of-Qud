using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Commands
{
	// Token: 0x020000B4 RID: 180
	public class TestMethodCommand : TestCommand
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x0000F7E2 File Offset: 0x0000D9E2
		public TestMethodCommand(TestMethod testMethod)
			: base(testMethod)
		{
			this.testMethod = testMethod;
			this.arguments = testMethod.Arguments;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0000F800 File Offset: 0x0000DA00
		public override TestResult Execute(ITestExecutionContext context)
		{
			object obj = this.RunTestMethod(context);
			if (this.testMethod.HasExpectedResult)
			{
				Assert.AreEqual(this.testMethod.ExpectedResult, obj);
			}
			context.CurrentResult.SetResult(ResultState.Success);
			return context.CurrentResult;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0000F849 File Offset: 0x0000DA49
		private object RunTestMethod(ITestExecutionContext context)
		{
			if (AsyncInvocationRegion.IsAsyncOperation(this.testMethod.Method.MethodInfo))
			{
				return this.RunAsyncTestMethod(context);
			}
			return this.RunNonAsyncTestMethod(context);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0000F874 File Offset: 0x0000DA74
		private object RunAsyncTestMethod(ITestExecutionContext context)
		{
			object obj2;
			using (AsyncInvocationRegion asyncInvocationRegion = AsyncInvocationRegion.Create(this.testMethod.Method.MethodInfo))
			{
				object obj = Reflect.InvokeMethod(this.testMethod.Method.MethodInfo, context.TestObject, this.arguments);
				try
				{
					obj2 = asyncInvocationRegion.WaitForPendingOperationsToComplete(obj);
				}
				catch (Exception ex)
				{
					throw new NUnitException("Rethrown", ex);
				}
			}
			return obj2;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0000F8F8 File Offset: 0x0000DAF8
		private object RunNonAsyncTestMethod(ITestExecutionContext context)
		{
			return this.testMethod.Method.Invoke(context.TestObject, this.arguments);
		}

		// Token: 0x0400015F RID: 351
		private readonly TestMethod testMethod;

		// Token: 0x04000160 RID: 352
		private readonly object[] arguments;
	}
}
