using System;
using System.Threading;

namespace System.Web
{
	// Token: 0x0200007C RID: 124
	internal class AsyncInvoker
	{
		// Token: 0x0600055C RID: 1372 RVA: 0x0000CA34 File Offset: 0x0000AC34
		public AsyncInvoker(BeginEventHandler bh, EndEventHandler eh, HttpApplication a, object d)
		{
			this.begin = bh;
			this.end = eh;
			this.data = d;
			this.app = a;
			this.callback = new AsyncCallback(this.doAsyncCallback);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000CA6B File Offset: 0x0000AC6B
		public AsyncInvoker(BeginEventHandler bh, EndEventHandler eh, HttpApplication app)
			: this(bh, eh, app, null)
		{
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000CA77 File Offset: 0x0000AC77
		public void Invoke(object sender, EventArgs e)
		{
			this.begin(this.app, e, this.callback, this.data);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000CA98 File Offset: 0x0000AC98
		private void doAsyncCallback(IAsyncResult res)
		{
			ThreadPool.QueueUserWorkItem(delegate(object ores)
			{
				IAsyncResult asyncResult = (IAsyncResult)ores;
				try
				{
					this.end(asyncResult);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine(ex.ToString());
				}
			}, res);
		}

		// Token: 0x04000EDD RID: 3805
		public BeginEventHandler begin;

		// Token: 0x04000EDE RID: 3806
		public EndEventHandler end;

		// Token: 0x04000EDF RID: 3807
		public object data;

		// Token: 0x04000EE0 RID: 3808
		private HttpApplication app;

		// Token: 0x04000EE1 RID: 3809
		private AsyncCallback callback;
	}
}
