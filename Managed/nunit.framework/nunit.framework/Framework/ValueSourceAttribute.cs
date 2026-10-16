using System;
using System.Collections;
using System.Reflection;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x02000046 RID: 70
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = false)]
	public class ValueSourceAttribute : DataAttribute, IParameterDataSource
	{
		// Token: 0x06000212 RID: 530 RVA: 0x00006041 File Offset: 0x00004241
		public ValueSourceAttribute(string sourceName)
		{
			this.SourceName = sourceName;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00006050 File Offset: 0x00004250
		public ValueSourceAttribute(Type sourceType, string sourceName)
		{
			this.SourceType = sourceType;
			this.SourceName = sourceName;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00006066 File Offset: 0x00004266
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000606E File Offset: 0x0000426E
		public string SourceName { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00006077 File Offset: 0x00004277
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000607F File Offset: 0x0000427F
		public Type SourceType { get; private set; }

		// Token: 0x06000218 RID: 536 RVA: 0x00006088 File Offset: 0x00004288
		public IEnumerable GetData(IParameterInfo parameter)
		{
			return this.GetDataSource(parameter);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006094 File Offset: 0x00004294
		private IEnumerable GetDataSource(IParameterInfo parameter)
		{
			Type type = this.SourceType ?? parameter.Method.TypeInfo.Type;
			if (this.SourceName == null)
			{
				return Reflect.Construct(type) as IEnumerable;
			}
			IEnumerable dataSourceValue = ValueSourceAttribute.GetDataSourceValue(type.GetMember(this.SourceName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy));
			if (dataSourceValue == null)
			{
				ValueSourceAttribute.ThrowInvalidDataSourceException();
			}
			return dataSourceValue;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000060EC File Offset: 0x000042EC
		private static IEnumerable GetDataSourceValue(MemberInfo[] members)
		{
			if (members.Length != 1)
			{
				return null;
			}
			MemberInfo memberInfo = members[0];
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			if (fieldInfo != null)
			{
				if (fieldInfo.IsStatic)
				{
					return (IEnumerable)fieldInfo.GetValue(null);
				}
				ValueSourceAttribute.ThrowInvalidDataSourceException();
			}
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo != null)
			{
				if (propertyInfo.GetGetMethod(true).IsStatic)
				{
					return (IEnumerable)propertyInfo.GetValue(null, null);
				}
				ValueSourceAttribute.ThrowInvalidDataSourceException();
			}
			MethodInfo methodInfo = memberInfo as MethodInfo;
			if (methodInfo != null)
			{
				if (methodInfo.IsStatic)
				{
					return (IEnumerable)methodInfo.Invoke(null, null);
				}
				ValueSourceAttribute.ThrowInvalidDataSourceException();
			}
			return null;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000618C File Offset: 0x0000438C
		private static void ThrowInvalidDataSourceException()
		{
			throw new InvalidDataSourceException("The sourceName specified on a ValueSourceAttribute must refer to a non null static field, property or method.");
		}
	}
}
