using System;

namespace System.Net.Sockets
{
	/// <summary>Defines constants that are used by the <see cref="M:System.Net.Sockets.Socket.Shutdown(System.Net.Sockets.SocketShutdown)" /> method.</summary>
	// Token: 0x020007BB RID: 1979
	public enum SocketShutdown
	{
		/// <summary>Disables a <see cref="T:System.Net.Sockets.Socket" /> for receiving. This field is constant.</summary>
		// Token: 0x040025CE RID: 9678
		Receive,
		/// <summary>Disables a <see cref="T:System.Net.Sockets.Socket" /> for sending. This field is constant.</summary>
		// Token: 0x040025CF RID: 9679
		Send,
		/// <summary>Disables a <see cref="T:System.Net.Sockets.Socket" /> for both sending and receiving. This field is constant.</summary>
		// Token: 0x040025D0 RID: 9680
		Both
	}
}
