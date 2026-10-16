using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000034 RID: 52
	internal class TaskAsyncOperation : AsyncOperationBase, INotifyCompletion
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000028C4 File Offset: 0x00000AC4
		public override bool IsCompleted
		{
			get
			{
				return this.m_Task.IsCompleted;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000028D1 File Offset: 0x00000AD1
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

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000290B File Offset: 0x00000B0B
		public override Exception Exception
		{
			get
			{
				Task task = this.m_Task;
				if (task == null)
				{
					return null;
				}
				return task.Exception;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000291E File Offset: 0x00000B1E
		public override void GetResult()
		{
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00002920 File Offset: 0x00000B20
		public override AsyncOperationBase GetAwaiter()
		{
			return this;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00002924 File Offset: 0x00000B24
		public TaskAsyncOperation(Task task)
		{
			if (TaskAsyncOperation.Scheduler == null)
			{
				TaskAsyncOperation.SetScheduler();
			}
			this.m_Task = task;
			task.ContinueWith(delegate(Task t, object state)
			{
				((TaskAsyncOperation)state).DidComplete();
			}, this, CancellationToken.None, TaskContinuationOptions.None, TaskAsyncOperation.Scheduler);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000297C File Offset: 0x00000B7C
		public static TaskAsyncOperation Run(Action action)
		{
			Task task = new Task(action);
			TaskAsyncOperation taskAsyncOperation = new TaskAsyncOperation(task);
			task.Start();
			return taskAsyncOperation;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000299C File Offset: 0x00000B9C
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		internal static void SetScheduler()
		{
			TaskAsyncOperation.Scheduler = TaskScheduler.FromCurrentSynchronizationContext();
		}

		// Token: 0x0400002F RID: 47
		internal static TaskScheduler Scheduler;

		// Token: 0x04000030 RID: 48
		private Task m_Task;
	}
}
