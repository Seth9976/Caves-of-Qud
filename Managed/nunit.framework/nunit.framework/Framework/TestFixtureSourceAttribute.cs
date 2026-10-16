using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace NUnit.Framework
{
	// Token: 0x02000040 RID: 64
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class TestFixtureSourceAttribute : NUnitAttribute, IFixtureBuilder
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x00005AD9 File Offset: 0x00003CD9
		public TestFixtureSourceAttribute(string sourceName)
		{
			this.SourceName = sourceName;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00005AF3 File Offset: 0x00003CF3
		public TestFixtureSourceAttribute(Type sourceType, string sourceName)
		{
			this.SourceType = sourceType;
			this.SourceName = sourceName;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00005B14 File Offset: 0x00003D14
		public TestFixtureSourceAttribute(Type sourceType)
		{
			this.SourceType = sourceType;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00005B2E File Offset: 0x00003D2E
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00005B36 File Offset: 0x00003D36
		public string SourceName { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00005B3F File Offset: 0x00003D3F
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00005B47 File Offset: 0x00003D47
		public Type SourceType { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00005B50 File Offset: 0x00003D50
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00005B58 File Offset: 0x00003D58
		public string Category { get; set; }

		// Token: 0x06000201 RID: 513 RVA: 0x00005B61 File Offset: 0x00003D61
		public IEnumerable<TestSuite> BuildFrom(ITypeInfo typeInfo)
		{
			Type type = this.SourceType ?? typeInfo.Type;
			foreach (ITestFixtureData testFixtureData in this.GetParametersFor(type))
			{
				TestFixtureParameters testFixtureParameters = (TestFixtureParameters)testFixtureData;
				yield return this._builder.BuildFrom(typeInfo, testFixtureParameters);
			}
			IEnumerator<ITestFixtureData> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00005B78 File Offset: 0x00003D78
		public IEnumerable<ITestFixtureData> GetParametersFor(Type sourceType)
		{
			List<ITestFixtureData> list = new List<ITestFixtureData>();
			try
			{
				IEnumerable testFixtureSource = this.GetTestFixtureSource(sourceType);
				if (testFixtureSource != null)
				{
					foreach (object obj in testFixtureSource)
					{
						ITestFixtureData testFixtureData = obj as ITestFixtureData;
						if (testFixtureData == null)
						{
							object[] array = obj as object[];
							if (array == null)
							{
								array = new object[] { obj };
							}
							testFixtureData = new TestFixtureParameters(array);
						}
						if (this.Category != null)
						{
							foreach (string text in this.Category.Split(new char[] { ',' }))
							{
								testFixtureData.Properties.Add("Category", text);
							}
						}
						list.Add(testFixtureData);
					}
				}
			}
			catch (Exception ex)
			{
				list.Clear();
				list.Add(new TestFixtureParameters(ex));
			}
			return list;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00005C88 File Offset: 0x00003E88
		private IEnumerable GetTestFixtureSource(Type sourceType)
		{
			if (this.SourceName == null)
			{
				return Reflect.Construct(sourceType) as IEnumerable;
			}
			MemberInfo[] member = sourceType.GetMember(this.SourceName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (member.Length == 1)
			{
				MemberInfo memberInfo = member[0];
				FieldInfo fieldInfo = memberInfo as FieldInfo;
				if (fieldInfo != null)
				{
					if (!fieldInfo.IsStatic)
					{
						return TestFixtureSourceAttribute.SourceMustBeStaticError();
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
							return TestFixtureSourceAttribute.SourceMustBeStaticError();
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
								return TestFixtureSourceAttribute.SourceMustBeStaticError();
							}
							return (IEnumerable)methodInfo.Invoke(null, null);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00005D54 File Offset: 0x00003F54
		private static IEnumerable SourceMustBeStaticError()
		{
			TestFixtureParameters testFixtureParameters = new TestFixtureParameters();
			testFixtureParameters.RunState = RunState.NotRunnable;
			testFixtureParameters.Properties.Set("_SKIPREASON", "The sourceName specified on a TestCaseSourceAttribute must refer to a static field, property or method.");
			return new TestFixtureParameters[] { testFixtureParameters };
		}

		// Token: 0x04000052 RID: 82
		private readonly NUnitTestFixtureBuilder _builder = new NUnitTestFixtureBuilder();

		// Token: 0x04000053 RID: 83
		public const string MUST_BE_STATIC = "The sourceName specified on a TestCaseSourceAttribute must refer to a static field, property or method.";
	}
}
