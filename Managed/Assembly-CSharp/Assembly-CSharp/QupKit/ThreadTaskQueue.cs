using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QupKit
{
	// Token: 0x020003AC RID: 940
	public class ThreadTaskQueue
	{
		// Token: 0x0600230D RID: 8973 RVA: 0x000B215C File Offset: 0x000B035C
		public void clear()
		{
			this.taskQueue.Clear();
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x000B2169 File Offset: 0x000B0369
		public void poolEntry(ThreadTaskQueueEntry e)
		{
			this.taskPool.Enqueue(e);
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x000B2177 File Offset: 0x000B0377
		public bool HasTask()
		{
			return this.taskQueue.Count > 0;
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x000B2188 File Offset: 0x000B0388
		public void executeTasks()
		{
			if (this.taskQueue.Count > 0)
			{
				int num = this.taskQueue.Count;
				while (this.taskQueue.Count > 0 && num > 0)
				{
					num--;
					ThreadTaskQueueEntry threadTaskQueueEntry = null;
					Queue<ThreadTaskQueueEntry> queue = this.taskQueue;
					lock (queue)
					{
						if (this.taskQueue.Count > 0)
						{
							threadTaskQueueEntry = this.taskQueue.Dequeue();
						}
					}
					if (threadTaskQueueEntry != null)
					{
						if (threadTaskQueueEntry.Delay > 0)
						{
							threadTaskQueueEntry.Delay--;
							this.delayedTasks.Enqueue(threadTaskQueueEntry);
						}
						else
						{
							threadTaskQueueEntry.Execute(this);
						}
					}
				}
				if (this.delayedTasks.Count > 0)
				{
					Queue<ThreadTaskQueueEntry> queue = this.taskQueue;
					lock (queue)
					{
						while (this.delayedTasks.Count > 0)
						{
							this.taskQueue.Enqueue(this.delayedTasks.Dequeue());
						}
					}
				}
			}
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000B22A0 File Offset: 0x000B04A0
		public Task<TResult> executeAsync<TResult>(Func<TResult> a, string TaskID = null, bool OverwritePrevious = false)
		{
			TaskCompletionSource<TResult> promise = new TaskCompletionSource<TResult>();
			Action action = delegate
			{
				try
				{
					promise.TrySetResult(a());
				}
				catch (Exception ex)
				{
					promise.TrySetException(ex);
				}
			};
			if (TaskID != null)
			{
				this.queueSingletonTask(TaskID, action, OverwritePrevious);
			}
			else
			{
				this.queueTask(action, 0);
			}
			return promise.Task;
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000B22F4 File Offset: 0x000B04F4
		public ThreadTaskQueueEntry queueSingletonTask(string TaskID, Action a, bool OverwritePrevious = false)
		{
			Queue<ThreadTaskQueueEntry> queue = this.taskQueue;
			ThreadTaskQueueEntry threadTaskQueueEntry3;
			lock (queue)
			{
				foreach (ThreadTaskQueueEntry threadTaskQueueEntry in this.taskQueue)
				{
					if (threadTaskQueueEntry.TaskID == TaskID)
					{
						if (OverwritePrevious)
						{
							threadTaskQueueEntry.SetAction(a);
						}
						return threadTaskQueueEntry;
					}
				}
				ThreadTaskQueueEntry threadTaskQueueEntry2;
				if (this.taskPool.Count > 0)
				{
					threadTaskQueueEntry2 = this.taskPool.Dequeue();
					threadTaskQueueEntry2.SetAction(a);
					threadTaskQueueEntry2.Delay = 0;
				}
				else
				{
					threadTaskQueueEntry2 = new ThreadTaskQueueEntry(a);
					threadTaskQueueEntry2.Delay = 0;
				}
				threadTaskQueueEntry2.TaskID = TaskID;
				this.taskQueue.Enqueue(threadTaskQueueEntry2);
				threadTaskQueueEntry3 = threadTaskQueueEntry2;
			}
			return threadTaskQueueEntry3;
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000B23DC File Offset: 0x000B05DC
		public void awaitTask(Action a)
		{
			if (Thread.CurrentThread == this.threadContext)
			{
				a();
				return;
			}
			ManualResetEvent awaitEvent = new ManualResetEvent(false);
			this.queueTask(delegate
			{
				try
				{
					a();
				}
				finally
				{
					awaitEvent.Set();
				}
			}, 0);
			awaitEvent.WaitOne();
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x000B243C File Offset: 0x000B063C
		public ThreadTaskQueueEntry queueTask(Action a, int delay = 0)
		{
			Queue<ThreadTaskQueueEntry> queue = this.taskQueue;
			ThreadTaskQueueEntry threadTaskQueueEntry2;
			lock (queue)
			{
				ThreadTaskQueueEntry threadTaskQueueEntry;
				if (this.taskPool.Count > 0)
				{
					threadTaskQueueEntry = this.taskPool.Dequeue();
					threadTaskQueueEntry.SetAction(a);
					threadTaskQueueEntry.Delay = delay;
				}
				else
				{
					threadTaskQueueEntry = new ThreadTaskQueueEntry(a);
					threadTaskQueueEntry.Delay = delay;
				}
				this.taskQueue.Enqueue(threadTaskQueueEntry);
				threadTaskQueueEntry2 = threadTaskQueueEntry;
			}
			return threadTaskQueueEntry2;
		}

		// Token: 0x040011AC RID: 4524
		private Queue<ThreadTaskQueueEntry> taskQueue = new Queue<ThreadTaskQueueEntry>(256);

		// Token: 0x040011AD RID: 4525
		private Queue<ThreadTaskQueueEntry> taskPool = new Queue<ThreadTaskQueueEntry>(256);

		// Token: 0x040011AE RID: 4526
		public Queue<ThreadTaskQueueEntry> delayedTasks = new Queue<ThreadTaskQueueEntry>();

		// Token: 0x040011AF RID: 4527
		public Thread threadContext;
	}
}
