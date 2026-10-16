using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000008 RID: 8
	internal static class TaskToApm
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000020C4 File Offset: 0x000002C4
		public static IAsyncResult Begin(Task task, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult;
			if (task.IsCompleted)
			{
				asyncResult = new TaskToApm.TaskWrapperAsyncResult(task, state, true);
				if (callback != null)
				{
					callback(asyncResult);
				}
			}
			else
			{
				IAsyncResult asyncResult3;
				if (task.AsyncState != state)
				{
					IAsyncResult asyncResult2 = new TaskToApm.TaskWrapperAsyncResult(task, state, false);
					asyncResult3 = asyncResult2;
				}
				else
				{
					asyncResult3 = task;
				}
				asyncResult = asyncResult3;
				if (callback != null)
				{
					TaskToApm.InvokeCallbackWhenTaskCompletes(task, callback, asyncResult);
				}
			}
			return asyncResult;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002114 File Offset: 0x00000314
		public static void End(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task task;
			if (taskWrapperAsyncResult != null)
			{
				task = taskWrapperAsyncResult.Task;
			}
			else
			{
				task = asyncResult as Task;
			}
			if (task == null)
			{
				throw new ArgumentNullException();
			}
			task.GetAwaiter().GetResult();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002154 File Offset: 0x00000354
		public static TResult End<TResult>(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task<TResult> task;
			if (taskWrapperAsyncResult != null)
			{
				task = taskWrapperAsyncResult.Task as Task<TResult>;
			}
			else
			{
				task = asyncResult as Task<TResult>;
			}
			if (task == null)
			{
				throw new ArgumentNullException();
			}
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002198 File Offset: 0x00000398
		private static void InvokeCallbackWhenTaskCompletes(Task antecedent, AsyncCallback callback, IAsyncResult asyncResult)
		{
			antecedent.ConfigureAwait(false).GetAwaiter().OnCompleted(delegate
			{
				callback(asyncResult);
			});
		}

		// Token: 0x02000009 RID: 9
		private sealed class TaskWrapperAsyncResult : IAsyncResult
		{
			// Token: 0x0600001B RID: 27 RVA: 0x000021DC File Offset: 0x000003DC
			internal TaskWrapperAsyncResult(Task task, object state, bool completedSynchronously)
			{
				this.Task = task;
				this._state = state;
				this._completedSynchronously = completedSynchronously;
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x0600001C RID: 28 RVA: 0x000021F9 File Offset: 0x000003F9
			object IAsyncResult.AsyncState
			{
				get
				{
					return this._state;
				}
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600001D RID: 29 RVA: 0x00002201 File Offset: 0x00000401
			bool IAsyncResult.CompletedSynchronously
			{
				get
				{
					return this._completedSynchronously;
				}
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600001E RID: 30 RVA: 0x00002209 File Offset: 0x00000409
			bool IAsyncResult.IsCompleted
			{
				get
				{
					return this.Task.IsCompleted;
				}
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600001F RID: 31 RVA: 0x00002216 File Offset: 0x00000416
			WaitHandle IAsyncResult.AsyncWaitHandle
			{
				get
				{
					return ((IAsyncResult)this.Task).AsyncWaitHandle;
				}
			}

			// Token: 0x0400007B RID: 123
			internal readonly Task Task;

			// Token: 0x0400007C RID: 124
			private readonly object _state;

			// Token: 0x0400007D RID: 125
			private readonly bool _completedSynchronously;
		}
	}
}
