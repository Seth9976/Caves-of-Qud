using System;

namespace System.Net.WebSockets
{
	/// <summary>Indicates the message type.</summary>
	// Token: 0x02000840 RID: 2112
	public enum WebSocketMessageType
	{
		/// <summary>The message is clear text.</summary>
		// Token: 0x040028D8 RID: 10456
		Text,
		/// <summary>The message is in binary format.</summary>
		// Token: 0x040028D9 RID: 10457
		Binary,
		/// <summary>A receive has completed because a close message was received.</summary>
		// Token: 0x040028DA RID: 10458
		Close
	}
}
