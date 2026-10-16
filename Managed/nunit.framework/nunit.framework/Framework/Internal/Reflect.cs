using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using NUnit.Compatibility;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000076 RID: 118
	public static class Reflect
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x00009C84 File Offset: 0x00007E84
		public static MethodInfo[] GetMethodsWithAttribute(Type fixtureType, Type attributeType, bool inherit)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			BindingFlags bindingFlags = Reflect.AllMembers | (inherit ? BindingFlags.FlattenHierarchy : BindingFlags.DeclaredOnly);
			foreach (MethodInfo methodInfo in fixtureType.GetMethods(bindingFlags))
			{
				if (methodInfo.IsDefined(attributeType, inherit))
				{
					list.Add(methodInfo);
				}
			}
			list.Sort(new Reflect.BaseTypesFirstComparer());
			return list.ToArray();
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00009CE8 File Offset: 0x00007EE8
		public static bool HasMethodWithAttribute(Type fixtureType, Type attributeType)
		{
			MethodInfo[] methods = fixtureType.GetMethods(Reflect.AllMembers | BindingFlags.FlattenHierarchy);
			for (int i = 0; i < methods.Length; i++)
			{
				if (methods[i].IsDefined(attributeType, false))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00009D24 File Offset: 0x00007F24
		public static object Construct(Type type)
		{
			if (Reflect.ConstructorCallWrapper != null)
			{
				return Reflect.ConstructorCallWrapper(type, null);
			}
			ConstructorInfo constructor = type.GetConstructor(Reflect.EmptyTypes);
			if (constructor == null)
			{
				throw new InvalidTestFixtureException(type.FullName + " does not have a default constructor");
			}
			return constructor.Invoke(null);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00009D78 File Offset: 0x00007F78
		public static object Construct(Type type, object[] arguments)
		{
			if (Reflect.ConstructorCallWrapper != null)
			{
				return Reflect.ConstructorCallWrapper(type, arguments);
			}
			if (arguments == null)
			{
				return Reflect.Construct(type);
			}
			Type[] typeArray = Reflect.GetTypeArray(arguments);
			ConstructorInfo constructor = ((ITypeInfo)new TypeWrapper(type)).GetConstructor(typeArray);
			if (constructor == null)
			{
				throw new InvalidTestFixtureException(type.FullName + " does not have a suitable constructor");
			}
			return constructor.Invoke(arguments);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00009DDC File Offset: 0x00007FDC
		internal static Type[] GetTypeArray(object[] objects)
		{
			Type[] array = new Type[objects.Length];
			int num = 0;
			foreach (object obj in objects)
			{
				array[num++] = ((obj == null) ? typeof(NUnitNullType) : obj.GetType());
			}
			return array;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00009E28 File Offset: 0x00008028
		public static object InvokeMethod(MethodInfo method, object fixture)
		{
			return Reflect.InvokeMethod(method, fixture, null);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00009E34 File Offset: 0x00008034
		public static object InvokeMethod(MethodInfo method, object fixture, params object[] args)
		{
			if (method != null)
			{
				try
				{
					if (Reflect.MethodCallWrapper != null)
					{
						return Reflect.MethodCallWrapper(() => method.Invoke(fixture, args));
					}
					return method.Invoke(fixture, args);
				}
				catch (ThreadAbortException)
				{
					return null;
				}
				catch (TargetInvocationException ex)
				{
					throw new NUnitException("Rethrown", ex.InnerException);
				}
				catch (Exception ex2)
				{
					throw new NUnitException("Rethrown", ex2);
				}
			}
			return null;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00009EF4 File Offset: 0x000080F4
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x00009EFB File Offset: 0x000080FB
		public static Func<Func<object>, object> MethodCallWrapper { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00009F03 File Offset: 0x00008103
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x00009F0A File Offset: 0x0000810A
		public static Func<Type, object[], object> ConstructorCallWrapper { get; set; }

		// Token: 0x040000B5 RID: 181
		private static readonly BindingFlags AllMembers = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x040000B6 RID: 182
		private static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x02000177 RID: 375
		private class BaseTypesFirstComparer : IComparer<MethodInfo>
		{
			// Token: 0x06000A96 RID: 2710 RVA: 0x00019874 File Offset: 0x00017A74
			public int Compare(MethodInfo m1, MethodInfo m2)
			{
				if (m1 == null || m2 == null)
				{
					return 0;
				}
				Type declaringType = m1.DeclaringType;
				Type declaringType2 = m2.DeclaringType;
				if (declaringType == declaringType2)
				{
					return 0;
				}
				if (declaringType.IsAssignableFrom(declaringType2))
				{
					return -1;
				}
				return 1;
			}
		}
	}
}
