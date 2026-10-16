using System;

namespace System.Net.Sockets
{
	// Token: 0x020007C9 RID: 1993
	public struct SocketReceiveMessageFromResult
	{
		// Token: 0x04002637 RID: 9783
		public int ReceivedBytes;

		// Token: 0x04002638 RID: 9784
		public SocketFlags SocketFlags;

		// Token: 0x04002639 RID: 9785
		public EndPoint RemoteEndPoint;

		// Token: 0x0400263A RID: 9786
		public IPPacketInformation PacketInformation;
	}
}
