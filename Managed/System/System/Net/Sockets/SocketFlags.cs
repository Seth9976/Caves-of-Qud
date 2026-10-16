using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies socket send and receive behaviors.</summary>
	// Token: 0x020007B6 RID: 1974
	[Flags]
	public enum SocketFlags
	{
		/// <summary>Use no flags for this call.</summary>
		// Token: 0x04002586 RID: 9606
		None = 0,
		/// <summary>Process out-of-band data.</summary>
		// Token: 0x04002587 RID: 9607
		OutOfBand = 1,
		/// <summary>Peek at the incoming message.</summary>
		// Token: 0x04002588 RID: 9608
		Peek = 2,
		/// <summary>Send without using routing tables.</summary>
		// Token: 0x04002589 RID: 9609
		DontRoute = 4,
		/// <summary>Provides a standard value for the number of WSABUF structures that are used to send and receive data. This value is not used or supported on .NET Framework 4.5.</summary>
		// Token: 0x0400258A RID: 9610
		MaxIOVectorLength = 16,
		/// <summary>The message was too large to fit into the specified buffer and was truncated.</summary>
		// Token: 0x0400258B RID: 9611
		Truncated = 256,
		/// <summary>Indicates that the control data did not fit into an internal 64-KB buffer and was truncated.</summary>
		// Token: 0x0400258C RID: 9612
		ControlDataTruncated = 512,
		/// <summary>Indicates a broadcast packet.</summary>
		// Token: 0x0400258D RID: 9613
		Broadcast = 1024,
		/// <summary>Indicates a multicast packet.</summary>
		// Token: 0x0400258E RID: 9614
		Multicast = 2048,
		/// <summary>Partial send or receive for message.</summary>
		// Token: 0x0400258F RID: 9615
		Partial = 32768
	}
}
