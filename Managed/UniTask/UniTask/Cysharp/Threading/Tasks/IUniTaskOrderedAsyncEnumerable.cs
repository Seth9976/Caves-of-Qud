using System;
using System.Collections.Generic;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200001D RID: 29
	public interface IUniTaskOrderedAsyncEnumerable<TElement> : IUniTaskAsyncEnumerable<TElement>
	{
		// Token: 0x06000077 RID: 119
		IUniTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending);

		// Token: 0x06000078 RID: 120
		IUniTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, UniTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending);

		// Token: 0x06000079 RID: 121
		IUniTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending);
	}
}
