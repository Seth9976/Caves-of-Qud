using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000115 RID: 277
	internal static class StatePool<T1, T2>
	{
		// Token: 0x06000658 RID: 1624 RVA: 0x0000EA34 File Offset: 0x0000CC34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static StateTuple<T1, T2> Create(T1 item1, T2 item2)
		{
			StateTuple<T1, T2> stateTuple;
			if (StatePool<T1, T2>.queue.TryDequeue(out stateTuple))
			{
				stateTuple.Item1 = item1;
				stateTuple.Item2 = item2;
				return stateTuple;
			}
			return new StateTuple<T1, T2>
			{
				Item1 = item1,
				Item2 = item2
			};
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0000EA72 File Offset: 0x0000CC72
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Return(StateTuple<T1, T2> tuple)
		{
			tuple.Item1 = default(T1);
			tuple.Item2 = default(T2);
			StatePool<T1, T2>.queue.Enqueue(tuple);
		}

		// Token: 0x04000124 RID: 292
		private static readonly ConcurrentQueue<StateTuple<T1, T2>> queue = new ConcurrentQueue<StateTuple<T1, T2>>();
	}
}
