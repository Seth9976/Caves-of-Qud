using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace HarmonyLib
{
	// Token: 0x0200003D RID: 61
	internal static class PatchTools
	{
		// Token: 0x06000145 RID: 325 RVA: 0x0000A30A File Offset: 0x0000850A
		internal static void RememberObject(object key, object value)
		{
			if (PatchTools.objectReferences == null)
			{
				PatchTools.objectReferences = new Dictionary<object, object>();
			}
			PatchTools.objectReferences[key] = value;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000A32C File Offset: 0x0000852C
		internal static MethodInfo GetPatchMethod(Type patchType, string attributeName)
		{
			Func<object, bool> <>9__1;
			MethodInfo methodInfo = patchType.GetMethods(AccessTools.all).FirstOrDefault<MethodInfo>(delegate(MethodInfo m)
			{
				IEnumerable<object> customAttributes = m.GetCustomAttributes(true);
				Func<object, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (object a) => a.GetType().FullName == attributeName);
				}
				return customAttributes.Any<object>(func);
			});
			if (methodInfo == null)
			{
				string text = attributeName.Replace("HarmonyLib.Harmony", "");
				methodInfo = patchType.GetMethod(text, AccessTools.all);
			}
			return methodInfo;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000A38C File Offset: 0x0000858C
		internal static AssemblyBuilder DefineDynamicAssembly(string name)
		{
			AssemblyName assemblyName = new AssemblyName(name);
			return AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000A3AC File Offset: 0x000085AC
		internal static List<AttributePatch> GetPatchMethods(Type type)
		{
			return (from method in AccessTools.GetDeclaredMethods(type)
				select AttributePatch.Create(method) into attributePatch
				where attributePatch != null
				select attributePatch).ToList<AttributePatch>();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000A40C File Offset: 0x0000860C
		internal static MethodBase GetOriginalMethod(this HarmonyMethod attr)
		{
			try
			{
				MethodType? methodType = attr.methodType;
				if (methodType != null)
				{
					switch (methodType.GetValueOrDefault())
					{
					case MethodType.Normal:
						if (attr.methodName == null)
						{
							return null;
						}
						return AccessTools.DeclaredMethod(attr.declaringType, attr.methodName, attr.argumentTypes, null);
					case MethodType.Getter:
						if (attr.methodName == null)
						{
							return null;
						}
						return AccessTools.DeclaredProperty(attr.declaringType, attr.methodName).GetGetMethod(true);
					case MethodType.Setter:
						if (attr.methodName == null)
						{
							return null;
						}
						return AccessTools.DeclaredProperty(attr.declaringType, attr.methodName).GetSetMethod(true);
					case MethodType.Constructor:
						return AccessTools.DeclaredConstructor(attr.declaringType, attr.argumentTypes, false);
					case MethodType.StaticConstructor:
						return (from c in AccessTools.GetDeclaredConstructors(attr.declaringType, null)
							where c.IsStatic
							select c).FirstOrDefault<ConstructorInfo>();
					case MethodType.Enumerator:
						if (attr.methodName == null)
						{
							return null;
						}
						return AccessTools.EnumeratorMoveNext(AccessTools.DeclaredMethod(attr.declaringType, attr.methodName, attr.argumentTypes, null));
					}
				}
			}
			catch (AmbiguousMatchException ex)
			{
				throw new HarmonyException("Ambiguous match for HarmonyMethod[" + attr.Description() + "]", ex.InnerException ?? ex);
			}
			return null;
		}

		// Token: 0x040000DD RID: 221
		[ThreadStatic]
		private static Dictionary<object, object> objectReferences;
	}
}
