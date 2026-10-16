using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200004D RID: 77
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ISetExtensions
	{
		// Token: 0x06000195 RID: 405 RVA: 0x000062C0 File Offset: 0x000044C0
		public static bool AddAll<[Nullable(2)] T>(this ISet<T> set, IEnumerable<T> values)
		{
			bool flag = false;
			foreach (T t in values)
			{
				flag |= set.Add(t);
			}
			return flag;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006310 File Offset: 0x00004510
		public static bool AddAll<[Nullable(2)] T>(this ISet<T> set, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> values)
		{
			bool flag = false;
			foreach (T t in values)
			{
				flag |= set.Add(t);
			}
			return flag;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006348 File Offset: 0x00004548
		public static bool RemoveAll<[Nullable(2)] T>(this ISet<T> set, IEnumerable<T> values)
		{
			bool flag = false;
			foreach (T t in values)
			{
				flag |= set.Remove(t);
			}
			return flag;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006398 File Offset: 0x00004598
		public static bool RemoveAll<[Nullable(2)] T>(this ISet<T> set, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> values)
		{
			bool flag = false;
			foreach (T t in values)
			{
				flag |= set.Remove(t);
			}
			return flag;
		}
	}
}
