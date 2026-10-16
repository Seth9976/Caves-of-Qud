using System;

namespace System.Web.WebSockets
{
	/// <summary>Specifies configuration settings for an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
	// Token: 0x02000107 RID: 263
	public sealed class AspNetWebSocketOptions
	{
		/// <summary>Gets or sets whether the URL that initiatedthe WebSocket connection corresponds to the current server.</summary>
		/// <returns>true if the URL that initiatedthe WebSocket connection corresponds to the current server; otherwise, false.</returns>
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x00024FF2 File Offset: 0x000231F2
		// (set) Token: 0x06000D98 RID: 3480 RVA: 0x00024FFA File Offset: 0x000231FA
		public bool RequireSameOrigin { get; set; }

		/// <summary>Gets or sets the name of an application-specific protocol that a remote client and a server can use to exchange data over an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
		/// <returns>The name of the protocol.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The protocol name assigned to the property contains invalid characters.</exception>
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x00025003 File Offset: 0x00023203
		// (set) Token: 0x06000D9A RID: 3482 RVA: 0x0002500B File Offset: 0x0002320B
		public string SubProtocol
		{
			get
			{
				return this._subProtocol;
			}
			set
			{
				if (value != null && !SubProtocolUtil.IsValidSubProtocolName(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._subProtocol = value;
			}
		}

		// Token: 0x0400116E RID: 4462
		private string _subProtocol;
	}
}
