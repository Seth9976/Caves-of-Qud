using System;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Builders
{
	// Token: 0x020000BA RID: 186
	public class NamespaceTreeBuilder
	{
		// Token: 0x06000677 RID: 1655 RVA: 0x00010355 File Offset: 0x0000E555
		public NamespaceTreeBuilder(TestSuite rootSuite)
		{
			this.rootSuite = rootSuite;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0001036F File Offset: 0x0000E56F
		public TestSuite RootSuite
		{
			get
			{
				return this.rootSuite;
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00010378 File Offset: 0x0000E578
		public void Add(IList<Test> fixtures)
		{
			foreach (Test test in fixtures)
			{
				TestSuite testSuite = (TestSuite)test;
				this.Add(testSuite);
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000103C8 File Offset: 0x0000E5C8
		public void Add(TestSuite fixture)
		{
			string namespaceForFixture = NamespaceTreeBuilder.GetNamespaceForFixture(fixture);
			TestSuite testSuite = this.BuildFromNameSpace(namespaceForFixture);
			if (fixture is SetUpFixture)
			{
				this.AddSetUpFixture(fixture, testSuite, namespaceForFixture);
				return;
			}
			testSuite.Add(fixture);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00010400 File Offset: 0x0000E600
		private static string GetNamespaceForFixture(TestSuite fixture)
		{
			string text = fixture.FullName;
			int num = text.IndexOfAny(new char[] { '[', '(' });
			if (num >= 0)
			{
				text = text.Substring(0, num);
			}
			num = text.LastIndexOf('.');
			return (num > 0) ? text.Substring(0, num) : string.Empty;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00010458 File Offset: 0x0000E658
		private TestSuite BuildFromNameSpace(string ns)
		{
			if (ns == null || ns == "")
			{
				return this.rootSuite;
			}
			TestSuite testSuite = (this.namespaceSuites.ContainsKey(ns) ? this.namespaceSuites[ns] : null);
			if (testSuite != null)
			{
				return testSuite;
			}
			int num = ns.LastIndexOf(".");
			if (num == -1)
			{
				testSuite = new TestSuite(ns);
				if (this.rootSuite == null)
				{
					this.rootSuite = testSuite;
				}
				else
				{
					this.rootSuite.Add(testSuite);
				}
			}
			else
			{
				string text = ns.Substring(0, num);
				TestSuite testSuite2 = this.BuildFromNameSpace(text);
				string text2 = ns.Substring(num + 1);
				testSuite = new TestSuite(text, text2);
				testSuite2.Add(testSuite);
			}
			this.namespaceSuites[ns] = testSuite;
			return testSuite;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001050C File Offset: 0x0000E70C
		private void AddSetUpFixture(TestSuite newSetupFixture, TestSuite containingSuite, string ns)
		{
			foreach (ITest test in containingSuite.Tests)
			{
				TestSuite testSuite = (TestSuite)test;
				newSetupFixture.Add(testSuite);
			}
			if (containingSuite is SetUpFixture)
			{
				containingSuite.Tests.Clear();
				containingSuite.Add(newSetupFixture);
			}
			else
			{
				TestSuite testSuite2 = (TestSuite)containingSuite.Parent;
				if (testSuite2 == null)
				{
					newSetupFixture.Name = this.rootSuite.Name;
					this.rootSuite = newSetupFixture;
				}
				else
				{
					testSuite2.Tests.Remove(containingSuite);
					testSuite2.Add(newSetupFixture);
				}
			}
			this.namespaceSuites[ns] = newSetupFixture;
		}

		// Token: 0x04000163 RID: 355
		private Dictionary<string, TestSuite> namespaceSuites = new Dictionary<string, TestSuite>();

		// Token: 0x04000164 RID: 356
		private TestSuite rootSuite;
	}
}
