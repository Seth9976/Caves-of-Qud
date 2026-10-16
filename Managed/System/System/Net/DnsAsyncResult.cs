using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000680 RID: 1664
	internal class DnsAsyncResult : IAsyncResult
	{
		// Token: 0x06003452 RID: 13394 RVA: 0x000B63BA File Offset: 0x000B45BA
		public DnsAsyncResult(AsyncCallback cb, object state)
		{
			this.callback = cb;
			this.state = state;
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000B63D0 File Offset: 0x000B45D0
		public void SetCompleted(bool synch, IPHostEntry entry, Exception e)
		{
			this.synch = synch;
			this.entry = entry;
			this.exc = e;
			lock (this)
			{
				if (this.is_completed)
				{
					return;
				}
				this.is_completed = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
			}
			if (this.callback != null)
			{
				ThreadPool.QueueUserWorkItem(DnsAsyncResult.internal_cb, this);
			}
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000B6454 File Offset: 0x000B4654
		public void SetCompleted(bool synch, Exception e)
		{
			this.SetCompleted(synch, null, e);
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000B645F File Offset: 0x000B465F
		public void SetCompleted(bool synch, IPHostEntry entry)
		{
			this.SetCompleted(synch, entry, null);
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x000B646C File Offset: 0x000B466C
		private static void CB(object _this)
		{
			DnsAsyncResult dnsAsyncResult = (DnsAsyncResult)_this;
			dnsAsyncResult.callback(dnsAsyncResult);
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06003457 RID: 13399 RVA: 0x000B648C File Offset: 0x000B468C
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06003458 RID: 13400 RVA: 0x000B6494 File Offset: 0x000B4694
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				lock (this)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.is_completed);
					}
				}
				return this.handle;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06003459 RID: 13401 RVA: 0x000B64E8 File Offset: 0x000B46E8
		public Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600345A RID: 13402 RVA: 0x000B64F0 File Offset: 0x000B46F0
		public IPHostEntry HostEntry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x0600345B RID: 13403 RVA: 0x000B64F8 File Offset: 0x000B46F8
		public bool CompletedSynchronously
		{
			get
			{
				return this.synch;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x0600345C RID: 13404 RVA: 0x000B6500 File Offset: 0x000B4700
		public bool IsCompleted
		{
			get
			{
				bool flag2;
				lock (this)
				{
					flag2 = this.is_completed;
				}
				return flag2;
			}
		}

		// Token: 0x04001EA7 RID: 7847
		private static WaitCallback internal_cb = new WaitCallback(DnsAsyncResult.CB);

		// Token: 0x04001EA8 RID: 7848
		private ManualResetEvent handle;

		// Token: 0x04001EA9 RID: 7849
		private bool synch;

		// Token: 0x04001EAA RID: 7850
		private bool is_completed;

		// Token: 0x04001EAB RID: 7851
		private AsyncCallback callback;

		// Token: 0x04001EAC RID: 7852
		private object state;

		// Token: 0x04001EAD RID: 7853
		private IPHostEntry entry;

		// Token: 0x04001EAE RID: 7854
		private Exception exc;
	}
}
