using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the states of a Transmission Control Protocol (TCP) connection.</summary>
	// Token: 0x02000709 RID: 1801
	public enum TcpState
	{
		/// <summary>The TCP connection state is unknown.</summary>
		// Token: 0x040021F1 RID: 8689
		Unknown,
		/// <summary>The TCP connection is closed.</summary>
		// Token: 0x040021F2 RID: 8690
		Closed,
		/// <summary>The local endpoint of the TCP connection is listening for a connection request from any remote endpoint.</summary>
		// Token: 0x040021F3 RID: 8691
		Listen,
		/// <summary>The local endpoint of the TCP connection has sent the remote endpoint a segment header with the synchronize (SYN) control bit set and is waiting for a matching connection request.</summary>
		// Token: 0x040021F4 RID: 8692
		SynSent,
		/// <summary>The local endpoint of the TCP connection has sent and received a connection request and is waiting for an acknowledgment.</summary>
		// Token: 0x040021F5 RID: 8693
		SynReceived,
		/// <summary>The TCP handshake is complete. The connection has been established and data can be sent.</summary>
		// Token: 0x040021F6 RID: 8694
		Established,
		/// <summary>The local endpoint of the TCP connection is waiting for a connection termination request from the remote endpoint or for an acknowledgement of the connection termination request sent previously.</summary>
		// Token: 0x040021F7 RID: 8695
		FinWait1,
		/// <summary>The local endpoint of the TCP connection is waiting for a connection termination request from the remote endpoint.</summary>
		// Token: 0x040021F8 RID: 8696
		FinWait2,
		/// <summary>The local endpoint of the TCP connection is waiting for a connection termination request from the local user.</summary>
		// Token: 0x040021F9 RID: 8697
		CloseWait,
		/// <summary>The local endpoint of the TCP connection is waiting for an acknowledgement of the connection termination request sent previously.</summary>
		// Token: 0x040021FA RID: 8698
		Closing,
		/// <summary>The local endpoint of the TCP connection is waiting for the final acknowledgement of the connection termination request sent previously.</summary>
		// Token: 0x040021FB RID: 8699
		LastAck,
		/// <summary>The local endpoint of the TCP connection is waiting for enough time to pass to ensure that the remote endpoint received the acknowledgement of its connection termination request.</summary>
		// Token: 0x040021FC RID: 8700
		TimeWait,
		/// <summary>The transmission control buffer (TCB) for the TCP connection is being deleted.</summary>
		// Token: 0x040021FD RID: 8701
		DeleteTcb
	}
}
