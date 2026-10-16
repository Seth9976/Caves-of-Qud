using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x0200008D RID: 141
	public class TestSuite : Test
	{
		// Token: 0x0600056F RID: 1391 RVA: 0x0000C490 File Offset: 0x0000A690
		public TestSuite(string name)
			: base(name)
		{
			this.Arguments = new object[0];
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000C4B0 File Offset: 0x0000A6B0
		public TestSuite(string parentSuiteName, string name)
			: base(parentSuiteName, name)
		{
			this.Arguments = new object[0];
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000C4D1 File Offset: 0x0000A6D1
		public TestSuite(ITypeInfo fixtureType)
			: base(fixtureType)
		{
			this.Arguments = new object[0];
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000C4F1 File Offset: 0x0000A6F1
		public TestSuite(Type fixtureType)
			: base(new TypeWrapper(fixtureType))
		{
			this.Arguments = new object[0];
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000C518 File Offset: 0x0000A718
		public void Sort()
		{
			if (!this.MaintainTestOrder)
			{
				this.tests.Sort();
				foreach (ITest test in this.Tests)
				{
					TestSuite testSuite = ((Test)test) as TestSuite;
					if (testSuite != null)
					{
						testSuite.Sort();
					}
				}
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000C584 File Offset: 0x0000A784
		public void Add(Test test)
		{
			test.Parent = this;
			this.tests.Add(test);
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0000C599 File Offset: 0x0000A799
		public override IList<ITest> Tests
		{
			get
			{
				return this.tests;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0000C5A4 File Offset: 0x0000A7A4
		public override int TestCaseCount
		{
			get
			{
				int num = 0;
				foreach (ITest test in this.Tests)
				{
					Test test2 = (Test)test;
					num += test2.TestCaseCount;
				}
				return num;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0000C5FC File Offset: 0x0000A7FC
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x0000C604 File Offset: 0x0000A804
		public object[] Arguments { get; internal set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0000C60D File Offset: 0x0000A80D
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x0000C615 File Offset: 0x0000A815
		protected bool MaintainTestOrder { get; set; }

		// Token: 0x0600057B RID: 1403 RVA: 0x0000C61E File Offset: 0x0000A81E
		public override TestResult MakeTestResult()
		{
			return new TestSuiteResult(this);
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0000C626 File Offset: 0x0000A826
		public override bool HasChildren
		{
			get
			{
				return this.tests.Count > 0;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0000C636 File Offset: 0x0000A836
		public override string XmlElementName
		{
			get
			{
				return "test-suite";
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000C640 File Offset: 0x0000A840
		public override TNode AddToXml(TNode parentNode, bool recursive)
		{
			TNode tnode = parentNode.AddElement("test-suite");
			tnode.AddAttribute("type", this.TestType);
			base.PopulateTestNode(tnode, recursive);
			tnode.AddAttribute("testcasecount", this.TestCaseCount.ToString());
			if (recursive)
			{
				foreach (ITest test in this.Tests)
				{
					((Test)test).AddToXml(tnode, recursive);
				}
			}
			return tnode;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		protected void CheckSetUpTearDownMethods(Type attrType)
		{
			foreach (MethodInfo methodInfo in Reflect.GetMethodsWithAttribute(base.TypeInfo.Type, attrType, true))
			{
				if (methodInfo.IsAbstract || (!methodInfo.IsPublic && !methodInfo.IsFamily) || methodInfo.GetParameters().Length != 0 || (methodInfo.ReturnType != typeof(void) && methodInfo.ReturnType != typeof(Task)))
				{
					base.Properties.Set("_SKIPREASON", string.Format("Invalid signature for SetUp or TearDown method: {0}", methodInfo.Name));
					base.RunState = RunState.NotRunnable;
					return;
				}
			}
		}

		// Token: 0x04000115 RID: 277
		private List<ITest> tests = new List<ITest>();
	}
}
