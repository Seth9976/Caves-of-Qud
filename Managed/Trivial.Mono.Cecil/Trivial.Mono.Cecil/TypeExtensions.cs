using System;
using System.Reflection;

namespace Trivial.Mono
{
	// Token: 0x02000007 RID: 7
	internal static class TypeExtensions
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020B4 File Offset: 0x000002B4
		public static TypeCode GetTypeCode(this Type type)
		{
			return Type.GetTypeCode(type);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020BC File Offset: 0x000002BC
		public static Assembly Assembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020C4 File Offset: 0x000002C4
		public static MethodBase DeclaringMethod(this Type type)
		{
			return type.DeclaringMethod;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020CC File Offset: 0x000002CC
		public static Type[] GetGenericArguments(this Type type)
		{
			return type.GetGenericArguments();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020D4 File Offset: 0x000002D4
		public static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020DC File Offset: 0x000002DC
		public static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020E4 File Offset: 0x000002E4
		public static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}
	}
}
