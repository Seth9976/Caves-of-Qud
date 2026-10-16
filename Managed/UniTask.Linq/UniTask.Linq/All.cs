using System;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x02000005 RID: 5
	internal static class All
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x00007014 File Offset: 0x00005214
		internal static async UniTask<bool> AllAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			bool flag;
			try
			{
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
				}
				while (predicate(e.Current));
				flag = false;
				goto IL_00CC;
				Block_9:
				flag = true;
				IL_00CC:
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
			bool flag2;
			if (num == 1)
			{
				flag2 = flag;
			}
			else
			{
				obj = null;
				e = null;
			}
			return flag2;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007068 File Offset: 0x00005268
		internal static async UniTask<bool> AllAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<bool>> predicate, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			bool flag;
			try
			{
				UniTask<bool>.Awaiter awaiter;
				do
				{
					awaiter = e.MoveNextAsync().GetAwaiter();
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
					awaiter = predicate(e.Current).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
				}
				while (awaiter.GetResult());
				flag = false;
				goto IL_012C;
				Block_10:
				flag = true;
				IL_012C:
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
			bool flag2;
			if (num == 1)
			{
				flag2 = flag;
			}
			else
			{
				obj = null;
				e = null;
			}
			return flag2;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000070BC File Offset: 0x000052BC
		internal static async UniTask<bool> AllAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<bool>> predicate, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			bool flag;
			try
			{
				UniTask<bool>.Awaiter awaiter;
				do
				{
					awaiter = e.MoveNextAsync().GetAwaiter();
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
					awaiter = predicate(e.Current, cancellationToken).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
				}
				while (awaiter.GetResult());
				flag = false;
				goto IL_0132;
				Block_10:
				flag = true;
				IL_0132:
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
			bool flag2;
			if (num == 1)
			{
				flag2 = flag;
			}
			else
			{
				obj = null;
				e = null;
			}
			return flag2;
		}
	}
}
