using System;

namespace System.Net.Sockets
{
	// Token: 0x020007C7 RID: 1991
	internal enum SocketOperation
	{
		// Token: 0x04002629 RID: 9769
		Accept,
		// Token: 0x0400262A RID: 9770
		Connect,
		// Token: 0x0400262B RID: 9771
		Receive,
		// Token: 0x0400262C RID: 9772
		ReceiveFrom,
		// Token: 0x0400262D RID: 9773
		Send,
		// Token: 0x0400262E RID: 9774
		SendTo,
		// Token: 0x0400262F RID: 9775
		RecvJustCallback,
		// Token: 0x04002630 RID: 9776
		SendJustCallback,
		// Token: 0x04002631 RID: 9777
		Disconnect,
		// Token: 0x04002632 RID: 9778
		AcceptReceive,
		// Token: 0x04002633 RID: 9779
		ReceiveGeneric,
		// Token: 0x04002634 RID: 9780
		SendGeneric
	}
}
