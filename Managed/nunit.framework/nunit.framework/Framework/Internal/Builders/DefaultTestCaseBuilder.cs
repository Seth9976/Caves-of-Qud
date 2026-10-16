using System;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Builders
{
	// Token: 0x020000B9 RID: 185
	public class DefaultTestCaseBuilder : ITestCaseBuilder
	{
		// Token: 0x06000670 RID: 1648 RVA: 0x00010183 File Offset: 0x0000E383
		public bool CanBuildFrom(IMethodInfo method)
		{
			return method.IsDefined<ITestBuilder>(false) || method.IsDefined<ISimpleTestBuilder>(false);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00010197 File Offset: 0x0000E397
		public Test BuildFrom(IMethodInfo method)
		{
			return this.BuildFrom(method, null);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000101A1 File Offset: 0x0000E3A1
		public bool CanBuildFrom(IMethodInfo method, Test parentSuite)
		{
			return this.CanBuildFrom(method);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x000101AC File Offset: 0x0000E3AC
		public Test BuildFrom(IMethodInfo method, Test parentSuite)
		{
			List<TestMethod> list = new List<TestMethod>();
			List<ITestBuilder> list2 = new List<ITestBuilder>(method.GetCustomAttributes<ITestBuilder>(false));
			bool flag = true;
			using (List<ITestBuilder>.Enumerator enumerator = list2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current is CombiningStrategyAttribute)
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				list2.Add(new CombinatorialAttribute());
			}
			foreach (ITestBuilder testBuilder in list2)
			{
				foreach (TestMethod testMethod in testBuilder.BuildFrom(method, parentSuite))
				{
					list.Add(testMethod);
				}
			}
			if (list.Count <= 0)
			{
				return this.BuildSingleTestMethod(method, parentSuite);
			}
			return this.BuildParameterizedMethodSuite(method, list);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x000102B4 File Offset: 0x0000E4B4
		private Test BuildParameterizedMethodSuite(IMethodInfo method, IEnumerable<TestMethod> tests)
		{
			ParameterizedMethodSuite parameterizedMethodSuite = new ParameterizedMethodSuite(method);
			parameterizedMethodSuite.ApplyAttributesToTest(method.MethodInfo);
			foreach (TestMethod testMethod in tests)
			{
				parameterizedMethodSuite.Add(testMethod);
			}
			return parameterizedMethodSuite;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00010310 File Offset: 0x0000E510
		private Test BuildSingleTestMethod(IMethodInfo method, Test suite)
		{
			ISimpleTestBuilder[] customAttributes = method.GetCustomAttributes<ISimpleTestBuilder>(false);
			if (customAttributes.Length == 0)
			{
				return this._nunitTestCaseBuilder.BuildTestMethod(method, suite, null);
			}
			return customAttributes[0].BuildFrom(method, suite);
		}

		// Token: 0x04000162 RID: 354
		private NUnitTestCaseBuilder _nunitTestCaseBuilder = new NUnitTestCaseBuilder();
	}
}
