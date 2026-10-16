using System;

namespace System.Net
{
	/// <summary>Defines transport types for the <see cref="T:System.Net.SocketPermission" /> and <see cref="T:System.Net.Sockets.Socket" /> classes.</summary>
	// Token: 0x02000607 RID: 1543
	public enum TransportType
	{
		/// <summary>UDP transport.</summary>
		// Token: 0x04001C5D RID: 7261
		Udp = 1,
		/// <summary>The transport type is connectionless, such as UDP. Specifying this value has the same effect as specifying <see cref="F:System.Net.TransportType.Udp" />.</summary>
		// Token: 0x04001C5E RID: 7262
		Connectionless = 1,
		/// <summary>TCP transport.</summary>
		// Token: 0x04001C5F RID: 7263
		Tcp,
		/// <summary>The transport is connection oriented, such as TCP. Specifying this value has the same effect as specifying <see cref="F:System.Net.TransportType.Tcp" />.</summary>
		// Token: 0x04001C60 RID: 7264
		ConnectionOriented = 2,
		/// <summary>All transport types.</summary>
		// Token: 0x04001C61 RID: 7265
		All
	}
}
