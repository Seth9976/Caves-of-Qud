using System;
using System.Runtime.Serialization;

namespace System.Net.Sockets
{
	/// <summary>Encapsulates the information that is necessary to duplicate a <see cref="T:System.Net.Sockets.Socket" />.</summary>
	// Token: 0x020007B7 RID: 1975
	[Serializable]
	public struct SocketInformation
	{
		/// <summary>Gets or sets the protocol information for a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" />.</returns>
		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06003ECF RID: 16079 RVA: 0x000D6AA4 File Offset: 0x000D4CA4
		// (set) Token: 0x06003ED0 RID: 16080 RVA: 0x000D6AAC File Offset: 0x000D4CAC
		public byte[] ProtocolInformation
		{
			get
			{
				return this.protocolInformation;
			}
			set
			{
				this.protocolInformation = value;
			}
		}

		/// <summary>Gets or sets the options for a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketInformationOptions" /> instance.</returns>
		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06003ED1 RID: 16081 RVA: 0x000D6AB5 File Offset: 0x000D4CB5
		// (set) Token: 0x06003ED2 RID: 16082 RVA: 0x000D6ABD File Offset: 0x000D4CBD
		public SocketInformationOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06003ED3 RID: 16083 RVA: 0x000D6AC6 File Offset: 0x000D4CC6
		// (set) Token: 0x06003ED4 RID: 16084 RVA: 0x000D6AD3 File Offset: 0x000D4CD3
		internal bool IsNonBlocking
		{
			get
			{
				return (this.options & SocketInformationOptions.NonBlocking) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.NonBlocking;
					return;
				}
				this.options &= ~SocketInformationOptions.NonBlocking;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06003ED5 RID: 16085 RVA: 0x000D6AF6 File Offset: 0x000D4CF6
		// (set) Token: 0x06003ED6 RID: 16086 RVA: 0x000D6B03 File Offset: 0x000D4D03
		internal bool IsConnected
		{
			get
			{
				return (this.options & SocketInformationOptions.Connected) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.Connected;
					return;
				}
				this.options &= ~SocketInformationOptions.Connected;
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06003ED7 RID: 16087 RVA: 0x000D6B26 File Offset: 0x000D4D26
		// (set) Token: 0x06003ED8 RID: 16088 RVA: 0x000D6B33 File Offset: 0x000D4D33
		internal bool IsListening
		{
			get
			{
				return (this.options & SocketInformationOptions.Listening) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.Listening;
					return;
				}
				this.options &= ~SocketInformationOptions.Listening;
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06003ED9 RID: 16089 RVA: 0x000D6B56 File Offset: 0x000D4D56
		// (set) Token: 0x06003EDA RID: 16090 RVA: 0x000D6B63 File Offset: 0x000D4D63
		internal bool UseOnlyOverlappedIO
		{
			get
			{
				return (this.options & SocketInformationOptions.UseOnlyOverlappedIO) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.UseOnlyOverlappedIO;
					return;
				}
				this.options &= ~SocketInformationOptions.UseOnlyOverlappedIO;
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06003EDB RID: 16091 RVA: 0x000D6B86 File Offset: 0x000D4D86
		// (set) Token: 0x06003EDC RID: 16092 RVA: 0x000D6B8E File Offset: 0x000D4D8E
		internal EndPoint RemoteEndPoint
		{
			get
			{
				return this.remoteEndPoint;
			}
			set
			{
				this.remoteEndPoint = value;
			}
		}

		// Token: 0x04002590 RID: 9616
		private byte[] protocolInformation;

		// Token: 0x04002591 RID: 9617
		private SocketInformationOptions options;

		// Token: 0x04002592 RID: 9618
		[OptionalField]
		private EndPoint remoteEndPoint;
	}
}
