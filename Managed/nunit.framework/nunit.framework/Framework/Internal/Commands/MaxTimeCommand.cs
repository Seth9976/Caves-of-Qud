using System;
using System.Diagnostics;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Commands
{
	// Token: 0x020000AD RID: 173
	public class MaxTimeCommand : DelegatingTestCommand
	{
		// Token: 0x06000644 RID: 1604 RVA: 0x0000F1C1 File Offset: 0x0000D3C1
		public MaxTimeCommand(TestCommand innerCommand, int maxTime)
			: base(innerCommand)
		{
			this.maxTime = maxTime;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		public override TestResult Execute(ITestExecutionContext context)
		{
			long timestamp = Stopwatch.GetTimestamp();
			TestResult testResult = this.innerCommand.Execute(context);
			double num = (double)(Stopwatch.GetTimestamp() - timestamp) / (double)Stopwatch.Frequency;
			testResult.Duration = num;
			if (testResult.ResultState == ResultState.Success)
			{
				double num2 = testResult.Duration * 1000.0;
				if (num2 > (double)this.maxTime)
				{
					testResult.SetResult(ResultState.Failure, string.Format("Elapsed time of {0}ms exceeds maximum of {1}ms", num2, this.maxTime));
				}
			}
			return testResult;
		}

		// Token: 0x04000156 RID: 342
		private int maxTime;
	}
}
