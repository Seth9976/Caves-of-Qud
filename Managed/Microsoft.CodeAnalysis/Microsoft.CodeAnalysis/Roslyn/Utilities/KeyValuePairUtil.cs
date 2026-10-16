using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200004F RID: 79
	[NullableContext(1)]
	[Nullable(0)]
	internal static class KeyValuePairUtil
	{
		// Token: 0x060001AE RID: 430 RVA: 0x000066F7 File Offset: 0x000048F7
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public static KeyValuePair<K, V> Create<[Nullable(2)] K, [Nullable(2)] V>(K key, V value)
		{
			return new KeyValuePair<K, V>(key, value);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006700 File Offset: 0x00004900
		public static void Deconstruct<[Nullable(2)] TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 0, 1, 1 })] this KeyValuePair<TKey, TValue> keyValuePair, out TKey key, out TValue value)
		{
			key = keyValuePair.Key;
			value = keyValuePair.Value;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000671C File Offset: 0x0000491C
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public static KeyValuePair<TKey, TValue> ToKeyValuePair<TKey, TValue>([Nullable(new byte[] { 0, 1, 1 })] this ValueTuple<TKey, TValue> tuple)
		{
			return KeyValuePairUtil.Create<TKey, TValue>(tuple.Item1, tuple.Item2);
		}
	}
}
