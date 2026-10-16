using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.TestTools
{
	// Token: 0x0200046A RID: 1130
	[NativeClass("ScriptingCoverage")]
	[NativeType("Runtime/Scripting/ScriptingCoverage.h")]
	public static class Coverage
	{
		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002C6D RID: 11373
		// (set) Token: 0x06002C6E RID: 11374
		public static extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06002C6F RID: 11375
		[FreeFunction("ScriptingCoverageGetCoverageForMethodInfoObject", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CoveredSequencePoint[] GetSequencePointsFor_Internal(MethodBase method);

		// Token: 0x06002C70 RID: 11376
		[FreeFunction("ScriptingCoverageResetForMethodInfoObject", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetFor_Internal(MethodBase method);

		// Token: 0x06002C71 RID: 11377 RVA: 0x00054070 File Offset: 0x00052270
		[FreeFunction("ScriptingCoverageGetStatsForMethodInfoObject", ThrowsException = true)]
		private static CoveredMethodStats GetStatsFor_Internal(MethodBase method)
		{
			CoveredMethodStats coveredMethodStats;
			Coverage.GetStatsFor_Internal_Injected(method, out coveredMethodStats);
			return coveredMethodStats;
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x00054088 File Offset: 0x00052288
		public static CoveredSequencePoint[] GetSequencePointsFor(MethodBase method)
		{
			bool flag = method == null;
			if (flag)
			{
				throw new ArgumentNullException("method");
			}
			return Coverage.GetSequencePointsFor_Internal(method);
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x000540B8 File Offset: 0x000522B8
		public static CoveredMethodStats GetStatsFor(MethodBase method)
		{
			bool flag = method == null;
			if (flag)
			{
				throw new ArgumentNullException("method");
			}
			return Coverage.GetStatsFor_Internal(method);
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x000540E8 File Offset: 0x000522E8
		public static CoveredMethodStats[] GetStatsFor(MethodBase[] methods)
		{
			bool flag = methods == null;
			if (flag)
			{
				throw new ArgumentNullException("methods");
			}
			CoveredMethodStats[] array = new CoveredMethodStats[methods.Length];
			for (int i = 0; i < methods.Length; i++)
			{
				array[i] = Coverage.GetStatsFor(methods[i]);
			}
			return array;
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x0005413C File Offset: 0x0005233C
		public static CoveredMethodStats[] GetStatsFor(Type type)
		{
			bool flag = type == null;
			if (flag)
			{
				throw new ArgumentNullException("type");
			}
			return Coverage.GetStatsFor(type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).OfType<MethodBase>().ToArray<MethodBase>());
		}

		// Token: 0x06002C76 RID: 11382
		[FreeFunction("ScriptingCoverageGetStatsForAllCoveredMethodsFromScripting", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern CoveredMethodStats[] GetStatsForAllCoveredMethods();

		// Token: 0x06002C77 RID: 11383 RVA: 0x0005417C File Offset: 0x0005237C
		public static void ResetFor(MethodBase method)
		{
			bool flag = method == null;
			if (flag)
			{
				throw new ArgumentNullException("method");
			}
			Coverage.ResetFor_Internal(method);
		}

		// Token: 0x06002C78 RID: 11384
		[FreeFunction("ScriptingCoverageResetAllFromScripting", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetAll();

		// Token: 0x06002C79 RID: 11385
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStatsFor_Internal_Injected(MethodBase method, out CoveredMethodStats ret);
	}
}
