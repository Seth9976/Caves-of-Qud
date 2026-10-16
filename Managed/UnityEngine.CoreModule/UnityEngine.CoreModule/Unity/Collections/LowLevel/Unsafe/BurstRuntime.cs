using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x020000B3 RID: 179
	internal static class BurstRuntime
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x00007C0C File Offset: 0x00005E0C
		public static long GetHashCode64<T>()
		{
			return BurstRuntime.HashCode64<T>.Value;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00007C24 File Offset: 0x00005E24
		internal static long HashStringWithFNV1A64(string text)
		{
			ulong num = 14695981039346656037UL;
			foreach (char c in text)
			{
				num = 1099511628211UL * (num ^ (ulong)((byte)(c & 'ÿ')));
				num = 1099511628211UL * (num ^ (ulong)((byte)(c >> 8)));
			}
			return (long)num;
		}

		// Token: 0x020000B4 RID: 180
		private struct HashCode64<T>
		{
			// Token: 0x0400028B RID: 651
			public static readonly long Value = BurstRuntime.HashStringWithFNV1A64(typeof(T).AssemblyQualifiedName);
		}
	}
}
