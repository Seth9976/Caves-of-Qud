using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies whether a <see cref="T:System.Net.Sockets.Socket" /> will remain connected after a call to the <see cref="M:System.Net.Sockets.Socket.Close" /> or <see cref="M:System.Net.Sockets.TcpClient.Close" /> methods and the length of time it will remain connected, if data remains to be sent.</summary>
	// Token: 0x020007AC RID: 1964
	public class LingerOption
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.LingerOption" /> class.</summary>
		/// <param name="enable">true to remain connected after the <see cref="M:System.Net.Sockets.Socket.Close" /> method is called; otherwise, false. </param>
		/// <param name="seconds">The number of seconds to remain connected after the <see cref="M:System.Net.Sockets.Socket.Close" /> method is called. </param>
		// Token: 0x06003EAE RID: 16046 RVA: 0x000D67BC File Offset: 0x000D49BC
		public LingerOption(bool enable, int seconds)
		{
			this.Enabled = enable;
			this.LingerTime = seconds;
		}

		/// <summary>Gets or sets a value that indicates whether to linger after the <see cref="T:System.Net.Sockets.Socket" /> is closed.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> should linger after <see cref="M:System.Net.Sockets.Socket.Close" /> is called; otherwise, false.</returns>
		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06003EAF RID: 16047 RVA: 0x000D67D2 File Offset: 0x000D49D2
		// (set) Token: 0x06003EB0 RID: 16048 RVA: 0x000D67DA File Offset: 0x000D49DA
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		/// <summary>Gets or sets the amount of time to remain connected after calling the <see cref="M:System.Net.Sockets.Socket.Close" /> method if data remains to be sent.</summary>
		/// <returns>The amount of time, in seconds, to remain connected after calling <see cref="M:System.Net.Sockets.Socket.Close" />.</returns>
		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06003EB1 RID: 16049 RVA: 0x000D67E3 File Offset: 0x000D49E3
		// (set) Token: 0x06003EB2 RID: 16050 RVA: 0x000D67EB File Offset: 0x000D49EB
		public int LingerTime
		{
			get
			{
				return this.lingerTime;
			}
			set
			{
				this.lingerTime = value;
			}
		}

		// Token: 0x040024FD RID: 9469
		private bool enabled;

		// Token: 0x040024FE RID: 9470
		private int lingerTime;
	}
}
