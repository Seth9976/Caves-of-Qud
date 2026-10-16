using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200004C RID: 76
	internal class UDPReflectionUtils
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00005EDC File Offset: 0x000040DC
		internal static Type GetTypeByName(string typeName)
		{
			if (UDPReflectionUtils.s_typeCache.ContainsKey(typeName))
			{
				return UDPReflectionUtils.s_typeCache[typeName];
			}
			using (IEnumerator<Assembly> enumerator = UDPReflectionUtils.GetAllAssemblies().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Assembly assembly = enumerator.Current;
					if (Array.Exists<string>(UDPReflectionUtils.k_whiteListedAssemblies, (string x) => assembly.FullName.StartsWith(x)))
					{
						foreach (Type type in UDPReflectionUtils.GetTypes(assembly))
						{
							if (type.FullName == typeName)
							{
								UDPReflectionUtils.s_typeCache[type.FullName] = type;
								return type;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005FC4 File Offset: 0x000041C4
		private static IEnumerable<Assembly> GetAllAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005FD0 File Offset: 0x000041D0
		private static IEnumerable<Type> GetTypes(Assembly assembly)
		{
			Type[] types;
			if (!UDPReflectionUtils.s_assemblyTypeCache.TryGetValue(assembly, out types))
			{
				types = assembly.GetTypes();
				UDPReflectionUtils.s_assemblyTypeCache[assembly] = types;
			}
			return types;
		}

		// Token: 0x040000C6 RID: 198
		internal const BindingFlags k_InstanceBindingFlags = BindingFlags.Instance | BindingFlags.Public;

		// Token: 0x040000C7 RID: 199
		internal const BindingFlags k_StaticBindingFlags = BindingFlags.Static | BindingFlags.Public;

		// Token: 0x040000C8 RID: 200
		internal const BindingFlags k_PrivateStaticBindingFlags = BindingFlags.Static | BindingFlags.NonPublic;

		// Token: 0x040000C9 RID: 201
		private static readonly Dictionary<Assembly, Type[]> s_assemblyTypeCache = new Dictionary<Assembly, Type[]>();

		// Token: 0x040000CA RID: 202
		private static readonly Dictionary<string, Type> s_typeCache = new Dictionary<string, Type>();

		// Token: 0x040000CB RID: 203
		private static readonly string[] k_whiteListedAssemblies = new string[] { "UnityEngine", "UnityEditor", "UDP", "com.unity" };
	}
}
