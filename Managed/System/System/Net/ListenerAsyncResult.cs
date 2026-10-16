using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x020006A3 RID: 1699
	internal class ListenerAsyncResult : IAsyncResult
	{
		// Token: 0x06003653 RID: 13907 RVA: 0x000BE494 File Offset: 0x000BC694
		public ListenerAsyncResult(AsyncCallback cb, object state)
		{
			this.cb = cb;
			this.state = state;
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x000BE4B8 File Offset: 0x000BC6B8
		internal void Complete(Exception exc)
		{
			if (this.forward != null)
			{
				this.forward.Complete(exc);
				return;
			}
			this.exception = exc;
			if (this.InGet && exc is ObjectDisposedException)
			{
				this.exception = new HttpListenerException(500, "Listener closed");
			}
			object obj = this.locker;
			lock (obj)
			{
				this.completed = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
				if (this.cb != null)
				{
					ThreadPool.UnsafeQueueUserWorkItem(ListenerAsyncResult.InvokeCB, this);
				}
			}
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x000BE564 File Offset: 0x000BC764
		private static void InvokeCallback(object o)
		{
			ListenerAsyncResult listenerAsyncResult = (ListenerAsyncResult)o;
			if (listenerAsyncResult.forward != null)
			{
				ListenerAsyncResult.InvokeCallback(listenerAsyncResult.forward);
				return;
			}
			try
			{
				listenerAsyncResult.cb(listenerAsyncResult);
			}
			catch
			{
			}
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x000BE5B0 File Offset: 0x000BC7B0
		internal void Complete(HttpListenerContext context)
		{
			this.Complete(context, false);
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x000BE5BC File Offset: 0x000BC7BC
		internal void Complete(HttpListenerContext context, bool synch)
		{
			if (this.forward != null)
			{
				this.forward.Complete(context, synch);
				return;
			}
			this.synch = synch;
			this.context = context;
			object obj = this.locker;
			lock (obj)
			{
				AuthenticationSchemes authenticationSchemes = context.Listener.SelectAuthenticationScheme(context);
				if ((authenticationSchemes == AuthenticationSchemes.Basic || context.Listener.AuthenticationSchemes == AuthenticationSchemes.Negotiate) && context.Request.Headers["Authorization"] == null)
				{
					context.Response.StatusCode = 401;
					context.Response.Headers["WWW-Authenticate"] = authenticationSchemes.ToString() + " realm=\"" + context.Listener.Realm + "\"";
					context.Response.OutputStream.Close();
					IAsyncResult asyncResult = context.Listener.BeginGetContext(this.cb, this.state);
					this.forward = (ListenerAsyncResult)asyncResult;
					object obj2 = this.forward.locker;
					lock (obj2)
					{
						if (this.handle != null)
						{
							this.forward.handle = this.handle;
						}
					}
					ListenerAsyncResult listenerAsyncResult = this.forward;
					int num = 0;
					while (listenerAsyncResult.forward != null)
					{
						if (num > 20)
						{
							this.Complete(new HttpListenerException(400, "Too many authentication errors"));
						}
						listenerAsyncResult = listenerAsyncResult.forward;
						num++;
					}
				}
				else
				{
					this.completed = true;
					this.synch = false;
					if (this.handle != null)
					{
						this.handle.Set();
					}
					if (this.cb != null)
					{
						ThreadPool.UnsafeQueueUserWorkItem(ListenerAsyncResult.InvokeCB, this);
					}
				}
			}
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x000BE7B4 File Offset: 0x000BC9B4
		internal HttpListenerContext GetContext()
		{
			if (this.forward != null)
			{
				return this.forward.GetContext();
			}
			if (this.exception != null)
			{
				throw this.exception;
			}
			return this.context;
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06003659 RID: 13913 RVA: 0x000BE7DF File Offset: 0x000BC9DF
		public object AsyncState
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.AsyncState;
				}
				return this.state;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x0600365A RID: 13914 RVA: 0x000BE7FC File Offset: 0x000BC9FC
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.AsyncWaitHandle;
				}
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

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x0600365B RID: 13915 RVA: 0x000BE86C File Offset: 0x000BCA6C
		public bool CompletedSynchronously
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.CompletedSynchronously;
				}
				return this.synch;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x0600365C RID: 13916 RVA: 0x000BE888 File Offset: 0x000BCA88
		public bool IsCompleted
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.IsCompleted;
				}
				object obj = this.locker;
				bool flag2;
				lock (obj)
				{
					flag2 = this.completed;
				}
				return flag2;
			}
		}

		// Token: 0x04001FBB RID: 8123
		private ManualResetEvent handle;

		// Token: 0x04001FBC RID: 8124
		private bool synch;

		// Token: 0x04001FBD RID: 8125
		private bool completed;

		// Token: 0x04001FBE RID: 8126
		private AsyncCallback cb;

		// Token: 0x04001FBF RID: 8127
		private object state;

		// Token: 0x04001FC0 RID: 8128
		private Exception exception;

		// Token: 0x04001FC1 RID: 8129
		private HttpListenerContext context;

		// Token: 0x04001FC2 RID: 8130
		private object locker = new object();

		// Token: 0x04001FC3 RID: 8131
		private ListenerAsyncResult forward;

		// Token: 0x04001FC4 RID: 8132
		internal bool EndCalled;

		// Token: 0x04001FC5 RID: 8133
		internal bool InGet;

		// Token: 0x04001FC6 RID: 8134
		private static WaitCallback InvokeCB = new WaitCallback(ListenerAsyncResult.InvokeCallback);
	}
}
