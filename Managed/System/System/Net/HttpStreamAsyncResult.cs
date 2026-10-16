using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000696 RID: 1686
	internal class HttpStreamAsyncResult : IAsyncResult
	{
		// Token: 0x06003571 RID: 13681 RVA: 0x000BABF2 File Offset: 0x000B8DF2
		public void Complete(Exception e)
		{
			this.Error = e;
			this.Complete();
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x000BAC04 File Offset: 0x000B8E04
		public void Complete()
		{
			object obj = this.locker;
			lock (obj)
			{
				if (!this.completed)
				{
					this.completed = true;
					if (this.handle != null)
					{
						this.handle.Set();
					}
					if (this.Callback != null)
					{
						this.Callback.BeginInvoke(this, null, null);
					}
				}
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06003573 RID: 13683 RVA: 0x000BAC7C File Offset: 0x000B8E7C
		public object AsyncState
		{
			get
			{
				return this.State;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06003574 RID: 13684 RVA: 0x000BAC84 File Offset: 0x000B8E84
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				object obj = this.locker;
				lock (obj)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.completed);
					}
				}
				return this.handle;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06003575 RID: 13685 RVA: 0x000BACE0 File Offset: 0x000B8EE0
		public bool CompletedSynchronously
		{
			get
			{
				return this.SynchRead == this.Count;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06003576 RID: 13686 RVA: 0x000BACF0 File Offset: 0x000B8EF0
		public bool IsCompleted
		{
			get
			{
				object obj = this.locker;
				bool flag2;
				lock (obj)
				{
					flag2 = this.completed;
				}
				return flag2;
			}
		}

		// Token: 0x04001F33 RID: 7987
		private object locker = new object();

		// Token: 0x04001F34 RID: 7988
		private ManualResetEvent handle;

		// Token: 0x04001F35 RID: 7989
		private bool completed;

		// Token: 0x04001F36 RID: 7990
		internal byte[] Buffer;

		// Token: 0x04001F37 RID: 7991
		internal int Offset;

		// Token: 0x04001F38 RID: 7992
		internal int Count;

		// Token: 0x04001F39 RID: 7993
		internal AsyncCallback Callback;

		// Token: 0x04001F3A RID: 7994
		internal object State;

		// Token: 0x04001F3B RID: 7995
		internal int SynchRead;

		// Token: 0x04001F3C RID: 7996
		internal Exception Error;
	}
}
