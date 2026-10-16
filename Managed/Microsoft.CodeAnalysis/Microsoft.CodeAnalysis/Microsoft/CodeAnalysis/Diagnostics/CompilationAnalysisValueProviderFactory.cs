using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000310 RID: 784
	internal sealed class CompilationAnalysisValueProviderFactory
	{
		// Token: 0x06002266 RID: 8806 RVA: 0x0006D86C File Offset: 0x0006BA6C
		public CompilationAnalysisValueProvider<TKey, TValue> GetValueProvider<TKey, TValue>(AnalysisValueProvider<TKey, TValue> analysisSharedStateProvider) where TKey : class
		{
			if (this._lazySharedStateProviderMap == null)
			{
				Interlocked.CompareExchange<Dictionary<object, object>>(ref this._lazySharedStateProviderMap, new Dictionary<object, object>(), null);
			}
			Dictionary<object, object> lazySharedStateProviderMap = this._lazySharedStateProviderMap;
			object obj;
			lock (lazySharedStateProviderMap)
			{
				if (!this._lazySharedStateProviderMap.TryGetValue(analysisSharedStateProvider, ref obj))
				{
					obj = new CompilationAnalysisValueProvider<TKey, TValue>(analysisSharedStateProvider);
					this._lazySharedStateProviderMap[analysisSharedStateProvider] = obj;
				}
			}
			return obj as CompilationAnalysisValueProvider<TKey, TValue>;
		}

		// Token: 0x04000EC5 RID: 3781
		private Dictionary<object, object> _lazySharedStateProviderMap;
	}
}
