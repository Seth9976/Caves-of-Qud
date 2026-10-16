using System;

namespace System.Net.WebSockets
{
	/// <summary> Defines the different states a WebSockets instance can be in.</summary>
	// Token: 0x02000842 RID: 2114
	public enum WebSocketState
	{
		/// <summary>Reserved for future use.</summary>
		// Token: 0x040028E1 RID: 10465
		None,
		/// <summary>The connection is negotiating the handshake with the remote endpoint.</summary>
		// Token: 0x040028E2 RID: 10466
		Connecting,
		/// <summary>The initial state after the HTTP handshake has been completed.</summary>
		// Token: 0x040028E3 RID: 10467
		Open,
		/// <summary>A close message was sent to the remote endpoint.</summary>
		// Token: 0x040028E4 RID: 10468
		CloseSent,
		/// <summary>A close message was received from the remote endpoint.</summary>
		// Token: 0x040028E5 RID: 10469
		CloseReceived,
		/// <summary>Indicates the WebSocket close handshake completed gracefully.</summary>
		// Token: 0x040028E6 RID: 10470
		Closed,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x040028E7 RID: 10471
		Aborted
	}
}
