using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200030F RID: 783
	internal sealed class CompilationAnalysisValueProvider<TKey, TValue> where TKey : class
	{
		// Token: 0x06002264 RID: 8804 RVA: 0x0006D78E File Offset: 0x0006B98E
		public CompilationAnalysisValueProvider(AnalysisValueProvider<TKey, TValue> analysisValueProvider)
		{
			this._analysisValueProvider = analysisValueProvider;
			this._valueMap = new Dictionary<TKey, TValue>(analysisValueProvider.KeyComparer);
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x0006D7B0 File Offset: 0x0006B9B0
		internal bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			Dictionary<TKey, TValue> dictionary = this._valueMap;
			lock (dictionary)
			{
				if (this._valueMap.TryGetValue(key, ref value))
				{
					return true;
				}
			}
			if (!this._analysisValueProvider.TryGetValue(key, out value))
			{
				value = default(TValue);
				return false;
			}
			dictionary = this._valueMap;
			lock (dictionary)
			{
				TValue tvalue;
				if (this._valueMap.TryGetValue(key, ref tvalue))
				{
					value = tvalue;
				}
				else
				{
					this._valueMap.Add(key, value);
				}
			}
			return true;
		}

		// Token: 0x04000EC3 RID: 3779
		private readonly AnalysisValueProvider<TKey, TValue> _analysisValueProvider;

		// Token: 0x04000EC4 RID: 3780
		private readonly Dictionary<TKey, TValue> _valueMap;
	}
}
