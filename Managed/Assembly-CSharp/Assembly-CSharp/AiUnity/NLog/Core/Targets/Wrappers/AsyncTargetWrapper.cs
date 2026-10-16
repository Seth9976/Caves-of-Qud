using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using AiUnity.Common.Attributes;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Internal;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets.Wrappers
{
	// Token: 0x02000230 RID: 560
	[Target("AsyncWrapper", IsWrapper = true)]
	[Preserve]
	public class AsyncTargetWrapper : WrapperTargetBase
	{
		// Token: 0x060014B7 RID: 5303 RVA: 0x000746A8 File Offset: 0x000728A8
		public AsyncTargetWrapper()
			: this(null)
		{
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x000746B1 File Offset: 0x000728B1
		public AsyncTargetWrapper(Target wrappedTarget)
			: this(wrappedTarget, 10000, AsyncTargetWrapperOverflowAction.Discard)
		{
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x000746C0 File Offset: 0x000728C0
		public AsyncTargetWrapper(Target wrappedTarget, int queueLimit, AsyncTargetWrapperOverflowAction overflowAction)
		{
			this.RequestQueue = new AsyncRequestQueue(10000, AsyncTargetWrapperOverflowAction.Discard);
			this.TimeToSleepBetweenBatches = 50;
			this.BatchSize = 100;
			base.WrappedTarget = wrappedTarget;
			this.QueueLimit = queueLimit;
			this.OverflowAction = overflowAction;
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x0007472A File Offset: 0x0007292A
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x00074731 File Offset: 0x00072931
		// (set) Token: 0x060014BC RID: 5308 RVA: 0x00074739 File Offset: 0x00072939
		[DefaultValue(100)]
		[Display("Batch size", "Number of log events to process in a batch.", false, 0)]
		public int BatchSize { get; set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x00074742 File Offset: 0x00072942
		// (set) Token: 0x060014BE RID: 5310 RVA: 0x0007474A File Offset: 0x0007294A
		[DefaultValue(50)]
		[Display("Batch timeout", "Process log events in batch after timeout interval.", false, 0)]
		public int TimeToSleepBetweenBatches { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x00074753 File Offset: 0x00072953
		// (set) Token: 0x060014C0 RID: 5312 RVA: 0x00074760 File Offset: 0x00072960
		[DefaultValue("Discard")]
		[Display("Overflow action", "Action to take if lazy writer queue limit is exceeded.", false, 0)]
		public AsyncTargetWrapperOverflowAction OverflowAction
		{
			get
			{
				return this.RequestQueue.OnOverflow;
			}
			set
			{
				this.RequestQueue.OnOverflow = value;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x0007476E File Offset: 0x0007296E
		// (set) Token: 0x060014C2 RID: 5314 RVA: 0x0007477B File Offset: 0x0007297B
		[DefaultValue(10000)]
		[Display("Queue limit", "The maximum limit of the lazy writer thread request queue.", false, 0)]
		public int QueueLimit
		{
			get
			{
				return this.RequestQueue.RequestLimit;
			}
			set
			{
				this.RequestQueue.RequestLimit = value;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x00074789 File Offset: 0x00072989
		// (set) Token: 0x060014C4 RID: 5316 RVA: 0x00074791 File Offset: 0x00072991
		internal AsyncRequestQueue RequestQueue { get; private set; }

		// Token: 0x060014C5 RID: 5317 RVA: 0x0007479C File Offset: 0x0007299C
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			object obj = this.continuationQueueLock;
			lock (obj)
			{
				this.flushAllContinuations.Enqueue(asyncContinuation);
			}
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x000747E4 File Offset: 0x000729E4
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			this.RequestQueue.Clear();
			this.lazyWriterTimer = new Timer(new TimerCallback(this.ProcessPendingEvents), null, -1, -1);
			this.StartLazyWriterTimer();
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x00074817 File Offset: 0x00072A17
		protected override void CloseTarget()
		{
			this.StopLazyWriterThread();
			if (this.RequestQueue.RequestCount > 0)
			{
				this.ProcessPendingEvents(null);
			}
			base.CloseTarget();
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0007483C File Offset: 0x00072A3C
		protected virtual void StartLazyWriterTimer()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.lazyWriterTimer != null)
				{
					this.lazyWriterTimer.Change(this.TimeToSleepBetweenBatches, -1);
				}
			}
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00074894 File Offset: 0x00072A94
		protected virtual void StopLazyWriterThread()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.lazyWriterTimer != null)
				{
					this.lazyWriterTimer.Change(-1, -1);
					this.lazyWriterTimer = null;
				}
			}
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x000748EC File Offset: 0x00072AEC
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			base.MergeEventProperties(logEvent.LogEvent);
			base.PrecalculateVolatileLayouts(logEvent.LogEvent);
			this.RequestQueue.Enqueue(logEvent);
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00074914 File Offset: 0x00072B14
		private void ProcessPendingEvents(object state)
		{
			object obj = this.continuationQueueLock;
			AsyncContinuation[] array;
			lock (obj)
			{
				array = ((this.flushAllContinuations.Count > 0) ? this.flushAllContinuations.ToArray() : new AsyncContinuation[1]);
				this.flushAllContinuations.Clear();
			}
			try
			{
				AsyncContinuation[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					AsyncContinuation continuation = array2[i];
					int num = this.BatchSize;
					if (continuation != null)
					{
						num = this.RequestQueue.RequestCount;
						AsyncTargetWrapper.Logger.Trace("Flushing {0} events.", new object[] { num });
					}
					if (this.RequestQueue.RequestCount == 0 && continuation != null)
					{
						continuation(null);
					}
					AsyncLogEventInfo[] array3 = this.RequestQueue.DequeueBatch(num);
					if (continuation != null)
					{
						base.WrappedTarget.WriteAsyncLogEvents(array3, delegate(Exception ex)
						{
							this.WrappedTarget.Flush(continuation);
						});
					}
					else
					{
						base.WrappedTarget.WriteAsyncLogEvents(array3);
					}
				}
			}
			catch (Exception ex)
			{
				Exception ex2;
				if (ex2.MustBeRethrown())
				{
					throw;
				}
				AsyncTargetWrapper.Logger.Error("Error in lazy writer timer procedure: {0}", new object[] { ex2 });
			}
			finally
			{
				this.StartLazyWriterTimer();
			}
		}

		// Token: 0x04000D5B RID: 3419
		private readonly object lockObject = new object();

		// Token: 0x04000D5C RID: 3420
		private Timer lazyWriterTimer;

		// Token: 0x04000D5D RID: 3421
		private readonly Queue<AsyncContinuation> flushAllContinuations = new Queue<AsyncContinuation>();

		// Token: 0x04000D5E RID: 3422
		private readonly object continuationQueueLock = new object();
	}
}
