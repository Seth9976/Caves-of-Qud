using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using Cysharp.Threading.Tasks.CompilerServices;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x02000041 RID: 65
	internal static class Min
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x0000AB6C File Offset: 0x00008D6C
		public static UniTask<TSource> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			Min.<MinAsync>d__0<TSource> <MinAsync>d__;
			<MinAsync>d__.<>t__builder = AsyncUniTaskMethodBuilder<TSource>.Create();
			<MinAsync>d__.source = source;
			<MinAsync>d__.cancellationToken = cancellationToken;
			<MinAsync>d__.<>1__state = -1;
			<MinAsync>d__.<>t__builder.Start<Min.<MinAsync>d__0<TSource>>(ref <MinAsync>d__);
			return <MinAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000ABB8 File Offset: 0x00008DB8
		public static UniTask<TResult> MinAsync<TSource, TResult>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TResult> selector, CancellationToken cancellationToken)
		{
			Min.<MinAsync>d__1<TSource, TResult> <MinAsync>d__;
			<MinAsync>d__.<>t__builder = AsyncUniTaskMethodBuilder<TResult>.Create();
			<MinAsync>d__.source = source;
			<MinAsync>d__.selector = selector;
			<MinAsync>d__.cancellationToken = cancellationToken;
			<MinAsync>d__.<>1__state = -1;
			<MinAsync>d__.<>t__builder.Start<Min.<MinAsync>d__1<TSource, TResult>>(ref <MinAsync>d__);
			return <MinAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000AC0C File Offset: 0x00008E0C
		public static UniTask<TResult> MinAwaitAsync<TSource, TResult>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TResult>> selector, CancellationToken cancellationToken)
		{
			Min.<MinAwaitAsync>d__2<TSource, TResult> <MinAwaitAsync>d__;
			<MinAwaitAsync>d__.<>t__builder = AsyncUniTaskMethodBuilder<TResult>.Create();
			<MinAwaitAsync>d__.source = source;
			<MinAwaitAsync>d__.selector = selector;
			<MinAwaitAsync>d__.cancellationToken = cancellationToken;
			<MinAwaitAsync>d__.<>1__state = -1;
			<MinAwaitAsync>d__.<>t__builder.Start<Min.<MinAwaitAsync>d__2<TSource, TResult>>(ref <MinAwaitAsync>d__);
			return <MinAwaitAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000AC60 File Offset: 0x00008E60
		public static UniTask<TResult> MinAwaitWithCancellationAsync<TSource, TResult>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TResult>> selector, CancellationToken cancellationToken)
		{
			Min.<MinAwaitWithCancellationAsync>d__3<TSource, TResult> <MinAwaitWithCancellationAsync>d__;
			<MinAwaitWithCancellationAsync>d__.<>t__builder = AsyncUniTaskMethodBuilder<TResult>.Create();
			<MinAwaitWithCancellationAsync>d__.source = source;
			<MinAwaitWithCancellationAsync>d__.selector = selector;
			<MinAwaitWithCancellationAsync>d__.cancellationToken = cancellationToken;
			<MinAwaitWithCancellationAsync>d__.<>1__state = -1;
			<MinAwaitWithCancellationAsync>d__.<>t__builder.Start<Min.<MinAwaitWithCancellationAsync>d__3<TSource, TResult>>(ref <MinAwaitWithCancellationAsync>d__);
			return <MinAwaitWithCancellationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000ACB4 File Offset: 0x00008EB4
		public static async UniTask<int> MinAsync(IUniTaskAsyncEnumerable<int> source, CancellationToken cancellationToken)
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
					if (value > num)
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

		// Token: 0x060002DA RID: 730 RVA: 0x0000AD00 File Offset: 0x00008F00
		public static async UniTask<int> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int> selector, CancellationToken cancellationToken)
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
					if (value > num)
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

		// Token: 0x060002DB RID: 731 RVA: 0x0000AD54 File Offset: 0x00008F54
		public static async UniTask<int> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<int>> selector, CancellationToken cancellationToken)
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
					if (value > num2)
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

		// Token: 0x060002DC RID: 732 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		public static async UniTask<int> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<int>> selector, CancellationToken cancellationToken)
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
					if (value > num2)
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

		// Token: 0x060002DD RID: 733 RVA: 0x0000ADFC File Offset: 0x00008FFC
		public static async UniTask<long> MinAsync(IUniTaskAsyncEnumerable<long> source, CancellationToken cancellationToken)
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
					if (value > num)
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

		// Token: 0x060002DE RID: 734 RVA: 0x0000AE48 File Offset: 0x00009048
		public static async UniTask<long> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, long> selector, CancellationToken cancellationToken)
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
					if (value > num)
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

		// Token: 0x060002DF RID: 735 RVA: 0x0000AE9C File Offset: 0x0000909C
		public static async UniTask<long> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<long>> selector, CancellationToken cancellationToken)
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
					if (value > num2)
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

		// Token: 0x060002E0 RID: 736 RVA: 0x0000AEF0 File Offset: 0x000090F0
		public static async UniTask<long> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<long>> selector, CancellationToken cancellationToken)
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
					if (value > num2)
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

		// Token: 0x060002E1 RID: 737 RVA: 0x0000AF44 File Offset: 0x00009144
		public static async UniTask<float> MinAsync(IUniTaskAsyncEnumerable<float> source, CancellationToken cancellationToken)
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
					if (value > num)
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

		// Token: 0x060002E2 RID: 738 RVA: 0x0000AF90 File Offset: 0x00009190
		public static async UniTask<float> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, float> selector, CancellationToken cancellationToken)
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
					if (value > num)
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

		// Token: 0x060002E3 RID: 739 RVA: 0x0000AFE4 File Offset: 0x000091E4
		public static async UniTask<float> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<float>> selector, CancellationToken cancellationToken)
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
					if (value > num2)
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

		// Token: 0x060002E4 RID: 740 RVA: 0x0000B038 File Offset: 0x00009238
		public static async UniTask<float> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<float>> selector, CancellationToken cancellationToken)
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
					if (value > num2)
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

		// Token: 0x060002E5 RID: 741 RVA: 0x0000B08C File Offset: 0x0000928C
		public static async UniTask<double> MinAsync(IUniTaskAsyncEnumerable<double> source, CancellationToken cancellationToken)
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
					if (value > num)
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

		// Token: 0x060002E6 RID: 742 RVA: 0x0000B0D8 File Offset: 0x000092D8
		public static async UniTask<double> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, double> selector, CancellationToken cancellationToken)
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
					if (value > num)
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

		// Token: 0x060002E7 RID: 743 RVA: 0x0000B12C File Offset: 0x0000932C
		public static async UniTask<double> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<double>> selector, CancellationToken cancellationToken)
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
					if (value > num2)
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

		// Token: 0x060002E8 RID: 744 RVA: 0x0000B180 File Offset: 0x00009380
		public static async UniTask<double> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<double>> selector, CancellationToken cancellationToken)
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
					if (value > num2)
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

		// Token: 0x060002E9 RID: 745 RVA: 0x0000B1D4 File Offset: 0x000093D4
		public static async UniTask<decimal> MinAsync(IUniTaskAsyncEnumerable<decimal> source, CancellationToken cancellationToken)
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
					if (value > num)
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

		// Token: 0x060002EA RID: 746 RVA: 0x0000B220 File Offset: 0x00009420
		public static async UniTask<decimal> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, decimal> selector, CancellationToken cancellationToken)
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
					if (value > num)
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

		// Token: 0x060002EB RID: 747 RVA: 0x0000B274 File Offset: 0x00009474
		public static async UniTask<decimal> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<decimal>> selector, CancellationToken cancellationToken)
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
					if (value > num2)
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

		// Token: 0x060002EC RID: 748 RVA: 0x0000B2C8 File Offset: 0x000094C8
		public static async UniTask<decimal> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<decimal>> selector, CancellationToken cancellationToken)
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
					if (value > num2)
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

		// Token: 0x060002ED RID: 749 RVA: 0x0000B31C File Offset: 0x0000951C
		public static async UniTask<int?> MinAsync(IUniTaskAsyncEnumerable<int?> source, CancellationToken cancellationToken)
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
						if ((num3.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
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

		// Token: 0x060002EE RID: 750 RVA: 0x0000B368 File Offset: 0x00009568
		public static async UniTask<int?> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int?> selector, CancellationToken cancellationToken)
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
						if ((num3.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
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

		// Token: 0x060002EF RID: 751 RVA: 0x0000B3BC File Offset: 0x000095BC
		public static async UniTask<int?> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<int?>> selector, CancellationToken cancellationToken)
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
						if ((num2.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
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

		// Token: 0x060002F0 RID: 752 RVA: 0x0000B410 File Offset: 0x00009610
		public static async UniTask<int?> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<int?>> selector, CancellationToken cancellationToken)
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
						if ((num2.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
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

		// Token: 0x060002F1 RID: 753 RVA: 0x0000B464 File Offset: 0x00009664
		public static async UniTask<long?> MinAsync(IUniTaskAsyncEnumerable<long?> source, CancellationToken cancellationToken)
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
						if ((num3.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
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

		// Token: 0x060002F2 RID: 754 RVA: 0x0000B4B0 File Offset: 0x000096B0
		public static async UniTask<long?> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, long?> selector, CancellationToken cancellationToken)
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
						if ((num3.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
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

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B504 File Offset: 0x00009704
		public static async UniTask<long?> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<long?>> selector, CancellationToken cancellationToken)
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
						if ((num2.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
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

		// Token: 0x060002F4 RID: 756 RVA: 0x0000B558 File Offset: 0x00009758
		public static async UniTask<long?> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<long?>> selector, CancellationToken cancellationToken)
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
						if ((num2.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
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

		// Token: 0x060002F5 RID: 757 RVA: 0x0000B5AC File Offset: 0x000097AC
		public static async UniTask<float?> MinAsync(IUniTaskAsyncEnumerable<float?> source, CancellationToken cancellationToken)
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
						if ((num3.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
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

		// Token: 0x060002F6 RID: 758 RVA: 0x0000B5F8 File Offset: 0x000097F8
		public static async UniTask<float?> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, float?> selector, CancellationToken cancellationToken)
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
						if ((num3.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
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

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B64C File Offset: 0x0000984C
		public static async UniTask<float?> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<float?>> selector, CancellationToken cancellationToken)
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
						if ((num2.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
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

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B6A0 File Offset: 0x000098A0
		public static async UniTask<float?> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<float?>> selector, CancellationToken cancellationToken)
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
						if ((num2.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
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

		// Token: 0x060002F9 RID: 761 RVA: 0x0000B6F4 File Offset: 0x000098F4
		public static async UniTask<double?> MinAsync(IUniTaskAsyncEnumerable<double?> source, CancellationToken cancellationToken)
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
						if ((num3.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
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

		// Token: 0x060002FA RID: 762 RVA: 0x0000B740 File Offset: 0x00009940
		public static async UniTask<double?> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, double?> selector, CancellationToken cancellationToken)
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
						if ((num3.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
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

		// Token: 0x060002FB RID: 763 RVA: 0x0000B794 File Offset: 0x00009994
		public static async UniTask<double?> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<double?>> selector, CancellationToken cancellationToken)
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
						if ((num2.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
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

		// Token: 0x060002FC RID: 764 RVA: 0x0000B7E8 File Offset: 0x000099E8
		public static async UniTask<double?> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<double?>> selector, CancellationToken cancellationToken)
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
						if ((num2.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
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

		// Token: 0x060002FD RID: 765 RVA: 0x0000B83C File Offset: 0x00009A3C
		public static async UniTask<decimal?> MinAsync(IUniTaskAsyncEnumerable<decimal?> source, CancellationToken cancellationToken)
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
						if ((num3.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
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

		// Token: 0x060002FE RID: 766 RVA: 0x0000B888 File Offset: 0x00009A88
		public static async UniTask<decimal?> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, decimal?> selector, CancellationToken cancellationToken)
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
						if ((num3.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
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

		// Token: 0x060002FF RID: 767 RVA: 0x0000B8DC File Offset: 0x00009ADC
		public static async UniTask<decimal?> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<decimal?>> selector, CancellationToken cancellationToken)
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
						if ((num2.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
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

		// Token: 0x06000300 RID: 768 RVA: 0x0000B930 File Offset: 0x00009B30
		public static async UniTask<decimal?> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<decimal?>> selector, CancellationToken cancellationToken)
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
						if ((num2.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num2 != null) & (num4 != null)))
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
