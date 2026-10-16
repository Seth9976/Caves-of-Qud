using System;
using System.Net.Sockets;

namespace AiUnity.NLog.Core.Internal.NetworkSenders
{
	// Token: 0x02000276 RID: 630
	internal interface ISocket
	{
		// Token: 0x0600168B RID: 5771
		bool ConnectAsync(SocketAsyncEventArgs args);

		// Token: 0x0600168C RID: 5772
		void Close();

		// Token: 0x0600168D RID: 5773
		bool SendAsync(SocketAsyncEventArgs args);

		// Token: 0x0600168E RID: 5774
		bool SendToAsync(SocketAsyncEventArgs args);
	}
}
