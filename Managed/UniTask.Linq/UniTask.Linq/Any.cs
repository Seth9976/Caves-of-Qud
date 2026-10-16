using System;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x02000006 RID: 6
	internal static class Any
	{
		// Token: 0x060001EA RID: 490 RVA: 0x00007110 File Offset: 0x00005310
		internal static async UniTask<bool> AnyAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			bool result;
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
				result = awaiter.GetResult();
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
			bool flag;
			if (num == 1)
			{
				flag = result;
			}
			else
			{
				obj = null;
				e = null;
			}
			return flag;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000715C File Offset: 0x0000535C
		internal static async UniTask<bool> AnyAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
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
				while (!predicate(e.Current));
				flag = true;
				goto IL_00CC;
				Block_9:
				flag = false;
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

		// Token: 0x060001EC RID: 492 RVA: 0x000071B0 File Offset: 0x000053B0
		internal static async UniTask<bool> AnyAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<bool>> predicate, CancellationToken cancellationToken)
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
				while (!awaiter.GetResult());
				flag = true;
				goto IL_012C;
				Block_10:
				flag = false;
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

		// Token: 0x060001ED RID: 493 RVA: 0x00007204 File Offset: 0x00005404
		internal static async UniTask<bool> AnyAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<bool>> predicate, CancellationToken cancellationToken)
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
				while (!awaiter.GetResult());
				flag = true;
				goto IL_0132;
				Block_10:
				flag = false;
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
