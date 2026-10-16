using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Collections
{
	// Token: 0x020004A4 RID: 1188
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RoslynImmutableInterlocked
	{
		// Token: 0x06002F22 RID: 12066 RVA: 0x00084740 File Offset: 0x00082940
		public static bool Update<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 0, 1, 1 })] ref ImmutableSegmentedDictionary<TKey, TValue> location, [Nullable(new byte[] { 1, 0, 1, 1, 0, 1, 1 })] Func<ImmutableSegmentedDictionary<TKey, TValue>, ImmutableSegmentedDictionary<TKey, TValue>> transformer)
		{
			if (transformer == null)
			{
				throw new ArgumentNullException("transformer");
			}
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = ImmutableSegmentedDictionary<TKey, TValue>.PrivateInterlocked.VolatileRead(in location);
			for (;;)
			{
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary2 = transformer.Invoke(immutableSegmentedDictionary);
				if (immutableSegmentedDictionary == immutableSegmentedDictionary2)
				{
					break;
				}
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary3 = RoslynImmutableInterlocked.InterlockedCompareExchange<TKey, TValue>(ref location, immutableSegmentedDictionary2, immutableSegmentedDictionary);
				if (immutableSegmentedDictionary == immutableSegmentedDictionary3)
				{
					return true;
				}
				immutableSegmentedDictionary = immutableSegmentedDictionary3;
			}
			return false;
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x0008478C File Offset: 0x0008298C
		public static bool Update<TKey, [Nullable(2)] TValue, [Nullable(2)] TArg>([Nullable(new byte[] { 0, 1, 1 })] ref ImmutableSegmentedDictionary<TKey, TValue> location, [Nullable(new byte[] { 1, 0, 1, 1, 1, 0, 1, 1 })] Func<ImmutableSegmentedDictionary<TKey, TValue>, TArg, ImmutableSegmentedDictionary<TKey, TValue>> transformer, TArg transformerArgument)
		{
			if (transformer == null)
			{
				throw new ArgumentNullException("transformer");
			}
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = ImmutableSegmentedDictionary<TKey, TValue>.PrivateInterlocked.VolatileRead(in location);
			for (;;)
			{
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary2 = transformer.Invoke(immutableSegmentedDictionary, transformerArgument);
				if (immutableSegmentedDictionary == immutableSegmentedDictionary2)
				{
					break;
				}
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary3 = RoslynImmutableInterlocked.InterlockedCompareExchange<TKey, TValue>(ref location, immutableSegmentedDictionary2, immutableSegmentedDictionary);
				if (immutableSegmentedDictionary == immutableSegmentedDictionary3)
				{
					return true;
				}
				immutableSegmentedDictionary = immutableSegmentedDictionary3;
			}
			return false;
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x000847D9 File Offset: 0x000829D9
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public static ImmutableSegmentedDictionary<TKey, TValue> InterlockedExchange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 0, 1, 1 })] ref ImmutableSegmentedDictionary<TKey, TValue> location, [Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue> value)
		{
			return ImmutableSegmentedDictionary<TKey, TValue>.PrivateInterlocked.InterlockedExchange(ref location, value);
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x000847E2 File Offset: 0x000829E2
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public static ImmutableSegmentedDictionary<TKey, TValue> InterlockedCompareExchange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 0, 1, 1 })] ref ImmutableSegmentedDictionary<TKey, TValue> location, [Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue> value, [Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue> comparand)
		{
			return ImmutableSegmentedDictionary<TKey, TValue>.PrivateInterlocked.InterlockedCompareExchange(ref location, value, comparand);
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x000847EC File Offset: 0x000829EC
		public static bool InterlockedInitialize<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 0, 1, 1 })] ref ImmutableSegmentedDictionary<TKey, TValue> location, [Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue> value)
		{
			return RoslynImmutableInterlocked.InterlockedCompareExchange<TKey, TValue>(ref location, value, default(ImmutableSegmentedDictionary<TKey, TValue>)).IsDefault;
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x00084814 File Offset: 0x00082A14
		public static TValue GetOrAdd<TKey, [Nullable(2)] TValue, [Nullable(2)] TArg>([Nullable(new byte[] { 0, 1, 1 })] ref ImmutableSegmentedDictionary<TKey, TValue> location, TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = ImmutableSegmentedDictionary<TKey, TValue>.PrivateInterlocked.VolatileRead(in location);
			if (immutableSegmentedDictionary.IsDefault)
			{
				throw new ArgumentNullException("location");
			}
			TValue tvalue;
			if (immutableSegmentedDictionary.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			tvalue = valueFactory.Invoke(key, factoryArgument);
			return RoslynImmutableInterlocked.GetOrAdd<TKey, TValue>(ref location, key, tvalue);
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x0008486C File Offset: 0x00082A6C
		public static TValue GetOrAdd<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 0, 1, 1 })] ref ImmutableSegmentedDictionary<TKey, TValue> location, TKey key, Func<TKey, TValue> valueFactory)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = ImmutableSegmentedDictionary<TKey, TValue>.PrivateInterlocked.VolatileRead(in location);
			if (immutableSegmentedDictionary.IsDefault)
			{
				throw new ArgumentNullException("location");
			}
			TValue tvalue;
			if (immutableSegmentedDictionary.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			tvalue = valueFactory.Invoke(key);
			return RoslynImmutableInterlocked.GetOrAdd<TKey, TValue>(ref location, key, tvalue);
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x000848C0 File Offset: 0x00082AC0
		public static TValue GetOrAdd<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 0, 1, 1 })] ref ImmutableSegmentedDictionary<TKey, TValue> location, TKey key, TValue value)
		{
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = ImmutableSegmentedDictionary<TKey, TValue>.PrivateInterlocked.VolatileRead(in location);
			while (!immutableSegmentedDictionary.IsDefault)
			{
				TValue tvalue;
				if (immutableSegmentedDictionary.TryGetValue(key, out tvalue))
				{
					return tvalue;
				}
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary2 = immutableSegmentedDictionary.Add(key, value);
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary3 = RoslynImmutableInterlocked.InterlockedCompareExchange<TKey, TValue>(ref location, immutableSegmentedDictionary2, immutableSegmentedDictionary);
				if (immutableSegmentedDictionary == immutableSegmentedDictionary3)
				{
					return value;
				}
				immutableSegmentedDictionary = immutableSegmentedDictionary3;
			}
			throw new ArgumentNullException("location");
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x00084918 File Offset: 0x00082B18
		public static TValue AddOrUpdate<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 0, 1, 1 })] ref ImmutableSegmentedDictionary<TKey, TValue> location, TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
		{
			if (addValueFactory == null)
			{
				throw new ArgumentNullException("addValueFactory");
			}
			if (updateValueFactory == null)
			{
				throw new ArgumentNullException("updateValueFactory");
			}
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = ImmutableSegmentedDictionary<TKey, TValue>.PrivateInterlocked.VolatileRead(in location);
			while (!immutableSegmentedDictionary.IsDefault)
			{
				TValue tvalue;
				TValue tvalue2;
				if (immutableSegmentedDictionary.TryGetValue(key, out tvalue))
				{
					tvalue2 = updateValueFactory.Invoke(key, tvalue);
				}
				else
				{
					tvalue2 = addValueFactory.Invoke(key);
				}
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary2 = immutableSegmentedDictionary.SetItem(key, tvalue2);
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary3 = RoslynImmutableInterlocked.InterlockedCompareExchange<TKey, TValue>(ref location, immutableSegmentedDictionary2, immutableSegmentedDictionary);
				if (immutableSegmentedDictionary == immutableSegmentedDictionary3)
				{
					return tvalue2;
				}
				immutableSegmentedDictionary = immutableSegmentedDictionary3;
			}
			throw new ArgumentNullException("location");
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x000849A0 File Offset: 0x00082BA0
		public static TValue AddOrUpdate<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 0, 1, 1 })] ref ImmutableSegmentedDictionary<TKey, TValue> location, TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
		{
			if (updateValueFactory == null)
			{
				throw new ArgumentNullException("updateValueFactory");
			}
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = ImmutableSegmentedDictionary<TKey, TValue>.PrivateInterlocked.VolatileRead(in location);
			while (!immutableSegmentedDictionary.IsDefault)
			{
				TValue tvalue;
				TValue tvalue2;
				if (immutableSegmentedDictionary.TryGetValue(key, out tvalue))
				{
					tvalue2 = updateValueFactory.Invoke(key, tvalue);
				}
				else
				{
					tvalue2 = addValue;
				}
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary2 = immutableSegmentedDictionary.SetItem(key, tvalue2);
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary3 = RoslynImmutableInterlocked.InterlockedCompareExchange<TKey, TValue>(ref location, immutableSegmentedDictionary2, immutableSegmentedDictionary);
				if (immutableSegmentedDictionary == immutableSegmentedDictionary3)
				{
					return tvalue2;
				}
				immutableSegmentedDictionary = immutableSegmentedDictionary3;
			}
			throw new ArgumentNullException("location");
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x00084A14 File Offset: 0x00082C14
		public static bool TryAdd<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 0, 1, 1 })] ref ImmutableSegmentedDictionary<TKey, TValue> location, TKey key, TValue value)
		{
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = ImmutableSegmentedDictionary<TKey, TValue>.PrivateInterlocked.VolatileRead(in location);
			while (!immutableSegmentedDictionary.IsDefault)
			{
				if (immutableSegmentedDictionary.ContainsKey(key))
				{
					return false;
				}
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary2 = immutableSegmentedDictionary.Add(key, value);
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary3 = RoslynImmutableInterlocked.InterlockedCompareExchange<TKey, TValue>(ref location, immutableSegmentedDictionary2, immutableSegmentedDictionary);
				if (immutableSegmentedDictionary == immutableSegmentedDictionary3)
				{
					return true;
				}
				immutableSegmentedDictionary = immutableSegmentedDictionary3;
			}
			throw new ArgumentNullException("location");
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x00084A6C File Offset: 0x00082C6C
		public static bool TryUpdate<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 0, 1, 1 })] ref ImmutableSegmentedDictionary<TKey, TValue> location, TKey key, TValue newValue, TValue comparisonValue)
		{
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = ImmutableSegmentedDictionary<TKey, TValue>.PrivateInterlocked.VolatileRead(in location);
			while (!immutableSegmentedDictionary.IsDefault)
			{
				TValue tvalue;
				if (!immutableSegmentedDictionary.TryGetValue(key, out tvalue) || !@default.Equals(tvalue, comparisonValue))
				{
					return false;
				}
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary2 = immutableSegmentedDictionary.SetItem(key, newValue);
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary3 = RoslynImmutableInterlocked.InterlockedCompareExchange<TKey, TValue>(ref location, immutableSegmentedDictionary2, immutableSegmentedDictionary);
				if (immutableSegmentedDictionary == immutableSegmentedDictionary3)
				{
					return true;
				}
				immutableSegmentedDictionary = immutableSegmentedDictionary3;
			}
			throw new ArgumentNullException("location");
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x00084AD8 File Offset: 0x00082CD8
		public static bool TryRemove<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 0, 1, 1 })] ref ImmutableSegmentedDictionary<TKey, TValue> location, TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = ImmutableSegmentedDictionary<TKey, TValue>.PrivateInterlocked.VolatileRead(in location);
			while (!immutableSegmentedDictionary.IsDefault)
			{
				if (!immutableSegmentedDictionary.TryGetValue(key, out value))
				{
					return false;
				}
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary2 = immutableSegmentedDictionary.Remove(key);
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary3 = RoslynImmutableInterlocked.InterlockedCompareExchange<TKey, TValue>(ref location, immutableSegmentedDictionary2, immutableSegmentedDictionary);
				if (immutableSegmentedDictionary == immutableSegmentedDictionary3)
				{
					return true;
				}
				immutableSegmentedDictionary = immutableSegmentedDictionary3;
			}
			throw new ArgumentNullException("location");
		}
	}
}
