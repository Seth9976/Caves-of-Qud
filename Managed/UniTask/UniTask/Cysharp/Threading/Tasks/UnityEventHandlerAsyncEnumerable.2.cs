using System;
using System.Threading;
using UnityEngine.Events;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000068 RID: 104
	public class UnityEventHandlerAsyncEnumerable<T> : IUniTaskAsyncEnumerable<T>
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x0000A3E7 File Offset: 0x000085E7
		public UnityEventHandlerAsyncEnumerable(UnityEvent<T> unityEvent, CancellationToken cancellationToken)
		{
			this.unityEvent = unityEvent;
			this.cancellationToken1 = cancellationToken;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000A3FD File Offset: 0x000085FD
		public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this.cancellationToken1 == cancellationToken)
			{
				return new UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator(this.unityEvent, this.cancellationToken1, CancellationToken.None);
			}
			return new UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator(this.unityEvent, this.cancellationToken1, cancellationToken);
		}

		// Token: 0x040000E3 RID: 227
		private readonly UnityEvent<T> unityEvent;

		// Token: 0x040000E4 RID: 228
		private readonly CancellationToken cancellationToken1;

		// Token: 0x02000202 RID: 514
		private class UnityEventHandlerAsyncEnumerator : MoveNextSource, IUniTaskAsyncEnumerator<T>, IUniTaskAsyncDisposable
		{
			// Token: 0x06000BA0 RID: 2976 RVA: 0x00029CE1 File Offset: 0x00027EE1
			public UnityEventHandlerAsyncEnumerator(UnityEvent<T> unityEvent, CancellationToken cancellationToken1, CancellationToken cancellationToken2)
			{
				this.unityEvent = unityEvent;
				this.cancellationToken1 = cancellationToken1;
				this.cancellationToken2 = cancellationToken2;
			}

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00029CFE File Offset: 0x00027EFE
			// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x00029D06 File Offset: 0x00027F06
			public T Current { get; private set; }

			// Token: 0x06000BA3 RID: 2979 RVA: 0x00029D10 File Offset: 0x00027F10
			public UniTask<bool> MoveNextAsync()
			{
				this.cancellationToken1.ThrowIfCancellationRequested();
				this.cancellationToken2.ThrowIfCancellationRequested();
				this.completionSource.Reset();
				if (this.unityAction == null)
				{
					this.unityAction = new UnityAction<T>(this.Invoke);
					this.unityEvent.AddListener(this.unityAction);
					if (this.cancellationToken1.CanBeCanceled)
					{
						this.registration1 = this.cancellationToken1.RegisterWithoutCaptureExecutionContext(UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator.cancel1, this);
					}
					if (this.cancellationToken2.CanBeCanceled)
					{
						this.registration2 = this.cancellationToken2.RegisterWithoutCaptureExecutionContext(UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator.cancel2, this);
					}
				}
				return new UniTask<bool>(this, this.completionSource.Version);
			}

			// Token: 0x06000BA4 RID: 2980 RVA: 0x00029DC2 File Offset: 0x00027FC2
			private void Invoke(T value)
			{
				this.Current = value;
				this.completionSource.TrySetResult(true);
			}

			// Token: 0x06000BA5 RID: 2981 RVA: 0x00029DD8 File Offset: 0x00027FD8
			private static void OnCanceled1(object state)
			{
				UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator unityEventHandlerAsyncEnumerator = (UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator)state;
				try
				{
					unityEventHandlerAsyncEnumerator.completionSource.TrySetCanceled(unityEventHandlerAsyncEnumerator.cancellationToken1);
				}
				finally
				{
					unityEventHandlerAsyncEnumerator.DisposeAsync().Forget();
				}
			}

			// Token: 0x06000BA6 RID: 2982 RVA: 0x00029E1C File Offset: 0x0002801C
			private static void OnCanceled2(object state)
			{
				UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator unityEventHandlerAsyncEnumerator = (UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator)state;
				try
				{
					unityEventHandlerAsyncEnumerator.completionSource.TrySetCanceled(unityEventHandlerAsyncEnumerator.cancellationToken2);
				}
				finally
				{
					unityEventHandlerAsyncEnumerator.DisposeAsync().Forget();
				}
			}

			// Token: 0x06000BA7 RID: 2983 RVA: 0x00029E60 File Offset: 0x00028060
			public UniTask DisposeAsync()
			{
				if (!this.isDisposed)
				{
					this.isDisposed = true;
					this.registration1.Dispose();
					this.registration2.Dispose();
					IDisposable disposable = this.unityEvent as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
					this.unityEvent.RemoveListener(this.unityAction);
					this.completionSource.TrySetCanceled(default(CancellationToken));
				}
				return default(UniTask);
			}

			// Token: 0x040004FF RID: 1279
			private static readonly Action<object> cancel1 = new Action<object>(UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator.OnCanceled1);

			// Token: 0x04000500 RID: 1280
			private static readonly Action<object> cancel2 = new Action<object>(UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator.OnCanceled2);

			// Token: 0x04000501 RID: 1281
			private readonly UnityEvent<T> unityEvent;

			// Token: 0x04000502 RID: 1282
			private CancellationToken cancellationToken1;

			// Token: 0x04000503 RID: 1283
			private CancellationToken cancellationToken2;

			// Token: 0x04000504 RID: 1284
			private UnityAction<T> unityAction;

			// Token: 0x04000505 RID: 1285
			private CancellationTokenRegistration registration1;

			// Token: 0x04000506 RID: 1286
			private CancellationTokenRegistration registration2;

			// Token: 0x04000507 RID: 1287
			private bool isDisposed;
		}
	}
}
