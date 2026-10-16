using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x02000004 RID: 4
	internal static class Aggregate
	{
		// Token: 0x060001DE RID: 478 RVA: 0x00006CD8 File Offset: 0x00004ED8
		internal static async UniTask<TSource> AggregateAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TSource, TSource> accumulator, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			TSource tsource;
			try
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
					throw Error.NoElements();
				}
				TSource value = e.Current;
				for (;;)
				{
					awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						break;
					}
					value = accumulator(value, e.Current);
				}
				tsource = value;
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

		// Token: 0x060001DF RID: 479 RVA: 0x00006D2C File Offset: 0x00004F2C
		internal static async UniTask<TAccumulate> AggregateAsync<TSource, TAccumulate>(IUniTaskAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> accumulator, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			TAccumulate taccumulate;
			try
			{
				TAccumulate value = seed;
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
					value = accumulator(value, e.Current);
				}
				taccumulate = value;
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
			TAccumulate taccumulate2;
			if (num == 1)
			{
				taccumulate2 = taccumulate;
			}
			else
			{
				obj = null;
				taccumulate = default(TAccumulate);
				e = null;
			}
			return taccumulate2;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00006D88 File Offset: 0x00004F88
		internal static async UniTask<TResult> AggregateAsync<TSource, TAccumulate, TResult>(IUniTaskAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> accumulator, Func<TAccumulate, TResult> resultSelector, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			TResult tresult;
			try
			{
				TAccumulate value = seed;
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
					value = accumulator(value, e.Current);
				}
				tresult = resultSelector(value);
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
			TResult tresult2;
			if (num == 1)
			{
				tresult2 = tresult;
			}
			else
			{
				obj = null;
				tresult = default(TResult);
				e = null;
			}
			return tresult2;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006DEC File Offset: 0x00004FEC
		internal static async UniTask<TSource> AggregateAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TSource, UniTask<TSource>> accumulator, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			TSource tsource;
			try
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
					throw Error.NoElements();
				}
				TSource value = e.Current;
				for (;;)
				{
					awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						break;
					}
					value = await accumulator(value, e.Current);
				}
				tsource = value;
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

		// Token: 0x060001E2 RID: 482 RVA: 0x00006E40 File Offset: 0x00005040
		internal static async UniTask<TAccumulate> AggregateAwaitAsync<TSource, TAccumulate>(IUniTaskAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, UniTask<TAccumulate>> accumulator, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			TAccumulate taccumulate2;
			try
			{
				TAccumulate value = seed;
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
					TAccumulate taccumulate = await accumulator(value, e.Current);
					value = taccumulate;
				}
				taccumulate2 = value;
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
			TAccumulate taccumulate3;
			if (num == 1)
			{
				taccumulate3 = taccumulate2;
			}
			else
			{
				obj = null;
				taccumulate2 = default(TAccumulate);
				e = null;
			}
			return taccumulate3;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006E9C File Offset: 0x0000509C
		internal static async UniTask<TResult> AggregateAwaitAsync<TSource, TAccumulate, TResult>(IUniTaskAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, UniTask<TAccumulate>> accumulator, Func<TAccumulate, UniTask<TResult>> resultSelector, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			TResult tresult;
			try
			{
				TAccumulate value = seed;
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
					TAccumulate taccumulate = await accumulator(value, e.Current);
					value = taccumulate;
				}
				tresult = await resultSelector(value);
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
			TResult tresult2;
			if (num == 1)
			{
				tresult2 = tresult;
			}
			else
			{
				obj = null;
				tresult = default(TResult);
				e = null;
			}
			return tresult2;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006F00 File Offset: 0x00005100
		internal static async UniTask<TSource> AggregateAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TSource, CancellationToken, UniTask<TSource>> accumulator, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			TSource tsource;
			try
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
					throw Error.NoElements();
				}
				TSource value = e.Current;
				for (;;)
				{
					awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						break;
					}
					value = await accumulator(value, e.Current, cancellationToken);
				}
				tsource = value;
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

		// Token: 0x060001E5 RID: 485 RVA: 0x00006F54 File Offset: 0x00005154
		internal static async UniTask<TAccumulate> AggregateAwaitWithCancellationAsync<TSource, TAccumulate>(IUniTaskAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, CancellationToken, UniTask<TAccumulate>> accumulator, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			TAccumulate taccumulate2;
			try
			{
				TAccumulate value = seed;
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
					TAccumulate taccumulate = await accumulator(value, e.Current, cancellationToken);
					value = taccumulate;
				}
				taccumulate2 = value;
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
			TAccumulate taccumulate3;
			if (num == 1)
			{
				taccumulate3 = taccumulate2;
			}
			else
			{
				obj = null;
				taccumulate2 = default(TAccumulate);
				e = null;
			}
			return taccumulate3;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006FB0 File Offset: 0x000051B0
		internal static async UniTask<TResult> AggregateAwaitWithCancellationAsync<TSource, TAccumulate, TResult>(IUniTaskAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, CancellationToken, UniTask<TAccumulate>> accumulator, Func<TAccumulate, CancellationToken, UniTask<TResult>> resultSelector, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			TResult tresult;
			try
			{
				TAccumulate value = seed;
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
					TAccumulate taccumulate = await accumulator(value, e.Current, cancellationToken);
					value = taccumulate;
				}
				tresult = await resultSelector(value, cancellationToken);
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
			TResult tresult2;
			if (num == 1)
			{
				tresult2 = tresult;
			}
			else
			{
				obj = null;
				tresult = default(TResult);
				e = null;
			}
			return tresult2;
		}
	}
}
