using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using Cysharp.Threading.Tasks.CompilerServices;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x0200003F RID: 63
	internal static class Max
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x00009D28 File Offset: 0x00007F28
		public static UniTask<TSource> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			Max.<MaxAsync>d__0<TSource> <MaxAsync>d__;
			<MaxAsync>d__.<>t__builder = AsyncUniTaskMethodBuilder<TSource>.Create();
			<MaxAsync>d__.source = source;
			<MaxAsync>d__.cancellationToken = cancellationToken;
			<MaxAsync>d__.<>1__state = -1;
			<MaxAsync>d__.<>t__builder.Start<Max.<MaxAsync>d__0<TSource>>(ref <MaxAsync>d__);
			return <MaxAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00009D74 File Offset: 0x00007F74
		public static UniTask<TResult> MaxAsync<TSource, TResult>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TResult> selector, CancellationToken cancellationToken)
		{
			Max.<MaxAsync>d__1<TSource, TResult> <MaxAsync>d__;
			<MaxAsync>d__.<>t__builder = AsyncUniTaskMethodBuilder<TResult>.Create();
			<MaxAsync>d__.source = source;
			<MaxAsync>d__.selector = selector;
			<MaxAsync>d__.cancellationToken = cancellationToken;
			<MaxAsync>d__.<>1__state = -1;
			<MaxAsync>d__.<>t__builder.Start<Max.<MaxAsync>d__1<TSource, TResult>>(ref <MaxAsync>d__);
			return <MaxAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00009DC8 File Offset: 0x00007FC8
		public static UniTask<TResult> MaxAwaitAsync<TSource, TResult>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TResult>> selector, CancellationToken cancellationToken)
		{
			Max.<MaxAwaitAsync>d__2<TSource, TResult> <MaxAwaitAsync>d__;
			<MaxAwaitAsync>d__.<>t__builder = AsyncUniTaskMethodBuilder<TResult>.Create();
			<MaxAwaitAsync>d__.source = source;
			<MaxAwaitAsync>d__.selector = selector;
			<MaxAwaitAsync>d__.cancellationToken = cancellationToken;
			<MaxAwaitAsync>d__.<>1__state = -1;
			<MaxAwaitAsync>d__.<>t__builder.Start<Max.<MaxAwaitAsync>d__2<TSource, TResult>>(ref <MaxAwaitAsync>d__);
			return <MaxAwaitAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00009E1C File Offset: 0x0000801C
		public static UniTask<TResult> MaxAwaitWithCancellationAsync<TSource, TResult>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TResult>> selector, CancellationToken cancellationToken)
		{
			Max.<MaxAwaitWithCancellationAsync>d__3<TSource, TResult> <MaxAwaitWithCancellationAsync>d__;
			<MaxAwaitWithCancellationAsync>d__.<>t__builder = AsyncUniTaskMethodBuilder<TResult>.Create();
			<MaxAwaitWithCancellationAsync>d__.source = source;
			<MaxAwaitWithCancellationAsync>d__.selector = selector;
			<MaxAwaitWithCancellationAsync>d__.cancellationToken = cancellationToken;
			<MaxAwaitWithCancellationAsync>d__.<>1__state = -1;
			<MaxAwaitWithCancellationAsync>d__.<>t__builder.Start<Max.<MaxAwaitWithCancellationAsync>d__3<TSource, TResult>>(ref <MaxAwaitWithCancellationAsync>d__);
			return <MaxAwaitWithCancellationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00009E70 File Offset: 0x00008070
		public static async UniTask<int> MaxAsync(IUniTaskAsyncEnumerable<int> source, CancellationToken cancellationToken)
		{
			int value = 0;
			IUniTaskAsyncEnumerator<int> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
					int num = e.Current;
					if (value < num)
					{
						value = num;
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
			return value;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00009EBC File Offset: 0x000080BC
		public static async UniTask<int> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int> selector, CancellationToken cancellationToken)
		{
			int value = 0;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
				value = selector(e.Current);
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
					int num = selector(e.Current);
					if (value < num)
					{
						value = num;
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
			return value;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00009F10 File Offset: 0x00008110
		public static async UniTask<int> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<int>> selector, CancellationToken cancellationToken)
		{
			int value = 0;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
				int num = await selector(e.Current);
				value = num;
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
					int num2 = await selector(e.Current);
					if (value < num2)
					{
						value = num2;
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
			return value;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00009F64 File Offset: 0x00008164
		public static async UniTask<int> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<int>> selector, CancellationToken cancellationToken)
		{
			int value = 0;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
				int num = await selector(e.Current, cancellationToken);
				value = num;
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
					int num2 = await selector(e.Current, cancellationToken);
					if (value < num2)
					{
						value = num2;
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
			return value;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00009FB8 File Offset: 0x000081B8
		public static async UniTask<long> MaxAsync(IUniTaskAsyncEnumerable<long> source, CancellationToken cancellationToken)
		{
			long value = 0L;
			IUniTaskAsyncEnumerator<long> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
					long num = e.Current;
					if (value < num)
					{
						value = num;
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
			return value;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000A004 File Offset: 0x00008204
		public static async UniTask<long> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, long> selector, CancellationToken cancellationToken)
		{
			long value = 0L;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
				value = selector(e.Current);
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
					long num = selector(e.Current);
					if (value < num)
					{
						value = num;
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
			return value;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000A058 File Offset: 0x00008258
		public static async UniTask<long> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<long>> selector, CancellationToken cancellationToken)
		{
			long value = 0L;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
				long num = await selector(e.Current);
				value = num;
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
					long num2 = await selector(e.Current);
					if (value < num2)
					{
						value = num2;
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
			return value;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000A0AC File Offset: 0x000082AC
		public static async UniTask<long> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<long>> selector, CancellationToken cancellationToken)
		{
			long value = 0L;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
				long num = await selector(e.Current, cancellationToken);
				value = num;
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
					long num2 = await selector(e.Current, cancellationToken);
					if (value < num2)
					{
						value = num2;
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
			return value;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A100 File Offset: 0x00008300
		public static async UniTask<float> MaxAsync(IUniTaskAsyncEnumerable<float> source, CancellationToken cancellationToken)
		{
			float value = 0f;
			IUniTaskAsyncEnumerator<float> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
					float num = e.Current;
					if (value < num)
					{
						value = num;
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
			return value;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000A14C File Offset: 0x0000834C
		public static async UniTask<float> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, float> selector, CancellationToken cancellationToken)
		{
			float value = 0f;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
				value = selector(e.Current);
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
					float num = selector(e.Current);
					if (value < num)
					{
						value = num;
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
			return value;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000A1A0 File Offset: 0x000083A0
		public static async UniTask<float> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<float>> selector, CancellationToken cancellationToken)
		{
			float value = 0f;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
				float num = await selector(e.Current);
				value = num;
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
					float num2 = await selector(e.Current);
					if (value < num2)
					{
						value = num2;
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
			return value;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000A1F4 File Offset: 0x000083F4
		public static async UniTask<float> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<float>> selector, CancellationToken cancellationToken)
		{
			float value = 0f;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
				float num = await selector(e.Current, cancellationToken);
				value = num;
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
					float num2 = await selector(e.Current, cancellationToken);
					if (value < num2)
					{
						value = num2;
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
			return value;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000A248 File Offset: 0x00008448
		public static async UniTask<double> MaxAsync(IUniTaskAsyncEnumerable<double> source, CancellationToken cancellationToken)
		{
			double value = 0.0;
			IUniTaskAsyncEnumerator<double> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
					double num = e.Current;
					if (value < num)
					{
						value = num;
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
			return value;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000A294 File Offset: 0x00008494
		public static async UniTask<double> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, double> selector, CancellationToken cancellationToken)
		{
			double value = 0.0;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
				value = selector(e.Current);
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
					double num = selector(e.Current);
					if (value < num)
					{
						value = num;
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
			return value;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000A2E8 File Offset: 0x000084E8
		public static async UniTask<double> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<double>> selector, CancellationToken cancellationToken)
		{
			double value = 0.0;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
				double num = await selector(e.Current);
				value = num;
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
					double num2 = await selector(e.Current);
					if (value < num2)
					{
						value = num2;
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
			return value;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000A33C File Offset: 0x0000853C
		public static async UniTask<double> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<double>> selector, CancellationToken cancellationToken)
		{
			double value = 0.0;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
				double num = await selector(e.Current, cancellationToken);
				value = num;
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
					double num2 = await selector(e.Current, cancellationToken);
					if (value < num2)
					{
						value = num2;
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
			return value;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000A390 File Offset: 0x00008590
		public static async UniTask<decimal> MaxAsync(IUniTaskAsyncEnumerable<decimal> source, CancellationToken cancellationToken)
		{
			decimal value = 0m;
			IUniTaskAsyncEnumerator<decimal> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
					decimal num = e.Current;
					if (value < num)
					{
						value = num;
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
			return value;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000A3DC File Offset: 0x000085DC
		public static async UniTask<decimal> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, decimal> selector, CancellationToken cancellationToken)
		{
			decimal value = 0m;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
				value = selector(e.Current);
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
					decimal num = selector(e.Current);
					if (value < num)
					{
						value = num;
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
			return value;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000A430 File Offset: 0x00008630
		public static async UniTask<decimal> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<decimal>> selector, CancellationToken cancellationToken)
		{
			decimal value = 0m;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
				decimal num = await selector(e.Current);
				value = num;
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
					decimal num2 = await selector(e.Current);
					if (value < num2)
					{
						value = num2;
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
			return value;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000A484 File Offset: 0x00008684
		public static async UniTask<decimal> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<decimal>> selector, CancellationToken cancellationToken)
		{
			decimal value = 0m;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
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
				decimal num = await selector(e.Current, cancellationToken);
				value = num;
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
					decimal num2 = await selector(e.Current, cancellationToken);
					if (value < num2)
					{
						value = num2;
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
			return value;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000A4D8 File Offset: 0x000086D8
		public static async UniTask<int?> MaxAsync(IUniTaskAsyncEnumerable<int?> source, CancellationToken cancellationToken)
		{
			int? value = null;
			IUniTaskAsyncEnumerator<int?> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			int? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					value = e.Current;
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					int? num2 = e.Current;
					if (num2 != null)
					{
						int? num3 = value;
						int? num4 = num2;
						if ((num3.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
						{
							value = num2;
						}
					}
				}
				goto IL_01B5;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_01B5:
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
			int? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000A524 File Offset: 0x00008724
		public static async UniTask<int?> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int?> selector, CancellationToken cancellationToken)
		{
			int? value = null;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			int? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					value = selector(e.Current);
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					int? num2 = selector(e.Current);
					if (num2 != null)
					{
						int? num3 = value;
						int? num4 = num2;
						if ((num3.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
						{
							value = num2;
						}
					}
				}
				goto IL_01CB;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_01CB:
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
			int? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000A578 File Offset: 0x00008778
		public static async UniTask<int?> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<int?>> selector, CancellationToken cancellationToken)
		{
			int? value = null;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			int? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					int? num2 = await selector(e.Current);
					value = num2;
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					int? num3 = await selector(e.Current);
					if (num3 != null)
					{
						int? num2 = value;
						int? num4 = num3;
						if ((num2.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
						{
							value = num3;
						}
					}
				}
				goto IL_0295;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_0295:
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
			int? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000A5CC File Offset: 0x000087CC
		public static async UniTask<int?> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<int?>> selector, CancellationToken cancellationToken)
		{
			int? value = null;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			int? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					int? num2 = await selector(e.Current, cancellationToken);
					value = num2;
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					int? num3 = await selector(e.Current, cancellationToken);
					if (num3 != null)
					{
						int? num2 = value;
						int? num4 = num3;
						if ((num2.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
						{
							value = num3;
						}
					}
				}
				goto IL_02A1;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_02A1:
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
			int? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000A620 File Offset: 0x00008820
		public static async UniTask<long?> MaxAsync(IUniTaskAsyncEnumerable<long?> source, CancellationToken cancellationToken)
		{
			long? value = null;
			IUniTaskAsyncEnumerator<long?> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			long? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					value = e.Current;
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					long? num2 = e.Current;
					if (num2 != null)
					{
						long? num3 = value;
						long? num4 = num2;
						if ((num3.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
						{
							value = num2;
						}
					}
				}
				goto IL_01B5;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_01B5:
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
			long? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000A66C File Offset: 0x0000886C
		public static async UniTask<long?> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, long?> selector, CancellationToken cancellationToken)
		{
			long? value = null;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			long? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					value = selector(e.Current);
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					long? num2 = selector(e.Current);
					if (num2 != null)
					{
						long? num3 = value;
						long? num4 = num2;
						if ((num3.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
						{
							value = num2;
						}
					}
				}
				goto IL_01CB;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_01CB:
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
			long? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000A6C0 File Offset: 0x000088C0
		public static async UniTask<long?> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<long?>> selector, CancellationToken cancellationToken)
		{
			long? value = null;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			long? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					long? num2 = await selector(e.Current);
					value = num2;
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					long? num3 = await selector(e.Current);
					if (num3 != null)
					{
						long? num2 = value;
						long? num4 = num3;
						if ((num2.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
						{
							value = num3;
						}
					}
				}
				goto IL_0295;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_0295:
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
			long? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000A714 File Offset: 0x00008914
		public static async UniTask<long?> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<long?>> selector, CancellationToken cancellationToken)
		{
			long? value = null;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			long? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					long? num2 = await selector(e.Current, cancellationToken);
					value = num2;
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					long? num3 = await selector(e.Current, cancellationToken);
					if (num3 != null)
					{
						long? num2 = value;
						long? num4 = num3;
						if ((num2.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
						{
							value = num3;
						}
					}
				}
				goto IL_02A1;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_02A1:
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
			long? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000A768 File Offset: 0x00008968
		public static async UniTask<float?> MaxAsync(IUniTaskAsyncEnumerable<float?> source, CancellationToken cancellationToken)
		{
			float? value = null;
			IUniTaskAsyncEnumerator<float?> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			float? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					value = e.Current;
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					float? num2 = e.Current;
					if (num2 != null)
					{
						float? num3 = value;
						float? num4 = num2;
						if ((num3.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
						{
							value = num2;
						}
					}
				}
				goto IL_01B5;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_01B5:
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
			float? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000A7B4 File Offset: 0x000089B4
		public static async UniTask<float?> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, float?> selector, CancellationToken cancellationToken)
		{
			float? value = null;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			float? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					value = selector(e.Current);
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					float? num2 = selector(e.Current);
					if (num2 != null)
					{
						float? num3 = value;
						float? num4 = num2;
						if ((num3.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
						{
							value = num2;
						}
					}
				}
				goto IL_01CB;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_01CB:
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
			float? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000A808 File Offset: 0x00008A08
		public static async UniTask<float?> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<float?>> selector, CancellationToken cancellationToken)
		{
			float? value = null;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			float? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					float? num2 = await selector(e.Current);
					value = num2;
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					float? num3 = await selector(e.Current);
					if (num3 != null)
					{
						float? num2 = value;
						float? num4 = num3;
						if ((num2.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
						{
							value = num3;
						}
					}
				}
				goto IL_0295;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_0295:
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
			float? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000A85C File Offset: 0x00008A5C
		public static async UniTask<float?> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<float?>> selector, CancellationToken cancellationToken)
		{
			float? value = null;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			float? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					float? num2 = await selector(e.Current, cancellationToken);
					value = num2;
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					float? num3 = await selector(e.Current, cancellationToken);
					if (num3 != null)
					{
						float? num2 = value;
						float? num4 = num3;
						if ((num2.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
						{
							value = num3;
						}
					}
				}
				goto IL_02A1;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_02A1:
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
			float? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000A8B0 File Offset: 0x00008AB0
		public static async UniTask<double?> MaxAsync(IUniTaskAsyncEnumerable<double?> source, CancellationToken cancellationToken)
		{
			double? value = null;
			IUniTaskAsyncEnumerator<double?> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			double? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					value = e.Current;
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					double? num2 = e.Current;
					if (num2 != null)
					{
						double? num3 = value;
						double? num4 = num2;
						if ((num3.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
						{
							value = num2;
						}
					}
				}
				goto IL_01B5;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_01B5:
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
			double? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000A8FC File Offset: 0x00008AFC
		public static async UniTask<double?> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, double?> selector, CancellationToken cancellationToken)
		{
			double? value = null;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			double? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					value = selector(e.Current);
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					double? num2 = selector(e.Current);
					if (num2 != null)
					{
						double? num3 = value;
						double? num4 = num2;
						if ((num3.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
						{
							value = num2;
						}
					}
				}
				goto IL_01CB;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_01CB:
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
			double? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000A950 File Offset: 0x00008B50
		public static async UniTask<double?> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<double?>> selector, CancellationToken cancellationToken)
		{
			double? value = null;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			double? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					double? num2 = await selector(e.Current);
					value = num2;
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					double? num3 = await selector(e.Current);
					if (num3 != null)
					{
						double? num2 = value;
						double? num4 = num3;
						if ((num2.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
						{
							value = num3;
						}
					}
				}
				goto IL_0295;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_0295:
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
			double? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000A9A4 File Offset: 0x00008BA4
		public static async UniTask<double?> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<double?>> selector, CancellationToken cancellationToken)
		{
			double? value = null;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			double? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					double? num2 = await selector(e.Current, cancellationToken);
					value = num2;
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					double? num3 = await selector(e.Current, cancellationToken);
					if (num3 != null)
					{
						double? num2 = value;
						double? num4 = num3;
						if ((num2.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
						{
							value = num3;
						}
					}
				}
				goto IL_02A1;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_02A1:
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
			double? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000A9F8 File Offset: 0x00008BF8
		public static async UniTask<decimal?> MaxAsync(IUniTaskAsyncEnumerable<decimal?> source, CancellationToken cancellationToken)
		{
			decimal? value = null;
			IUniTaskAsyncEnumerator<decimal?> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			decimal? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					value = e.Current;
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					decimal? num2 = e.Current;
					if (num2 != null)
					{
						decimal? num3 = value;
						decimal? num4 = num2;
						if ((num3.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
						{
							value = num2;
						}
					}
				}
				goto IL_01B8;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_01B8:
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
			decimal? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000AA44 File Offset: 0x00008C44
		public static async UniTask<decimal?> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, decimal?> selector, CancellationToken cancellationToken)
		{
			decimal? value = null;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			decimal? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					value = selector(e.Current);
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					decimal? num2 = selector(e.Current);
					if (num2 != null)
					{
						decimal? num3 = value;
						decimal? num4 = num2;
						if ((num3.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
						{
							value = num2;
						}
					}
				}
				goto IL_01CE;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_01CE:
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
			decimal? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000AA98 File Offset: 0x00008C98
		public static async UniTask<decimal?> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<decimal?>> selector, CancellationToken cancellationToken)
		{
			decimal? value = null;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			decimal? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					decimal? num2 = await selector(e.Current);
					value = num2;
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					decimal? num3 = await selector(e.Current);
					if (num3 != null)
					{
						decimal? num2 = value;
						decimal? num4 = num3;
						if ((num2.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
						{
							value = num3;
						}
					}
				}
				goto IL_0298;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_0298:
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
			decimal? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000AAEC File Offset: 0x00008CEC
		public static async UniTask<decimal?> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<decimal?>> selector, CancellationToken cancellationToken)
		{
			decimal? value = null;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			decimal? num5;
			try
			{
				UniTask<bool>.Awaiter awaiter2;
				do
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						goto Block_8;
					}
					decimal? num2 = await selector(e.Current, cancellationToken);
					value = num2;
				}
				while (value == null);
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = e.MoveNextAsync().GetAwaiter();
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
					decimal? num3 = await selector(e.Current, cancellationToken);
					if (num3 != null)
					{
						decimal? num2 = value;
						decimal? num4 = num3;
						if ((num2.GetValueOrDefault() < num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
						{
							value = num3;
						}
					}
				}
				goto IL_02A4;
				Block_8:
				num5 = null;
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_02A4:
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
			decimal? num6;
			if (num == 1)
			{
				num6 = num5;
			}
			else
			{
				obj = null;
				num5 = null;
				num6 = value;
			}
			return num6;
		}
	}
}
