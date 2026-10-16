using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using AiUnity.NLog.Core.Common;

namespace AiUnity.NLog.Core.Internal.NetworkSenders
{
	// Token: 0x0200027A RID: 634
	internal class TcpNetworkSender : NetworkSender
	{
		// Token: 0x060016AB RID: 5803 RVA: 0x00079377 File Offset: 0x00077577
		public TcpNetworkSender(string url, AddressFamily addressFamily)
			: base(url)
		{
			this.AddressFamily = addressFamily;
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x00079392 File Offset: 0x00077592
		// (set) Token: 0x060016AD RID: 5805 RVA: 0x0007939A File Offset: 0x0007759A
		internal AddressFamily AddressFamily { get; set; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x000793A3 File Offset: 0x000775A3
		// (set) Token: 0x060016AF RID: 5807 RVA: 0x000793AB File Offset: 0x000775AB
		internal int MaxQueueSize { get; set; }

		// Token: 0x060016B0 RID: 5808 RVA: 0x000793B4 File Offset: 0x000775B4
		protected internal virtual ISocket CreateSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			return new SocketProxy(addressFamily, socketType, protocolType);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x000793C0 File Offset: 0x000775C0
		protected override void DoInitialize()
		{
			TcpNetworkSender.MySocketAsyncEventArgs e = new TcpNetworkSender.MySocketAsyncEventArgs();
			e.RemoteEndPoint = this.ParseEndpointAddress(new Uri(base.Address), this.AddressFamily);
			e.Completed += this.SocketOperationCompleted;
			e.UserToken = null;
			this.socket = this.CreateSocket(e.RemoteEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			this.asyncOperationInProgress = true;
			if (!this.socket.ConnectAsync(e))
			{
				this.SocketOperationCompleted(this.socket, e);
			}
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00079444 File Offset: 0x00077644
		protected override void DoClose(AsyncContinuation continuation)
		{
			lock (this)
			{
				if (this.asyncOperationInProgress)
				{
					this.closeContinuation = continuation;
				}
				else
				{
					this.CloseSocket(continuation);
				}
			}
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00079494 File Offset: 0x00077694
		protected override void DoFlush(AsyncContinuation continuation)
		{
			lock (this)
			{
				if (!this.asyncOperationInProgress && this.pendingRequests.Count == 0)
				{
					continuation(null);
				}
				else
				{
					this.flushContinuation = continuation;
				}
			}
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x000794F0 File Offset: 0x000776F0
		protected override void DoSend(byte[] bytes, int offset, int length, AsyncContinuation asyncContinuation)
		{
			TcpNetworkSender.MySocketAsyncEventArgs e = new TcpNetworkSender.MySocketAsyncEventArgs();
			e.SetBuffer(bytes, offset, length);
			e.UserToken = asyncContinuation;
			e.Completed += this.SocketOperationCompleted;
			lock (this)
			{
				if (this.MaxQueueSize != 0 && this.pendingRequests.Count >= this.MaxQueueSize)
				{
					SocketAsyncEventArgs e2 = this.pendingRequests.Dequeue();
					if (e2 != null)
					{
						e2.Dispose();
					}
				}
				this.pendingRequests.Enqueue(e);
			}
			this.ProcessNextQueuedItem();
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00079590 File Offset: 0x00077790
		private void CloseSocket(AsyncContinuation continuation)
		{
			try
			{
				ISocket socket = this.socket;
				this.socket = null;
				if (socket != null)
				{
					socket.Close();
				}
				continuation(null);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				continuation(ex);
			}
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x000795E4 File Offset: 0x000777E4
		private void SocketOperationCompleted(object sender, SocketAsyncEventArgs e)
		{
			lock (this)
			{
				this.asyncOperationInProgress = false;
				AsyncContinuation asyncContinuation = e.UserToken as AsyncContinuation;
				if (e.SocketError != SocketError.Success)
				{
					this.pendingError = new IOException("Error: " + e.SocketError.ToString());
				}
				e.Dispose();
				if (asyncContinuation != null)
				{
					asyncContinuation(this.pendingError);
				}
			}
			this.ProcessNextQueuedItem();
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00079678 File Offset: 0x00077878
		private void ProcessNextQueuedItem()
		{
			lock (this)
			{
				if (!this.asyncOperationInProgress)
				{
					if (this.pendingError != null)
					{
						while (this.pendingRequests.Count != 0)
						{
							SocketAsyncEventArgs e = this.pendingRequests.Dequeue();
							AsyncContinuation asyncContinuation = (AsyncContinuation)e.UserToken;
							e.Dispose();
							asyncContinuation(this.pendingError);
						}
					}
					if (this.pendingRequests.Count == 0)
					{
						AsyncContinuation asyncContinuation2 = this.flushContinuation;
						if (asyncContinuation2 != null)
						{
							this.flushContinuation = null;
							asyncContinuation2(this.pendingError);
						}
						AsyncContinuation asyncContinuation3 = this.closeContinuation;
						if (asyncContinuation3 != null)
						{
							this.closeContinuation = null;
							this.CloseSocket(asyncContinuation3);
						}
					}
					else
					{
						SocketAsyncEventArgs e = this.pendingRequests.Dequeue();
						this.asyncOperationInProgress = true;
						if (!this.socket.SendAsync(e))
						{
							this.SocketOperationCompleted(this.socket, e);
						}
					}
				}
			}
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00079770 File Offset: 0x00077970
		public override void CheckSocket()
		{
			if (this.socket == null)
			{
				this.DoInitialize();
			}
		}

		// Token: 0x04000DE8 RID: 3560
		private readonly Queue<SocketAsyncEventArgs> pendingRequests = new Queue<SocketAsyncEventArgs>();

		// Token: 0x04000DE9 RID: 3561
		private ISocket socket;

		// Token: 0x04000DEA RID: 3562
		private Exception pendingError;

		// Token: 0x04000DEB RID: 3563
		private bool asyncOperationInProgress;

		// Token: 0x04000DEC RID: 3564
		private AsyncContinuation closeContinuation;

		// Token: 0x04000DED RID: 3565
		private AsyncContinuation flushContinuation;

		// Token: 0x02001678 RID: 5752
		internal class MySocketAsyncEventArgs : SocketAsyncEventArgs
		{
			// Token: 0x0600D82B RID: 55339 RVA: 0x004FD7D6 File Offset: 0x004FB9D6
			public void RaiseCompleted()
			{
				this.OnCompleted(this);
			}
		}
	}
}
