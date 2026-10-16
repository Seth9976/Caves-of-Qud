using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using AiUnity.NLog.Core.Common;

namespace AiUnity.NLog.Core.Internal.NetworkSenders
{
	// Token: 0x02000277 RID: 631
	internal abstract class NetworkSender : IDisposable
	{
		// Token: 0x0600168F RID: 5775 RVA: 0x00079079 File Offset: 0x00077279
		protected NetworkSender(string url)
		{
			this.Address = url;
			this.LastSendTime = Interlocked.Increment(ref NetworkSender.currentSendTime);
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00079098 File Offset: 0x00077298
		~NetworkSender()
		{
			this.Dispose(false);
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x000790C8 File Offset: 0x000772C8
		// (set) Token: 0x06001692 RID: 5778 RVA: 0x000790D0 File Offset: 0x000772D0
		public string Address { get; private set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x000790D9 File Offset: 0x000772D9
		// (set) Token: 0x06001694 RID: 5780 RVA: 0x000790E1 File Offset: 0x000772E1
		public int LastSendTime { get; private set; }

		// Token: 0x06001695 RID: 5781 RVA: 0x000790EA File Offset: 0x000772EA
		public void Initialize()
		{
			this.DoInitialize();
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x000790F2 File Offset: 0x000772F2
		public void Close(AsyncContinuation continuation)
		{
			this.DoClose(continuation);
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x000790FB File Offset: 0x000772FB
		public void FlushAsync(AsyncContinuation continuation)
		{
			this.DoFlush(continuation);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x00079104 File Offset: 0x00077304
		public void Send(byte[] bytes, int offset, int length, AsyncContinuation asyncContinuation)
		{
			this.LastSendTime = Interlocked.Increment(ref NetworkSender.currentSendTime);
			this.DoSend(bytes, offset, length, asyncContinuation);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00079121 File Offset: 0x00077321
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00079130 File Offset: 0x00077330
		protected virtual void DoInitialize()
		{
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00079132 File Offset: 0x00077332
		protected virtual void DoClose(AsyncContinuation continuation)
		{
			continuation(null);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0007913B File Offset: 0x0007733B
		protected virtual void DoFlush(AsyncContinuation continuation)
		{
			continuation(null);
		}

		// Token: 0x0600169D RID: 5789
		protected abstract void DoSend(byte[] bytes, int offset, int length, AsyncContinuation asyncContinuation);

		// Token: 0x0600169E RID: 5790 RVA: 0x00079144 File Offset: 0x00077344
		protected virtual EndPoint ParseEndpointAddress(Uri uri, AddressFamily addressFamily)
		{
			UriHostNameType hostNameType = uri.HostNameType;
			if (hostNameType - UriHostNameType.IPv4 <= 1)
			{
				return new IPEndPoint(IPAddress.Parse(uri.Host), uri.Port);
			}
			foreach (IPAddress ipaddress in Dns.GetHostEntry(uri.Host).AddressList)
			{
				if (ipaddress.AddressFamily == addressFamily || addressFamily == AddressFamily.Unspecified)
				{
					return new IPEndPoint(ipaddress, uri.Port);
				}
			}
			throw new IOException(string.Concat(new string[]
			{
				"Cannot resolve '",
				uri.Host,
				"' to an address in '",
				addressFamily.ToString(),
				"'"
			}));
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x000791F2 File Offset: 0x000773F2
		public virtual void CheckSocket()
		{
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x000791F4 File Offset: 0x000773F4
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close(delegate(Exception ex)
				{
				});
			}
		}

		// Token: 0x04000DE3 RID: 3555
		private static int currentSendTime;
	}
}
