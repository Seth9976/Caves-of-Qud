using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x0200003D RID: 61
	internal static class Last
	{
		// Token: 0x0600029F RID: 671 RVA: 0x00009A78 File Offset: 0x00007C78
		public static async UniTask<TSource> LastAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken, bool defaultIfEmpty)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			TSource tsource;
			try
			{
				TSource value = default(TSource);
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
					value = e.Current;
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
						value = e.Current;
					}
					tsource = value;
				}
				else
				{
					if (!defaultIfEmpty)
					{
						throw Error.NoElements();
					}
					tsource = value;
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

		// Token: 0x060002A0 RID: 672 RVA: 0x00009ACC File Offset: 0x00007CCC
		public static async UniTask<TSource> LastAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken, bool defaultIfEmpty)
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
						break;
					}
					TSource tsource = e.Current;
					if (predicate(tsource))
					{
						found = true;
						value = tsource;
					}
				}
				if (defaultIfEmpty)
				{
					tsource2 = value;
				}
				else
				{
					if (!found)
					{
						throw Error.NoElements();
					}
					tsource2 = value;
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

		// Token: 0x060002A1 RID: 673 RVA: 0x00009B28 File Offset: 0x00007D28
		public static async UniTask<TSource> LastAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<bool>> predicate, CancellationToken cancellationToken, bool defaultIfEmpty)
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
						break;
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
						found = true;
						value = v;
					}
					v = default(TSource);
				}
				if (defaultIfEmpty)
				{
					tsource = value;
				}
				else
				{
					if (!found)
					{
						throw Error.NoElements();
					}
					tsource = value;
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

		// Token: 0x060002A2 RID: 674 RVA: 0x00009B84 File Offset: 0x00007D84
		public static async UniTask<TSource> LastAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<bool>> predicate, CancellationToken cancellationToken, bool defaultIfEmpty)
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
						break;
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
						found = true;
						value = v;
					}
					v = default(TSource);
				}
				if (defaultIfEmpty)
				{
					tsource = value;
				}
				else
				{
					if (!found)
					{
						throw Error.NoElements();
					}
					tsource = value;
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
	}
}
