using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020002BF RID: 703
	internal sealed class UnitySynchronizationContext : SynchronizationContext
	{
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x000432C9 File Offset: 0x000414C9
		internal int MainThreadId
		{
			get
			{
				return this.m_MainThreadID;
			}
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000432D1 File Offset: 0x000414D1
		private UnitySynchronizationContext(int mainThreadID)
		{
			this.m_AsyncWorkQueue = new List<UnitySynchronizationContext.WorkRequest>(20);
			this.m_MainThreadID = mainThreadID;
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x00043303 File Offset: 0x00041503
		private UnitySynchronizationContext(List<UnitySynchronizationContext.WorkRequest> queue, int mainThreadID)
		{
			this.m_AsyncWorkQueue = queue;
			this.m_MainThreadID = mainThreadID;
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x00043330 File Offset: 0x00041530
		public override void Send(SendOrPostCallback callback, object state)
		{
			bool flag = this.m_MainThreadID == Thread.CurrentThread.ManagedThreadId;
			if (flag)
			{
				callback(state);
			}
			else
			{
				using (ManualResetEvent manualResetEvent = new ManualResetEvent(false))
				{
					List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
					lock (asyncWorkQueue)
					{
						this.m_AsyncWorkQueue.Add(new UnitySynchronizationContext.WorkRequest(callback, state, manualResetEvent));
					}
					manualResetEvent.WaitOne();
				}
			}
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x000433D0 File Offset: 0x000415D0
		public override void OperationStarted()
		{
			Interlocked.Increment(ref this.m_TrackedCount);
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x000433DF File Offset: 0x000415DF
		public override void OperationCompleted()
		{
			Interlocked.Decrement(ref this.m_TrackedCount);
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x000433F0 File Offset: 0x000415F0
		public override void Post(SendOrPostCallback callback, object state)
		{
			List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
			lock (asyncWorkQueue)
			{
				this.m_AsyncWorkQueue.Add(new UnitySynchronizationContext.WorkRequest(callback, state, null));
			}
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x00043444 File Offset: 0x00041644
		public override SynchronizationContext CreateCopy()
		{
			return new UnitySynchronizationContext(this.m_AsyncWorkQueue, this.m_MainThreadID);
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x00043468 File Offset: 0x00041668
		public void Exec()
		{
			List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
			lock (asyncWorkQueue)
			{
				this.m_CurrentFrameWork.AddRange(this.m_AsyncWorkQueue);
				this.m_AsyncWorkQueue.Clear();
			}
			while (this.m_CurrentFrameWork.Count > 0)
			{
				UnitySynchronizationContext.WorkRequest workRequest = this.m_CurrentFrameWork[0];
				this.m_CurrentFrameWork.RemoveAt(0);
				workRequest.Invoke();
			}
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x00043500 File Offset: 0x00041700
		private bool HasPendingTasks()
		{
			return this.m_AsyncWorkQueue.Count != 0 || this.m_TrackedCount != 0;
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x0004352C File Offset: 0x0004172C
		[RequiredByNativeCode]
		private static void InitializeSynchronizationContext()
		{
			UnitySynchronizationContext unitySynchronizationContext = new UnitySynchronizationContext(Thread.CurrentThread.ManagedThreadId);
			SynchronizationContext.SetSynchronizationContext(unitySynchronizationContext);
			Awaitable.SetSynchronizationContext(unitySynchronizationContext);
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x00043558 File Offset: 0x00041758
		[RequiredByNativeCode]
		private static void ExecuteTasks()
		{
			UnitySynchronizationContext unitySynchronizationContext = SynchronizationContext.Current as UnitySynchronizationContext;
			bool flag = unitySynchronizationContext != null;
			if (flag)
			{
				unitySynchronizationContext.Exec();
			}
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x00043580 File Offset: 0x00041780
		[RequiredByNativeCode]
		private static bool ExecutePendingTasks(long millisecondsTimeout)
		{
			UnitySynchronizationContext unitySynchronizationContext = SynchronizationContext.Current as UnitySynchronizationContext;
			bool flag = unitySynchronizationContext == null;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				while (unitySynchronizationContext.HasPendingTasks())
				{
					bool flag3 = stopwatch.ElapsedMilliseconds > millisecondsTimeout;
					if (flag3)
					{
						break;
					}
					unitySynchronizationContext.Exec();
					Thread.Sleep(1);
				}
				flag2 = !unitySynchronizationContext.HasPendingTasks();
			}
			return flag2;
		}

		// Token: 0x04000A00 RID: 2560
		private const int kAwqInitialCapacity = 20;

		// Token: 0x04000A01 RID: 2561
		private readonly List<UnitySynchronizationContext.WorkRequest> m_AsyncWorkQueue;

		// Token: 0x04000A02 RID: 2562
		private readonly List<UnitySynchronizationContext.WorkRequest> m_CurrentFrameWork = new List<UnitySynchronizationContext.WorkRequest>(20);

		// Token: 0x04000A03 RID: 2563
		private readonly int m_MainThreadID;

		// Token: 0x04000A04 RID: 2564
		private int m_TrackedCount = 0;

		// Token: 0x020002C0 RID: 704
		private struct WorkRequest
		{
			// Token: 0x0600226B RID: 8811 RVA: 0x000435F0 File Offset: 0x000417F0
			public WorkRequest(SendOrPostCallback callback, object state, ManualResetEvent waitHandle = null)
			{
				this.m_DelagateCallback = callback;
				this.m_DelagateState = state;
				this.m_WaitHandle = waitHandle;
			}

			// Token: 0x0600226C RID: 8812 RVA: 0x00043608 File Offset: 0x00041808
			public void Invoke()
			{
				try
				{
					this.m_DelagateCallback(this.m_DelagateState);
				}
				finally
				{
					ManualResetEvent waitHandle = this.m_WaitHandle;
					if (waitHandle != null)
					{
						waitHandle.Set();
					}
				}
			}

			// Token: 0x04000A05 RID: 2565
			private readonly SendOrPostCallback m_DelagateCallback;

			// Token: 0x04000A06 RID: 2566
			private readonly object m_DelagateState;

			// Token: 0x04000A07 RID: 2567
			private readonly ManualResetEvent m_WaitHandle;
		}
	}
}
