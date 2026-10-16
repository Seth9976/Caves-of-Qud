using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x0200005C RID: 92
	internal static class SequenceEqual
	{
		// Token: 0x06000345 RID: 837 RVA: 0x0000C284 File Offset: 0x0000A484
		internal static async UniTask<bool> SequenceEqualAsync<TSource>(IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second, IEqualityComparer<TSource> comparer, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = first.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			object obj3;
			bool flag2;
			try
			{
				IUniTaskAsyncEnumerator<TSource> e2 = second.GetAsyncEnumerator(cancellationToken);
				object obj2 = null;
				int num2 = 0;
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
							goto IL_0180;
						}
						awaiter = e2.MoveNextAsync().GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							await awaiter;
							awaiter = awaiter2;
							awaiter2 = default(UniTask<bool>.Awaiter);
						}
						if (!awaiter.GetResult())
						{
							goto IL_0177;
						}
					}
					while (comparer.Equals(e.Current, e2.Current));
					flag = false;
					goto IL_01F5;
					IL_0177:
					flag = false;
					goto IL_01F5;
					IL_0180:
					awaiter = e2.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						UniTask<bool>.Awaiter awaiter2;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					flag = !awaiter.GetResult();
					IL_01F5:
					num2 = 1;
				}
				catch (object obj2)
				{
				}
				if (e2 != null)
				{
					await e2.DisposeAsync();
				}
				obj3 = obj2;
				if (obj3 != null)
				{
					Exception ex = obj3 as Exception;
					if (ex == null)
					{
						throw obj3;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				if (num2 == 1)
				{
					flag2 = flag;
					num = 1;
				}
				else
				{
					obj2 = null;
					e2 = null;
				}
			}
			catch (object obj)
			{
			}
			if (e != null)
			{
				await e.DisposeAsync();
			}
			obj3 = obj;
			if (obj3 != null)
			{
				Exception ex2 = obj3 as Exception;
				if (ex2 == null)
				{
					throw obj3;
				}
				ExceptionDispatchInfo.Capture(ex2).Throw();
			}
			bool flag3;
			if (num == 1)
			{
				flag3 = flag2;
			}
			else
			{
				obj = null;
				e = null;
			}
			return flag3;
		}
	}
}
