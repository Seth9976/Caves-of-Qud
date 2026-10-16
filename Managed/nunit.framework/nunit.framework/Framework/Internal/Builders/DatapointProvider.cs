using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Compatibility;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Builders
{
	// Token: 0x020000B7 RID: 183
	public class DatapointProvider : IParameterDataProvider
	{
		// Token: 0x06000665 RID: 1637 RVA: 0x0000FA58 File Offset: 0x0000DC58
		public bool HasDataFor(IParameterInfo parameter)
		{
			IMethodInfo method = parameter.Method;
			if (!method.IsDefined<TheoryAttribute>(true))
			{
				return false;
			}
			Type parameterType = parameter.ParameterType;
			if (parameterType == typeof(bool) || parameterType.GetTypeInfo().IsEnum)
			{
				return true;
			}
			foreach (MemberInfo memberInfo in method.TypeInfo.Type.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
			{
				if (memberInfo.IsDefined(typeof(DatapointAttribute), true) && this.GetTypeFromMemberInfo(memberInfo) == parameterType)
				{
					return true;
				}
				if (memberInfo.IsDefined(typeof(DatapointSourceAttribute), true) && this.GetElementTypeFromMemberInfo(memberInfo) == parameterType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0000FB14 File Offset: 0x0000DD14
		public IEnumerable GetDataFor(IParameterInfo parameter)
		{
			List<object> list = new List<object>();
			Type parameterType = parameter.ParameterType;
			Type type = parameter.Method.TypeInfo.Type;
			foreach (MemberInfo memberInfo in type.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
			{
				if (memberInfo.IsDefined(typeof(DatapointAttribute), true))
				{
					FieldInfo fieldInfo = memberInfo as FieldInfo;
					if (this.GetTypeFromMemberInfo(memberInfo) == parameterType && fieldInfo != null)
					{
						if (fieldInfo.IsStatic)
						{
							list.Add(fieldInfo.GetValue(null));
						}
						else
						{
							list.Add(fieldInfo.GetValue(ProviderCache.GetInstanceOf(type)));
						}
					}
				}
				else if (memberInfo.IsDefined(typeof(DatapointSourceAttribute), true) && this.GetElementTypeFromMemberInfo(memberInfo) == parameterType)
				{
					FieldInfo fieldInfo2 = memberInfo as FieldInfo;
					PropertyInfo propertyInfo = memberInfo as PropertyInfo;
					MethodInfo methodInfo = memberInfo as MethodInfo;
					if (fieldInfo2 != null)
					{
						object obj = (fieldInfo2.IsStatic ? null : ProviderCache.GetInstanceOf(type));
						using (IEnumerator enumerator = ((IEnumerable)fieldInfo2.GetValue(obj)).GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj2 = enumerator.Current;
								list.Add(obj2);
							}
							goto IL_0234;
						}
					}
					if (propertyInfo != null)
					{
						object obj = (propertyInfo.GetGetMethod(true).IsStatic ? null : ProviderCache.GetInstanceOf(type));
						using (IEnumerator enumerator = ((IEnumerable)propertyInfo.GetValue(obj, null)).GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj3 = enumerator.Current;
								list.Add(obj3);
							}
							goto IL_0234;
						}
					}
					if (methodInfo != null)
					{
						object obj = (methodInfo.IsStatic ? null : ProviderCache.GetInstanceOf(type));
						MethodBase methodBase = methodInfo;
						object obj4 = obj;
						object[] array = new Type[0];
						foreach (object obj5 in ((IEnumerable)methodBase.Invoke(obj4, array)))
						{
							list.Add(obj5);
						}
					}
				}
				IL_0234:;
			}
			if (list.Count == 0)
			{
				if (parameterType == typeof(bool))
				{
					list.Add(true);
					list.Add(false);
				}
				else if (parameterType.GetTypeInfo().IsEnum)
				{
					foreach (object obj6 in TypeHelper.GetEnumValues(parameterType))
					{
						list.Add(obj6);
					}
				}
			}
			return list;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0000FE1C File Offset: 0x0000E01C
		private Type GetTypeFromMemberInfo(MemberInfo member)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo != null)
			{
				return fieldInfo.FieldType;
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return propertyInfo.PropertyType;
			}
			MethodInfo methodInfo = member as MethodInfo;
			if (methodInfo != null)
			{
				return methodInfo.ReturnType;
			}
			return null;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0000FE70 File Offset: 0x0000E070
		private Type GetElementTypeFromMemberInfo(MemberInfo member)
		{
			Type typeFromMemberInfo = this.GetTypeFromMemberInfo(member);
			if (typeFromMemberInfo == null)
			{
				return null;
			}
			if (typeFromMemberInfo.IsArray)
			{
				return typeFromMemberInfo.GetElementType();
			}
			if (typeFromMemberInfo.GetTypeInfo().IsGenericType && typeFromMemberInfo.Name == "IEnumerable`1")
			{
				return typeFromMemberInfo.GetGenericArguments()[0];
			}
			return null;
		}
	}
}
