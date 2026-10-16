using System;

namespace System.Net.WebSockets
{
	/// <summary>Contains the list of possible WebSocket errors.</summary>
	// Token: 0x0200083E RID: 2110
	public enum WebSocketError
	{
		/// <summary>Indicates that there was no native error information for the exception.</summary>
		// Token: 0x040028CC RID: 10444
		Success,
		/// <summary>Indicates that a WebSocket frame with an unknown opcode was received.</summary>
		// Token: 0x040028CD RID: 10445
		InvalidMessageType,
		/// <summary>Indicates a general error.</summary>
		// Token: 0x040028CE RID: 10446
		Faulted,
		/// <summary>Indicates that an unknown native error occurred.</summary>
		// Token: 0x040028CF RID: 10447
		NativeError,
		/// <summary>Indicates that the incoming request was not a valid websocket request.</summary>
		// Token: 0x040028D0 RID: 10448
		NotAWebSocket,
		/// <summary>Indicates that the client requested an unsupported version of the WebSocket protocol.</summary>
		// Token: 0x040028D1 RID: 10449
		UnsupportedVersion,
		/// <summary>Indicates that the client requested an unsupported WebSocket subprotocol.</summary>
		// Token: 0x040028D2 RID: 10450
		UnsupportedProtocol,
		/// <summary>Indicates an error occurred when parsing the HTTP headers during the opening handshake.</summary>
		// Token: 0x040028D3 RID: 10451
		HeaderError,
		/// <summary>Indicates that the connection was terminated unexpectedly.</summary>
		// Token: 0x040028D4 RID: 10452
		ConnectionClosedPrematurely,
		/// <summary>Indicates the WebSocket is an invalid state for the given operation (such as being closed or aborted).</summary>
		// Token: 0x040028D5 RID: 10453
		InvalidState
	}
}
