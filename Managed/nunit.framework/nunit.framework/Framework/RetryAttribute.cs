using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;

namespace NUnit.Framework
{
	// Token: 0x02000031 RID: 49
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class RetryAttribute : PropertyAttribute, IWrapSetUpTearDown, ICommandWrapper
	{
		// Token: 0x06000184 RID: 388 RVA: 0x00004A13 File Offset: 0x00002C13
		public RetryAttribute(int count)
			: base(count)
		{
			this._count = count;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00004A28 File Offset: 0x00002C28
		public TestCommand Wrap(TestCommand command)
		{
			return new RetryAttribute.RetryCommand(command, this._count);
		}

		// Token: 0x04000030 RID: 48
		private int _count;

		// Token: 0x02000167 RID: 359
		public class RetryCommand : DelegatingTestCommand
		{
			// Token: 0x06000A53 RID: 2643 RVA: 0x00018F8F File Offset: 0x0001718F
			public RetryCommand(TestCommand innerCommand, int retryCount)
				: base(innerCommand)
			{
				this._retryCount = retryCount;
			}

			// Token: 0x06000A54 RID: 2644 RVA: 0x00018FA0 File Offset: 0x000171A0
			public override TestResult Execute(ITestExecutionContext context)
			{
				int retryCount = this._retryCount;
				while (retryCount-- > 0)
				{
					context.CurrentResult = this.innerCommand.Execute(context);
					if (context.CurrentResult.ResultState != ResultState.Failure)
					{
						break;
					}
				}
				return context.CurrentResult;
			}

			// Token: 0x0400022E RID: 558
			private int _retryCount;
		}
	}
}
