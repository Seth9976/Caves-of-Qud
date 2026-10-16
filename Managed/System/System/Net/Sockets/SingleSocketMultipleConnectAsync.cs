using System;

namespace System.Net.Sockets
{
	// Token: 0x02000793 RID: 1939
	internal sealed class SingleSocketMultipleConnectAsync : MultipleConnectAsync
	{
		// Token: 0x06003CF1 RID: 15601 RVA: 0x000CF4E6 File Offset: 0x000CD6E6
		public SingleSocketMultipleConnectAsync(Socket socket, bool userSocket)
		{
			this._socket = socket;
			this._userSocket = userSocket;
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x000CF4FC File Offset: 0x000CD6FC
		protected override IPAddress GetNextAddress(out Socket attemptSocket)
		{
			this._socket.ReplaceHandleIfNecessaryAfterFailedConnect();
			while (this._nextAddress < this._addressList.Length)
			{
				IPAddress ipaddress = this._addressList[this._nextAddress];
				this._nextAddress++;
				if (this._socket.CanTryAddressFamily(ipaddress.AddressFamily))
				{
					attemptSocket = this._socket;
					return ipaddress;
				}
			}
			attemptSocket = null;
			return null;
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x000CF563 File Offset: 0x000CD763
		protected override void OnFail(bool abortive)
		{
			if (abortive || !this._userSocket)
			{
				this._socket.Dispose();
			}
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void OnSucceed()
		{
		}

		// Token: 0x04002439 RID: 9273
		private Socket _socket;

		// Token: 0x0400243A RID: 9274
		private bool _userSocket;
	}
}
