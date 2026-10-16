using System;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x0200000A RID: 10
	internal static class Average
	{
		// Token: 0x0600020D RID: 525 RVA: 0x00007808 File Offset: 0x00005A08
		public static async UniTask<double> AverageAsync(IUniTaskAsyncEnumerable<int> source, CancellationToken cancellationToken)
		{
			long count = 0L;
			int sum = 0;
			IUniTaskAsyncEnumerator<int> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						sum += e.Current;
						long num = count;
						count = num + 1L;
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
				return (double)sum / (double)count;
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007854 File Offset: 0x00005A54
		public static async UniTask<double> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			int sum = 0;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						sum += selector(e.Current);
						long num = count;
						count = num + 1L;
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
				return (double)sum / (double)count;
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000078A8 File Offset: 0x00005AA8
		public static async UniTask<double> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<int>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			int sum = 0;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						int num = sum;
						int num2 = await selector(e.Current);
						sum = num + num2;
						count += 1L;
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
				return (double)sum / (double)count;
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000078FC File Offset: 0x00005AFC
		public static async UniTask<double> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<int>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			int sum = 0;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						int num = sum;
						int num2 = await selector(e.Current, cancellationToken);
						sum = num + num2;
						count += 1L;
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
				return (double)sum / (double)count;
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007950 File Offset: 0x00005B50
		public static async UniTask<double> AverageAsync(IUniTaskAsyncEnumerable<long> source, CancellationToken cancellationToken)
		{
			long count = 0L;
			long sum = 0L;
			IUniTaskAsyncEnumerator<long> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						sum += e.Current;
						long num = count;
						count = num + 1L;
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
				return (double)sum / (double)count;
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000799C File Offset: 0x00005B9C
		public static async UniTask<double> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, long> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			long sum = 0L;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						sum += selector(e.Current);
						long num = count;
						count = num + 1L;
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
				return (double)sum / (double)count;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000079F0 File Offset: 0x00005BF0
		public static async UniTask<double> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<long>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			long sum = 0L;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						long num = sum;
						long num2 = await selector(e.Current);
						sum = num + num2;
						num2 = count;
						count = num2 + 1L;
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
				return (double)sum / (double)count;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007A44 File Offset: 0x00005C44
		public static async UniTask<double> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<long>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			long sum = 0L;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						long num = sum;
						long num2 = await selector(e.Current, cancellationToken);
						sum = num + num2;
						num2 = count;
						count = num2 + 1L;
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
				return (double)sum / (double)count;
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007A98 File Offset: 0x00005C98
		public static async UniTask<float> AverageAsync(IUniTaskAsyncEnumerable<float> source, CancellationToken cancellationToken)
		{
			long count = 0L;
			float sum = 0f;
			IUniTaskAsyncEnumerator<float> e = source.GetAsyncEnumerator(cancellationToken);
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
					sum += e.Current;
					long num = count;
					count = checked(num + 1L);
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
			return sum / (float)count;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007AE4 File Offset: 0x00005CE4
		public static async UniTask<float> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, float> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			float sum = 0f;
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
					sum += selector(e.Current);
					long num = count;
					count = checked(num + 1L);
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
			return sum / (float)count;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007B38 File Offset: 0x00005D38
		public static async UniTask<float> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<float>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			float sum = 0f;
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
					float num = sum;
					float num2 = await selector(e.Current);
					sum = num + num2;
					checked
					{
						count += 1L;
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
			return sum / (float)count;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00007B8C File Offset: 0x00005D8C
		public static async UniTask<float> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<float>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			float sum = 0f;
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
					float num = sum;
					float num2 = await selector(e.Current, cancellationToken);
					sum = num + num2;
					checked
					{
						count += 1L;
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
			return sum / (float)count;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00007BE0 File Offset: 0x00005DE0
		public static async UniTask<double> AverageAsync(IUniTaskAsyncEnumerable<double> source, CancellationToken cancellationToken)
		{
			long count = 0L;
			double sum = 0.0;
			IUniTaskAsyncEnumerator<double> e = source.GetAsyncEnumerator(cancellationToken);
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
					sum += e.Current;
					long num = count;
					count = checked(num + 1L);
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
			return sum / (double)count;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007C2C File Offset: 0x00005E2C
		public static async UniTask<double> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, double> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			double sum = 0.0;
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
					sum += selector(e.Current);
					long num = count;
					count = checked(num + 1L);
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
			return sum / (double)count;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007C80 File Offset: 0x00005E80
		public static async UniTask<double> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<double>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			double sum = 0.0;
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
					double num = sum;
					double num2 = await selector(e.Current);
					sum = num + num2;
					checked
					{
						count += 1L;
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
			return sum / (double)count;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007CD4 File Offset: 0x00005ED4
		public static async UniTask<double> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<double>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			double sum = 0.0;
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
					double num = sum;
					double num2 = await selector(e.Current, cancellationToken);
					sum = num + num2;
					checked
					{
						count += 1L;
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
			return sum / (double)count;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007D28 File Offset: 0x00005F28
		public static async UniTask<decimal> AverageAsync(IUniTaskAsyncEnumerable<decimal> source, CancellationToken cancellationToken)
		{
			long count = 0L;
			decimal sum = 0m;
			IUniTaskAsyncEnumerator<decimal> e = source.GetAsyncEnumerator(cancellationToken);
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
					sum += e.Current;
					long num = count;
					count = checked(num + 1L);
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
			return sum / count;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007D74 File Offset: 0x00005F74
		public static async UniTask<decimal> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, decimal> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			decimal sum = 0m;
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
					sum += selector(e.Current);
					long num = count;
					count = checked(num + 1L);
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
			return sum / count;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007DC8 File Offset: 0x00005FC8
		public static async UniTask<decimal> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<decimal>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			decimal sum = 0m;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						decimal num = sum;
						decimal num2 = await selector(e.Current);
						sum = num + num2;
						count += 1L;
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
				return sum / count;
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007E1C File Offset: 0x0000601C
		public static async UniTask<decimal> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<decimal>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			decimal sum = 0m;
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						decimal num = sum;
						decimal num2 = await selector(e.Current, cancellationToken);
						sum = num + num2;
						count += 1L;
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
				return sum / count;
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007E70 File Offset: 0x00006070
		public static async UniTask<double?> AverageAsync(IUniTaskAsyncEnumerable<int?> source, CancellationToken cancellationToken)
		{
			long count = 0L;
			int? sum = new int?(0);
			IUniTaskAsyncEnumerator<int?> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						int? num = e.Current;
						if (num != null)
						{
							sum += num.Value;
							long num2 = count;
							count = num2 + 1L;
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
				return new double?((double)sum.Value / (double)count);
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007EBC File Offset: 0x000060BC
		public static async UniTask<double?> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int?> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			int? sum = new int?(0);
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						int? num = selector(e.Current);
						if (num != null)
						{
							sum += num.Value;
							long num2 = count;
							count = num2 + 1L;
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
				return new double?((double)sum.Value / (double)count);
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007F10 File Offset: 0x00006110
		public static async UniTask<double?> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<int?>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			int? sum = new int?(0);
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						int? num = await selector(e.Current);
						if (num != null)
						{
							sum += num.Value;
							count += 1L;
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
				return new double?((double)sum.Value / (double)count);
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007F64 File Offset: 0x00006164
		public static async UniTask<double?> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<int?>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			int? sum = new int?(0);
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						int? num = await selector(e.Current, cancellationToken);
						if (num != null)
						{
							sum += num.Value;
							count += 1L;
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
				return new double?((double)sum.Value / (double)count);
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007FB8 File Offset: 0x000061B8
		public static async UniTask<double?> AverageAsync(IUniTaskAsyncEnumerable<long?> source, CancellationToken cancellationToken)
		{
			long count = 0L;
			long? sum = new long?(0L);
			IUniTaskAsyncEnumerator<long?> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						long? num = e.Current;
						if (num != null)
						{
							sum += num.Value;
							long num2 = count;
							count = num2 + 1L;
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
				return new double?((double)sum.Value / (double)count);
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00008004 File Offset: 0x00006204
		public static async UniTask<double?> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, long?> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			long? sum = new long?(0L);
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						long? num = selector(e.Current);
						if (num != null)
						{
							sum += num.Value;
							long num2 = count;
							count = num2 + 1L;
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
				return new double?((double)sum.Value / (double)count);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00008058 File Offset: 0x00006258
		public static async UniTask<double?> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<long?>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			long? sum = new long?(0L);
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						long? num = await selector(e.Current);
						if (num != null)
						{
							sum += num.Value;
							count += 1L;
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
				return new double?((double)sum.Value / (double)count);
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000080AC File Offset: 0x000062AC
		public static async UniTask<double?> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<long?>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			long? sum = new long?(0L);
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						long? num = await selector(e.Current, cancellationToken);
						if (num != null)
						{
							sum += num.Value;
							count += 1L;
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
				return new double?((double)sum.Value / (double)count);
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00008100 File Offset: 0x00006300
		public static async UniTask<float?> AverageAsync(IUniTaskAsyncEnumerable<float?> source, CancellationToken cancellationToken)
		{
			long count = 0L;
			float? sum = new float?(0f);
			IUniTaskAsyncEnumerator<float?> e = source.GetAsyncEnumerator(cancellationToken);
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
					float? num = e.Current;
					if (num != null)
					{
						sum += num.Value;
						long num2 = count;
						count = checked(num2 + 1L);
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
			return new float?((sum / (float)count).Value);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000814C File Offset: 0x0000634C
		public static async UniTask<float?> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, float?> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			float? sum = new float?(0f);
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
					float? num = selector(e.Current);
					if (num != null)
					{
						sum += num.Value;
						long num2 = count;
						count = checked(num2 + 1L);
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
			return new float?((sum / (float)count).Value);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000081A0 File Offset: 0x000063A0
		public static async UniTask<float?> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<float?>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			float? sum = new float?(0f);
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
					float? num = await selector(e.Current);
					if (num != null)
					{
						sum += num.Value;
						checked
						{
							count += 1L;
						}
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
			return new float?((sum / (float)count).Value);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000081F4 File Offset: 0x000063F4
		public static async UniTask<float?> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<float?>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			float? sum = new float?(0f);
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
					float? num = await selector(e.Current, cancellationToken);
					if (num != null)
					{
						sum += num.Value;
						checked
						{
							count += 1L;
						}
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
			return new float?((sum / (float)count).Value);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00008248 File Offset: 0x00006448
		public static async UniTask<double?> AverageAsync(IUniTaskAsyncEnumerable<double?> source, CancellationToken cancellationToken)
		{
			long count = 0L;
			double? sum = new double?(0.0);
			IUniTaskAsyncEnumerator<double?> e = source.GetAsyncEnumerator(cancellationToken);
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
					double? num = e.Current;
					if (num != null)
					{
						sum += num.Value;
						long num2 = count;
						count = checked(num2 + 1L);
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
			return sum / (double)count;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00008294 File Offset: 0x00006494
		public static async UniTask<double?> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, double?> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			double? sum = new double?(0.0);
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
					double? num = selector(e.Current);
					if (num != null)
					{
						sum += num.Value;
						long num2 = count;
						count = checked(num2 + 1L);
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
			return sum / (double)count;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000082E8 File Offset: 0x000064E8
		public static async UniTask<double?> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<double?>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			double? sum = new double?(0.0);
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
					double? num = await selector(e.Current);
					if (num != null)
					{
						sum += num.Value;
						checked
						{
							count += 1L;
						}
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
			return sum / (double)count;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000833C File Offset: 0x0000653C
		public static async UniTask<double?> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<double?>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			double? sum = new double?(0.0);
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
					double? num = await selector(e.Current, cancellationToken);
					if (num != null)
					{
						sum += num.Value;
						checked
						{
							count += 1L;
						}
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
			return sum / (double)count;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00008390 File Offset: 0x00006590
		public static async UniTask<decimal?> AverageAsync(IUniTaskAsyncEnumerable<decimal?> source, CancellationToken cancellationToken)
		{
			long count = 0L;
			decimal? sum = new decimal?(0m);
			IUniTaskAsyncEnumerator<decimal?> e = source.GetAsyncEnumerator(cancellationToken);
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
					decimal? num = e.Current;
					if (num != null)
					{
						sum += num.Value;
						long num2 = count;
						count = checked(num2 + 1L);
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
			return sum / count;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000083DC File Offset: 0x000065DC
		public static async UniTask<decimal?> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, decimal?> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			decimal? sum = new decimal?(0m);
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
					decimal? num = selector(e.Current);
					if (num != null)
					{
						sum += num.Value;
						long num2 = count;
						count = checked(num2 + 1L);
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
			return sum / count;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00008430 File Offset: 0x00006630
		public static async UniTask<decimal?> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<decimal?>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			decimal? sum = new decimal?(0m);
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						decimal? num = await selector(e.Current);
						if (num != null)
						{
							sum += num.Value;
							count += 1L;
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
				return sum / count;
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00008484 File Offset: 0x00006684
		public static async UniTask<decimal?> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<decimal?>> selector, CancellationToken cancellationToken)
		{
			long count = 0L;
			decimal? sum = new decimal?(0m);
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			checked
			{
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
						decimal? num = await selector(e.Current, cancellationToken);
						if (num != null)
						{
							sum += num.Value;
							count += 1L;
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
				return sum / count;
			}
		}
	}
}
