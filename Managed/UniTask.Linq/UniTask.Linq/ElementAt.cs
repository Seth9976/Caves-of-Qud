using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x0200002B RID: 43
	internal static class ElementAt
	{
		// Token: 0x06000275 RID: 629 RVA: 0x00009224 File Offset: 0x00007424
		public static async UniTask<TSource> ElementAtAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, int index, CancellationToken cancellationToken, bool defaultIfEmpty)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			int num2;
			TSource tsource;
			try
			{
				int i = 0;
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
					num2 = i;
					i = num2 + 1;
				}
				while (num2 != index);
				tsource = e.Current;
				goto IL_00FC;
				Block_9:
				if (!defaultIfEmpty)
				{
					throw Error.ArgumentOutOfRange("index");
				}
				tsource = default(TSource);
				IL_00FC:
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
			num2 = num;
			TSource tsource2;
			if (num2 == 1)
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
