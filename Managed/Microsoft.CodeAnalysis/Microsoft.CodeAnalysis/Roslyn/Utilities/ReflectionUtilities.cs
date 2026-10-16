using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200005A RID: 90
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ReflectionUtilities
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x00006D58 File Offset: 0x00004F58
		[return: Nullable(2)]
		public static Type TryGetType(string assemblyQualifiedName)
		{
			Type type;
			try
			{
				type = Type.GetType(assemblyQualifiedName, false);
			}
			catch
			{
				type = null;
			}
			return type;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006D88 File Offset: 0x00004F88
		[NullableContext(2)]
		public static Type TryGetType([NotNull] ref Type lazyType, [Nullable(1)] string assemblyQualifiedName)
		{
			if (lazyType == null)
			{
				lazyType = ReflectionUtilities.TryGetType(assemblyQualifiedName) ?? ReflectionUtilities.Missing;
			}
			if (!(lazyType == ReflectionUtilities.Missing))
			{
				return lazyType;
			}
			return null;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006DB8 File Offset: 0x00004FB8
		[return: Nullable(2)]
		public static Type GetTypeFromEither(string contractName, string desktopName)
		{
			Type type = ReflectionUtilities.TryGetType(contractName);
			if (type == null)
			{
				type = ReflectionUtilities.TryGetType(desktopName);
			}
			return type;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006DDD File Offset: 0x00004FDD
		[return: Nullable(2)]
		public static Type GetTypeFromEither([Nullable(2)] [NotNull] ref Type lazyType, string contractName, string desktopName)
		{
			if (lazyType == null)
			{
				lazyType = ReflectionUtilities.GetTypeFromEither(contractName, desktopName) ?? ReflectionUtilities.Missing;
			}
			if (!(lazyType == ReflectionUtilities.Missing))
			{
				return lazyType;
			}
			return null;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006E10 File Offset: 0x00005010
		[return: Nullable(2)]
		public static T FindItem<[Nullable(0)] T>(IEnumerable<T> collection, params Type[] paramTypes) where T : MethodBase
		{
			foreach (T t in collection)
			{
				ParameterInfo[] parameters = t.GetParameters();
				if (parameters.Length == paramTypes.Length)
				{
					bool flag = true;
					for (int i = 0; i < paramTypes.Length; i++)
					{
						if (parameters[i].ParameterType != paramTypes[i])
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return t;
					}
				}
			}
			return default(T);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006EA8 File Offset: 0x000050A8
		[return: Nullable(2)]
		internal static MethodInfo GetDeclaredMethod(this TypeInfo typeInfo, string name, params Type[] paramTypes)
		{
			return ReflectionUtilities.FindItem<MethodInfo>(typeInfo.GetDeclaredMethods(name), paramTypes);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006EB7 File Offset: 0x000050B7
		[return: Nullable(2)]
		internal static ConstructorInfo GetDeclaredConstructor(this TypeInfo typeInfo, params Type[] paramTypes)
		{
			return ReflectionUtilities.FindItem<ConstructorInfo>(typeInfo.DeclaredConstructors, paramTypes);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00006EC8 File Offset: 0x000050C8
		[NullableContext(2)]
		public static T CreateDelegate<[Nullable(0)] T>(this MethodInfo methodInfo) where T : Delegate
		{
			if (methodInfo == null)
			{
				return default(T);
			}
			return (T)((object)methodInfo.CreateDelegate(typeof(T)));
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006F00 File Offset: 0x00005100
		[NullableContext(2)]
		public static T InvokeConstructor<T>(this ConstructorInfo constructorInfo, [Nullable(new byte[] { 1, 2 })] params object[] args)
		{
			T t;
			if (constructorInfo == null)
			{
				t = default(T);
				return t;
			}
			try
			{
				t = (T)((object)constructorInfo.Invoke(args));
			}
			catch (TargetInvocationException ex)
			{
				ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				t = default(T);
			}
			return t;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006F5C File Offset: 0x0000515C
		[return: Nullable(2)]
		public static object InvokeConstructor(this ConstructorInfo constructorInfo, [Nullable(new byte[] { 1, 2 })] params object[] args)
		{
			return constructorInfo.InvokeConstructor<object>(args);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006F65 File Offset: 0x00005165
		[return: Nullable(2)]
		public static T Invoke<[Nullable(2)] T>(this MethodInfo methodInfo, object obj, [Nullable(new byte[] { 1, 2 })] params object[] args)
		{
			return (T)((object)methodInfo.Invoke(obj, args));
		}

		// Token: 0x04000085 RID: 133
		private static readonly Type Missing = typeof(void);
	}
}
