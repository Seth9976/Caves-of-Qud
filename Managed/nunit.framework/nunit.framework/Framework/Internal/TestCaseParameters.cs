using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x0200007C RID: 124
	public class TestCaseParameters : TestParameters, ITestCaseData, ITestData, IApplyToTest
	{
		// Token: 0x06000494 RID: 1172 RVA: 0x0000AB6E File Offset: 0x00008D6E
		public TestCaseParameters()
		{
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000AB76 File Offset: 0x00008D76
		public TestCaseParameters(Exception exception)
			: base(exception)
		{
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000AB7F File Offset: 0x00008D7F
		public TestCaseParameters(object[] args)
			: base(args)
		{
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000AB88 File Offset: 0x00008D88
		public TestCaseParameters(ITestCaseData data)
			: base(data)
		{
			if (data.HasExpectedResult)
			{
				this.ExpectedResult = data.ExpectedResult;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000ABA5 File Offset: 0x00008DA5
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x0000ABAD File Offset: 0x00008DAD
		public object ExpectedResult
		{
			get
			{
				return this._expectedResult;
			}
			set
			{
				this._expectedResult = value;
				this.HasExpectedResult = true;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000ABBD File Offset: 0x00008DBD
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x0000ABC5 File Offset: 0x00008DC5
		public bool HasExpectedResult { get; set; }

		// Token: 0x040000D6 RID: 214
		private object _expectedResult;
	}
}
