using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;

namespace NUnit.Framework
{
	// Token: 0x0200002D RID: 45
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class RepeatAttribute : PropertyAttribute, IWrapSetUpTearDown, ICommandWrapper
	{
		// Token: 0x0600017D RID: 381 RVA: 0x00004972 File Offset: 0x00002B72
		public RepeatAttribute(int count)
			: base(count)
		{
			this._count = count;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00004987 File Offset: 0x00002B87
		public TestCommand Wrap(TestCommand command)
		{
			return new RepeatAttribute.RepeatedTestCommand(command, this._count);
		}

		// Token: 0x0400002F RID: 47
		private int _count;

		// Token: 0x02000166 RID: 358
		public class RepeatedTestCommand : DelegatingTestCommand
		{
			// Token: 0x06000A51 RID: 2641 RVA: 0x00018F2A File Offset: 0x0001712A
			public RepeatedTestCommand(TestCommand innerCommand, int repeatCount)
				: base(innerCommand)
			{
				this.repeatCount = repeatCount;
			}

			// Token: 0x06000A52 RID: 2642 RVA: 0x00018F3C File Offset: 0x0001713C
			public override TestResult Execute(ITestExecutionContext context)
			{
				int num = this.repeatCount;
				while (num-- > 0)
				{
					context.CurrentResult = this.innerCommand.Execute(context);
					if (context.CurrentResult.ResultState != ResultState.Success)
					{
						break;
					}
					context.CurrentTest.SetNextId();
				}
				return context.CurrentResult;
			}

			// Token: 0x0400022D RID: 557
			private int repeatCount;
		}
	}
}
