using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace NUnit.Framework
{
	// Token: 0x0200003D RID: 61
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	public class TestCaseSourceAttribute : NUnitAttribute, ITestBuilder, IImplyFixture
	{
		// Token: 0x060001CA RID: 458 RVA: 0x000053DF File Offset: 0x000035DF
		public TestCaseSourceAttribute(string sourceName)
		{
			this.SourceName = sourceName;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000053F9 File Offset: 0x000035F9
		public TestCaseSourceAttribute(Type sourceType, string sourceName, object[] methodParams)
		{
			this.MethodParams = methodParams;
			this.SourceType = sourceType;
			this.SourceName = sourceName;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005421 File Offset: 0x00003621
		public TestCaseSourceAttribute(Type sourceType, string sourceName)
		{
			this.SourceType = sourceType;
			this.SourceName = sourceName;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005442 File Offset: 0x00003642
		public TestCaseSourceAttribute(Type sourceType)
		{
			this.SourceType = sourceType;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000545C File Offset: 0x0000365C
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00005464 File Offset: 0x00003664
		public object[] MethodParams { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000546D File Offset: 0x0000366D
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00005475 File Offset: 0x00003675
		public string SourceName { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000547E File Offset: 0x0000367E
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00005486 File Offset: 0x00003686
		public Type SourceType { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000548F File Offset: 0x0000368F
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00005497 File Offset: 0x00003697
		public string Category { get; set; }

		// Token: 0x060001D6 RID: 470 RVA: 0x000054A0 File Offset: 0x000036A0
		public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
		{
			foreach (ITestCaseData testCaseData in this.GetTestCasesFor(method))
			{
				TestCaseParameters testCaseParameters = (TestCaseParameters)testCaseData;
				yield return this._builder.BuildTestMethod(method, suite, testCaseParameters);
			}
			IEnumerator<ITestCaseData> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000054C0 File Offset: 0x000036C0
		private IEnumerable<ITestCaseData> GetTestCasesFor(IMethodInfo method)
		{
			List<ITestCaseData> list = new List<ITestCaseData>();
			try
			{
				IEnumerable testCaseSource = this.GetTestCaseSource(method);
				if (testCaseSource != null)
				{
					foreach (object obj in testCaseSource)
					{
						ITestCaseData testCaseData;
						if (obj != null)
						{
							testCaseData = obj as ITestCaseData;
						}
						else
						{
							ITestCaseData testCaseData2 = new TestCaseParameters(new object[1]);
							testCaseData = testCaseData2;
						}
						ITestCaseData testCaseData3 = testCaseData;
						if (testCaseData3 == null)
						{
							object[] array = obj as object[];
							if (array == null && obj is Array)
							{
								Array array2 = obj as Array;
								int num = (false ? array2.Length : method.GetParameters().Length);
								if (array2 != null && array2.Rank == 1 && array2.Length == num)
								{
									array = new object[array2.Length];
									for (int i = 0; i < array2.Length; i++)
									{
										array[i] = array2.GetValue(i);
									}
								}
							}
							if (array != null)
							{
								IParameterInfo[] parameters = method.GetParameters();
								int num2 = parameters.Length;
								int num3 = array.Length;
								if (num2 == 1)
								{
									Type parameterType = parameters[0].ParameterType;
									if ((num3 == 0 || typeof(object[]).IsAssignableFrom(parameterType)) && (num3 > 1 || parameterType.IsAssignableFrom(array.GetType())))
									{
										array = new object[] { obj };
									}
								}
							}
							else
							{
								array = new object[] { obj };
							}
							testCaseData3 = new TestCaseParameters(array);
						}
						if (this.Category != null)
						{
							foreach (string text in this.Category.Split(new char[] { ',' }))
							{
								testCaseData3.Properties.Add("Category", text);
							}
						}
						list.Add(testCaseData3);
					}
				}
			}
			catch (Exception ex)
			{
				list.Clear();
				list.Add(new TestCaseParameters(ex));
			}
			return list;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000056CC File Offset: 0x000038CC
		private IEnumerable GetTestCaseSource(IMethodInfo method)
		{
			Type type = this.SourceType ?? method.TypeInfo.Type;
			if (this.SourceName == null)
			{
				return Reflect.Construct(type, null) as IEnumerable;
			}
			MemberInfo[] member = type.GetMember(this.SourceName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (member.Length == 1)
			{
				MemberInfo memberInfo = member[0];
				FieldInfo fieldInfo = memberInfo as FieldInfo;
				if (fieldInfo != null)
				{
					if (!fieldInfo.IsStatic)
					{
						return TestCaseSourceAttribute.ReturnErrorAsParameter("The sourceName specified on a TestCaseSourceAttribute must refer to a static field, property or method.");
					}
					if (this.MethodParams != null)
					{
						return TestCaseSourceAttribute.ReturnErrorAsParameter("You have specified a data source field but also given a set of parameters. Fields cannot take parameters, please revise the 3rd parameter passed to the TestCaseSourceAttribute and either remove it or specify a method.");
					}
					return (IEnumerable)fieldInfo.GetValue(null);
				}
				else
				{
					PropertyInfo propertyInfo = memberInfo as PropertyInfo;
					if (propertyInfo != null)
					{
						if (!propertyInfo.GetGetMethod(true).IsStatic)
						{
							return TestCaseSourceAttribute.ReturnErrorAsParameter("The sourceName specified on a TestCaseSourceAttribute must refer to a static field, property or method.");
						}
						if (this.MethodParams != null)
						{
							return TestCaseSourceAttribute.ReturnErrorAsParameter("You have specified a data source property but also given a set of parameters. Properties cannot take parameters, please revise the 3rd parameter passed to the TestCaseSource attribute and either remove it or specify a method.");
						}
						return (IEnumerable)propertyInfo.GetValue(null, null);
					}
					else
					{
						MethodInfo methodInfo = memberInfo as MethodInfo;
						if (methodInfo != null)
						{
							if (!methodInfo.IsStatic)
							{
								return TestCaseSourceAttribute.ReturnErrorAsParameter("The sourceName specified on a TestCaseSourceAttribute must refer to a static field, property or method.");
							}
							if (this.MethodParams != null && methodInfo.GetParameters().Length != this.MethodParams.Length)
							{
								return TestCaseSourceAttribute.ReturnErrorAsParameter("You have given the wrong number of arguments to the method in the TestCaseSourceAttribute, please check the number of parameters passed in the object is correct in the 3rd parameter for the TestCaseSourceAttribute and this matches the number of parameters in the target method and try again.");
							}
							return (IEnumerable)methodInfo.Invoke(null, this.MethodParams);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00005814 File Offset: 0x00003A14
		private static IEnumerable ReturnErrorAsParameter(string errorMessage)
		{
			TestCaseParameters testCaseParameters = new TestCaseParameters();
			testCaseParameters.RunState = RunState.NotRunnable;
			testCaseParameters.Properties.Set("_SKIPREASON", errorMessage);
			return new TestCaseParameters[] { testCaseParameters };
		}

		// Token: 0x04000042 RID: 66
		private NUnitTestCaseBuilder _builder = new NUnitTestCaseBuilder();

		// Token: 0x04000047 RID: 71
		private const string SourceMustBeStatic = "The sourceName specified on a TestCaseSourceAttribute must refer to a static field, property or method.";

		// Token: 0x04000048 RID: 72
		private const string ParamGivenToField = "You have specified a data source field but also given a set of parameters. Fields cannot take parameters, please revise the 3rd parameter passed to the TestCaseSourceAttribute and either remove it or specify a method.";

		// Token: 0x04000049 RID: 73
		private const string ParamGivenToProperty = "You have specified a data source property but also given a set of parameters. Properties cannot take parameters, please revise the 3rd parameter passed to the TestCaseSource attribute and either remove it or specify a method.";

		// Token: 0x0400004A RID: 74
		private const string NumberOfArgsDoesNotMatch = "You have given the wrong number of arguments to the method in the TestCaseSourceAttribute, please check the number of parameters passed in the object is correct in the 3rd parameter for the TestCaseSourceAttribute and this matches the number of parameters in the target method and try again.";
	}
}
