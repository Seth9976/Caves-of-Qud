using System;

namespace System.Net.Sockets
{
	// Token: 0x02000794 RID: 1940
	internal sealed class DualSocketMultipleConnectAsync : MultipleConnectAsync
	{
		// Token: 0x06003CF5 RID: 15605 RVA: 0x000CF57B File Offset: 0x000CD77B
		public DualSocketMultipleConnectAsync(SocketType socketType, ProtocolType protocolType)
		{
			if (Socket.OSSupportsIPv4)
			{
				this._socket4 = new Socket(AddressFamily.InterNetwork, socketType, protocolType);
			}
			if (Socket.OSSupportsIPv6)
			{
				this._socket6 = new Socket(AddressFamily.InterNetworkV6, socketType, protocolType);
			}
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x000CF5B0 File Offset: 0x000CD7B0
		protected override IPAddress GetNextAddress(out Socket attemptSocket)
		{
			IPAddress ipaddress = null;
			attemptSocket = null;
			while (attemptSocket == null)
			{
				if (this._nextAddress >= this._addressList.Length)
				{
					return null;
				}
				ipaddress = this._addressList[this._nextAddress];
				this._nextAddress++;
				if (ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
				{
					attemptSocket = this._socket6;
				}
				else if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
				{
					attemptSocket = this._socket4;
				}
			}
			Socket socket = attemptSocket;
			if (socket != null)
			{
				socket.ReplaceHandleIfNecessaryAfterFailedConnect();
			}
			return ipaddress;
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x000CF62C File Offset: 0x000CD82C
		protected override void OnSucceed()
		{
			if (this._socket4 != null && !this._socket4.Connected)
			{
				this._socket4.Dispose();
			}
			if (this._socket6 != null && !this._socket6.Connected)
			{
				this._socket6.Dispose();
			}
		}

		// Token: 0x06003CF8 RID: 15608 RVA: 0x000CF679 File Offset: 0x000CD879
		protected override void OnFail(bool abortive)
		{
			Socket socket = this._socket4;
			if (socket != null)
			{
				socket.Dispose();
			}
			Socket socket2 = this._socket6;
			if (socket2 == null)
			{
				return;
			}
			socket2.Dispose();
		}

		// Token: 0x0400243B RID: 9275
		private Socket _socket4;

		// Token: 0x0400243C RID: 9276
		private Socket _socket6;
	}
}
