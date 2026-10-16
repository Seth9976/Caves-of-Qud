using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Web.Configuration;

namespace System.Web
{
	// Token: 0x020000CE RID: 206
	internal sealed class QueueManager
	{
		// Token: 0x06000AF8 RID: 2808 RVA: 0x0001C55C File Offset: 0x0001A75C
		public QueueManager()
		{
			Exception ex = null;
			try
			{
				HttpRuntimeSection section = HttpRuntime.Section;
				if (section != null)
				{
					this.minFree = section.MinFreeThreads;
					this.minLocalFree = section.MinLocalRequestFreeThreads;
					this.queueLimit = section.AppRequestQueueLimit;
				}
			}
			catch (Exception ex)
			{
			}
			try
			{
				this.queue = new Queue(this.queueLimit);
			}
			catch (Exception ex2)
			{
				if (ex == null)
				{
					this.initialException = ex2;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder("Several exceptions occurred:\n");
					stringBuilder.AppendFormat("--- Exception Q1:\n{0}\n", ex.ToString());
					stringBuilder.AppendFormat("--- Exception Q2:\n{0}\n", ex2.ToString());
					this.initialException = new Exception(stringBuilder.ToString());
				}
			}
			if (this.initialException == null && ex != null)
			{
				this.initialException = ex;
			}
			this.requestsQueuedCounter = new PerformanceCounter("ASP.NET", "Requests Queued");
			this.requestsQueuedCounter.RawValue = 0L;
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0001C670 File Offset: 0x0001A870
		public bool HasException
		{
			get
			{
				return this.initialException != null;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0001C67B File Offset: 0x0001A87B
		public Exception InitialException
		{
			get
			{
				return this.initialException;
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0001C684 File Offset: 0x0001A884
		private bool CanExecuteRequest(HttpWorkerRequest req)
		{
			if (this.disposing)
			{
				return false;
			}
			int num;
			int num2;
			ThreadPool.GetAvailableThreads(out num, out num2);
			bool flag = req != null && req.GetLocalAddress() == "127.0.0.1";
			return num > this.minFree || (flag && num > this.minLocalFree);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0001C6D4 File Offset: 0x0001A8D4
		public HttpWorkerRequest GetNextRequest(HttpWorkerRequest req)
		{
			Queue queue;
			if (!this.CanExecuteRequest(req))
			{
				if (!this.disposing && req != null)
				{
					queue = this.queue;
					lock (queue)
					{
						this.Queue(req);
					}
				}
				return null;
			}
			queue = this.queue;
			HttpWorkerRequest httpWorkerRequest;
			lock (queue)
			{
				httpWorkerRequest = this.Dequeue();
				if (httpWorkerRequest != null)
				{
					if (req != null)
					{
						this.Queue(req);
					}
				}
				else
				{
					httpWorkerRequest = req;
				}
			}
			return httpWorkerRequest;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0001C770 File Offset: 0x0001A970
		private void Queue(HttpWorkerRequest wr)
		{
			if (this.queue.Count < this.queueLimit)
			{
				this.queue.Enqueue(wr);
				this.requestsQueuedCounter.Increment();
				return;
			}
			HttpRuntime.FinishUnavailable(wr);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0001C7A4 File Offset: 0x0001A9A4
		private HttpWorkerRequest Dequeue()
		{
			if (this.queue.Count > 0)
			{
				HttpWorkerRequest httpWorkerRequest = (HttpWorkerRequest)this.queue.Dequeue();
				this.requestsQueuedCounter.Decrement();
				return httpWorkerRequest;
			}
			return null;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0001C7D4 File Offset: 0x0001A9D4
		public void Dispose()
		{
			if (this.disposing)
			{
				return;
			}
			this.disposing = true;
			HttpWorkerRequest nextRequest;
			while ((nextRequest = this.GetNextRequest(null)) != null)
			{
				HttpRuntime.FinishUnavailable(nextRequest);
			}
			this.queue = null;
		}

		// Token: 0x0400108A RID: 4234
		private int minFree = 8;

		// Token: 0x0400108B RID: 4235
		private int minLocalFree = 4;

		// Token: 0x0400108C RID: 4236
		private int queueLimit = 5000;

		// Token: 0x0400108D RID: 4237
		private Queue queue;

		// Token: 0x0400108E RID: 4238
		private bool disposing;

		// Token: 0x0400108F RID: 4239
		private Exception initialException;

		// Token: 0x04001090 RID: 4240
		private PerformanceCounter requestsQueuedCounter;
	}
}
