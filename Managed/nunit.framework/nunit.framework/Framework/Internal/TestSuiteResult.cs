using System;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000092 RID: 146
	public class TestSuiteResult : TestResult
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x0000D29A File Offset: 0x0000B49A
		public TestSuiteResult(TestSuite suite)
			: base(suite)
		{
			this._children = new List<ITestResult>();
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000D2AE File Offset: 0x0000B4AE
		public override int FailCount
		{
			get
			{
				return this._failCount;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000D2B6 File Offset: 0x0000B4B6
		public override int PassCount
		{
			get
			{
				return this._passCount;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0000D2BE File Offset: 0x0000B4BE
		public override int SkipCount
		{
			get
			{
				return this._skipCount;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000D2C6 File Offset: 0x0000B4C6
		public override int InconclusiveCount
		{
			get
			{
				return this._inconclusiveCount;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000D2CE File Offset: 0x0000B4CE
		public override bool HasChildren
		{
			get
			{
				return this._children.Count != 0;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0000D2DE File Offset: 0x0000B4DE
		public override IEnumerable<ITestResult> Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000D2E8 File Offset: 0x0000B4E8
		public virtual void AddResult(ITestResult result)
		{
			IList<ITestResult> list = this.Children as IList<ITestResult>;
			if (list != null)
			{
				list.Add(result);
				if (base.ResultState != ResultState.Cancelled)
				{
					switch (result.ResultState.Status)
					{
					case TestStatus.Skipped:
						if (result.ResultState.Label == "Ignored" && (base.ResultState.Status == TestStatus.Inconclusive || base.ResultState.Status == TestStatus.Passed))
						{
							base.SetResult(ResultState.Ignored, TestResult.CHILD_IGNORE_MESSAGE);
						}
						break;
					case TestStatus.Passed:
						if (base.ResultState.Status == TestStatus.Inconclusive)
						{
							base.SetResult(ResultState.Success);
						}
						break;
					case TestStatus.Failed:
						if (base.ResultState.Status != TestStatus.Failed)
						{
							base.SetResult(ResultState.ChildFailure, TestResult.CHILD_ERRORS_MESSAGE);
						}
						break;
					}
				}
				this.InternalAssertCount += result.AssertCount;
				this._passCount += result.PassCount;
				this._failCount += result.FailCount;
				this._skipCount += result.SkipCount;
				this._inconclusiveCount += result.InconclusiveCount;
				return;
			}
			throw new NotSupportedException("cannot add results to Children");
		}

		// Token: 0x0400011D RID: 285
		private int _passCount;

		// Token: 0x0400011E RID: 286
		private int _failCount;

		// Token: 0x0400011F RID: 287
		private int _skipCount;

		// Token: 0x04000120 RID: 288
		private int _inconclusiveCount;

		// Token: 0x04000121 RID: 289
		private List<ITestResult> _children;
	}
}
