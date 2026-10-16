using System;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x0200008C RID: 140
	public class TestMethod : Test
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
		public TestMethod(IMethodInfo method)
			: base(method)
		{
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000C3C1 File Offset: 0x0000A5C1
		public TestMethod(IMethodInfo method, Test parentSuite)
			: base(method)
		{
			if (parentSuite != null)
			{
				base.FullName = parentSuite.FullName + "." + base.Name;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0000C3E9 File Offset: 0x0000A5E9
		internal bool HasExpectedResult
		{
			get
			{
				return this.parms != null && this.parms.HasExpectedResult;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0000C400 File Offset: 0x0000A600
		internal object ExpectedResult
		{
			get
			{
				if (this.parms == null)
				{
					return null;
				}
				return this.parms.ExpectedResult;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x0000C417 File Offset: 0x0000A617
		internal object[] Arguments
		{
			get
			{
				if (this.parms == null)
				{
					return null;
				}
				return this.parms.Arguments;
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000C42E File Offset: 0x0000A62E
		public override TestResult MakeTestResult()
		{
			return new TestCaseResult(this);
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00004B9A File Offset: 0x00002D9A
		public override bool HasChildren
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0000C438 File Offset: 0x0000A638
		public override TNode AddToXml(TNode parentNode, bool recursive)
		{
			TNode tnode = parentNode.AddElement(this.XmlElementName);
			base.PopulateTestNode(tnode, recursive);
			tnode.AddAttribute("seed", base.Seed.ToString());
			return tnode;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0000C474 File Offset: 0x0000A674
		public override IList<ITest> Tests
		{
			get
			{
				return new ITest[0];
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0000C47C File Offset: 0x0000A67C
		public override string XmlElementName
		{
			get
			{
				return "test-case";
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0000C483 File Offset: 0x0000A683
		public override string MethodName
		{
			get
			{
				return base.Method.Name;
			}
		}

		// Token: 0x04000114 RID: 276
		public TestCaseParameters parms;
	}
}
