using System;

namespace System.Net
{
	/// <summary>Defines status codes for the <see cref="T:System.Net.WebException" /> class.</summary>
	// Token: 0x0200060A RID: 1546
	public enum WebExceptionStatus
	{
		/// <summary>No error was encountered.</summary>
		// Token: 0x04001C6B RID: 7275
		Success,
		/// <summary>The name resolver service could not resolve the host name.</summary>
		// Token: 0x04001C6C RID: 7276
		NameResolutionFailure,
		/// <summary>The remote service point could not be contacted at the transport level.</summary>
		// Token: 0x04001C6D RID: 7277
		ConnectFailure,
		/// <summary>A complete response was not received from the remote server.</summary>
		// Token: 0x04001C6E RID: 7278
		ReceiveFailure,
		/// <summary>A complete request could not be sent to the remote server.</summary>
		// Token: 0x04001C6F RID: 7279
		SendFailure,
		/// <summary>The request was a piplined request and the connection was closed before the response was received.</summary>
		// Token: 0x04001C70 RID: 7280
		PipelineFailure,
		/// <summary>The request was canceled, the <see cref="M:System.Net.WebRequest.Abort" /> method was called, or an unclassifiable error occurred. This is the default value for <see cref="P:System.Net.WebException.Status" />.</summary>
		// Token: 0x04001C71 RID: 7281
		RequestCanceled,
		/// <summary>The response received from the server was complete but indicated a protocol-level error. For example, an HTTP protocol error such as 401 Access Denied would use this status.</summary>
		// Token: 0x04001C72 RID: 7282
		ProtocolError,
		/// <summary>The connection was prematurely closed.</summary>
		// Token: 0x04001C73 RID: 7283
		ConnectionClosed,
		/// <summary>A server certificate could not be validated.</summary>
		// Token: 0x04001C74 RID: 7284
		TrustFailure,
		/// <summary>An error occurred while establishing a connection using SSL.</summary>
		// Token: 0x04001C75 RID: 7285
		SecureChannelFailure,
		/// <summary>The server response was not a valid HTTP response.</summary>
		// Token: 0x04001C76 RID: 7286
		ServerProtocolViolation,
		/// <summary>The connection for a request that specifies the Keep-alive header was closed unexpectedly.</summary>
		// Token: 0x04001C77 RID: 7287
		KeepAliveFailure,
		/// <summary>An internal asynchronous request is pending.</summary>
		// Token: 0x04001C78 RID: 7288
		Pending,
		/// <summary>No response was received during the time-out period for a request.</summary>
		// Token: 0x04001C79 RID: 7289
		Timeout,
		/// <summary>The name resolver service could not resolve the proxy host name.</summary>
		// Token: 0x04001C7A RID: 7290
		ProxyNameResolutionFailure,
		/// <summary>An exception of unknown type has occurred.</summary>
		// Token: 0x04001C7B RID: 7291
		UnknownError,
		/// <summary>A message was received that exceeded the specified limit when sending a request or receiving a response from the server.</summary>
		// Token: 0x04001C7C RID: 7292
		MessageLengthLimitExceeded,
		/// <summary>The specified cache entry was not found.</summary>
		// Token: 0x04001C7D RID: 7293
		CacheEntryNotFound,
		/// <summary>The request was not permitted by the cache policy. In general, this occurs when a request is not cacheable and the effective policy prohibits sending the request to the server. You might receive this status if a request method implies the presence of a request body, a request method requires direct interaction with the server, or a request contains a conditional header.</summary>
		// Token: 0x04001C7E RID: 7294
		RequestProhibitedByCachePolicy,
		/// <summary>This request was not permitted by the proxy.</summary>
		// Token: 0x04001C7F RID: 7295
		RequestProhibitedByProxy
	}
}
