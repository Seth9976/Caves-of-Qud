using System;

namespace System.Net.Sockets
{
	/// <summary>The type of asynchronous socket operation most recently performed with this context object.</summary>
	// Token: 0x020007B2 RID: 1970
	public enum SocketAsyncOperation
	{
		/// <summary>None of the socket operations.</summary>
		// Token: 0x04002543 RID: 9539
		None,
		/// <summary>A socket Accept operation. </summary>
		// Token: 0x04002544 RID: 9540
		Accept,
		/// <summary>A socket Connect operation.</summary>
		// Token: 0x04002545 RID: 9541
		Connect,
		/// <summary>A socket Disconnect operation.</summary>
		// Token: 0x04002546 RID: 9542
		Disconnect,
		/// <summary>A socket Receive operation.</summary>
		// Token: 0x04002547 RID: 9543
		Receive,
		/// <summary>A socket ReceiveFrom operation.</summary>
		// Token: 0x04002548 RID: 9544
		ReceiveFrom,
		/// <summary>A socket ReceiveMessageFrom operation.</summary>
		// Token: 0x04002549 RID: 9545
		ReceiveMessageFrom,
		/// <summary>A socket Send operation.</summary>
		// Token: 0x0400254A RID: 9546
		Send,
		/// <summary>A socket SendPackets operation.</summary>
		// Token: 0x0400254B RID: 9547
		SendPackets,
		/// <summary>A socket SendTo operation.</summary>
		// Token: 0x0400254C RID: 9548
		SendTo
	}
}
