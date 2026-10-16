using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x0200002E RID: 46
	internal static class First
	{
		// Token: 0x0600027B RID: 635 RVA: 0x000092D4 File Offset: 0x000074D4
		public static async UniTask<TSource> FirstAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken, bool defaultIfEmpty)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			TSource tsource;
			try
			{
				UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					await awaiter;
					UniTask<bool>.Awaiter awaiter2;
					awaiter = awaiter2;
					awaiter2 = default(UniTask<bool>.Awaiter);
				}
				if (awaiter.GetResult())
				{
					tsource = e.Current;
				}
				else
				{
					if (!defaultIfEmpty)
					{
						throw Error.NoElements();
					}
					tsource = default(TSource);
				}
				num = 1;
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
			TSource tsource2;
			if (num == 1)
			{
				tsource2 = tsource;
			}
			else
			{
				obj = null;
				tsource = default(TSource);
				e = null;
			}
			return tsource2;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00009328 File Offset: 0x00007528
		public static async UniTask<TSource> FirstAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken, bool defaultIfEmpty)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			TSource tsource2;
			try
			{
				TSource tsource;
				do
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
						goto Block_9;
					}
					tsource = e.Current;
				}
				while (!predicate(tsource));
				tsource2 = tsource;
				goto IL_00E7;
				Block_9:
				if (!defaultIfEmpty)
				{
					throw Error.NoElements();
				}
				tsource2 = default(TSource);
				IL_00E7:
				num = 1;
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
			TSource tsource3;
			if (num == 1)
			{
				tsource3 = tsource2;
			}
			else
			{
				obj = null;
				tsource2 = default(TSource);
				e = null;
			}
			return tsource3;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009384 File Offset: 0x00007584
		public static async UniTask<TSource> FirstAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<bool>> predicate, CancellationToken cancellationToken, bool defaultIfEmpty)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			TSource tsource;
			try
			{
				TSource v;
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					UniTask<bool>.Awaiter awaiter2;
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_10;
					}
					v = e.Current;
					awaiter = predicate(v).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (awaiter.GetResult())
					{
						break;
					}
					v = default(TSource);
				}
				tsource = v;
				goto IL_0164;
				Block_10:
				if (!defaultIfEmpty)
				{
					throw Error.NoElements();
				}
				tsource = default(TSource);
				IL_0164:
				num = 1;
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
			TSource tsource2;
			if (num == 1)
			{
				tsource2 = tsource;
			}
			else
			{
				obj = null;
				tsource = default(TSource);
				e = null;
			}
			return tsource2;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000093E0 File Offset: 0x000075E0
		public static async UniTask<TSource> FirstAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<bool>> predicate, CancellationToken cancellationToken, bool defaultIfEmpty)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			TSource tsource;
			try
			{
				TSource v;
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					UniTask<bool>.Awaiter awaiter2;
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_10;
					}
					v = e.Current;
					awaiter = predicate(v, cancellationToken).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (awaiter.GetResult())
					{
						break;
					}
					v = default(TSource);
				}
				tsource = v;
				goto IL_016A;
				Block_10:
				if (!defaultIfEmpty)
				{
					throw Error.NoElements();
				}
				tsource = default(TSource);
				IL_016A:
				num = 1;
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
			TSource tsource2;
			if (num == 1)
			{
				tsource2 = tsource;
			}
			else
			{
				obj = null;
				tsource = default(TSource);
				e = null;
			}
			return tsource2;
		}
	}
}
