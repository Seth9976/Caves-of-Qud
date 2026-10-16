using System;
using System.Runtime.ExceptionServices;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200011E RID: 286
	internal sealed class AsyncSubject<T> : IObservable<T>, IObserver<T>
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0000F4BB File Offset: 0x0000D6BB
		public T Value
		{
			get
			{
				this.ThrowIfDisposed();
				if (!this.isStopped)
				{
					throw new InvalidOperationException("AsyncSubject is not completed yet");
				}
				if (this.lastError != null)
				{
					ExceptionDispatchInfo.Capture(this.lastError).Throw();
				}
				return this.lastValue;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
		public bool HasObservers
		{
			get
			{
				return !(this.outObserver is EmptyObserver<T>) && !this.isStopped && !this.isDisposed;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0000F516 File Offset: 0x0000D716
		public bool IsCompleted
		{
			get
			{
				return this.isStopped;
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0000F520 File Offset: 0x0000D720
		public void OnCompleted()
		{
			object obj = this.observerLock;
			IObserver<T> observer;
			T t;
			bool flag2;
			lock (obj)
			{
				this.ThrowIfDisposed();
				if (this.isStopped)
				{
					return;
				}
				observer = this.outObserver;
				this.outObserver = EmptyObserver<T>.Instance;
				this.isStopped = true;
				t = this.lastValue;
				flag2 = this.hasValue;
			}
			if (flag2)
			{
				observer.OnNext(t);
				observer.OnCompleted();
				return;
			}
			observer.OnCompleted();
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0000F5AC File Offset: 0x0000D7AC
		public void OnError(Exception error)
		{
			if (error == null)
			{
				throw new ArgumentNullException("error");
			}
			object obj = this.observerLock;
			IObserver<T> observer;
			lock (obj)
			{
				this.ThrowIfDisposed();
				if (this.isStopped)
				{
					return;
				}
				observer = this.outObserver;
				this.outObserver = EmptyObserver<T>.Instance;
				this.isStopped = true;
				this.lastError = error;
			}
			observer.OnError(error);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0000F62C File Offset: 0x0000D82C
		public void OnNext(T value)
		{
			object obj = this.observerLock;
			lock (obj)
			{
				this.ThrowIfDisposed();
				if (!this.isStopped)
				{
					this.hasValue = true;
					this.lastValue = value;
				}
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0000F684 File Offset: 0x0000D884
		public IDisposable Subscribe(IObserver<T> observer)
		{
			if (observer == null)
			{
				throw new ArgumentNullException("observer");
			}
			Exception ex = null;
			T t = default(T);
			bool flag = false;
			object obj = this.observerLock;
			lock (obj)
			{
				this.ThrowIfDisposed();
				if (!this.isStopped)
				{
					ListObserver<T> listObserver = this.outObserver as ListObserver<T>;
					if (listObserver != null)
					{
						this.outObserver = listObserver.Add(observer);
					}
					else
					{
						IObserver<T> observer2 = this.outObserver;
						if (observer2 is EmptyObserver<T>)
						{
							this.outObserver = observer;
						}
						else
						{
							this.outObserver = new ListObserver<T>(new ImmutableList<IObserver<T>>(new IObserver<T>[] { observer2, observer }));
						}
					}
					return new AsyncSubject<T>.Subscription(this, observer);
				}
				ex = this.lastError;
				t = this.lastValue;
				flag = this.hasValue;
			}
			if (ex != null)
			{
				observer.OnError(ex);
			}
			else if (flag)
			{
				observer.OnNext(t);
				observer.OnCompleted();
			}
			else
			{
				observer.OnCompleted();
			}
			return EmptyDisposable.Instance;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0000F790 File Offset: 0x0000D990
		public void Dispose()
		{
			object obj = this.observerLock;
			lock (obj)
			{
				this.isDisposed = true;
				this.outObserver = DisposedObserver<T>.Instance;
				this.lastError = null;
				this.lastValue = default(T);
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0000F7F0 File Offset: 0x0000D9F0
		private void ThrowIfDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("");
			}
		}

		// Token: 0x0400014E RID: 334
		private object observerLock = new object();

		// Token: 0x0400014F RID: 335
		private T lastValue;

		// Token: 0x04000150 RID: 336
		private bool hasValue;

		// Token: 0x04000151 RID: 337
		private bool isStopped;

		// Token: 0x04000152 RID: 338
		private bool isDisposed;

		// Token: 0x04000153 RID: 339
		private Exception lastError;

		// Token: 0x04000154 RID: 340
		private IObserver<T> outObserver = EmptyObserver<T>.Instance;

		// Token: 0x02000221 RID: 545
		private class Subscription : IDisposable
		{
			// Token: 0x06000C06 RID: 3078 RVA: 0x0002B553 File Offset: 0x00029753
			public Subscription(AsyncSubject<T> parent, IObserver<T> unsubscribeTarget)
			{
				this.parent = parent;
				this.unsubscribeTarget = unsubscribeTarget;
			}

			// Token: 0x06000C07 RID: 3079 RVA: 0x0002B574 File Offset: 0x00029774
			public void Dispose()
			{
				object obj = this.gate;
				lock (obj)
				{
					if (this.parent != null)
					{
						object observerLock = this.parent.observerLock;
						lock (observerLock)
						{
							ListObserver<T> listObserver = this.parent.outObserver as ListObserver<T>;
							if (listObserver != null)
							{
								this.parent.outObserver = listObserver.Remove(this.unsubscribeTarget);
							}
							else
							{
								this.parent.outObserver = EmptyObserver<T>.Instance;
							}
							this.unsubscribeTarget = null;
							this.parent = null;
						}
					}
				}
			}

			// Token: 0x04000561 RID: 1377
			private readonly object gate = new object();

			// Token: 0x04000562 RID: 1378
			private AsyncSubject<T> parent;

			// Token: 0x04000563 RID: 1379
			private IObserver<T> unsubscribeTarget;
		}
	}
}
