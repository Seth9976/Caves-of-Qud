using System;

namespace System.Net.WebSockets
{
	// Token: 0x02000838 RID: 2104
	public readonly struct ValueWebSocketReceiveResult
	{
		// Token: 0x06004318 RID: 17176 RVA: 0x000E9B72 File Offset: 0x000E7D72
		public ValueWebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage)
		{
			if (count < 0)
			{
				ValueWebSocketReceiveResult.ThrowCountOutOfRange();
			}
			if (messageType > WebSocketMessageType.Close)
			{
				ValueWebSocketReceiveResult.ThrowMessageTypeOutOfRange();
			}
			this._countAndEndOfMessage = (uint)(count | (endOfMessage ? int.MinValue : 0));
			this._messageType = messageType;
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06004319 RID: 17177 RVA: 0x000E9BA0 File Offset: 0x000E7DA0
		public int Count
		{
			get
			{
				return (int)(this._countAndEndOfMessage & 2147483647U);
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x0600431A RID: 17178 RVA: 0x000E9BAE File Offset: 0x000E7DAE
		public bool EndOfMessage
		{
			get
			{
				return (this._countAndEndOfMessage & 2147483648U) == 2147483648U;
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x0600431B RID: 17179 RVA: 0x000E9BC3 File Offset: 0x000E7DC3
		public WebSocketMessageType MessageType
		{
			get
			{
				return this._messageType;
			}
		}

		// Token: 0x0600431C RID: 17180 RVA: 0x000E9BCB File Offset: 0x000E7DCB
		private static void ThrowCountOutOfRange()
		{
			throw new ArgumentOutOfRangeException("count");
		}

		// Token: 0x0600431D RID: 17181 RVA: 0x000E9BD7 File Offset: 0x000E7DD7
		private static void ThrowMessageTypeOutOfRange()
		{
			throw new ArgumentOutOfRangeException("messageType");
		}

		// Token: 0x040028AE RID: 10414
		private readonly uint _countAndEndOfMessage;

		// Token: 0x040028AF RID: 10415
		private readonly WebSocketMessageType _messageType;
	}
}
