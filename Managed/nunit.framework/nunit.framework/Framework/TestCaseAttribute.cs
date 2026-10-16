using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using NUnit.Compatibility;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace NUnit.Framework
{
	// Token: 0x0200003C RID: 60
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	public class TestCaseAttribute : NUnitAttribute, ITestBuilder, ITestCaseData, ITestData, IImplyFixture
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00004CEE File Offset: 0x00002EEE
		public TestCaseAttribute(params object[] arguments)
		{
			this.RunState = RunState.Runnable;
			if (arguments == null)
			{
				this.Arguments = new object[1];
			}
			else
			{
				this.Arguments = arguments;
			}
			this.Properties = new PropertyBag();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00004D20 File Offset: 0x00002F20
		public TestCaseAttribute(object arg)
		{
			this.RunState = RunState.Runnable;
			this.Arguments = new object[] { arg };
			this.Properties = new PropertyBag();
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00004D4A File Offset: 0x00002F4A
		public TestCaseAttribute(object arg1, object arg2)
		{
			this.RunState = RunState.Runnable;
			this.Arguments = new object[] { arg1, arg2 };
			this.Properties = new PropertyBag();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00004D78 File Offset: 0x00002F78
		public TestCaseAttribute(object arg1, object arg2, object arg3)
		{
			this.RunState = RunState.Runnable;
			this.Arguments = new object[] { arg1, arg2, arg3 };
			this.Properties = new PropertyBag();
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00004DAA File Offset: 0x00002FAA
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00004DB2 File Offset: 0x00002FB2
		public string TestName { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00004DBB File Offset: 0x00002FBB
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00004DC3 File Offset: 0x00002FC3
		public RunState RunState { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00004DCC File Offset: 0x00002FCC
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00004DD4 File Offset: 0x00002FD4
		public object[] Arguments { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00004DDD File Offset: 0x00002FDD
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00004DE5 File Offset: 0x00002FE5
		public IPropertyBag Properties { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00004DEE File Offset: 0x00002FEE
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00004DF6 File Offset: 0x00002FF6
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00004E06 File Offset: 0x00003006
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00004E0E File Offset: 0x0000300E
		public bool HasExpectedResult { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00004E17 File Offset: 0x00003017
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00004E2E File Offset: 0x0000302E
		public string Description
		{
			get
			{
				return this.Properties.Get("Description") as string;
			}
			set
			{
				this.Properties.Set("Description", value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00004E41 File Offset: 0x00003041
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00004E58 File Offset: 0x00003058
		public string Author
		{
			get
			{
				return this.Properties.Get("Author") as string;
			}
			set
			{
				this.Properties.Set("Author", value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00004E6B File Offset: 0x0000306B
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00004E73 File Offset: 0x00003073
		public Type TestOf
		{
			get
			{
				return this._testOf;
			}
			set
			{
				this._testOf = value;
				this.Properties.Set("TestOf", value.FullName);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00004E92 File Offset: 0x00003092
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00004E9A File Offset: 0x0000309A
		public string Ignore
		{
			get
			{
				return this.IgnoreReason;
			}
			set
			{
				this.IgnoreReason = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00004EA3 File Offset: 0x000030A3
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00004EAE File Offset: 0x000030AE
		public bool Explicit
		{
			get
			{
				return this.RunState == RunState.Explicit;
			}
			set
			{
				this.RunState = (value ? RunState.Explicit : RunState.Runnable);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00004EBD File Offset: 0x000030BD
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00004ED4 File Offset: 0x000030D4
		public string Reason
		{
			get
			{
				return this.Properties.Get("_SKIPREASON") as string;
			}
			set
			{
				this.Properties.Set("_SKIPREASON", value);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00004EE7 File Offset: 0x000030E7
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00004EEF File Offset: 0x000030EF
		public string IgnoreReason
		{
			get
			{
				return this.Reason;
			}
			set
			{
				this.RunState = RunState.Ignored;
				this.Reason = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00004EFF File Offset: 0x000030FF
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00004F07 File Offset: 0x00003107
		public string IncludePlatform { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00004F10 File Offset: 0x00003110
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00004F18 File Offset: 0x00003118
		public string ExcludePlatform { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00004F21 File Offset: 0x00003121
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00004F38 File Offset: 0x00003138
		public string Category
		{
			get
			{
				return this.Properties.Get("Category") as string;
			}
			set
			{
				foreach (string text in value.Split(new char[] { ',' }))
				{
					this.Properties.Add("Category", text);
				}
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00004F7C File Offset: 0x0000317C
		private TestCaseParameters GetParametersForTestCase(IMethodInfo method)
		{
			TestCaseParameters testCaseParameters;
			try
			{
				IParameterInfo[] parameters = method.GetParameters();
				int num = parameters.Length;
				int num2 = this.Arguments.Length;
				testCaseParameters = new TestCaseParameters(this);
				if (num > 0 && num2 >= num - 1)
				{
					IParameterInfo parameterInfo = parameters[num - 1];
					Type parameterType = parameterInfo.ParameterType;
					Type elementType = parameterType.GetElementType();
					if (parameterType.IsArray && parameterInfo.IsDefined<ParamArrayAttribute>(false))
					{
						if (num2 == num)
						{
							Type type = testCaseParameters.Arguments[num2 - 1].GetType();
							if (!parameterType.GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
							{
								Array array = Array.CreateInstance(elementType, 1);
								array.SetValue(testCaseParameters.Arguments[num2 - 1], 0);
								testCaseParameters.Arguments[num2 - 1] = array;
							}
						}
						else
						{
							object[] array2 = new object[num];
							int num3 = 0;
							while (num3 < num && num3 < num2)
							{
								array2[num3] = testCaseParameters.Arguments[num3];
								num3++;
							}
							int num4 = num2 - num + 1;
							Array array3 = Array.CreateInstance(elementType, num4);
							for (int i = 0; i < num4; i++)
							{
								array3.SetValue(testCaseParameters.Arguments[num + i - 1], i);
							}
							array2[num - 1] = array3;
							testCaseParameters.Arguments = array2;
							num2 = num;
						}
					}
				}
				if (testCaseParameters.Arguments.Length < num)
				{
					object[] array4 = new object[parameters.Length];
					Array.Copy(testCaseParameters.Arguments, array4, testCaseParameters.Arguments.Length);
					for (int j = testCaseParameters.Arguments.Length; j < parameters.Length; j++)
					{
						if (parameters[j].IsOptional)
						{
							array4[j] = Type.Missing;
						}
						else
						{
							if (j >= testCaseParameters.Arguments.Length)
							{
								throw new TargetParameterCountException("Incorrect number of parameters specified for TestCase");
							}
							array4[j] = testCaseParameters.Arguments[j];
						}
					}
					testCaseParameters.Arguments = array4;
				}
				if (num == 1 && method.GetParameters()[0].ParameterType == typeof(object[]) && (num2 > 1 || (num2 == 1 && testCaseParameters.Arguments[0].GetType() != typeof(object[]))))
				{
					testCaseParameters.Arguments = new object[] { testCaseParameters.Arguments };
				}
				if (num2 == num)
				{
					TestCaseAttribute.PerformSpecialConversions(testCaseParameters.Arguments, parameters);
				}
			}
			catch (Exception ex)
			{
				testCaseParameters = new TestCaseParameters(ex);
			}
			return testCaseParameters;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000051D0 File Offset: 0x000033D0
		private static void PerformSpecialConversions(object[] arglist, IParameterInfo[] parameters)
		{
			for (int i = 0; i < arglist.Length; i++)
			{
				object obj = arglist[i];
				Type parameterType = parameters[i].ParameterType;
				if (obj != null)
				{
					if (obj is SpecialValue && (SpecialValue)obj == SpecialValue.Null)
					{
						arglist[i] = null;
					}
					else if (!parameterType.IsAssignableFrom(obj.GetType()))
					{
						if (obj is DBNull)
						{
							arglist[i] = null;
						}
						else
						{
							bool flag = false;
							if (parameterType == typeof(short) || parameterType == typeof(byte) || parameterType == typeof(sbyte) || parameterType == typeof(short?) || parameterType == typeof(byte?) || parameterType == typeof(sbyte?) || parameterType == typeof(double?))
							{
								flag = obj is int;
							}
							else if (parameterType == typeof(decimal) || parameterType == typeof(decimal?))
							{
								flag = obj is double || obj is string || obj is int;
							}
							else if (parameterType == typeof(DateTime) || parameterType == typeof(DateTime?))
							{
								flag = obj is string;
							}
							if (flag)
							{
								Type type = ((parameterType.GetTypeInfo().IsGenericType && parameterType.GetGenericTypeDefinition() == typeof(Nullable<>)) ? parameterType.GetGenericArguments()[0] : parameterType);
								arglist[i] = Convert.ChangeType(obj, type, CultureInfo.InvariantCulture);
							}
							else if ((parameterType == typeof(TimeSpan) || parameterType == typeof(TimeSpan?)) && obj is string)
							{
								arglist[i] = TimeSpan.Parse((string)obj);
							}
						}
					}
				}
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000053C1 File Offset: 0x000035C1
		public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
		{
			TestMethod testMethod = new NUnitTestCaseBuilder().BuildTestMethod(method, suite, this.GetParametersForTestCase(method));
			if (testMethod.RunState != RunState.NotRunnable && testMethod.RunState != RunState.Ignored)
			{
				PlatformHelper platformHelper = new PlatformHelper();
				if (!platformHelper.IsPlatformSupported(this))
				{
					testMethod.RunState = RunState.Skipped;
					testMethod.Properties.Add("_SKIPREASON", platformHelper.Reason);
				}
			}
			yield return testMethod;
			yield break;
		}

		// Token: 0x0400003D RID: 61
		private object _expectedResult;

		// Token: 0x0400003F RID: 63
		private Type _testOf;
	}
}
