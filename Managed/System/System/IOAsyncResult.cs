using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x02000173 RID: 371
	[StructLayout(LayoutKind.Sequential)]
	internal abstract class IOAsyncResult : IAsyncResult
	{
		// Token: 0x060009D1 RID: 2513 RVA: 0x0000219B File Offset: 0x0000039B
		protected IOAsyncResult()
		{
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0002B54B File Offset: 0x0002974B
		protected void Init(AsyncCallback async_callback, object async_state)
		{
			this.async_callback = async_callback;
			this.async_state = async_state;
			this.completed = false;
			this.completed_synchronously = false;
			if (this.wait_handle != null)
			{
				this.wait_handle.Reset();
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0002B57D File Offset: 0x0002977D
		protected IOAsyncResult(AsyncCallback async_callback, object async_state)
		{
			this.async_callback = async_callback;
			this.async_state = async_state;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0002B593 File Offset: 0x00029793
		public AsyncCallback AsyncCallback
		{
			get
			{
				return this.async_callback;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0002B59B File Offset: 0x0002979B
		public object AsyncState
		{
			get
			{
				return this.async_state;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0002B5A4 File Offset: 0x000297A4
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				WaitHandle waitHandle;
				lock (this)
				{
					if (this.wait_handle == null)
					{
						this.wait_handle = new ManualResetEvent(this.completed);
					}
					waitHandle = this.wait_handle;
				}
				return waitHandle;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x0002B5FC File Offset: 0x000297FC
		// (set) Token: 0x060009D8 RID: 2520 RVA: 0x0002B604 File Offset: 0x00029804
		public bool CompletedSynchronously
		{
			get
			{
				return this.completed_synchronously;
			}
			protected set
			{
				this.completed_synchronously = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0002B60D File Offset: 0x0002980D
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x0002B618 File Offset: 0x00029818
		public bool IsCompleted
		{
			get
			{
				return this.completed;
			}
			protected set
			{
				this.completed = value;
				lock (this)
				{
					if (value && this.wait_handle != null)
					{
						this.wait_handle.Set();
					}
				}
			}
		}

		// Token: 0x060009DB RID: 2523
		internal abstract void CompleteDisposed();

		// Token: 0x040006C6 RID: 1734
		private AsyncCallback async_callback;

		// Token: 0x040006C7 RID: 1735
		private object async_state;

		// Token: 0x040006C8 RID: 1736
		private ManualResetEvent wait_handle;

		// Token: 0x040006C9 RID: 1737
		private bool completed_synchronously;

		// Token: 0x040006CA RID: 1738
		private bool completed;
	}
}
