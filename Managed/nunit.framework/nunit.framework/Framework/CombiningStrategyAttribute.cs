using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace NUnit.Framework
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public abstract class CombiningStrategyAttribute : NUnitAttribute, ITestBuilder, IApplyToTest
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00003AEC File Offset: 0x00001CEC
		protected CombiningStrategyAttribute(ICombiningStrategy strategy, IParameterDataProvider provider)
		{
			this._strategy = strategy;
			this._dataProvider = provider;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003B0D File Offset: 0x00001D0D
		protected CombiningStrategyAttribute(object strategy, object provider)
			: this((ICombiningStrategy)strategy, (IParameterDataProvider)provider)
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00003B24 File Offset: 0x00001D24
		public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
		{
			List<TestMethod> list = new List<TestMethod>();
			IParameterInfo[] parameters = method.GetParameters();
			if (parameters.Length != 0)
			{
				IEnumerable[] array = new IEnumerable[parameters.Length];
				try
				{
					for (int i = 0; i < parameters.Length; i++)
					{
						array[i] = this._dataProvider.GetDataFor(parameters[i]);
					}
				}
				catch (InvalidDataSourceException ex)
				{
					TestCaseParameters testCaseParameters = new TestCaseParameters();
					testCaseParameters.RunState = RunState.NotRunnable;
					testCaseParameters.Properties.Set("_SKIPREASON", ex.Message);
					list.Add(this._builder.BuildTestMethod(method, suite, testCaseParameters));
					return list;
				}
				foreach (ITestCaseData testCaseData in this._strategy.GetTestCases(array))
				{
					list.Add(this._builder.BuildTestMethod(method, suite, (TestCaseParameters)testCaseData));
				}
				return list;
			}
			return list;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00003C24 File Offset: 0x00001E24
		public void ApplyToTest(Test test)
		{
			string text = this._strategy.GetType().Name;
			if (text.EndsWith("Strategy"))
			{
				text = text.Substring(0, text.Length - 8);
			}
			test.Properties.Set("_JOINTYPE", text);
		}

		// Token: 0x04000017 RID: 23
		private NUnitTestCaseBuilder _builder = new NUnitTestCaseBuilder();

		// Token: 0x04000018 RID: 24
		private ICombiningStrategy _strategy;

		// Token: 0x04000019 RID: 25
		private IParameterDataProvider _dataProvider;
	}
}
