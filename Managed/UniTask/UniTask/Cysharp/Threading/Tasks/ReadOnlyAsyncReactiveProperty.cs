using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000009 RID: 9
	public class ReadOnlyAsyncReactiveProperty<T> : IReadOnlyAsyncReactiveProperty<T>, IUniTaskAsyncEnumerable<T>, IDisposable
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000262A File Offset: 0x0000082A
		public T Value
		{
			get
			{
				return this.latestValue;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002634 File Offset: 0x00000834
		public ReadOnlyAsyncReactiveProperty(T initialValue, IUniTaskAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			this.latestValue = initialValue;
			this.ConsumeEnumerator(source, cancellationToken).Forget();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002660 File Offset: 0x00000860
		public ReadOnlyAsyncReactiveProperty(IUniTaskAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			this.ConsumeEnumerator(source, cancellationToken).Forget();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002684 File Offset: 0x00000884
		private async UniTaskVoid ConsumeEnumerator(IUniTaskAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			this.enumerator = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			try
			{
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = this.enumerator.MoveNextAsync().GetAwaiter();
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
					T t = this.enumerator.Current;
					this.latestValue = t;
					this.triggerEvent.SetResult(t);
				}
			}
			catch (object obj)
			{
			}
			await this.enumerator.DisposeAsync();
			this.enumerator = null;
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
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026D7 File Offset: 0x000008D7
		public IUniTaskAsyncEnumerable<T> WithoutCurrent()
		{
			return new ReadOnlyAsyncReactiveProperty<T>.WithoutCurrentEnumerable(this);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026DF File Offset: 0x000008DF
		public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken)
		{
			return new ReadOnlyAsyncReactiveProperty<T>.Enumerator(this, cancellationToken, true);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026E9 File Offset: 0x000008E9
		public void Dispose()
		{
			if (this.enumerator != null)
			{
				this.enumerator.DisposeAsync().Forget();
			}
			this.triggerEvent.SetCompleted();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000270E File Offset: 0x0000090E
		public static implicit operator T(ReadOnlyAsyncReactiveProperty<T> value)
		{
			return value.Value;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002718 File Offset: 0x00000918
		public override string ToString()
		{
			if (ReadOnlyAsyncReactiveProperty<T>.isValueType)
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

		// Token: 0x0600002F RID: 47 RVA: 0x00002774 File Offset: 0x00000974
		public UniTask<T> WaitAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			short num;
			return new UniTask<T>(ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource.Create(this, cancellationToken, out num), num);
		}

		// Token: 0x04000011 RID: 17
		private TriggerEvent<T> triggerEvent;

		// Token: 0x04000012 RID: 18
		private T latestValue;

		// Token: 0x04000013 RID: 19
		private IUniTaskAsyncEnumerator<T> enumerator;

		// Token: 0x04000014 RID: 20
		private static bool isValueType = typeof(T).IsValueType;

		// Token: 0x02000133 RID: 307
		private sealed class WaitAsyncSource : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>, ITriggerHandler<T>, ITaskPoolNode<ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource>
		{
			// Token: 0x1700004E RID: 78
			// (get) Token: 0x06000714 RID: 1812 RVA: 0x000104F2 File Offset: 0x0000E6F2
			ref ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource ITaskPoolNode<ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource>.NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000715 RID: 1813 RVA: 0x000104FA File Offset: 0x0000E6FA
			static WaitAsyncSource()
			{
				TaskPool.RegisterSizeGetter(typeof(ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource), () => ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource.pool.Size);
			}

			// Token: 0x06000716 RID: 1814 RVA: 0x0001052C File Offset: 0x0000E72C
			private WaitAsyncSource()
			{
			}

			// Token: 0x06000717 RID: 1815 RVA: 0x00010534 File Offset: 0x0000E734
			public static IUniTaskSource<T> Create(ReadOnlyAsyncReactiveProperty<T> parent, CancellationToken cancellationToken, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<T>.CreateFromCanceled(cancellationToken, out token);
				}
				ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource waitAsyncSource;
				if (!ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource.pool.TryPop(out waitAsyncSource))
				{
					waitAsyncSource = new ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource();
				}
				waitAsyncSource.parent = parent;
				waitAsyncSource.cancellationToken = cancellationToken;
				if (cancellationToken.CanBeCanceled)
				{
					waitAsyncSource.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource.cancellationCallback, waitAsyncSource);
				}
				waitAsyncSource.parent.triggerEvent.Add(waitAsyncSource);
				token = waitAsyncSource.core.Version;
				return waitAsyncSource;
			}

			// Token: 0x06000718 RID: 1816 RVA: 0x000105B0 File Offset: 0x0000E7B0
			private bool TryReturn()
			{
				this.core.Reset();
				this.cancellationTokenRegistration.Dispose();
				this.cancellationTokenRegistration = default(CancellationTokenRegistration);
				this.parent.triggerEvent.Remove(this);
				this.parent = null;
				this.cancellationToken = default(CancellationToken);
				return ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource.pool.TryPush(this);
			}

			// Token: 0x06000719 RID: 1817 RVA: 0x0001060E File Offset: 0x0000E80E
			private static void CancellationCallback(object state)
			{
				ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource waitAsyncSource = (ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource)state;
				waitAsyncSource.OnCanceled(waitAsyncSource.cancellationToken);
			}

			// Token: 0x0600071A RID: 1818 RVA: 0x00010624 File Offset: 0x0000E824
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

			// Token: 0x0600071B RID: 1819 RVA: 0x0001065C File Offset: 0x0000E85C
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0600071C RID: 1820 RVA: 0x00010666 File Offset: 0x0000E866
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600071D RID: 1821 RVA: 0x00010676 File Offset: 0x0000E876
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600071E RID: 1822 RVA: 0x00010684 File Offset: 0x0000E884
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x0600071F RID: 1823 RVA: 0x00010691 File Offset: 0x0000E891
			// (set) Token: 0x06000720 RID: 1824 RVA: 0x00010699 File Offset: 0x0000E899
			ITriggerHandler<T> ITriggerHandler<T>.Prev { get; set; }

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x06000721 RID: 1825 RVA: 0x000106A2 File Offset: 0x0000E8A2
			// (set) Token: 0x06000722 RID: 1826 RVA: 0x000106AA File Offset: 0x0000E8AA
			ITriggerHandler<T> ITriggerHandler<T>.Next { get; set; }

			// Token: 0x06000723 RID: 1827 RVA: 0x000106B3 File Offset: 0x0000E8B3
			public void OnCanceled(CancellationToken cancellationToken)
			{
				this.core.TrySetCanceled(cancellationToken);
			}

			// Token: 0x06000724 RID: 1828 RVA: 0x000106C2 File Offset: 0x0000E8C2
			public void OnCompleted()
			{
				this.core.TrySetCanceled(CancellationToken.None);
			}

			// Token: 0x06000725 RID: 1829 RVA: 0x000106D5 File Offset: 0x0000E8D5
			public void OnError(Exception ex)
			{
				this.core.TrySetException(ex);
			}

			// Token: 0x06000726 RID: 1830 RVA: 0x000106E4 File Offset: 0x0000E8E4
			public void OnNext(T value)
			{
				this.core.TrySetResult(value);
			}

			// Token: 0x04000189 RID: 393
			private static Action<object> cancellationCallback = new Action<object>(ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource.CancellationCallback);

			// Token: 0x0400018A RID: 394
			private static TaskPool<ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource> pool;

			// Token: 0x0400018B RID: 395
			private ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource nextNode;

			// Token: 0x0400018C RID: 396
			private ReadOnlyAsyncReactiveProperty<T> parent;

			// Token: 0x0400018D RID: 397
			private CancellationToken cancellationToken;

			// Token: 0x0400018E RID: 398
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400018F RID: 399
			private UniTaskCompletionSourceCore<T> core;
		}

		// Token: 0x02000134 RID: 308
		private sealed class WithoutCurrentEnumerable : IUniTaskAsyncEnumerable<T>
		{
			// Token: 0x06000727 RID: 1831 RVA: 0x000106F3 File Offset: 0x0000E8F3
			public WithoutCurrentEnumerable(ReadOnlyAsyncReactiveProperty<T> parent)
			{
				this.parent = parent;
			}

			// Token: 0x06000728 RID: 1832 RVA: 0x00010702 File Offset: 0x0000E902
			public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
			{
				return new ReadOnlyAsyncReactiveProperty<T>.Enumerator(this.parent, cancellationToken, false);
			}

			// Token: 0x04000192 RID: 402
			private readonly ReadOnlyAsyncReactiveProperty<T> parent;
		}

		// Token: 0x02000135 RID: 309
		private sealed class Enumerator : MoveNextSource, IUniTaskAsyncEnumerator<T>, IUniTaskAsyncDisposable, ITriggerHandler<T>
		{
			// Token: 0x06000729 RID: 1833 RVA: 0x00010714 File Offset: 0x0000E914
			public Enumerator(ReadOnlyAsyncReactiveProperty<T> parent, CancellationToken cancellationToken, bool publishCurrentValue)
			{
				this.parent = parent;
				this.cancellationToken = cancellationToken;
				this.firstCall = publishCurrentValue;
				parent.triggerEvent.Add(this);
				if (cancellationToken.CanBeCanceled)
				{
					this.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(ReadOnlyAsyncReactiveProperty<T>.Enumerator.cancellationCallback, this);
				}
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x0600072A RID: 1834 RVA: 0x00010763 File Offset: 0x0000E963
			public T Current
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001076B File Offset: 0x0000E96B
			// (set) Token: 0x0600072C RID: 1836 RVA: 0x00010773 File Offset: 0x0000E973
			ITriggerHandler<T> ITriggerHandler<T>.Prev { get; set; }

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001077C File Offset: 0x0000E97C
			// (set) Token: 0x0600072E RID: 1838 RVA: 0x00010784 File Offset: 0x0000E984
			ITriggerHandler<T> ITriggerHandler<T>.Next { get; set; }

			// Token: 0x0600072F RID: 1839 RVA: 0x00010790 File Offset: 0x0000E990
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

			// Token: 0x06000730 RID: 1840 RVA: 0x000107E0 File Offset: 0x0000E9E0
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

			// Token: 0x06000731 RID: 1841 RVA: 0x00010828 File Offset: 0x0000EA28
			public void OnNext(T value)
			{
				this.value = value;
				this.completionSource.TrySetResult(true);
			}

			// Token: 0x06000732 RID: 1842 RVA: 0x0001083E File Offset: 0x0000EA3E
			public void OnCanceled(CancellationToken cancellationToken)
			{
				this.DisposeAsync().Forget();
			}

			// Token: 0x06000733 RID: 1843 RVA: 0x0001084B File Offset: 0x0000EA4B
			public void OnCompleted()
			{
				this.completionSource.TrySetResult(false);
			}

			// Token: 0x06000734 RID: 1844 RVA: 0x0001085A File Offset: 0x0000EA5A
			public void OnError(Exception ex)
			{
				this.completionSource.TrySetException(ex);
			}

			// Token: 0x06000735 RID: 1845 RVA: 0x00010869 File Offset: 0x0000EA69
			private static void CancellationCallback(object state)
			{
				((ReadOnlyAsyncReactiveProperty<T>.Enumerator)state).DisposeAsync().Forget();
			}

			// Token: 0x04000193 RID: 403
			private static Action<object> cancellationCallback = new Action<object>(ReadOnlyAsyncReactiveProperty<T>.Enumerator.CancellationCallback);

			// Token: 0x04000194 RID: 404
			private readonly ReadOnlyAsyncReactiveProperty<T> parent;

			// Token: 0x04000195 RID: 405
			private readonly CancellationToken cancellationToken;

			// Token: 0x04000196 RID: 406
			private readonly CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000197 RID: 407
			private T value;

			// Token: 0x04000198 RID: 408
			private bool isDisposed;

			// Token: 0x04000199 RID: 409
			private bool firstCall;
		}
	}
}
