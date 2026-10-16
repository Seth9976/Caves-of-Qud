using System;
using System.Diagnostics;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200062F RID: 1583
	internal class LazyAsyncResult : IAsyncResult
	{
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x060031E0 RID: 12768 RVA: 0x000AD210 File Offset: 0x000AB410
		private static LazyAsyncResult.ThreadContext CurrentThreadContext
		{
			get
			{
				LazyAsyncResult.ThreadContext threadContext = LazyAsyncResult.t_ThreadContext;
				if (threadContext == null)
				{
					threadContext = new LazyAsyncResult.ThreadContext();
					LazyAsyncResult.t_ThreadContext = threadContext;
				}
				return threadContext;
			}
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000AD233 File Offset: 0x000AB433
		internal LazyAsyncResult(object myObject, object myState, AsyncCallback myCallBack)
		{
			this.m_AsyncObject = myObject;
			this.m_AsyncState = myState;
			this.m_AsyncCallback = myCallBack;
			this.m_Result = DBNull.Value;
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x000AD25B File Offset: 0x000AB45B
		internal LazyAsyncResult(object myObject, object myState, AsyncCallback myCallBack, object result)
		{
			this.m_AsyncObject = myObject;
			this.m_AsyncState = myState;
			this.m_AsyncCallback = myCallBack;
			this.m_Result = result;
			this.m_IntCompleted = 1;
			if (this.m_AsyncCallback != null)
			{
				this.m_AsyncCallback(this);
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x060031E3 RID: 12771 RVA: 0x000AD29B File Offset: 0x000AB49B
		internal object AsyncObject
		{
			get
			{
				return this.m_AsyncObject;
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060031E4 RID: 12772 RVA: 0x000AD2A3 File Offset: 0x000AB4A3
		public object AsyncState
		{
			get
			{
				return this.m_AsyncState;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x000AD2AB File Offset: 0x000AB4AB
		// (set) Token: 0x060031E6 RID: 12774 RVA: 0x000AD2B3 File Offset: 0x000AB4B3
		protected AsyncCallback AsyncCallback
		{
			get
			{
				return this.m_AsyncCallback;
			}
			set
			{
				this.m_AsyncCallback = value;
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x060031E7 RID: 12775 RVA: 0x000AD2BC File Offset: 0x000AB4BC
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				this.m_UserEvent = true;
				if (this.m_IntCompleted == 0)
				{
					Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.m_Event;
				while (manualResetEvent == null)
				{
					this.LazilyCreateEvent(out manualResetEvent);
				}
				return manualResetEvent;
			}
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x000AD308 File Offset: 0x000AB508
		private bool LazilyCreateEvent(out ManualResetEvent waitHandle)
		{
			waitHandle = new ManualResetEvent(false);
			bool flag;
			try
			{
				if (Interlocked.CompareExchange(ref this.m_Event, waitHandle, null) == null)
				{
					if (this.InternalPeekCompleted)
					{
						waitHandle.Set();
					}
					flag = true;
				}
				else
				{
					waitHandle.Close();
					waitHandle = (ManualResetEvent)this.m_Event;
					flag = false;
				}
			}
			catch
			{
				this.m_Event = null;
				if (waitHandle != null)
				{
					waitHandle.Close();
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG")]
		protected void DebugProtectState(bool protect)
		{
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x060031EA RID: 12778 RVA: 0x000AD380 File Offset: 0x000AB580
		public bool CompletedSynchronously
		{
			get
			{
				int num = this.m_IntCompleted;
				if (num == 0)
				{
					num = Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				return num > 0;
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x000AD3B0 File Offset: 0x000AB5B0
		public bool IsCompleted
		{
			get
			{
				int num = this.m_IntCompleted;
				if (num == 0)
				{
					num = Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				return (num & int.MaxValue) != 0;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x000AD3E3 File Offset: 0x000AB5E3
		internal bool InternalPeekCompleted
		{
			get
			{
				return (this.m_IntCompleted & int.MaxValue) != 0;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x060031ED RID: 12781 RVA: 0x000AD3F4 File Offset: 0x000AB5F4
		// (set) Token: 0x060031EE RID: 12782 RVA: 0x000AD40B File Offset: 0x000AB60B
		internal object Result
		{
			get
			{
				if (this.m_Result != DBNull.Value)
				{
					return this.m_Result;
				}
				return null;
			}
			set
			{
				this.m_Result = value;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x060031EF RID: 12783 RVA: 0x000AD414 File Offset: 0x000AB614
		// (set) Token: 0x060031F0 RID: 12784 RVA: 0x000AD41C File Offset: 0x000AB61C
		internal bool EndCalled
		{
			get
			{
				return this.m_EndCalled;
			}
			set
			{
				this.m_EndCalled = value;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x060031F1 RID: 12785 RVA: 0x000AD425 File Offset: 0x000AB625
		// (set) Token: 0x060031F2 RID: 12786 RVA: 0x000AD42D File Offset: 0x000AB62D
		internal int ErrorCode
		{
			get
			{
				return this.m_ErrorCode;
			}
			set
			{
				this.m_ErrorCode = value;
			}
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x000AD438 File Offset: 0x000AB638
		protected void ProtectedInvokeCallback(object result, IntPtr userToken)
		{
			if (result == DBNull.Value)
			{
				throw new ArgumentNullException("result");
			}
			if ((this.m_IntCompleted & 2147483647) == 0 && (Interlocked.Increment(ref this.m_IntCompleted) & 2147483647) == 1)
			{
				if (this.m_Result == DBNull.Value)
				{
					this.m_Result = result;
				}
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.m_Event;
				if (manualResetEvent != null)
				{
					try
					{
						manualResetEvent.Set();
					}
					catch (ObjectDisposedException)
					{
					}
				}
				this.Complete(userToken);
			}
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x000AD4C0 File Offset: 0x000AB6C0
		internal void InvokeCallback(object result)
		{
			this.ProtectedInvokeCallback(result, IntPtr.Zero);
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x000AD4CE File Offset: 0x000AB6CE
		internal void InvokeCallback()
		{
			this.ProtectedInvokeCallback(null, IntPtr.Zero);
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x000AD4DC File Offset: 0x000AB6DC
		protected virtual void Complete(IntPtr userToken)
		{
			bool flag = false;
			LazyAsyncResult.ThreadContext currentThreadContext = LazyAsyncResult.CurrentThreadContext;
			try
			{
				currentThreadContext.m_NestedIOCount++;
				if (this.m_AsyncCallback != null)
				{
					if (currentThreadContext.m_NestedIOCount >= 50)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.WorkerThreadComplete));
						flag = true;
					}
					else
					{
						this.m_AsyncCallback(this);
					}
				}
			}
			finally
			{
				currentThreadContext.m_NestedIOCount--;
				if (!flag)
				{
					this.Cleanup();
				}
			}
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x000AD560 File Offset: 0x000AB760
		private void WorkerThreadComplete(object state)
		{
			try
			{
				this.m_AsyncCallback(this);
			}
			finally
			{
				this.Cleanup();
			}
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void Cleanup()
		{
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x000AD594 File Offset: 0x000AB794
		internal object InternalWaitForCompletion()
		{
			return this.WaitForCompletion(true);
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x000AD5A0 File Offset: 0x000AB7A0
		private object WaitForCompletion(bool snap)
		{
			ManualResetEvent manualResetEvent = null;
			bool flag = false;
			if (!(snap ? this.IsCompleted : this.InternalPeekCompleted))
			{
				manualResetEvent = (ManualResetEvent)this.m_Event;
				if (manualResetEvent == null)
				{
					flag = this.LazilyCreateEvent(out manualResetEvent);
				}
			}
			if (manualResetEvent == null)
			{
				goto IL_0073;
			}
			try
			{
				manualResetEvent.WaitOne(-1, false);
				goto IL_0073;
			}
			catch (ObjectDisposedException)
			{
				goto IL_0073;
			}
			finally
			{
				if (flag && !this.m_UserEvent)
				{
					ManualResetEvent manualResetEvent2 = (ManualResetEvent)this.m_Event;
					this.m_Event = null;
					if (!this.m_UserEvent)
					{
						manualResetEvent2.Close();
					}
				}
			}
			IL_006D:
			Thread.SpinWait(1);
			IL_0073:
			if (this.m_Result != DBNull.Value)
			{
				return this.m_Result;
			}
			goto IL_006D;
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x000AD650 File Offset: 0x000AB850
		internal void InternalCleanup()
		{
			if ((this.m_IntCompleted & 2147483647) == 0 && (Interlocked.Increment(ref this.m_IntCompleted) & 2147483647) == 1)
			{
				this.m_Result = null;
				this.Cleanup();
			}
		}

		// Token: 0x04001D50 RID: 7504
		private const int c_HighBit = -2147483648;

		// Token: 0x04001D51 RID: 7505
		private const int c_ForceAsyncCount = 50;

		// Token: 0x04001D52 RID: 7506
		[ThreadStatic]
		private static LazyAsyncResult.ThreadContext t_ThreadContext;

		// Token: 0x04001D53 RID: 7507
		private object m_AsyncObject;

		// Token: 0x04001D54 RID: 7508
		private object m_AsyncState;

		// Token: 0x04001D55 RID: 7509
		private AsyncCallback m_AsyncCallback;

		// Token: 0x04001D56 RID: 7510
		private object m_Result;

		// Token: 0x04001D57 RID: 7511
		private int m_ErrorCode;

		// Token: 0x04001D58 RID: 7512
		private int m_IntCompleted;

		// Token: 0x04001D59 RID: 7513
		private bool m_EndCalled;

		// Token: 0x04001D5A RID: 7514
		private bool m_UserEvent;

		// Token: 0x04001D5B RID: 7515
		private object m_Event;

		// Token: 0x02000630 RID: 1584
		private class ThreadContext
		{
			// Token: 0x04001D5C RID: 7516
			internal int m_NestedIOCount;
		}
	}
}
