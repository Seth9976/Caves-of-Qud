using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web
{
	// Token: 0x020000D9 RID: 217
	internal sealed class TaskAsyncResult : IAsyncResult
	{
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0001F026 File Offset: 0x0001D226
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x0001F02E File Offset: 0x0001D22E
		public object AsyncState { get; private set; }

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0001F037 File Offset: 0x0001D237
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return ((IAsyncResult)this.task).AsyncWaitHandle;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0001F044 File Offset: 0x0001D244
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x0001F04C File Offset: 0x0001D24C
		public bool CompletedSynchronously { get; private set; }

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0001F055 File Offset: 0x0001D255
		public bool IsCompleted
		{
			get
			{
				return this.task.IsCompleted;
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0001F062 File Offset: 0x0001D262
		private TaskAsyncResult(Task task, AsyncCallback callback, object state)
		{
			this.task = task;
			this.callback = callback;
			this.AsyncState = state;
			this.CompletedSynchronously = task.IsCompleted;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0001F08C File Offset: 0x0001D28C
		public static IAsyncResult GetAsyncResult(Task task, AsyncCallback callback, object state)
		{
			if (task == null)
			{
				return null;
			}
			TaskAsyncResult taskAsyncResult = new TaskAsyncResult(task, callback, state);
			if (callback != null)
			{
				if (taskAsyncResult.CompletedSynchronously)
				{
					callback(taskAsyncResult);
				}
				else
				{
					task.ContinueWith(TaskAsyncResult.invokeCallback, taskAsyncResult);
				}
			}
			return taskAsyncResult;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0001F0CC File Offset: 0x0001D2CC
		public static void Wait(IAsyncResult result)
		{
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			TaskAsyncResult taskAsyncResult = result as TaskAsyncResult;
			if (taskAsyncResult == null)
			{
				throw new ArgumentException("The provided IAsyncResult is invalid.", "result");
			}
			taskAsyncResult.task.GetAwaiter().GetResult();
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0001F114 File Offset: 0x0001D314
		private static void InvokeCallback(Task task, object state)
		{
			TaskAsyncResult taskAsyncResult = (TaskAsyncResult)state;
			taskAsyncResult.callback(taskAsyncResult);
		}

		// Token: 0x040010B4 RID: 4276
		private static readonly Action<Task, object> invokeCallback = new Action<Task, object>(TaskAsyncResult.InvokeCallback);

		// Token: 0x040010B5 RID: 4277
		private readonly Task task;

		// Token: 0x040010B6 RID: 4278
		private readonly AsyncCallback callback;
	}
}
