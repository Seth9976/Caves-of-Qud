using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000016 RID: 22
	internal class SingleConsumerUnboundedChannel<T> : Channel<T>
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00002B9E File Offset: 0x00000D9E
		public SingleConsumerUnboundedChannel()
		{
			this.items = new Queue<T>();
			base.Writer = new SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelWriter(this);
			this.readerSource = new SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader(this);
			base.Reader = this.readerSource;
		}

		// Token: 0x0400001D RID: 29
		private readonly Queue<T> items;

		// Token: 0x0400001E RID: 30
		private readonly SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader readerSource;

		// Token: 0x0400001F RID: 31
		private UniTaskCompletionSource completedTaskSource;

		// Token: 0x04000020 RID: 32
		private UniTask completedTask;

		// Token: 0x04000021 RID: 33
		private Exception completionError;

		// Token: 0x04000022 RID: 34
		private bool closed;

		// Token: 0x0200013A RID: 314
		private sealed class SingleConsumerUnboundedChannelWriter : ChannelWriter<T>
		{
			// Token: 0x06000742 RID: 1858 RVA: 0x00010C8E File Offset: 0x0000EE8E
			public SingleConsumerUnboundedChannelWriter(SingleConsumerUnboundedChannel<T> parent)
			{
				this.parent = parent;
			}

			// Token: 0x06000743 RID: 1859 RVA: 0x00010CA0 File Offset: 0x0000EEA0
			public override bool TryWrite(T item)
			{
				Queue<T> items = this.parent.items;
				bool isWaiting;
				lock (items)
				{
					if (this.parent.closed)
					{
						return false;
					}
					this.parent.items.Enqueue(item);
					isWaiting = this.parent.readerSource.isWaiting;
				}
				if (isWaiting)
				{
					this.parent.readerSource.SingalContinuation();
				}
				return true;
			}

			// Token: 0x06000744 RID: 1860 RVA: 0x00010D28 File Offset: 0x0000EF28
			public override bool TryComplete(Exception error = null)
			{
				Queue<T> items = this.parent.items;
				lock (items)
				{
					if (this.parent.closed)
					{
						return false;
					}
					this.parent.closed = true;
					bool isWaiting = this.parent.readerSource.isWaiting;
					if (this.parent.items.Count == 0)
					{
						if (error == null)
						{
							if (this.parent.completedTaskSource != null)
							{
								this.parent.completedTaskSource.TrySetResult();
							}
							else
							{
								this.parent.completedTask = UniTask.CompletedTask;
							}
						}
						else if (this.parent.completedTaskSource != null)
						{
							this.parent.completedTaskSource.TrySetException(error);
						}
						else
						{
							this.parent.completedTask = UniTask.FromException(error);
						}
						if (isWaiting)
						{
							this.parent.readerSource.SingalCompleted(error);
						}
					}
					this.parent.completionError = error;
				}
				return true;
			}

			// Token: 0x040001B0 RID: 432
			private readonly SingleConsumerUnboundedChannel<T> parent;
		}

		// Token: 0x0200013B RID: 315
		private sealed class SingleConsumerUnboundedChannelReader : ChannelReader<T>, IUniTaskSource<bool>, IUniTaskSource, IValueTaskSource, IValueTaskSource<bool>
		{
			// Token: 0x06000745 RID: 1861 RVA: 0x00010E34 File Offset: 0x0000F034
			public SingleConsumerUnboundedChannelReader(SingleConsumerUnboundedChannel<T> parent)
			{
				this.parent = parent;
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000746 RID: 1862 RVA: 0x00010E58 File Offset: 0x0000F058
			public override UniTask Completion
			{
				get
				{
					if (this.parent.completedTaskSource != null)
					{
						return this.parent.completedTaskSource.Task;
					}
					if (this.parent.closed)
					{
						return this.parent.completedTask;
					}
					this.parent.completedTaskSource = new UniTaskCompletionSource();
					return this.parent.completedTaskSource.Task;
				}
			}

			// Token: 0x06000747 RID: 1863 RVA: 0x00010EBC File Offset: 0x0000F0BC
			public override bool TryRead(out T item)
			{
				Queue<T> items = this.parent.items;
				lock (items)
				{
					if (this.parent.items.Count == 0)
					{
						item = default(T);
						return false;
					}
					item = this.parent.items.Dequeue();
					if (this.parent.closed && this.parent.items.Count == 0)
					{
						if (this.parent.completionError != null)
						{
							if (this.parent.completedTaskSource != null)
							{
								this.parent.completedTaskSource.TrySetException(this.parent.completionError);
							}
							else
							{
								this.parent.completedTask = UniTask.FromException(this.parent.completionError);
							}
						}
						else if (this.parent.completedTaskSource != null)
						{
							this.parent.completedTaskSource.TrySetResult();
						}
						else
						{
							this.parent.completedTask = UniTask.CompletedTask;
						}
					}
				}
				return true;
			}

			// Token: 0x06000748 RID: 1864 RVA: 0x00010FE0 File Offset: 0x0000F1E0
			public override UniTask<bool> WaitToReadAsync(CancellationToken cancellationToken)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return UniTask.FromCanceled<bool>(cancellationToken);
				}
				Queue<T> items = this.parent.items;
				UniTask<bool> uniTask;
				lock (items)
				{
					if (this.parent.items.Count != 0)
					{
						uniTask = CompletedTasks.True;
					}
					else if (this.parent.closed)
					{
						if (this.parent.completionError == null)
						{
							uniTask = CompletedTasks.False;
						}
						else
						{
							uniTask = UniTask.FromException<bool>(this.parent.completionError);
						}
					}
					else
					{
						this.cancellationTokenRegistration.Dispose();
						this.core.Reset();
						this.isWaiting = true;
						this.cancellationToken = cancellationToken;
						if (this.cancellationToken.CanBeCanceled)
						{
							this.cancellationTokenRegistration = this.cancellationToken.RegisterWithoutCaptureExecutionContext(this.CancellationCallbackDelegate, this);
						}
						uniTask = new UniTask<bool>(this, this.core.Version);
					}
				}
				return uniTask;
			}

			// Token: 0x06000749 RID: 1865 RVA: 0x000110E0 File Offset: 0x0000F2E0
			public void SingalContinuation()
			{
				this.core.TrySetResult(true);
			}

			// Token: 0x0600074A RID: 1866 RVA: 0x000110EF File Offset: 0x0000F2EF
			public void SingalCancellation(CancellationToken cancellationToken)
			{
				this.core.TrySetCanceled(cancellationToken);
			}

			// Token: 0x0600074B RID: 1867 RVA: 0x000110FE File Offset: 0x0000F2FE
			public void SingalCompleted(Exception error)
			{
				if (error != null)
				{
					this.core.TrySetException(error);
					return;
				}
				this.core.TrySetResult(false);
			}

			// Token: 0x0600074C RID: 1868 RVA: 0x0001111E File Offset: 0x0000F31E
			public override IUniTaskAsyncEnumerable<T> ReadAllAsync(CancellationToken cancellationToken = default(CancellationToken))
			{
				return new SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader.ReadAllAsyncEnumerable(this, cancellationToken);
			}

			// Token: 0x0600074D RID: 1869 RVA: 0x00011127 File Offset: 0x0000F327
			bool IUniTaskSource<bool>.GetResult(short token)
			{
				return this.core.GetResult(token);
			}

			// Token: 0x0600074E RID: 1870 RVA: 0x00011135 File Offset: 0x0000F335
			void IUniTaskSource.GetResult(short token)
			{
				this.core.GetResult(token);
			}

			// Token: 0x0600074F RID: 1871 RVA: 0x00011144 File Offset: 0x0000F344
			UniTaskStatus IUniTaskSource.GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000750 RID: 1872 RVA: 0x00011152 File Offset: 0x0000F352
			void IUniTaskSource.OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000751 RID: 1873 RVA: 0x00011162 File Offset: 0x0000F362
			UniTaskStatus IUniTaskSource.UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000752 RID: 1874 RVA: 0x0001116F File Offset: 0x0000F36F
			private static void CancellationCallback(object state)
			{
				SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader singleConsumerUnboundedChannelReader = (SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader)state;
				singleConsumerUnboundedChannelReader.SingalCancellation(singleConsumerUnboundedChannelReader.cancellationToken);
			}

			// Token: 0x040001B1 RID: 433
			private readonly Action<object> CancellationCallbackDelegate = new Action<object>(SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader.CancellationCallback);

			// Token: 0x040001B2 RID: 434
			private readonly SingleConsumerUnboundedChannel<T> parent;

			// Token: 0x040001B3 RID: 435
			private CancellationToken cancellationToken;

			// Token: 0x040001B4 RID: 436
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x040001B5 RID: 437
			private UniTaskCompletionSourceCore<bool> core;

			// Token: 0x040001B6 RID: 438
			internal bool isWaiting;

			// Token: 0x0200022A RID: 554
			private sealed class ReadAllAsyncEnumerable : IUniTaskAsyncEnumerable<T>, IUniTaskAsyncEnumerator<T>, IUniTaskAsyncDisposable
			{
				// Token: 0x06000C1A RID: 3098 RVA: 0x0002B6FA File Offset: 0x000298FA
				public ReadAllAsyncEnumerable(SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader parent, CancellationToken cancellationToken)
				{
					this.parent = parent;
					this.cancellationToken1 = cancellationToken;
				}

				// Token: 0x06000C1B RID: 3099 RVA: 0x0002B734 File Offset: 0x00029934
				public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
				{
					if (this.running)
					{
						throw new InvalidOperationException("Enumerator is already running, does not allow call GetAsyncEnumerator twice.");
					}
					if (this.cancellationToken1 != cancellationToken)
					{
						this.cancellationToken2 = cancellationToken;
					}
					if (this.cancellationToken1.CanBeCanceled)
					{
						this.cancellationTokenRegistration1 = this.cancellationToken1.RegisterWithoutCaptureExecutionContext(this.CancellationCallback1Delegate, this);
					}
					if (this.cancellationToken2.CanBeCanceled)
					{
						this.cancellationTokenRegistration2 = this.cancellationToken2.RegisterWithoutCaptureExecutionContext(this.CancellationCallback2Delegate, this);
					}
					this.running = true;
					return this;
				}

				// Token: 0x1700008B RID: 139
				// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0002B7BB File Offset: 0x000299BB
				public T Current
				{
					get
					{
						if (this.cacheValue)
						{
							return this.current;
						}
						this.parent.TryRead(out this.current);
						return this.current;
					}
				}

				// Token: 0x06000C1D RID: 3101 RVA: 0x0002B7E4 File Offset: 0x000299E4
				public UniTask<bool> MoveNextAsync()
				{
					this.cacheValue = false;
					return this.parent.WaitToReadAsync(CancellationToken.None);
				}

				// Token: 0x06000C1E RID: 3102 RVA: 0x0002B800 File Offset: 0x00029A00
				public UniTask DisposeAsync()
				{
					this.cancellationTokenRegistration1.Dispose();
					this.cancellationTokenRegistration2.Dispose();
					return default(UniTask);
				}

				// Token: 0x06000C1F RID: 3103 RVA: 0x0002B82C File Offset: 0x00029A2C
				private static void CancellationCallback1(object state)
				{
					SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader.ReadAllAsyncEnumerable readAllAsyncEnumerable = (SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader.ReadAllAsyncEnumerable)state;
					readAllAsyncEnumerable.parent.SingalCancellation(readAllAsyncEnumerable.cancellationToken1);
				}

				// Token: 0x06000C20 RID: 3104 RVA: 0x0002B854 File Offset: 0x00029A54
				private static void CancellationCallback2(object state)
				{
					SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader.ReadAllAsyncEnumerable readAllAsyncEnumerable = (SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader.ReadAllAsyncEnumerable)state;
					readAllAsyncEnumerable.parent.SingalCancellation(readAllAsyncEnumerable.cancellationToken2);
				}

				// Token: 0x0400056B RID: 1387
				private readonly Action<object> CancellationCallback1Delegate = new Action<object>(SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader.ReadAllAsyncEnumerable.CancellationCallback1);

				// Token: 0x0400056C RID: 1388
				private readonly Action<object> CancellationCallback2Delegate = new Action<object>(SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader.ReadAllAsyncEnumerable.CancellationCallback2);

				// Token: 0x0400056D RID: 1389
				private readonly SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader parent;

				// Token: 0x0400056E RID: 1390
				private CancellationToken cancellationToken1;

				// Token: 0x0400056F RID: 1391
				private CancellationToken cancellationToken2;

				// Token: 0x04000570 RID: 1392
				private CancellationTokenRegistration cancellationTokenRegistration1;

				// Token: 0x04000571 RID: 1393
				private CancellationTokenRegistration cancellationTokenRegistration2;

				// Token: 0x04000572 RID: 1394
				private T current;

				// Token: 0x04000573 RID: 1395
				private bool cacheValue;

				// Token: 0x04000574 RID: 1396
				private bool running;
			}
		}
	}
}
