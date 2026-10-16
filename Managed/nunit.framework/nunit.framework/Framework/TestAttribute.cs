using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace NUnit.Framework
{
	// Token: 0x0200003B RID: 59
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class TestAttribute : NUnitAttribute, ISimpleTestBuilder, IApplyToTest, IImplyFixture
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00004B9D File Offset: 0x00002D9D
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00004BA5 File Offset: 0x00002DA5
		public string Description { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00004BAE File Offset: 0x00002DAE
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00004BB6 File Offset: 0x00002DB6
		public string Author { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00004BBF File Offset: 0x00002DBF
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00004BC7 File Offset: 0x00002DC7
		public Type TestOf { get; set; }

		// Token: 0x0600019C RID: 412 RVA: 0x00004BD0 File Offset: 0x00002DD0
		public void ApplyToTest(Test test)
		{
			if (!test.Properties.ContainsKey("Description") && this.Description != null)
			{
				test.Properties.Set("Description", this.Description);
			}
			if (!test.Properties.ContainsKey("Author") && this.Author != null)
			{
				test.Properties.Set("Author", this.Author);
			}
			if (!test.Properties.ContainsKey("TestOf") && this.TestOf != null)
			{
				test.Properties.Set("TestOf", this.TestOf.FullName);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00004C78 File Offset: 0x00002E78
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00004C80 File Offset: 0x00002E80
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00004C90 File Offset: 0x00002E90
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00004C98 File Offset: 0x00002E98
		public bool HasExpectedResult { get; private set; }

		// Token: 0x060001A1 RID: 417 RVA: 0x00004CA4 File Offset: 0x00002EA4
		public TestMethod BuildFrom(IMethodInfo method, Test suite)
		{
			TestCaseParameters testCaseParameters = null;
			if (this.HasExpectedResult)
			{
				testCaseParameters = new TestCaseParameters();
				testCaseParameters.ExpectedResult = this.ExpectedResult;
			}
			return this._builder.BuildTestMethod(method, suite, testCaseParameters);
		}

		// Token: 0x04000033 RID: 51
		private object _expectedResult;

		// Token: 0x04000034 RID: 52
		private readonly NUnitTestCaseBuilder _builder = new NUnitTestCaseBuilder();
	}
}
