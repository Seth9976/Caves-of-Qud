using System;
using System.Threading;
using System.Threading.Tasks.Sources;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	public class AsyncReactiveProperty<T> : IAsyncReactiveProperty<T>, IReadOnlyAsyncReactiveProperty<T>, IUniTaskAsyncEnumerable<T>, IDisposable
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000253B File Offset: 0x0000073B
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002543 File Offset: 0x00000743
		public T Value
		{
			get
			{
				return this.latestValue;
			}
			set
			{
				this.latestValue = value;
				this.triggerEvent.SetResult(value);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002558 File Offset: 0x00000758
		public AsyncReactiveProperty(T value)
		{
			this.latestValue = value;
			this.triggerEvent = default(TriggerEvent<T>);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002573 File Offset: 0x00000773
		public IUniTaskAsyncEnumerable<T> WithoutCurrent()
		{
			return new AsyncReactiveProperty<T>.WithoutCurrentEnumerable(this);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000257B File Offset: 0x0000077B
		public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken)
		{
			return new AsyncReactiveProperty<T>.Enumerator(this, cancellationToken, true);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002585 File Offset: 0x00000785
		public void Dispose()
		{
			this.triggerEvent.SetCompleted();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002592 File Offset: 0x00000792
		public static implicit operator T(AsyncReactiveProperty<T> value)
		{
			return value.Value;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000259C File Offset: 0x0000079C
		public override string ToString()
		{
			if (AsyncReactiveProperty<T>.isValueType)
			{
				return this.latestValue.ToString();
			}
			ref T ptr = ref this.latestValue;
			T t = default(T);
			if (t == null)
			{
				t = this.latestValue;
				ptr = ref t;
				if (t == null)
				{
					return null;
				}
			}
			return ptr.ToString();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025F8 File Offset: 0x000007F8
		public UniTask<T> WaitAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			short num;
			return new UniTask<T>(AsyncReactiveProperty<T>.WaitAsyncSource.Create(this, cancellationToken, out num), num);
		}

		// Token: 0x0400000E RID: 14
		private TriggerEvent<T> triggerEvent;

		// Token: 0x0400000F RID: 15
		[SerializeField]
		private T latestValue;

		// Token: 0x04000010 RID: 16
		private static bool isValueType = typeof(T).IsValueType;

		// Token: 0x02000130 RID: 304
		private sealed class WaitAsyncSource : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>, ITriggerHandler<T>, ITaskPoolNode<AsyncReactiveProperty<T>.WaitAsyncSource>
		{
			// Token: 0x17000048 RID: 72
			// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00010155 File Offset: 0x0000E355
			ref AsyncReactiveProperty<T>.WaitAsyncSource ITaskPoolNode<AsyncReactiveProperty<T>.WaitAsyncSource>.NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x060006F2 RID: 1778 RVA: 0x0001015D File Offset: 0x0000E35D
			static WaitAsyncSource()
			{
				TaskPool.RegisterSizeGetter(typeof(AsyncReactiveProperty<T>.WaitAsyncSource), () => AsyncReactiveProperty<T>.WaitAsyncSource.pool.Size);
			}

			// Token: 0x060006F3 RID: 1779 RVA: 0x0001018F File Offset: 0x0000E38F
			private WaitAsyncSource()
			{
			}

			// Token: 0x060006F4 RID: 1780 RVA: 0x00010198 File Offset: 0x0000E398
			public static IUniTaskSource<T> Create(AsyncReactiveProperty<T> parent, CancellationToken cancellationToken, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<T>.CreateFromCanceled(cancellationToken, out token);
				}
				AsyncReactiveProperty<T>.WaitAsyncSource waitAsyncSource;
				if (!AsyncReactiveProperty<T>.WaitAsyncSource.pool.TryPop(out waitAsyncSource))
				{
					waitAsyncSource = new AsyncReactiveProperty<T>.WaitAsyncSource();
				}
				waitAsyncSource.parent = parent;
				waitAsyncSource.cancellationToken = cancellationToken;
				if (cancellationToken.CanBeCanceled)
				{
					waitAsyncSource.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(AsyncReactiveProperty<T>.WaitAsyncSource.cancellationCallback, waitAsyncSource);
				}
				waitAsyncSource.parent.triggerEvent.Add(waitAsyncSource);
				token = waitAsyncSource.core.Version;
				return waitAsyncSource;
			}

			// Token: 0x060006F5 RID: 1781 RVA: 0x00010214 File Offset: 0x0000E414
			private bool TryReturn()
			{
				this.core.Reset();
				this.cancellationTokenRegistration.Dispose();
				this.cancellationTokenRegistration = default(CancellationTokenRegistration);
				this.parent.triggerEvent.Remove(this);
				this.parent = null;
				this.cancellationToken = default(CancellationToken);
				return AsyncReactiveProperty<T>.WaitAsyncSource.pool.TryPush(this);
			}

			// Token: 0x060006F6 RID: 1782 RVA: 0x00010272 File Offset: 0x0000E472
			private static void CancellationCallback(object state)
			{
				AsyncReactiveProperty<T>.WaitAsyncSource waitAsyncSource = (AsyncReactiveProperty<T>.WaitAsyncSource)state;
				waitAsyncSource.OnCanceled(waitAsyncSource.cancellationToken);
			}

			// Token: 0x060006F7 RID: 1783 RVA: 0x00010288 File Offset: 0x0000E488
			public T GetResult(short token)
			{
				T result;
				try
				{
					result = this.core.GetResult(token);
				}
				finally
				{
					this.TryReturn();
				}
				return result;
			}

			// Token: 0x060006F8 RID: 1784 RVA: 0x000102C0 File Offset: 0x0000E4C0
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x060006F9 RID: 1785 RVA: 0x000102CA File Offset: 0x0000E4CA
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060006FA RID: 1786 RVA: 0x000102DA File Offset: 0x0000E4DA
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060006FB RID: 1787 RVA: 0x000102E8 File Offset: 0x0000E4E8
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x060006FC RID: 1788 RVA: 0x000102F5 File Offset: 0x0000E4F5
			// (set) Token: 0x060006FD RID: 1789 RVA: 0x000102FD File Offset: 0x0000E4FD
			ITriggerHandler<T> ITriggerHandler<T>.Prev { get; set; }

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x060006FE RID: 1790 RVA: 0x00010306 File Offset: 0x0000E506
			// (set) Token: 0x060006FF RID: 1791 RVA: 0x0001030E File Offset: 0x0000E50E
			ITriggerHandler<T> ITriggerHandler<T>.Next { get; set; }

			// Token: 0x06000700 RID: 1792 RVA: 0x00010317 File Offset: 0x0000E517
			public void OnCanceled(CancellationToken cancellationToken)
			{
				this.core.TrySetCanceled(cancellationToken);
			}

			// Token: 0x06000701 RID: 1793 RVA: 0x00010326 File Offset: 0x0000E526
			public void OnCompleted()
			{
				this.core.TrySetCanceled(CancellationToken.None);
			}

			// Token: 0x06000702 RID: 1794 RVA: 0x00010339 File Offset: 0x0000E539
			public void OnError(Exception ex)
			{
				this.core.TrySetException(ex);
			}

			// Token: 0x06000703 RID: 1795 RVA: 0x00010348 File Offset: 0x0000E548
			public void OnNext(T value)
			{
				this.core.TrySetResult(value);
			}

			// Token: 0x04000176 RID: 374
			private static Action<object> cancellationCallback = new Action<object>(AsyncReactiveProperty<T>.WaitAsyncSource.CancellationCallback);

			// Token: 0x04000177 RID: 375
			private static TaskPool<AsyncReactiveProperty<T>.WaitAsyncSource> pool;

			// Token: 0x04000178 RID: 376
			private AsyncReactiveProperty<T>.WaitAsyncSource nextNode;

			// Token: 0x04000179 RID: 377
			private AsyncReactiveProperty<T> parent;

			// Token: 0x0400017A RID: 378
			private CancellationToken cancellationToken;

			// Token: 0x0400017B RID: 379
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400017C RID: 380
			private UniTaskCompletionSourceCore<T> core;
		}

		// Token: 0x02000131 RID: 305
		private sealed class WithoutCurrentEnumerable : IUniTaskAsyncEnumerable<T>
		{
			// Token: 0x06000704 RID: 1796 RVA: 0x00010357 File Offset: 0x0000E557
			public WithoutCurrentEnumerable(AsyncReactiveProperty<T> parent)
			{
				this.parent = parent;
			}

			// Token: 0x06000705 RID: 1797 RVA: 0x00010366 File Offset: 0x0000E566
			public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
			{
				return new AsyncReactiveProperty<T>.Enumerator(this.parent, cancellationToken, false);
			}

			// Token: 0x0400017F RID: 383
			private readonly AsyncReactiveProperty<T> parent;
		}

		// Token: 0x02000132 RID: 306
		private sealed class Enumerator : MoveNextSource, IUniTaskAsyncEnumerator<T>, IUniTaskAsyncDisposable, ITriggerHandler<T>
		{
			// Token: 0x06000706 RID: 1798 RVA: 0x00010378 File Offset: 0x0000E578
			public Enumerator(AsyncReactiveProperty<T> parent, CancellationToken cancellationToken, bool publishCurrentValue)
			{
				this.parent = parent;
				this.cancellationToken = cancellationToken;
				this.firstCall = publishCurrentValue;
				parent.triggerEvent.Add(this);
				if (cancellationToken.CanBeCanceled)
				{
					this.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(AsyncReactiveProperty<T>.Enumerator.cancellationCallback, this);
				}
			}

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x06000707 RID: 1799 RVA: 0x000103C7 File Offset: 0x0000E5C7
			public T Current
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x06000708 RID: 1800 RVA: 0x000103CF File Offset: 0x0000E5CF
			// (set) Token: 0x06000709 RID: 1801 RVA: 0x000103D7 File Offset: 0x0000E5D7
			ITriggerHandler<T> ITriggerHandler<T>.Prev { get; set; }

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x0600070A RID: 1802 RVA: 0x000103E0 File Offset: 0x0000E5E0
			// (set) Token: 0x0600070B RID: 1803 RVA: 0x000103E8 File Offset: 0x0000E5E8
			ITriggerHandler<T> ITriggerHandler<T>.Next { get; set; }

			// Token: 0x0600070C RID: 1804 RVA: 0x000103F4 File Offset: 0x0000E5F4
			public UniTask<bool> MoveNextAsync()
			{
				if (this.firstCall)
				{
					this.firstCall = false;
					this.value = this.parent.Value;
					return CompletedTasks.True;
				}
				this.completionSource.Reset();
				return new UniTask<bool>(this, this.completionSource.Version);
			}

			// Token: 0x0600070D RID: 1805 RVA: 0x00010444 File Offset: 0x0000E644
			public UniTask DisposeAsync()
			{
				if (!this.isDisposed)
				{
					this.isDisposed = true;
					this.completionSource.TrySetCanceled(this.cancellationToken);
					this.parent.triggerEvent.Remove(this);
				}
				return default(UniTask);
			}

			// Token: 0x0600070E RID: 1806 RVA: 0x0001048C File Offset: 0x0000E68C
			public void OnNext(T value)
			{
				this.value = value;
				this.completionSource.TrySetResult(true);
			}

			// Token: 0x0600070F RID: 1807 RVA: 0x000104A2 File Offset: 0x0000E6A2
			public void OnCanceled(CancellationToken cancellationToken)
			{
				this.DisposeAsync().Forget();
			}

			// Token: 0x06000710 RID: 1808 RVA: 0x000104AF File Offset: 0x0000E6AF
			public void OnCompleted()
			{
				this.completionSource.TrySetResult(false);
			}

			// Token: 0x06000711 RID: 1809 RVA: 0x000104BE File Offset: 0x0000E6BE
			public void OnError(Exception ex)
			{
				this.completionSource.TrySetException(ex);
			}

			// Token: 0x06000712 RID: 1810 RVA: 0x000104CD File Offset: 0x0000E6CD
			private static void CancellationCallback(object state)
			{
				((AsyncReactiveProperty<T>.Enumerator)state).DisposeAsync().Forget();
			}

			// Token: 0x04000180 RID: 384
			private static Action<object> cancellationCallback = new Action<object>(AsyncReactiveProperty<T>.Enumerator.CancellationCallback);

			// Token: 0x04000181 RID: 385
			private readonly AsyncReactiveProperty<T> parent;

			// Token: 0x04000182 RID: 386
			private readonly CancellationToken cancellationToken;

			// Token: 0x04000183 RID: 387
			private readonly CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000184 RID: 388
			private T value;

			// Token: 0x04000185 RID: 389
			private bool isDisposed;

			// Token: 0x04000186 RID: 390
			private bool firstCall;
		}
	}
}
