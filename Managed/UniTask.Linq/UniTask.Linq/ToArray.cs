using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x02000076 RID: 118
	internal static class ToArray
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x0000D77C File Offset: 0x0000B97C
		internal static async UniTask<TSource[]> ToArrayAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			ArrayPool<TSource> pool = ArrayPool<TSource>.Shared;
			TSource[] array = pool.Rent(16);
			TSource[] result = null;
			IUniTaskAsyncEnumerator<TSource> e = null;
			object obj = null;
			try
			{
				e = source.GetAsyncEnumerator(cancellationToken);
				int i = 0;
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
					ArrayPoolUtil.EnsureCapacity<TSource>(ref array, i, pool);
					TSource[] array2 = array;
					int num = i;
					i = num + 1;
					array2[num] = e.Current;
				}
				if (i == 0)
				{
					result = Array.Empty<TSource>();
				}
				else
				{
					result = new TSource[i];
					Array.Copy(array, result, i);
				}
			}
			catch (object obj)
			{
			}
			pool.Return(array, !RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<TSource>());
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
			return result;
		}
	}
}
