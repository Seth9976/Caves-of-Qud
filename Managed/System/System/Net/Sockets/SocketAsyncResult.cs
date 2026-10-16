using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x020007C5 RID: 1989
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class SocketAsyncResult : IOAsyncResult
	{
		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06003FA1 RID: 16289 RVA: 0x000D907B File Offset: 0x000D727B
		public IntPtr Handle
		{
			get
			{
				if (this.socket == null)
				{
					return IntPtr.Zero;
				}
				return this.socket.Handle;
			}
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x000D9096 File Offset: 0x000D7296
		public SocketAsyncResult()
		{
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x000D90A0 File Offset: 0x000D72A0
		public void Init(Socket socket, AsyncCallback callback, object state, SocketOperation operation)
		{
			base.Init(callback, state);
			this.socket = socket;
			this.operation = operation;
			this.DelayedException = null;
			this.EndPoint = null;
			this.Buffer = null;
			this.Offset = 0;
			this.Size = 0;
			this.SockFlags = SocketFlags.None;
			this.AcceptSocket = null;
			this.Addresses = null;
			this.Port = 0;
			this.Buffers = null;
			this.ReuseSocket = false;
			this.CurrentAddress = 0;
			this.AcceptedSocket = null;
			this.Total = 0;
			this.error = 0;
			this.EndCalled = 0;
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x000D9139 File Offset: 0x000D7339
		public SocketAsyncResult(Socket socket, AsyncCallback callback, object state, SocketOperation operation)
			: base(callback, state)
		{
			this.socket = socket;
			this.operation = operation;
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06003FA5 RID: 16293 RVA: 0x000D9154 File Offset: 0x000D7354
		public SocketError ErrorCode
		{
			get
			{
				SocketException ex = this.DelayedException as SocketException;
				if (ex != null)
				{
					return ex.SocketErrorCode;
				}
				if (this.error != 0)
				{
					return (SocketError)this.error;
				}
				return SocketError.Success;
			}
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x000D9187 File Offset: 0x000D7387
		public void CheckIfThrowDelayedException()
		{
			if (this.DelayedException != null)
			{
				this.socket.is_connected = false;
				throw this.DelayedException;
			}
			if (this.error != 0)
			{
				this.socket.is_connected = false;
				throw new SocketException(this.error);
			}
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x000D91C4 File Offset: 0x000D73C4
		internal override void CompleteDisposed()
		{
			this.Complete();
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x000D91CC File Offset: 0x000D73CC
		public void Complete()
		{
			if (this.operation != SocketOperation.Receive && this.socket.CleanedUp)
			{
				this.DelayedException = new ObjectDisposedException(this.socket.GetType().ToString());
			}
			base.IsCompleted = true;
			Socket socket = this.socket;
			SocketOperation socketOperation = this.operation;
			if (!base.CompletedSynchronously && base.AsyncCallback != null)
			{
				ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
				{
					((SocketAsyncResult)state).AsyncCallback((SocketAsyncResult)state);
				}, this);
			}
			switch (socketOperation)
			{
			case SocketOperation.Accept:
			case SocketOperation.Receive:
			case SocketOperation.ReceiveFrom:
			case SocketOperation.ReceiveGeneric:
				socket.ReadSem.Release();
				return;
			case SocketOperation.Connect:
			case SocketOperation.RecvJustCallback:
			case SocketOperation.SendJustCallback:
			case SocketOperation.Disconnect:
			case SocketOperation.AcceptReceive:
				break;
			case SocketOperation.Send:
			case SocketOperation.SendTo:
			case SocketOperation.SendGeneric:
				socket.WriteSem.Release();
				break;
			default:
				return;
			}
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x000D92A5 File Offset: 0x000D74A5
		public void Complete(bool synch)
		{
			base.CompletedSynchronously = synch;
			this.Complete();
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x000D92B4 File Offset: 0x000D74B4
		public void Complete(int total)
		{
			this.Total = total;
			this.Complete();
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x000D92C3 File Offset: 0x000D74C3
		public void Complete(Exception e, bool synch)
		{
			this.DelayedException = e;
			base.CompletedSynchronously = synch;
			this.Complete();
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x000D92D9 File Offset: 0x000D74D9
		public void Complete(Exception e)
		{
			this.DelayedException = e;
			this.Complete();
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x000D92E8 File Offset: 0x000D74E8
		public void Complete(Socket s)
		{
			this.AcceptedSocket = s;
			this.Complete();
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x000D92F7 File Offset: 0x000D74F7
		public void Complete(Socket s, int total)
		{
			this.AcceptedSocket = s;
			this.Total = total;
			this.Complete();
		}

		// Token: 0x04002614 RID: 9748
		public Socket socket;

		// Token: 0x04002615 RID: 9749
		public SocketOperation operation;

		// Token: 0x04002616 RID: 9750
		private Exception DelayedException;

		// Token: 0x04002617 RID: 9751
		public EndPoint EndPoint;

		// Token: 0x04002618 RID: 9752
		public Memory<byte> Buffer;

		// Token: 0x04002619 RID: 9753
		public int Offset;

		// Token: 0x0400261A RID: 9754
		public int Size;

		// Token: 0x0400261B RID: 9755
		public SocketFlags SockFlags;

		// Token: 0x0400261C RID: 9756
		public Socket AcceptSocket;

		// Token: 0x0400261D RID: 9757
		public IPAddress[] Addresses;

		// Token: 0x0400261E RID: 9758
		public int Port;

		// Token: 0x0400261F RID: 9759
		public IList<ArraySegment<byte>> Buffers;

		// Token: 0x04002620 RID: 9760
		public bool ReuseSocket;

		// Token: 0x04002621 RID: 9761
		public int CurrentAddress;

		// Token: 0x04002622 RID: 9762
		public Socket AcceptedSocket;

		// Token: 0x04002623 RID: 9763
		public int Total;

		// Token: 0x04002624 RID: 9764
		internal int error;

		// Token: 0x04002625 RID: 9765
		public int EndCalled;
	}
}
