using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000070 RID: 112
	public abstract class AsyncTriggerBase<T> : MonoBehaviour, IUniTaskAsyncEnumerable<T>
	{
		// Token: 0x06000388 RID: 904 RVA: 0x0000B04B File Offset: 0x0000924B
		private void Awake()
		{
			this.calledAwake = true;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000B054 File Offset: 0x00009254
		private void OnDestroy()
		{
			if (this.calledDestroy)
			{
				return;
			}
			this.calledDestroy = true;
			this.triggerEvent.SetCompleted();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000B071 File Offset: 0x00009271
		internal void AddHandler(ITriggerHandler<T> handler)
		{
			if (!this.calledAwake)
			{
				PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, new AsyncTriggerBase<T>.AwakeMonitor(this));
			}
			this.triggerEvent.Add(handler);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000B093 File Offset: 0x00009293
		internal void RemoveHandler(ITriggerHandler<T> handler)
		{
			if (!this.calledAwake)
			{
				PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, new AsyncTriggerBase<T>.AwakeMonitor(this));
			}
			this.triggerEvent.Remove(handler);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000B0B5 File Offset: 0x000092B5
		protected void RaiseEvent(T value)
		{
			this.triggerEvent.SetResult(value);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000B0C3 File Offset: 0x000092C3
		public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
		{
			return new AsyncTriggerBase<T>.AsyncTriggerEnumerator(this, cancellationToken);
		}

		// Token: 0x040000F0 RID: 240
		private TriggerEvent<T> triggerEvent;

		// Token: 0x040000F1 RID: 241
		protected internal bool calledAwake;

		// Token: 0x040000F2 RID: 242
		protected internal bool calledDestroy;

		// Token: 0x0200020B RID: 523
		private sealed class AsyncTriggerEnumerator : MoveNextSource, IUniTaskAsyncEnumerator<T>, IUniTaskAsyncDisposable, ITriggerHandler<T>
		{
			// Token: 0x06000BBA RID: 3002 RVA: 0x0002AAB1 File Offset: 0x00028CB1
			public AsyncTriggerEnumerator(AsyncTriggerBase<T> parent, CancellationToken cancellationToken)
			{
				this.parent = parent;
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x06000BBB RID: 3003 RVA: 0x0002AAC7 File Offset: 0x00028CC7
			public void OnCanceled(CancellationToken cancellationToken = default(CancellationToken))
			{
				this.completionSource.TrySetCanceled(cancellationToken);
			}

			// Token: 0x06000BBC RID: 3004 RVA: 0x0002AAD6 File Offset: 0x00028CD6
			public void OnNext(T value)
			{
				this.Current = value;
				this.completionSource.TrySetResult(true);
			}

			// Token: 0x06000BBD RID: 3005 RVA: 0x0002AAEC File Offset: 0x00028CEC
			public void OnCompleted()
			{
				this.completionSource.TrySetResult(false);
			}

			// Token: 0x06000BBE RID: 3006 RVA: 0x0002AAFB File Offset: 0x00028CFB
			public void OnError(Exception ex)
			{
				this.completionSource.TrySetException(ex);
			}

			// Token: 0x06000BBF RID: 3007 RVA: 0x0002AB0C File Offset: 0x00028D0C
			private static void CancellationCallback(object state)
			{
				AsyncTriggerBase<T>.AsyncTriggerEnumerator asyncTriggerEnumerator = (AsyncTriggerBase<T>.AsyncTriggerEnumerator)state;
				asyncTriggerEnumerator.DisposeAsync().Forget();
				asyncTriggerEnumerator.completionSource.TrySetCanceled(asyncTriggerEnumerator.cancellationToken);
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0002AB3D File Offset: 0x00028D3D
			// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x0002AB45 File Offset: 0x00028D45
			public T Current { get; private set; }

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0002AB4E File Offset: 0x00028D4E
			// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x0002AB56 File Offset: 0x00028D56
			ITriggerHandler<T> ITriggerHandler<T>.Prev { get; set; }

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0002AB5F File Offset: 0x00028D5F
			// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x0002AB67 File Offset: 0x00028D67
			ITriggerHandler<T> ITriggerHandler<T>.Next { get; set; }

			// Token: 0x06000BC6 RID: 3014 RVA: 0x0002AB70 File Offset: 0x00028D70
			public UniTask<bool> MoveNextAsync()
			{
				this.cancellationToken.ThrowIfCancellationRequested();
				this.completionSource.Reset();
				if (!this.called)
				{
					this.called = true;
					this.parent.AddHandler(this);
					if (this.cancellationToken.CanBeCanceled)
					{
						this.registration = this.cancellationToken.RegisterWithoutCaptureExecutionContext(AsyncTriggerBase<T>.AsyncTriggerEnumerator.cancellationCallback, this);
					}
				}
				return new UniTask<bool>(this, this.completionSource.Version);
			}

			// Token: 0x06000BC7 RID: 3015 RVA: 0x0002ABE4 File Offset: 0x00028DE4
			public UniTask DisposeAsync()
			{
				if (!this.isDisposed)
				{
					this.isDisposed = true;
					this.registration.Dispose();
					this.parent.RemoveHandler(this);
				}
				return default(UniTask);
			}

			// Token: 0x04000545 RID: 1349
			private static Action<object> cancellationCallback = new Action<object>(AsyncTriggerBase<T>.AsyncTriggerEnumerator.CancellationCallback);

			// Token: 0x04000546 RID: 1350
			private readonly AsyncTriggerBase<T> parent;

			// Token: 0x04000547 RID: 1351
			private CancellationToken cancellationToken;

			// Token: 0x04000548 RID: 1352
			private CancellationTokenRegistration registration;

			// Token: 0x04000549 RID: 1353
			private bool called;

			// Token: 0x0400054A RID: 1354
			private bool isDisposed;
		}

		// Token: 0x0200020C RID: 524
		private class AwakeMonitor : IPlayerLoopItem
		{
			// Token: 0x06000BC9 RID: 3017 RVA: 0x0002AC33 File Offset: 0x00028E33
			public AwakeMonitor(AsyncTriggerBase<T> trigger)
			{
				this.trigger = trigger;
			}

			// Token: 0x06000BCA RID: 3018 RVA: 0x0002AC42 File Offset: 0x00028E42
			public bool MoveNext()
			{
				if (this.trigger.calledAwake)
				{
					return false;
				}
				if (this.trigger == null)
				{
					this.trigger.OnDestroy();
					return false;
				}
				return true;
			}

			// Token: 0x0400054E RID: 1358
			private readonly AsyncTriggerBase<T> trigger;
		}
	}
}
