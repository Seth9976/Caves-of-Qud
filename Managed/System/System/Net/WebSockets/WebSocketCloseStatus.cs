using System;

namespace System.Net.WebSockets
{
	/// <summary>Represents well known WebSocket close codes as defined in section 11.7 of the WebSocket protocol spec.</summary>
	// Token: 0x0200083C RID: 2108
	public enum WebSocketCloseStatus
	{
		/// <summary>(1000) The connection has closed after the request was fulfilled.</summary>
		// Token: 0x040028C1 RID: 10433
		NormalClosure = 1000,
		/// <summary>(1001) Indicates an endpoint is being removed. Either the server or client will become unavailable.</summary>
		// Token: 0x040028C2 RID: 10434
		EndpointUnavailable,
		/// <summary>(1002) The client or server is terminating the connection because of a protocol error.</summary>
		// Token: 0x040028C3 RID: 10435
		ProtocolError,
		/// <summary>(1003) The client or server is terminating the connection because it cannot accept the data type it received.</summary>
		// Token: 0x040028C4 RID: 10436
		InvalidMessageType,
		/// <summary>No error specified.</summary>
		// Token: 0x040028C5 RID: 10437
		Empty = 1005,
		/// <summary>(1007) The client or server is terminating the connection because it has received data inconsistent with the message type.</summary>
		// Token: 0x040028C6 RID: 10438
		InvalidPayloadData = 1007,
		/// <summary>(1008) The connection will be closed because an endpoint has received a message that violates its policy.</summary>
		// Token: 0x040028C7 RID: 10439
		PolicyViolation,
		/// <summary>(1004) Reserved for future use.</summary>
		// Token: 0x040028C8 RID: 10440
		MessageTooBig,
		/// <summary>(1010) The client is terminating the connection because it expected the server to negotiate an extension.</summary>
		// Token: 0x040028C9 RID: 10441
		MandatoryExtension,
		/// <summary>The connection will be closed by the server because of an error on the server.</summary>
		// Token: 0x040028CA RID: 10442
		InternalServerError
	}
}
