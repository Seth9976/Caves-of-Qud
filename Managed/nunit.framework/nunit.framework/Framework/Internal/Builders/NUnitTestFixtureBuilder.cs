using System;
using System.Reflection;
using NUnit.Compatibility;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Builders
{
	// Token: 0x020000BC RID: 188
	public class NUnitTestFixtureBuilder
	{
		// Token: 0x06000682 RID: 1666 RVA: 0x0001096C File Offset: 0x0000EB6C
		public TestSuite BuildFrom(ITypeInfo typeInfo)
		{
			TestFixture testFixture = new TestFixture(typeInfo);
			if (testFixture.RunState != RunState.NotRunnable)
			{
				NUnitTestFixtureBuilder.CheckTestFixtureIsValid(testFixture);
			}
			testFixture.ApplyAttributesToTest(typeInfo.Type.GetTypeInfo());
			this.AddTestCasesToFixture(testFixture);
			return testFixture;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000109A8 File Offset: 0x0000EBA8
		public TestSuite BuildFrom(ITypeInfo typeInfo, ITestFixtureData testFixtureData)
		{
			Guard.ArgumentNotNull(testFixtureData, "testFixtureData");
			object[] array = testFixtureData.Arguments;
			if (typeInfo.ContainsGenericParameters)
			{
				Type[] array2 = testFixtureData.TypeArgs;
				if (array2.Length == 0)
				{
					int num = 0;
					object[] array3 = array;
					int num2 = 0;
					while (num2 < array3.Length && array3[num2] is Type)
					{
						num++;
						num2++;
					}
					array2 = new Type[num];
					for (int i = 0; i < num; i++)
					{
						array2[i] = (Type)array[i];
					}
					if (num > 0)
					{
						object[] array4 = new object[array.Length - num];
						for (int j = 0; j < array4.Length; j++)
						{
							array4[j] = array[num + j];
						}
						array = array4;
					}
				}
				if (array2.Length != 0 || TypeHelper.CanDeduceTypeArgsFromArgs(typeInfo.Type, array, ref array2))
				{
					typeInfo = typeInfo.MakeGenericType(array2);
				}
			}
			TestFixture testFixture = new TestFixture(typeInfo);
			if (array != null && array.Length != 0)
			{
				string text = (testFixture.Name = typeInfo.GetDisplayName(array));
				string @namespace = typeInfo.Namespace;
				testFixture.FullName = ((@namespace != null && @namespace != "") ? (@namespace + "." + text) : text);
				testFixture.Arguments = array;
			}
			if (testFixture.RunState != RunState.NotRunnable)
			{
				testFixture.RunState = testFixtureData.RunState;
			}
			foreach (string text2 in testFixtureData.Properties.Keys)
			{
				foreach (object obj in testFixtureData.Properties[text2])
				{
					testFixture.Properties.Add(text2, obj);
				}
			}
			if (testFixture.RunState != RunState.NotRunnable)
			{
				NUnitTestFixtureBuilder.CheckTestFixtureIsValid(testFixture);
			}
			testFixture.ApplyAttributesToTest(typeInfo.Type.GetTypeInfo());
			this.AddTestCasesToFixture(testFixture);
			return testFixture;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00010BAC File Offset: 0x0000EDAC
		private void AddTestCasesToFixture(TestFixture fixture)
		{
			if (fixture.TypeInfo.ContainsGenericParameters)
			{
				fixture.RunState = RunState.NotRunnable;
				fixture.Properties.Set("_SKIPREASON", NUnitTestFixtureBuilder.NO_TYPE_ARGS_MSG);
				return;
			}
			foreach (IMethodInfo methodInfo in fixture.TypeInfo.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				Test test = this.BuildTestCase(methodInfo, fixture);
				if (test != null)
				{
					fixture.Add(test);
				}
			}
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00010C16 File Offset: 0x0000EE16
		private Test BuildTestCase(IMethodInfo method, TestSuite suite)
		{
			if (!this._testBuilder.CanBuildFrom(method, suite))
			{
				return null;
			}
			return this._testBuilder.BuildFrom(method, suite);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00010C38 File Offset: 0x0000EE38
		private static void CheckTestFixtureIsValid(TestFixture fixture)
		{
			if (fixture.TypeInfo.ContainsGenericParameters)
			{
				fixture.RunState = RunState.NotRunnable;
				fixture.Properties.Set("_SKIPREASON", NUnitTestFixtureBuilder.NO_TYPE_ARGS_MSG);
				return;
			}
			if (!fixture.TypeInfo.IsStaticClass)
			{
				Type[] typeArray = Reflect.GetTypeArray(fixture.Arguments);
				if (!fixture.TypeInfo.HasConstructor(typeArray))
				{
					fixture.RunState = RunState.NotRunnable;
					fixture.Properties.Set("_SKIPREASON", "No suitable constructor was found");
				}
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0000E4A0 File Offset: 0x0000C6A0
		private static bool IsStaticClass(Type type)
		{
			return type.GetTypeInfo().IsAbstract && type.GetTypeInfo().IsSealed;
		}

		// Token: 0x04000167 RID: 359
		private static readonly string NO_TYPE_ARGS_MSG = "Fixture type contains generic parameters. You must either provide Type arguments or specify constructor arguments that allow NUnit to deduce the Type arguments.";

		// Token: 0x04000168 RID: 360
		private ITestCaseBuilder _testBuilder = new DefaultTestCaseBuilder();
	}
}
