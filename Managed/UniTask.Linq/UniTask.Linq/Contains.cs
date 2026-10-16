using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x0200001D RID: 29
	internal static class Contains
	{
		// Token: 0x06000259 RID: 601 RVA: 0x00008E54 File Offset: 0x00007054
		internal static async UniTask<bool> ContainsAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer, CancellationToken cancellationToken)
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
				while (!comparer.Equals(value, e.Current));
				flag = true;
				goto IL_00D2;
				Block_9:
				flag = false;
				IL_00D2:
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
