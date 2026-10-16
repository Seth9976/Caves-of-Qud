using System;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000091 RID: 145
	public class TestCaseResult : TestResult
	{
		// Token: 0x060005A7 RID: 1447 RVA: 0x0000D249 File Offset: 0x0000B449
		public TestCaseResult(TestMethod test)
			: base(test)
		{
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000D252 File Offset: 0x0000B452
		public override int FailCount
		{
			get
			{
				return (base.ResultState.Status == TestStatus.Failed) ? 1 : 0;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0000D262 File Offset: 0x0000B462
		public override int PassCount
		{
			get
			{
				return (base.ResultState.Status == TestStatus.Passed) ? 1 : 0;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000D272 File Offset: 0x0000B472
		public override int SkipCount
		{
			get
			{
				return (base.ResultState.Status == TestStatus.Skipped) ? 1 : 0;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0000D282 File Offset: 0x0000B482
		public override int InconclusiveCount
		{
			get
			{
				return (base.ResultState.Status == TestStatus.Inconclusive) ? 1 : 0;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00004B9A File Offset: 0x00002D9A
		public override bool HasChildren
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000D292 File Offset: 0x0000B492
		public override IEnumerable<ITestResult> Children
		{
			get
			{
				return new ITestResult[0];
			}
		}
	}
}
