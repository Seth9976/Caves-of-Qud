using System;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x02000069 RID: 105
	internal static class Subscribe
	{
		// Token: 0x06000361 RID: 865 RVA: 0x0000C650 File Offset: 0x0000A850
		public static async UniTaskVoid SubscribeCore<TSource>(IUniTaskAsyncEnumerable<TSource> source, Action<TSource> onNext, Action<Exception> onError, Action onCompleted, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			try
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
						try
						{
							onNext(e.Current);
						}
						catch (Exception ex)
						{
							UniTaskScheduler.PublishUnobservedTaskException(ex);
						}
					}
					onCompleted();
				}
				catch (Exception ex2)
				{
					if (onError == Subscribe.NopError)
					{
						UniTaskScheduler.PublishUnobservedTaskException(ex2);
						goto IL_0109;
					}
					if (ex2 is OperationCanceledException)
					{
						goto IL_0109;
					}
					onError(ex2);
				}
				goto IL_011E;
				IL_0109:
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_011E:
			if (e != null)
			{
				await e.DisposeAsync();
			}
			object obj2 = obj;
			if (obj2 != null)
			{
				Exception ex3 = obj2 as Exception;
				if (ex3 == null)
				{
					throw obj2;
				}
				ExceptionDispatchInfo.Capture(ex3).Throw();
			}
			if (num != 1)
			{
				obj = null;
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
		public static async UniTaskVoid SubscribeCore<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTaskVoid> onNext, Action<Exception> onError, Action onCompleted, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			try
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
						try
						{
							onNext(e.Current).Forget();
						}
						catch (Exception ex)
						{
							UniTaskScheduler.PublishUnobservedTaskException(ex);
						}
					}
					onCompleted();
				}
				catch (Exception ex2)
				{
					if (onError == Subscribe.NopError)
					{
						UniTaskScheduler.PublishUnobservedTaskException(ex2);
						goto IL_0115;
					}
					if (ex2 is OperationCanceledException)
					{
						goto IL_0115;
					}
					onError(ex2);
				}
				goto IL_012A;
				IL_0115:
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_012A:
			if (e != null)
			{
				await e.DisposeAsync();
			}
			object obj2 = obj;
			if (obj2 != null)
			{
				Exception ex3 = obj2 as Exception;
				if (ex3 == null)
				{
					throw obj2;
				}
				ExceptionDispatchInfo.Capture(ex3).Throw();
			}
			if (num != 1)
			{
				obj = null;
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000C718 File Offset: 0x0000A918
		public static async UniTaskVoid SubscribeCore<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTaskVoid> onNext, Action<Exception> onError, Action onCompleted, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			try
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
						try
						{
							onNext(e.Current, cancellationToken).Forget();
						}
						catch (Exception ex)
						{
							UniTaskScheduler.PublishUnobservedTaskException(ex);
						}
					}
					onCompleted();
				}
				catch (Exception ex2)
				{
					if (onError == Subscribe.NopError)
					{
						UniTaskScheduler.PublishUnobservedTaskException(ex2);
						goto IL_011B;
					}
					if (ex2 is OperationCanceledException)
					{
						goto IL_011B;
					}
					onError(ex2);
				}
				goto IL_0130;
				IL_011B:
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_0130:
			if (e != null)
			{
				await e.DisposeAsync();
			}
			object obj2 = obj;
			if (obj2 != null)
			{
				Exception ex3 = obj2 as Exception;
				if (ex3 == null)
				{
					throw obj2;
				}
				ExceptionDispatchInfo.Capture(ex3).Throw();
			}
			if (num != 1)
			{
				obj = null;
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000C77C File Offset: 0x0000A97C
		public static async UniTaskVoid SubscribeCore<TSource>(IUniTaskAsyncEnumerable<TSource> source, IObserver<TSource> observer, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			try
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
						try
						{
							observer.OnNext(e.Current);
						}
						catch (Exception ex)
						{
							UniTaskScheduler.PublishUnobservedTaskException(ex);
						}
					}
					observer.OnCompleted();
				}
				catch (Exception ex2)
				{
					if (ex2 is OperationCanceledException)
					{
						goto IL_00EF;
					}
					observer.OnError(ex2);
				}
				goto IL_0104;
				IL_00EF:
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_0104:
			if (e != null)
			{
				await e.DisposeAsync();
			}
			object obj2 = obj;
			if (obj2 != null)
			{
				Exception ex3 = obj2 as Exception;
				if (ex3 == null)
				{
					throw obj2;
				}
				ExceptionDispatchInfo.Capture(ex3).Throw();
			}
			if (num != 1)
			{
				obj = null;
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000C7D0 File Offset: 0x0000A9D0
		public static async UniTaskVoid SubscribeAwaitCore<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask> onNext, Action<Exception> onError, Action onCompleted, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			try
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
						try
						{
							await onNext(e.Current);
						}
						catch (Exception ex)
						{
							UniTaskScheduler.PublishUnobservedTaskException(ex);
						}
					}
					onCompleted();
				}
				catch (Exception ex2)
				{
					if (onError == Subscribe.NopError)
					{
						UniTaskScheduler.PublishUnobservedTaskException(ex2);
						goto IL_0173;
					}
					if (ex2 is OperationCanceledException)
					{
						goto IL_0173;
					}
					onError(ex2);
				}
				goto IL_0188;
				IL_0173:
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_0188:
			if (e != null)
			{
				await e.DisposeAsync();
			}
			object obj2 = obj;
			if (obj2 != null)
			{
				Exception ex3 = obj2 as Exception;
				if (ex3 == null)
				{
					throw obj2;
				}
				ExceptionDispatchInfo.Capture(ex3).Throw();
			}
			if (num != 1)
			{
				obj = null;
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000C834 File Offset: 0x0000AA34
		public static async UniTaskVoid SubscribeAwaitCore<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask> onNext, Action<Exception> onError, Action onCompleted, CancellationToken cancellationToken)
		{
			IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			int num = 0;
			try
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
						try
						{
							await onNext(e.Current, cancellationToken);
						}
						catch (Exception ex)
						{
							UniTaskScheduler.PublishUnobservedTaskException(ex);
						}
					}
					onCompleted();
				}
				catch (Exception ex2)
				{
					if (onError == Subscribe.NopError)
					{
						UniTaskScheduler.PublishUnobservedTaskException(ex2);
						goto IL_017C;
					}
					if (ex2 is OperationCanceledException)
					{
						goto IL_017C;
					}
					onError(ex2);
				}
				goto IL_0191;
				IL_017C:
				num = 1;
			}
			catch (object obj)
			{
			}
			IL_0191:
			if (e != null)
			{
				await e.DisposeAsync();
			}
			object obj2 = obj;
			if (obj2 != null)
			{
				Exception ex3 = obj2 as Exception;
				if (ex3 == null)
				{
					throw obj2;
				}
				ExceptionDispatchInfo.Capture(ex3).Throw();
			}
			if (num != 1)
			{
				obj = null;
			}
		}

		// Token: 0x0400015F RID: 351
		public static readonly Action<Exception> NopError = delegate(Exception _)
		{
		};

		// Token: 0x04000160 RID: 352
		public static readonly Action NopCompleted = delegate
		{
		};
	}
}
