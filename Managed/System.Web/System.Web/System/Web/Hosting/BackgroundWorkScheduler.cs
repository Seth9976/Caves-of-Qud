using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x0200052F RID: 1327
	internal sealed class BackgroundWorkScheduler : IRegisteredObject
	{
		// Token: 0x06003A1E RID: 14878 RVA: 0x0009AD7E File Offset: 0x00098F7E
		internal BackgroundWorkScheduler(Action<BackgroundWorkScheduler> unregisterCallback, Action<AppDomain, Exception> logCallback, Action workItemCompleteCallback = null)
		{
			this._unregisterCallback = unregisterCallback;
			this._logCallback = logCallback;
			this._workItemCompleteCallback = workItemCompleteCallback;
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x0009ADA7 File Offset: 0x00098FA7
		private void FinalShutdown()
		{
			this._unregisterCallback(this);
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x0009ADB8 File Offset: 0x00098FB8
		private async void RunWorkItemImpl(Func<CancellationToken, Task> workItem)
		{
			Task returnedTask = null;
			try
			{
				returnedTask = workItem(this._cancellationTokenHelper.Token);
				await returnedTask.ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (returnedTask == null || !returnedTask.IsCanceled)
				{
					OperationCanceledException ex2 = ex as OperationCanceledException;
					if (ex2 == null || !(ex2.CancellationToken == this._cancellationTokenHelper.Token))
					{
						this._logCallback(AppDomain.CurrentDomain, ex);
					}
				}
			}
			finally
			{
				this.WorkItemComplete();
			}
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x0009ADF7 File Offset: 0x00098FF7
		public void ScheduleWorkItem(Func<CancellationToken, Task> workItem)
		{
			if (this._cancellationTokenHelper.IsCancellationRequested)
			{
				return;
			}
			ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
			{
				lock (this)
				{
					if (this._cancellationTokenHelper.IsCancellationRequested)
					{
						return;
					}
					this._numExecutingWorkItems++;
				}
				this.RunWorkItemImpl((Func<CancellationToken, Task>)state);
			}, workItem);
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x0009AE1C File Offset: 0x0009901C
		public void Stop(bool immediate)
		{
			int numExecutingWorkItems;
			lock (this)
			{
				this._cancellationTokenHelper.Cancel();
				numExecutingWorkItems = this._numExecutingWorkItems;
			}
			if (numExecutingWorkItems == 0)
			{
				this.FinalShutdown();
			}
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x0009AE6C File Offset: 0x0009906C
		private void WorkItemComplete()
		{
			int num2;
			bool isCancellationRequested;
			lock (this)
			{
				int num = this._numExecutingWorkItems - 1;
				this._numExecutingWorkItems = num;
				num2 = num;
				isCancellationRequested = this._cancellationTokenHelper.IsCancellationRequested;
			}
			if (this._workItemCompleteCallback != null)
			{
				this._workItemCompleteCallback();
			}
			if (num2 == 0 && isCancellationRequested)
			{
				this.FinalShutdown();
			}
		}

		// Token: 0x04001FB5 RID: 8117
		private readonly CancellationTokenHelper _cancellationTokenHelper = new CancellationTokenHelper(false);

		// Token: 0x04001FB6 RID: 8118
		private int _numExecutingWorkItems;

		// Token: 0x04001FB7 RID: 8119
		private readonly Action<BackgroundWorkScheduler> _unregisterCallback;

		// Token: 0x04001FB8 RID: 8120
		private readonly Action<AppDomain, Exception> _logCallback;

		// Token: 0x04001FB9 RID: 8121
		private readonly Action _workItemCompleteCallback;
	}
}
