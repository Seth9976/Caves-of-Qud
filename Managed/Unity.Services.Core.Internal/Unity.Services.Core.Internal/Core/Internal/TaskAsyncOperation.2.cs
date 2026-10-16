using System;
using System.Threading;
using System.Threading.Tasks;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000035 RID: 53
	internal class TaskAsyncOperation<T> : AsyncOperationBase<T>
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000029A8 File Offset: 0x00000BA8
		public override bool IsCompleted
		{
			get
			{
				return this.m_Task.IsCompleted;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000029B5 File Offset: 0x00000BB5
		public override T Result
		{
			get
			{
				return this.m_Task.Result;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000029C4 File Offset: 0x00000BC4
		public override T GetResult()
		{
			return this.m_Task.GetAwaiter().GetResult();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000029E4 File Offset: 0x00000BE4
		public override AsyncOperationBase<T> GetAwaiter()
		{
			return this;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000029E7 File Offset: 0x00000BE7
		public override AsyncOperationStatus Status
		{
			get
			{
				if (this.m_Task == null)
				{
					return AsyncOperationStatus.None;
				}
				if (!this.m_Task.IsCompleted)
				{
					return AsyncOperationStatus.InProgress;
				}
				if (this.m_Task.IsCanceled)
				{
					return AsyncOperationStatus.Cancelled;
				}
				if (this.m_Task.IsFaulted)
				{
					return AsyncOperationStatus.Failed;
				}
				return AsyncOperationStatus.Succeeded;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00002A21 File Offset: 0x00000C21
		public override Exception Exception
		{
			get
			{
				Task<T> task = this.m_Task;
				if (task == null)
				{
					return null;
				}
				return task.Exception;
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00002A34 File Offset: 0x00000C34
		public TaskAsyncOperation(Task<T> task)
		{
			if (TaskAsyncOperation.Scheduler == null)
			{
				TaskAsyncOperation.SetScheduler();
			}
			this.m_Task = task;
			task.ContinueWith(delegate(Task<T> t, object state)
			{
				((TaskAsyncOperation<T>)state).DidComplete();
			}, this, CancellationToken.None, TaskContinuationOptions.None, TaskAsyncOperation.Scheduler);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00002A8C File Offset: 0x00000C8C
		public static TaskAsyncOperation<T> Run(Func<T> func)
		{
			Task<T> task = new Task<T>(func);
			TaskAsyncOperation<T> taskAsyncOperation = new TaskAsyncOperation<T>(task);
			task.Start();
			return taskAsyncOperation;
		}

		// Token: 0x04000031 RID: 49
		private Task<T> m_Task;
	}
}
