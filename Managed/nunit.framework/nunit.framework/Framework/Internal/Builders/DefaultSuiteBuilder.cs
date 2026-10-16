using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Builders
{
	// Token: 0x020000B8 RID: 184
	public class DefaultSuiteBuilder : ISuiteBuilder
	{
		// Token: 0x0600066A RID: 1642 RVA: 0x0000FEC8 File Offset: 0x0000E0C8
		public bool CanBuildFrom(ITypeInfo typeInfo)
		{
			return (!typeInfo.IsAbstract || typeInfo.IsSealed) && (typeInfo.IsDefined<IFixtureBuilder>(true) || (!typeInfo.IsGenericTypeDefinition && typeInfo.HasMethodWithAttribute(typeof(IImplyFixture))));
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0000FF04 File Offset: 0x0000E104
		public TestSuite BuildFrom(ITypeInfo typeInfo)
		{
			List<TestSuite> list = new List<TestSuite>();
			TestSuite testSuite2;
			try
			{
				IFixtureBuilder[] fixtureBuilderAttributes = this.GetFixtureBuilderAttributes(typeInfo);
				for (int i = 0; i < fixtureBuilderAttributes.Length; i++)
				{
					foreach (TestSuite testSuite in fixtureBuilderAttributes[i].BuildFrom(typeInfo))
					{
						list.Add(testSuite);
					}
				}
				if (typeInfo.IsGenericType)
				{
					testSuite2 = this.BuildMultipleFixtures(typeInfo, list);
				}
				else
				{
					int i = list.Count;
					if (i != 0)
					{
						if (i != 1)
						{
							testSuite2 = this.BuildMultipleFixtures(typeInfo, list);
						}
						else
						{
							testSuite2 = list[0];
						}
					}
					else
					{
						testSuite2 = this._defaultBuilder.BuildFrom(typeInfo);
					}
				}
			}
			catch (Exception innerException)
			{
				TestFixture testFixture = new TestFixture(typeInfo);
				testFixture.RunState = RunState.NotRunnable;
				if (innerException is TargetInvocationException)
				{
					innerException = innerException.InnerException;
				}
				string text = "An exception was thrown while loading the test." + Env.NewLine + innerException.ToString();
				testFixture.Properties.Add("_SKIPREASON", text);
				testSuite2 = testFixture;
			}
			return testSuite2;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00010020 File Offset: 0x0000E220
		private TestSuite BuildMultipleFixtures(ITypeInfo typeInfo, IEnumerable<TestSuite> fixtures)
		{
			TestSuite testSuite = new ParameterizedFixtureSuite(typeInfo);
			foreach (TestSuite testSuite2 in fixtures)
			{
				testSuite.Add(testSuite2);
			}
			return testSuite;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00010070 File Offset: 0x0000E270
		private IFixtureBuilder[] GetFixtureBuilderAttributes(ITypeInfo typeInfo)
		{
			IFixtureBuilder[] array = new IFixtureBuilder[0];
			while (typeInfo != null && !typeInfo.IsType(typeof(object)))
			{
				array = typeInfo.GetCustomAttributes<IFixtureBuilder>(false);
				if (array.Length != 0)
				{
					if (array.Length == 1)
					{
						return array;
					}
					int num = 0;
					foreach (IFixtureBuilder fixtureBuilder in array)
					{
						if (this.HasArguments(fixtureBuilder))
						{
							num++;
						}
					}
					if (num == array.Length)
					{
						return array;
					}
					if (num == 0)
					{
						return new IFixtureBuilder[] { array[0] };
					}
					IFixtureBuilder[] array3 = new IFixtureBuilder[num];
					int num2 = 0;
					foreach (IFixtureBuilder fixtureBuilder2 in array)
					{
						if (this.HasArguments(fixtureBuilder2))
						{
							array3[num2++] = fixtureBuilder2;
						}
					}
					return array3;
				}
				else
				{
					typeInfo = typeInfo.BaseType;
				}
			}
			return array;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00010144 File Offset: 0x0000E344
		private bool HasArguments(IFixtureBuilder attr)
		{
			TestFixtureAttribute testFixtureAttribute = attr as TestFixtureAttribute;
			return testFixtureAttribute == null || testFixtureAttribute.Arguments.Length != 0 || testFixtureAttribute.TypeArgs.Length != 0;
		}

		// Token: 0x04000161 RID: 353
		private NUnitTestFixtureBuilder _defaultBuilder = new NUnitTestFixtureBuilder();
	}
}
