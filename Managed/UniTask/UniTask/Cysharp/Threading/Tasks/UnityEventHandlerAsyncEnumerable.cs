using System;
using System.Threading;
using UnityEngine.Events;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000067 RID: 103
	public class UnityEventHandlerAsyncEnumerable : IUniTaskAsyncEnumerable<AsyncUnit>
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0000A398 File Offset: 0x00008598
		public UnityEventHandlerAsyncEnumerable(UnityEvent unityEvent, CancellationToken cancellationToken)
		{
			this.unityEvent = unityEvent;
			this.cancellationToken1 = cancellationToken;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000A3AE File Offset: 0x000085AE
		public IUniTaskAsyncEnumerator<AsyncUnit> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this.cancellationToken1 == cancellationToken)
			{
				return new UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator(this.unityEvent, this.cancellationToken1, CancellationToken.None);
			}
			return new UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator(this.unityEvent, this.cancellationToken1, cancellationToken);
		}

		// Token: 0x040000E1 RID: 225
		private readonly UnityEvent unityEvent;

		// Token: 0x040000E2 RID: 226
		private readonly CancellationToken cancellationToken1;

		// Token: 0x02000201 RID: 513
		private class UnityEventHandlerAsyncEnumerator : MoveNextSource, IUniTaskAsyncEnumerator<AsyncUnit>, IUniTaskAsyncDisposable
		{
			// Token: 0x06000B98 RID: 2968 RVA: 0x00029AD8 File Offset: 0x00027CD8
			public UnityEventHandlerAsyncEnumerator(UnityEvent unityEvent, CancellationToken cancellationToken1, CancellationToken cancellationToken2)
			{
				this.unityEvent = unityEvent;
				this.cancellationToken1 = cancellationToken1;
				this.cancellationToken2 = cancellationToken2;
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x06000B99 RID: 2969 RVA: 0x00029AF8 File Offset: 0x00027CF8
			public AsyncUnit Current
			{
				get
				{
					return default(AsyncUnit);
				}
			}

			// Token: 0x06000B9A RID: 2970 RVA: 0x00029B10 File Offset: 0x00027D10
			public UniTask<bool> MoveNextAsync()
			{
				this.cancellationToken1.ThrowIfCancellationRequested();
				this.cancellationToken2.ThrowIfCancellationRequested();
				this.completionSource.Reset();
				if (this.unityAction == null)
				{
					this.unityAction = new UnityAction(this.Invoke);
					this.unityEvent.AddListener(this.unityAction);
					if (this.cancellationToken1.CanBeCanceled)
					{
						this.registration1 = this.cancellationToken1.RegisterWithoutCaptureExecutionContext(UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator.cancel1, this);
					}
					if (this.cancellationToken2.CanBeCanceled)
					{
						this.registration2 = this.cancellationToken2.RegisterWithoutCaptureExecutionContext(UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator.cancel2, this);
					}
				}
				return new UniTask<bool>(this, this.completionSource.Version);
			}

			// Token: 0x06000B9B RID: 2971 RVA: 0x00029BC2 File Offset: 0x00027DC2
			private void Invoke()
			{
				this.completionSource.TrySetResult(true);
			}

			// Token: 0x06000B9C RID: 2972 RVA: 0x00029BD4 File Offset: 0x00027DD4
			private static void OnCanceled1(object state)
			{
				UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator unityEventHandlerAsyncEnumerator = (UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator)state;
				try
				{
					unityEventHandlerAsyncEnumerator.completionSource.TrySetCanceled(unityEventHandlerAsyncEnumerator.cancellationToken1);
				}
				finally
				{
					unityEventHandlerAsyncEnumerator.DisposeAsync().Forget();
				}
			}

			// Token: 0x06000B9D RID: 2973 RVA: 0x00029C18 File Offset: 0x00027E18
			private static void OnCanceled2(object state)
			{
				UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator unityEventHandlerAsyncEnumerator = (UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator)state;
				try
				{
					unityEventHandlerAsyncEnumerator.completionSource.TrySetCanceled(unityEventHandlerAsyncEnumerator.cancellationToken2);
				}
				finally
				{
					unityEventHandlerAsyncEnumerator.DisposeAsync().Forget();
				}
			}

			// Token: 0x06000B9E RID: 2974 RVA: 0x00029C5C File Offset: 0x00027E5C
			public UniTask DisposeAsync()
			{
				if (!this.isDisposed)
				{
					this.isDisposed = true;
					this.registration1.Dispose();
					this.registration2.Dispose();
					this.unityEvent.RemoveListener(this.unityAction);
					this.completionSource.TrySetCanceled(default(CancellationToken));
				}
				return default(UniTask);
			}

			// Token: 0x040004F6 RID: 1270
			private static readonly Action<object> cancel1 = new Action<object>(UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator.OnCanceled1);

			// Token: 0x040004F7 RID: 1271
			private static readonly Action<object> cancel2 = new Action<object>(UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator.OnCanceled2);

			// Token: 0x040004F8 RID: 1272
			private readonly UnityEvent unityEvent;

			// Token: 0x040004F9 RID: 1273
			private CancellationToken cancellationToken1;

			// Token: 0x040004FA RID: 1274
			private CancellationToken cancellationToken2;

			// Token: 0x040004FB RID: 1275
			private UnityAction unityAction;

			// Token: 0x040004FC RID: 1276
			private CancellationTokenRegistration registration1;

			// Token: 0x040004FD RID: 1277
			private CancellationTokenRegistration registration2;

			// Token: 0x040004FE RID: 1278
			private bool isDisposed;
		}
	}
}
