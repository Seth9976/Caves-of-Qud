using System;

namespace System.Net.Sockets
{
	/// <summary>Defines socket option levels for the <see cref="M:System.Net.Sockets.Socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel,System.Net.Sockets.SocketOptionName,System.Int32)" /> and <see cref="M:System.Net.Sockets.Socket.GetSocketOption(System.Net.Sockets.SocketOptionLevel,System.Net.Sockets.SocketOptionName)" /> methods.</summary>
	// Token: 0x020007B9 RID: 1977
	public enum SocketOptionLevel
	{
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply to all sockets.</summary>
		// Token: 0x04002599 RID: 9625
		Socket = 65535,
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply only to IP sockets.</summary>
		// Token: 0x0400259A RID: 9626
		IP = 0,
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply only to IPv6 sockets.</summary>
		// Token: 0x0400259B RID: 9627
		IPv6 = 41,
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply only to TCP sockets.</summary>
		// Token: 0x0400259C RID: 9628
		Tcp = 6,
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply only to UDP sockets.</summary>
		// Token: 0x0400259D RID: 9629
		Udp = 17
	}
}
