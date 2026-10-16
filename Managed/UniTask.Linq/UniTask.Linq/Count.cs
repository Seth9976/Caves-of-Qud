using System;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x0200001E RID: 30
	internal static class Count
	{
		// Token: 0x0600025A RID: 602 RVA: 0x00008EB0 File Offset: 0x000070B0
		internal static async UniTask<int> CountAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			int count = 0;
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
					int num = count;
					count = checked(num + 1);
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

		// Token: 0x0600025B RID: 603 RVA: 0x00008EFC File Offset: 0x000070FC
		internal static async UniTask<int> CountAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			int count = 0;
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
						int num = count;
						count = checked(num + 1);
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

		// Token: 0x0600025C RID: 604 RVA: 0x00008F50 File Offset: 0x00007150
		internal static async UniTask<int> CountAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<bool>> predicate, CancellationToken cancellationToken)
		{
			int count = 0;
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
							count++;
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

		// Token: 0x0600025D RID: 605 RVA: 0x00008FA4 File Offset: 0x000071A4
		internal static async UniTask<int> CountAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<bool>> predicate, CancellationToken cancellationToken)
		{
			int count = 0;
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
							count++;
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
