using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x0200005D RID: 93
	internal static class SingleOperator
	{
		// Token: 0x06000346 RID: 838 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
		public static async UniTask<TSource> SingleAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken, bool defaultIfEmpty)
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
				if (awaiter.GetResult())
				{
					TSource v = e.Current;
					awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (awaiter.GetResult())
					{
						throw Error.MoreThanOneElement();
					}
					tsource = v;
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

		// Token: 0x06000347 RID: 839 RVA: 0x0000C334 File Offset: 0x0000A534
		public static async UniTask<TSource> SingleAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken, bool defaultIfEmpty)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			TSource tsource2;
			try
			{
				TSource value = default(TSource);
				bool found = false;
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
						goto Block_10;
					}
					TSource tsource = e.Current;
					if (predicate(tsource))
					{
						if (found)
						{
							break;
						}
						found = true;
						value = tsource;
					}
				}
				throw Error.MoreThanOneElement();
				Block_10:
				if (!found && !defaultIfEmpty)
				{
					throw Error.NoElements();
				}
				tsource2 = value;
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

		// Token: 0x06000348 RID: 840 RVA: 0x0000C390 File Offset: 0x0000A590
		public static async UniTask<TSource> SingleAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<bool>> predicate, CancellationToken cancellationToken, bool defaultIfEmpty)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			TSource tsource;
			try
			{
				TSource value = default(TSource);
				bool found = false;
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
						goto Block_11;
					}
					TSource v = e.Current;
					awaiter = predicate(v).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (awaiter.GetResult())
					{
						if (found)
						{
							break;
						}
						found = true;
						value = v;
					}
					v = default(TSource);
				}
				throw Error.MoreThanOneElement();
				Block_11:
				if (!found && !defaultIfEmpty)
				{
					throw Error.NoElements();
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

		// Token: 0x06000349 RID: 841 RVA: 0x0000C3EC File Offset: 0x0000A5EC
		public static async UniTask<TSource> SingleAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<bool>> predicate, CancellationToken cancellationToken, bool defaultIfEmpty)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			TSource tsource;
			try
			{
				TSource value = default(TSource);
				bool found = false;
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
						goto Block_11;
					}
					TSource v = e.Current;
					awaiter = predicate(v, cancellationToken).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (awaiter.GetResult())
					{
						if (found)
						{
							break;
						}
						found = true;
						value = v;
					}
					v = default(TSource);
				}
				throw Error.MoreThanOneElement();
				Block_11:
				if (!found && !defaultIfEmpty)
				{
					throw Error.NoElements();
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
	}
}
