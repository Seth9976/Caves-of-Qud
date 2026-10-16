using System;

namespace System.Net.WebSockets
{
	/// <summary>An instance of this class represents the result of performing a single ReceiveAsync operation on a WebSocket.</summary>
	// Token: 0x02000841 RID: 2113
	public class WebSocketReceiveResult
	{
		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketReceiveResult" /> class.</summary>
		/// <param name="count">The number of bytes received.</param>
		/// <param name="messageType">The type of message that was received.</param>
		/// <param name="endOfMessage">Indicates whether this is the final message.</param>
		// Token: 0x0600435B RID: 17243 RVA: 0x000EA4DC File Offset: 0x000E86DC
		public WebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage)
			: this(count, messageType, endOfMessage, null, null)
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketReceiveResult" /> class.</summary>
		/// <param name="count">The number of bytes received.</param>
		/// <param name="messageType">The type of message that was received.</param>
		/// <param name="endOfMessage">Indicates whether this is the final message.</param>
		/// <param name="closeStatus">Indicates the <see cref="T:System.Net.WebSockets.WebSocketCloseStatus" /> of the connection.</param>
		/// <param name="closeStatusDescription">The description of <paramref name="closeStatus" />.</param>
		// Token: 0x0600435C RID: 17244 RVA: 0x000EA4FC File Offset: 0x000E86FC
		public WebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage, WebSocketCloseStatus? closeStatus, string closeStatusDescription)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Count = count;
			this.EndOfMessage = endOfMessage;
			this.MessageType = messageType;
			this.CloseStatus = closeStatus;
			this.CloseStatusDescription = closeStatusDescription;
		}

		/// <summary>Indicates the number of bytes that the WebSocket received.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.</returns>
		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x0600435D RID: 17245 RVA: 0x000EA538 File Offset: 0x000E8738
		public int Count { get; }

		/// <summary>Indicates whether the message has been received completely.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x0600435E RID: 17246 RVA: 0x000EA540 File Offset: 0x000E8740
		public bool EndOfMessage { get; }

		/// <summary>Indicates whether the current message is a UTF-8 message or a binary message.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketMessageType" />.</returns>
		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x0600435F RID: 17247 RVA: 0x000EA548 File Offset: 0x000E8748
		public WebSocketMessageType MessageType { get; }

		/// <summary>Indicates the reason why the remote endpoint initiated the close handshake.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketCloseStatus" />.</returns>
		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06004360 RID: 17248 RVA: 0x000EA550 File Offset: 0x000E8750
		public WebSocketCloseStatus? CloseStatus { get; }

		/// <summary>Returns the optional description that describes why the close handshake has been initiated by the remote endpoint.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06004361 RID: 17249 RVA: 0x000EA558 File Offset: 0x000E8758
		public string CloseStatusDescription { get; }
	}
}
