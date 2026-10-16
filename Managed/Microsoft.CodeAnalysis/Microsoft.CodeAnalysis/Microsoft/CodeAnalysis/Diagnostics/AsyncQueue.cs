using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200030D RID: 781
	internal sealed class AsyncQueue<TElement>
	{
		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x0006D257 File Offset: 0x0006B457
		private object SyncObject
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x0006D260 File Offset: 0x0006B460
		public int Count
		{
			get
			{
				object syncObject = this.SyncObject;
				int count;
				lock (syncObject)
				{
					count = this._data.Count;
				}
				return count;
			}
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x0006D2A8 File Offset: 0x0006B4A8
		public void Enqueue(TElement value)
		{
			if (!this.EnqueueCore(value))
			{
				throw new InvalidOperationException("Cannot call Enqueue when the queue is already completed.");
			}
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x0006D2BE File Offset: 0x0006B4BE
		public bool TryEnqueue(TElement value)
		{
			return this.EnqueueCore(value);
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x0006D2C8 File Offset: 0x0006B4C8
		private bool EnqueueCore(TElement value)
		{
			while (!this._disallowEnqueue)
			{
				object syncObject = this.SyncObject;
				TaskCompletionSource<Optional<TElement>> taskCompletionSource;
				lock (syncObject)
				{
					if (this._completed)
					{
						return false;
					}
					if (this._waiters == null || this._waiters.Count == 0)
					{
						this._data.Enqueue(value);
						return true;
					}
					taskCompletionSource = this._waiters.Dequeue();
				}
				if (taskCompletionSource.TrySetResult(value))
				{
					return true;
				}
				continue;
			}
			throw new InvalidOperationException("Cannot enqueue data after PromiseNotToEnqueue.");
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x0006D364 File Offset: 0x0006B564
		public bool TryDequeue(out TElement d)
		{
			object syncObject = this.SyncObject;
			bool flag2;
			lock (syncObject)
			{
				if (this._data.Count == 0)
				{
					d = default(TElement);
					flag2 = false;
				}
				else
				{
					d = this._data.Dequeue();
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x0006D3CC File Offset: 0x0006B5CC
		public bool IsCompleted
		{
			get
			{
				object syncObject = this.SyncObject;
				bool completed;
				lock (syncObject)
				{
					completed = this._completed;
				}
				return completed;
			}
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x0006D410 File Offset: 0x0006B610
		public void Complete()
		{
			if (!this.CompleteCore())
			{
				throw new InvalidOperationException("Cannot call Complete when the queue is already completed.");
			}
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x0006D425 File Offset: 0x0006B625
		public void PromiseNotToEnqueue()
		{
			this._disallowEnqueue = true;
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x0006D42E File Offset: 0x0006B62E
		public bool TryComplete()
		{
			return this.CompleteCore();
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x0006D438 File Offset: 0x0006B638
		private bool CompleteCore()
		{
			object syncObject = this.SyncObject;
			Queue<TaskCompletionSource<Optional<TElement>>> waiters;
			lock (syncObject)
			{
				if (this._completed)
				{
					return false;
				}
				this._completed = true;
				waiters = this._waiters;
				this._waiters = null;
			}
			if (waiters != null && waiters.Count > 0)
			{
				foreach (TaskCompletionSource<Optional<TElement>> taskCompletionSource in waiters)
				{
					taskCompletionSource.TrySetResult(default(Optional<TElement>));
				}
			}
			this._whenCompleted.SetResult(true);
			return true;
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002259 RID: 8793 RVA: 0x0006D4F4 File Offset: 0x0006B6F4
		public Task WhenCompletedTask
		{
			get
			{
				return this._whenCompleted.Task;
			}
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x0006D504 File Offset: 0x0006B704
		public Task<TElement> DequeueAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			ValueTask<Optional<TElement>> valueTask = this.TryDequeueAsync(cancellationToken);
			if (!valueTask.IsCompletedSuccessfully)
			{
				return AsyncQueue<TElement>.<DequeueAsync>g__dequeueSlowAsync|21_0(valueTask);
			}
			Optional<TElement> result = valueTask.Result;
			if (!result.HasValue)
			{
				return Task.FromCanceled<TElement>(new CancellationToken(true));
			}
			return Task.FromResult<TElement>(result.Value);
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x0006D554 File Offset: 0x0006B754
		public ValueTask<Optional<TElement>> TryDequeueAsync(CancellationToken cancellationToken)
		{
			object syncObject = this.SyncObject;
			ValueTask<Optional<TElement>> valueTask;
			lock (syncObject)
			{
				if (this._data.Count > 0)
				{
					valueTask = ValueTaskFactory.FromResult<Optional<TElement>>(this._data.Dequeue());
				}
				else if (this._completed)
				{
					valueTask = ValueTaskFactory.FromResult<Optional<TElement>>(default(Optional<TElement>));
				}
				else
				{
					if (this._waiters == null)
					{
						this._waiters = new Queue<TaskCompletionSource<Optional<TElement>>>();
					}
					TaskCompletionSource<Optional<TElement>> taskCompletionSource = new TaskCompletionSource<Optional<TElement>>(64);
					AsyncQueue<TElement>.AttachCancellation<Optional<TElement>>(taskCompletionSource, cancellationToken);
					this._waiters.Enqueue(taskCompletionSource);
					valueTask = new ValueTask<Optional<TElement>>(taskCompletionSource.Task);
				}
			}
			return valueTask;
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x0006D60C File Offset: 0x0006B80C
		private static void AttachCancellation<T>(TaskCompletionSource<T> taskCompletionSource, CancellationToken cancellationToken)
		{
			if (!cancellationToken.CanBeCanceled || taskCompletionSource.Task.IsCompleted)
			{
				return;
			}
			if (cancellationToken.IsCancellationRequested)
			{
				taskCompletionSource.TrySetCanceled(cancellationToken);
				return;
			}
			AsyncQueue<TElement>.CancelableTaskCompletionSource<T> cancelableTaskCompletionSource = new AsyncQueue<TElement>.CancelableTaskCompletionSource<T>(taskCompletionSource, cancellationToken);
			cancelableTaskCompletionSource.CancellationTokenRegistration = cancellationToken.Register(delegate(object s)
			{
				AsyncQueue<TElement>.CancelableTaskCompletionSource<T> cancelableTaskCompletionSource2 = (AsyncQueue<TElement>.CancelableTaskCompletionSource<T>)s;
				cancelableTaskCompletionSource2.TaskCompletionSource.TrySetCanceled(cancelableTaskCompletionSource2.CancellationToken);
			}, cancelableTaskCompletionSource, false);
			taskCompletionSource.Task.ContinueWith(delegate(Task<T> _, object s)
			{
				((AsyncQueue<TElement>.CancelableTaskCompletionSource<T>)s).CancellationTokenRegistration.Dispose();
			}, cancelableTaskCompletionSource, CancellationToken.None, 524288, TaskScheduler.Default);
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x0006D6D4 File Offset: 0x0006B8D4
		[CompilerGenerated]
		internal static Task<TElement> <DequeueAsync>g__dequeueSlowAsync|21_0(ValueTask<Optional<TElement>> optionalResult)
		{
			AsyncQueue<TElement>.<<DequeueAsync>g__dequeueSlowAsync|21_0>d <<DequeueAsync>g__dequeueSlowAsync|21_0>d;
			<<DequeueAsync>g__dequeueSlowAsync|21_0>d.<>t__builder = AsyncTaskMethodBuilder<TElement>.Create();
			<<DequeueAsync>g__dequeueSlowAsync|21_0>d.optionalResult = optionalResult;
			<<DequeueAsync>g__dequeueSlowAsync|21_0>d.<>1__state = -1;
			<<DequeueAsync>g__dequeueSlowAsync|21_0>d.<>t__builder.Start<AsyncQueue<TElement>.<<DequeueAsync>g__dequeueSlowAsync|21_0>d>(ref <<DequeueAsync>g__dequeueSlowAsync|21_0>d);
			return <<DequeueAsync>g__dequeueSlowAsync|21_0>d.<>t__builder.Task;
		}

		// Token: 0x04000EBC RID: 3772
		private readonly TaskCompletionSource<bool> _whenCompleted = new TaskCompletionSource<bool>(64);

		// Token: 0x04000EBD RID: 3773
		private readonly Queue<TElement> _data = new Queue<TElement>();

		// Token: 0x04000EBE RID: 3774
		private Queue<TaskCompletionSource<Optional<TElement>>> _waiters;

		// Token: 0x04000EBF RID: 3775
		private bool _completed;

		// Token: 0x04000EC0 RID: 3776
		private bool _disallowEnqueue;

		// Token: 0x020006E0 RID: 1760
		private sealed class CancelableTaskCompletionSource<T>
		{
			// Token: 0x06003AC1 RID: 15041 RVA: 0x000A5DFA File Offset: 0x000A3FFA
			internal CancelableTaskCompletionSource(TaskCompletionSource<T> taskCompletionSource, CancellationToken cancellationToken)
			{
				this.TaskCompletionSource = taskCompletionSource;
				this.CancellationToken = cancellationToken;
			}

			// Token: 0x17001001 RID: 4097
			// (get) Token: 0x06003AC2 RID: 15042 RVA: 0x000A5E10 File Offset: 0x000A4010
			internal CancellationToken CancellationToken { get; }

			// Token: 0x17001002 RID: 4098
			// (get) Token: 0x06003AC3 RID: 15043 RVA: 0x000A5E18 File Offset: 0x000A4018
			internal TaskCompletionSource<T> TaskCompletionSource { get; }

			// Token: 0x17001003 RID: 4099
			// (get) Token: 0x06003AC4 RID: 15044 RVA: 0x000A5E20 File Offset: 0x000A4020
			// (set) Token: 0x06003AC5 RID: 15045 RVA: 0x000A5E28 File Offset: 0x000A4028
			internal CancellationTokenRegistration CancellationTokenRegistration { get; set; }
		}
	}
}
