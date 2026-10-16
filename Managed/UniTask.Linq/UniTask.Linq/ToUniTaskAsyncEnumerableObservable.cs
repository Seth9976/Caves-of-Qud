using System;
using System.Collections.Generic;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x0200007F RID: 127
	internal class ToUniTaskAsyncEnumerableObservable<T> : IUniTaskAsyncEnumerable<T>
	{
		// Token: 0x060003BE RID: 958 RVA: 0x0000DDD2 File Offset: 0x0000BFD2
		public ToUniTaskAsyncEnumerableObservable(IObservable<T> source)
		{
			this.source = source;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000DDE1 File Offset: 0x0000BFE1
		public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
		{
			return new ToUniTaskAsyncEnumerableObservable<T>._ToUniTaskAsyncEnumerableObservable(this.source, cancellationToken);
		}

		// Token: 0x0400017B RID: 379
		private readonly IObservable<T> source;

		// Token: 0x020001F3 RID: 499
		private class _ToUniTaskAsyncEnumerableObservable : MoveNextSource, IUniTaskAsyncEnumerator<T>, IUniTaskAsyncDisposable, IObserver<T>
		{
			// Token: 0x060008BD RID: 2237 RVA: 0x0004CEAA File Offset: 0x0004B0AA
			public _ToUniTaskAsyncEnumerableObservable(IObservable<T> source, CancellationToken cancellationToken)
			{
				this.source = source;
				this.cancellationToken = cancellationToken;
				this.queuedResult = new Queue<T>();
				if (cancellationToken.CanBeCanceled)
				{
					this.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(ToUniTaskAsyncEnumerableObservable<T>._ToUniTaskAsyncEnumerableObservable.OnCanceledDelegate, this);
				}
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x060008BE RID: 2238 RVA: 0x0004CEE8 File Offset: 0x0004B0E8
			public T Current
			{
				get
				{
					if (this.useCachedCurrent)
					{
						return this.current;
					}
					Queue<T> queue = this.queuedResult;
					T t;
					lock (queue)
					{
						if (this.queuedResult.Count != 0)
						{
							this.current = this.queuedResult.Dequeue();
							this.useCachedCurrent = true;
							t = this.current;
						}
						else
						{
							t = default(T);
						}
					}
					return t;
				}
			}

			// Token: 0x060008BF RID: 2239 RVA: 0x0004CF6C File Offset: 0x0004B16C
			public UniTask<bool> MoveNextAsync()
			{
				Queue<T> queue = this.queuedResult;
				UniTask<bool> uniTask;
				lock (queue)
				{
					this.useCachedCurrent = false;
					if (this.cancellationToken.IsCancellationRequested)
					{
						uniTask = UniTask.FromCanceled<bool>(this.cancellationToken);
					}
					else
					{
						if (this.subscription == null)
						{
							this.subscription = this.source.Subscribe(this);
						}
						if (this.error != null)
						{
							uniTask = UniTask.FromException<bool>(this.error);
						}
						else if (this.queuedResult.Count != 0)
						{
							uniTask = CompletedTasks.True;
						}
						else if (this.subscribeCompleted)
						{
							uniTask = CompletedTasks.False;
						}
						else
						{
							this.completionSource.Reset();
							uniTask = new UniTask<bool>(this, this.completionSource.Version);
						}
					}
				}
				return uniTask;
			}

			// Token: 0x060008C0 RID: 2240 RVA: 0x0004D03C File Offset: 0x0004B23C
			public UniTask DisposeAsync()
			{
				this.subscription.Dispose();
				this.cancellationTokenRegistration.Dispose();
				this.completionSource.Reset();
				return default(UniTask);
			}

			// Token: 0x060008C1 RID: 2241 RVA: 0x0004D074 File Offset: 0x0004B274
			public void OnCompleted()
			{
				Queue<T> queue = this.queuedResult;
				lock (queue)
				{
					this.subscribeCompleted = true;
					this.completionSource.TrySetResult(false);
				}
			}

			// Token: 0x060008C2 RID: 2242 RVA: 0x0004D0C4 File Offset: 0x0004B2C4
			public void OnError(Exception error)
			{
				Queue<T> queue = this.queuedResult;
				lock (queue)
				{
					this.error = error;
					this.completionSource.TrySetException(error);
				}
			}

			// Token: 0x060008C3 RID: 2243 RVA: 0x0004D114 File Offset: 0x0004B314
			public void OnNext(T value)
			{
				Queue<T> queue = this.queuedResult;
				lock (queue)
				{
					this.queuedResult.Enqueue(value);
					this.completionSource.TrySetResult(true);
				}
			}

			// Token: 0x060008C4 RID: 2244 RVA: 0x0004D168 File Offset: 0x0004B368
			private static void OnCanceled(object state)
			{
				ToUniTaskAsyncEnumerableObservable<T>._ToUniTaskAsyncEnumerableObservable toUniTaskAsyncEnumerableObservable = (ToUniTaskAsyncEnumerableObservable<T>._ToUniTaskAsyncEnumerableObservable)state;
				Queue<T> queue = toUniTaskAsyncEnumerableObservable.queuedResult;
				lock (queue)
				{
					toUniTaskAsyncEnumerableObservable.completionSource.TrySetCanceled(toUniTaskAsyncEnumerableObservable.cancellationToken);
				}
			}

			// Token: 0x040012E8 RID: 4840
			private static readonly Action<object> OnCanceledDelegate = new Action<object>(ToUniTaskAsyncEnumerableObservable<T>._ToUniTaskAsyncEnumerableObservable.OnCanceled);

			// Token: 0x040012E9 RID: 4841
			private readonly IObservable<T> source;

			// Token: 0x040012EA RID: 4842
			private CancellationToken cancellationToken;

			// Token: 0x040012EB RID: 4843
			private bool useCachedCurrent;

			// Token: 0x040012EC RID: 4844
			private T current;

			// Token: 0x040012ED RID: 4845
			private bool subscribeCompleted;

			// Token: 0x040012EE RID: 4846
			private readonly Queue<T> queuedResult;

			// Token: 0x040012EF RID: 4847
			private Exception error;

			// Token: 0x040012F0 RID: 4848
			private IDisposable subscription;

			// Token: 0x040012F1 RID: 4849
			private CancellationTokenRegistration cancellationTokenRegistration;
		}
	}
}
