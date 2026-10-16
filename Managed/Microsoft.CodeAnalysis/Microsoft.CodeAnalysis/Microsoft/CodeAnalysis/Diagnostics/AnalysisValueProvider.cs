using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x020002FF RID: 767
	[NullableContext(1)]
	[Nullable(0)]
	internal class AnalysisValueProvider<TKey, [Nullable(2)] TValue> where TKey : class
	{
		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x00066A2F File Offset: 0x00064C2F
		// (set) Token: 0x06002116 RID: 8470 RVA: 0x00066A37 File Offset: 0x00064C37
		internal IEqualityComparer<TKey> KeyComparer { get; private set; }

		// Token: 0x06002117 RID: 8471 RVA: 0x00066A40 File Offset: 0x00064C40
		public AnalysisValueProvider(Func<TKey, TValue> computeValue, IEqualityComparer<TKey> keyComparer)
		{
			this._computeValue = computeValue;
			this.KeyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
			this._valueCache = new ConditionalWeakTable<TKey, AnalysisValueProvider<TKey, TValue>.WrappedValue>();
			this._valueCacheCallback = new ConditionalWeakTable<TKey, AnalysisValueProvider<TKey, TValue>.WrappedValue>.CreateValueCallback(this.ComputeValue);
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x00066A7C File Offset: 0x00064C7C
		[return: Nullable(new byte[] { 1, 0, 0 })]
		private AnalysisValueProvider<TKey, TValue>.WrappedValue ComputeValue(TKey key)
		{
			return new AnalysisValueProvider<TKey, TValue>.WrappedValue(this._computeValue.Invoke(key));
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x00066A90 File Offset: 0x00064C90
		internal bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			bool flag;
			try
			{
				value = this._valueCache.GetValue(key, this._valueCacheCallback).Value;
				flag = true;
			}
			catch (Exception)
			{
				value = default(TValue);
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000E58 RID: 3672
		private readonly Func<TKey, TValue> _computeValue;

		// Token: 0x04000E59 RID: 3673
		[Nullable(new byte[] { 1, 1, 1, 0, 0 })]
		private readonly ConditionalWeakTable<TKey, AnalysisValueProvider<TKey, TValue>.WrappedValue> _valueCache;

		// Token: 0x04000E5A RID: 3674
		[Nullable(new byte[] { 1, 1, 1, 0, 0 })]
		private readonly ConditionalWeakTable<TKey, AnalysisValueProvider<TKey, TValue>.WrappedValue>.CreateValueCallback _valueCacheCallback;

		// Token: 0x02000692 RID: 1682
		[Nullable(0)]
		private sealed class WrappedValue
		{
			// Token: 0x060039AF RID: 14767 RVA: 0x000A063E File Offset: 0x0009E83E
			public WrappedValue(TValue value)
			{
				this.Value = value;
			}

			// Token: 0x17000FE8 RID: 4072
			// (get) Token: 0x060039B0 RID: 14768 RVA: 0x000A064D File Offset: 0x0009E84D
			public TValue Value { get; }
		}
	}
}
