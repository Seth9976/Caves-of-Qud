using System;

namespace System.Net.Sockets
{
	/// <summary>Describes states for a <see cref="T:System.Net.Sockets.Socket" />.</summary>
	// Token: 0x020007B8 RID: 1976
	[Flags]
	public enum SocketInformationOptions
	{
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is nonblocking.</summary>
		// Token: 0x04002594 RID: 9620
		NonBlocking = 1,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is connected.</summary>
		// Token: 0x04002595 RID: 9621
		Connected = 2,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is listening for new connections.</summary>
		// Token: 0x04002596 RID: 9622
		Listening = 4,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> uses overlapped I/O.</summary>
		// Token: 0x04002597 RID: 9623
		UseOnlyOverlappedIO = 8
	}
}
