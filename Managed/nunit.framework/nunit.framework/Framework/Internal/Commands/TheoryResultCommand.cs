using System;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Commands
{
	// Token: 0x020000B5 RID: 181
	public class TheoryResultCommand : DelegatingTestCommand
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x0000F916 File Offset: 0x0000DB16
		public TheoryResultCommand(TestCommand command)
			: base(command)
		{
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0000F920 File Offset: 0x0000DB20
		public override TestResult Execute(ITestExecutionContext context)
		{
			TestResult testResult = this.innerCommand.Execute(context);
			if (testResult.ResultState == ResultState.Success)
			{
				if (!testResult.HasChildren)
				{
					testResult.SetResult(ResultState.Failure, "No test cases were provided");
				}
				else
				{
					bool flag = true;
					using (IEnumerator<ITestResult> enumerator = testResult.Children.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (((TestResult)enumerator.Current).ResultState == ResultState.Success)
							{
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						testResult.SetResult(ResultState.Failure, "All test cases were inconclusive");
					}
				}
			}
			return testResult;
		}
	}
}
