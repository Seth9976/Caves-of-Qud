using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x02000077 RID: 119
	internal static class ToDictionary
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x0000D7C8 File Offset: 0x0000B9C8
		internal static async UniTask<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			Dictionary<TKey, TSource> dict = new Dictionary<TKey, TSource>(comparer);
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			try
			{
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						UniTask<bool>.Awaiter awaiter2;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						break;
					}
					TSource tsource = e.Current;
					TKey tkey = keySelector(tsource);
					dict.Add(tkey, tsource);
				}
			}
			catch (object obj)
			{
			}
			if (e != null)
			{
				await e.DisposeAsync();
			}
			object obj2 = obj;
			if (obj2 != null)
			{
				Exception ex = obj2 as Exception;
				if (ex == null)
				{
					throw obj2;
				}
				ExceptionDispatchInfo.Capture(ex).Throw();
			}
			obj = null;
			return dict;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000D824 File Offset: 0x0000BA24
		internal static async UniTask<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			Dictionary<TKey, TElement> dict = new Dictionary<TKey, TElement>(comparer);
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			try
			{
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						UniTask<bool>.Awaiter awaiter2;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						break;
					}
					TSource tsource = e.Current;
					TKey tkey = keySelector(tsource);
					TElement telement = elementSelector(tsource);
					dict.Add(tkey, telement);
				}
			}
			catch (object obj)
			{
			}
			if (e != null)
			{
				await e.DisposeAsync();
			}
			object obj2 = obj;
			if (obj2 != null)
			{
				Exception ex = obj2 as Exception;
				if (ex == null)
				{
					throw obj2;
				}
				ExceptionDispatchInfo.Capture(ex).Throw();
			}
			obj = null;
			return dict;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000D888 File Offset: 0x0000BA88
		internal static async UniTask<Dictionary<TKey, TSource>> ToDictionaryAwaitAsync<TSource, TKey>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			Dictionary<TKey, TSource> dict = new Dictionary<TKey, TSource>(comparer);
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			try
			{
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						UniTask<bool>.Awaiter awaiter2;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						break;
					}
					TSource v = e.Current;
					TKey tkey = await keySelector(v);
					dict.Add(tkey, v);
					v = default(TSource);
				}
			}
			catch (object obj)
			{
			}
			if (e != null)
			{
				await e.DisposeAsync();
			}
			object obj2 = obj;
			if (obj2 != null)
			{
				Exception ex = obj2 as Exception;
				if (ex == null)
				{
					throw obj2;
				}
				ExceptionDispatchInfo.Capture(ex).Throw();
			}
			obj = null;
			return dict;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000D8E4 File Offset: 0x0000BAE4
		internal static async UniTask<Dictionary<TKey, TElement>> ToDictionaryAwaitAsync<TSource, TKey, TElement>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, Func<TSource, UniTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			Dictionary<TKey, TElement> dict = new Dictionary<TKey, TElement>(comparer);
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			try
			{
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						UniTask<bool>.Awaiter awaiter2;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						break;
					}
					TSource v = e.Current;
					TKey tkey = await keySelector(v);
					TKey key = tkey;
					TElement telement = await elementSelector(v);
					dict.Add(key, telement);
					v = default(TSource);
					key = default(TKey);
				}
			}
			catch (object obj)
			{
			}
			if (e != null)
			{
				await e.DisposeAsync();
			}
			object obj2 = obj;
			if (obj2 != null)
			{
				Exception ex = obj2 as Exception;
				if (ex == null)
				{
					throw obj2;
				}
				ExceptionDispatchInfo.Capture(ex).Throw();
			}
			obj = null;
			return dict;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000D948 File Offset: 0x0000BB48
		internal static async UniTask<Dictionary<TKey, TSource>> ToDictionaryAwaitWithCancellationAsync<TSource, TKey>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			Dictionary<TKey, TSource> dict = new Dictionary<TKey, TSource>(comparer);
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			try
			{
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						UniTask<bool>.Awaiter awaiter2;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						break;
					}
					TSource v = e.Current;
					TKey tkey = await keySelector(v, cancellationToken);
					dict.Add(tkey, v);
					v = default(TSource);
				}
			}
			catch (object obj)
			{
			}
			if (e != null)
			{
				await e.DisposeAsync();
			}
			object obj2 = obj;
			if (obj2 != null)
			{
				Exception ex = obj2 as Exception;
				if (ex == null)
				{
					throw obj2;
				}
				ExceptionDispatchInfo.Capture(ex).Throw();
			}
			obj = null;
			return dict;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000D9A4 File Offset: 0x0000BBA4
		internal static async UniTask<Dictionary<TKey, TElement>> ToDictionaryAwaitWithCancellationAsync<TSource, TKey, TElement>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, Func<TSource, CancellationToken, UniTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			Dictionary<TKey, TElement> dict = new Dictionary<TKey, TElement>(comparer);
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			try
			{
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						UniTask<bool>.Awaiter awaiter2;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						break;
					}
					TSource v = e.Current;
					TKey tkey = await keySelector(v, cancellationToken);
					TKey key = tkey;
					TElement telement = await elementSelector(v, cancellationToken);
					dict.Add(key, telement);
					v = default(TSource);
					key = default(TKey);
				}
			}
			catch (object obj)
			{
			}
			if (e != null)
			{
				await e.DisposeAsync();
			}
			object obj2 = obj;
			if (obj2 != null)
			{
				Exception ex = obj2 as Exception;
				if (ex == null)
				{
					throw obj2;
				}
				ExceptionDispatchInfo.Capture(ex).Throw();
			}
			obj = null;
			return dict;
		}
	}
}
