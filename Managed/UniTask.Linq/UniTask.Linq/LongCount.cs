using System;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x0200003E RID: 62
	internal static class LongCount
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x00009BE0 File Offset: 0x00007DE0
		internal static async UniTask<long> LongCountAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			long count = 0L;
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
					long num = count;
					count = checked(num + 1L);
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
			return count;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00009C2C File Offset: 0x00007E2C
		internal static async UniTask<long> LongCountAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			long count = 0L;
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
					if (predicate(e.Current))
					{
						long num = count;
						count = checked(num + 1L);
					}
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
			return count;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00009C80 File Offset: 0x00007E80
		internal static async UniTask<long> LongCountAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<bool>> predicate, CancellationToken cancellationToken)
		{
			long count = 0L;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
				try
				{
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
							break;
						}
						awaiter = predicate(e.Current).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							await awaiter;
							awaiter = awaiter2;
							awaiter2 = default(UniTask<bool>.Awaiter);
						}
						if (awaiter.GetResult())
						{
							count += 1L;
						}
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
				return count;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00009CD4 File Offset: 0x00007ED4
		internal static async UniTask<long> LongCountAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<bool>> predicate, CancellationToken cancellationToken)
		{
			long count = 0L;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
				try
				{
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
							break;
						}
						awaiter = predicate(e.Current, cancellationToken).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							await awaiter;
							awaiter = awaiter2;
							awaiter2 = default(UniTask<bool>.Awaiter);
						}
						if (awaiter.GetResult())
						{
							count += 1L;
						}
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
				return count;
			}
		}
	}
}
